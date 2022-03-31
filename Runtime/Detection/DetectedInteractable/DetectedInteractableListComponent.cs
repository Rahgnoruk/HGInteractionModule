using HyperGnosys.Core;
using System.Collections;
using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public class DetectedInteractableListComponent : AObservableListComponent<DetectedInteractable>
    {
        [SerializeField] private bool renderVisualization = false;
        [SerializeField] private float updateRate = 1;
        private Coroutine visualization;
        private bool isVisualizing = false;
        private void Awake()
        {
            if (renderVisualization)
            {
                StartVisualizing();
            }
        }
        private IEnumerator Visualize()
        {
            while (isVisualizing)
            {
                foreach (DetectedInteractable detectedInteractable in List)
                {
                    foreach (Detector detector in detectedInteractable.DetectingDetectors.List)
                    {
                        HGDebug.DrawLine(detector.transform.position, detectedInteractable.Interactable.transform.position,
                            true, Color.red, updateRate);
                    }
                }
                yield return new WaitForSeconds(updateRate);
            }
        }
        private void StartVisualizing()
        {
            if (isVisualizing) return;
            isVisualizing = true;
            visualization = StartCoroutine(Visualize());
        }
        public void StopVisualizing()
        {
            if (!isVisualizing) return;
            StopCoroutine(visualization);
            isVisualizing = false;
        }
        private void OnDisable()
        {
            StopVisualizing();
        }
    }
}