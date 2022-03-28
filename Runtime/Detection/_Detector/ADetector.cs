using HyperGnosys.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class ADetector : MonoBehaviour
    {
        [SerializeField] private bool debugging = false;
        [Header("Detection")]
        [Tooltip("If <=0, detections will run with FixedUpdate")]
        [SerializeField] private float detectionUpdateRate = -1;
        [SerializeField] private List<Sensor> sensors = new List<Sensor>();
        [SerializeField] private ObservableList<DetectedInteractable> detectedInteractables = new ObservableList<DetectedInteractable>();
        [SerializeField] private DetectedInteractableEvent onInteractableDetected;
        [SerializeField] private DetectedInteractableEvent onInteractableUndetected;
        [Header("Continuous Detection")]
        [Tooltip("If you care that the targets maye become undetectable after entering this Detector's zone")]
        [SerializeField] private bool runContinuousDetection = false;
        [SerializeField] private ObservableList<DetectedInteractable> interactablesInRange = new ObservableList<DetectedInteractable>();
        private Coroutine detectionUpdate;
        private bool isRunningDetectionUpdates = false;
        private void Awake()
        {
            detectedInteractables.AddOnItemAddedListener(onInteractableDetected.Invoke);
            detectedInteractables.AddOnItemRemovedListener(onInteractableUndetected.Invoke);
            if (runContinuousDetection)
            {
                StartRunningDetectionUpdates();
            }
        }
        private void StartRunningDetectionUpdates()
        {
            HGDebug.Log("Starting Detection Update", this, debugging);
            if (isRunningDetectionUpdates) return;
            isRunningDetectionUpdates = true;
            detectionUpdate = StartCoroutine(DetectionUpdate());
        }
        private void StopRunningDetectionUpdates()
        {
            if (!isRunningDetectionUpdates) return;
            StopCoroutine(detectionUpdate);
            isRunningDetectionUpdates = false;
        }
        private IEnumerator DetectionUpdate()
        {
            while (isRunningDetectionUpdates)
            {
                HGDebug.Log("Updating detection", this, debugging);
                foreach(DetectedInteractable detectedInteractable in interactablesInRange.List)
                {
                    UpdateDetection(detectedInteractable);
                }
                if (detectionUpdateRate <= 0)
                {
                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    yield return new WaitForSeconds(detectionUpdateRate);
                }
            }
        }
        protected bool OnObjectEnter(GameObject detectedObject, bool registerExiting)
        {
            InteractableTag interactable = detectedObject.GetComponent<InteractableTag>();
            if (interactable == null)
            {
                return false;
            }
            return OnObjectEnter(interactable, registerExiting);
        }
        protected bool OnObjectEnter(InteractableTag interactable, bool registerExiting)
        {
            HGDebug.Log($"Entering Object {interactable.name} is interactable", interactable.gameObject, debugging);
            DetectedInteractable detectedInteractable = GetDetectedInteractable(interactable);
            if (!registerExiting)
            {
                interactablesInRange.Add(detectedInteractable);
                return Detect(detectedInteractable);
            }
            else
            {
                interactablesInRange.Remove(detectedInteractable);
                return UnDetect(detectedInteractable);
            }
        }
        protected bool OnObjectExit(GameObject detectedObject, bool registerExiting)
        {
            InteractableTag interactable = detectedObject.GetComponent<InteractableTag>();
            if (interactable == null)
            {
                return false;
            }
            return OnObjectExit(interactable, registerExiting);
        }
        protected bool OnObjectExit(InteractableTag interactable, bool registerExiting)
        {
            HGDebug.Log($"Exiting Object {interactable.name} is interactable", interactable.gameObject, debugging);
            DetectedInteractable detectedInteractable = GetDetectedInteractable(interactable);
            if (!registerExiting)
            {
                interactablesInRange.Remove(detectedInteractable);
                return UnDetect(detectedInteractable);
            }
            else
            {
                interactablesInRange.Add(detectedInteractable);
                return Detect(detectedInteractable);
            }
        }
        private void OnInteractableDisabled(InteractableTag interactable)
        {
            DetectedInteractable detectedInteractable = GetDetectedInteractable(interactable);
            UnDetect(detectedInteractable);
            interactable.OnInteractableDisabled.RemoveListener(OnInteractableDisabled);
        }
        private bool Detect(DetectedInteractable detectedInteractable)
        {
            detectedInteractable.Interactable.OnInteractableDisabled.AddListener(OnInteractableDisabled);
            if (sensors.Count > 0 && !SensorsPass(detectedInteractable))
            {
                return false;
            }
            AddDetectedInteractable(detectedInteractable);
            HGDebug.Log($"{detectedInteractable.Interactable.name} was detected", this, debugging);
            return true;
        }
        private void UpdateDetection(DetectedInteractable detectedInteractable)
        {
            if (sensors.Count > 0 && !SensorsPass(detectedInteractable))
            {
                UnDetect(detectedInteractable);
                return;
            }
            AddDetectedInteractable(detectedInteractable);
            HGDebug.Log($"{detectedInteractable.Interactable.name} was detected", this, debugging);
            return;
        }
        protected bool SensorsPass(DetectedInteractable detectedInteractable)
        {
            bool matchFound = false;
            foreach (Sensor sensor in sensors)
            {
                List<InteractionType> currentSensorMatches = sensor.Sense(transform, detectedInteractable.Interactable, debugging);
                if (currentSensorMatches == null) continue;
                foreach (InteractionType match in currentSensorMatches)
                {
                    matchFound = true;
                    detectedInteractable.InteractionTypeMatches.Add(match);
                }
            }
            return matchFound;
        }
        private bool UnDetect(DetectedInteractable detectedInteractable)
        {
            if (detectedInteractable.DetectingDetectors.Contains(this))
            {
                detectedInteractable.OnInteractableInteractionTypesChanged.RemoveListener(UpdateDetection);
                detectedInteractable.DetectingDetectors.Remove(this);
                RemoveDetectedInteractable(detectedInteractable);
                return true;
            }
            return false;
        }
        private void AddDetectedInteractable(DetectedInteractable detectedInteractable)
        {
            if (detectedInteractable.DetectingDetectors.Contains(this)) return;
            detectedInteractables.Add(detectedInteractable);
            detectedInteractable.OnInteractableInteractionTypesChanged.AddListener(UpdateDetection);
            detectedInteractable.DetectingDetectors.Add(this);
        }
        private void RemoveDetectedInteractable(DetectedInteractable detectedInteractable)
        {
            if (detectedInteractable.DetectingDetectors.Count <= 0)
            {
                detectedInteractable.OnUndetected.Invoke();
                detectedInteractables.Remove(detectedInteractable);
            }
        }
        private DetectedInteractable GetDetectedInteractable(InteractableTag interactable)
        {
            foreach(DetectedInteractable detectedInteractable in interactablesInRange.List)
            {
                if (detectedInteractable.Interactable.Equals(interactable))
                {
                    HGDebug.Log("Interactable already in range", this, debugging);
                    return detectedInteractable;
                }
            }
            return new DetectedInteractable(interactable);
        }
        private void OnDisable()
        {
            StopRunningDetectionUpdates();
        }
        protected bool Debugging { get => debugging; set => debugging = value; }
        public ObservableList<DetectedInteractable> DetectedInteractables { get => detectedInteractables; set => detectedInteractables = value; }
        protected float DetectionUpdateRate { get => detectionUpdateRate; set => detectionUpdateRate = value; }
    }
}