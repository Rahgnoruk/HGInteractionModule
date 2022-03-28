using HyperGnosys.Core;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    [Serializable]
    public class DetectedInteractable
    {
        [SerializeField] private InteractableTag interactable;
        [SerializeField] private ObservableList<InteractionType> interactionTypeMatches = new ObservableList<InteractionType>();
        [SerializeField] private ObservableList<ADetector> detectingDetectors = new ObservableList<ADetector>();
        private UnityEvent<DetectedInteractable> onInteractableInteractionTypesChanged = new UnityEvent<DetectedInteractable>();
        private UnityEvent onUndetected = new UnityEvent();
        public DetectedInteractable(InteractableTag interactable)
        {
            this.interactable = interactable;
            interactable.OnInteractionTypesChanged.AddListener(RaiseTypesChanged);
        }
        private void RaiseTypesChanged(InteractableTag interactable)
        {
            onInteractableInteractionTypesChanged.Invoke(this);
        }
        public bool NoMatches()
        {
            if (interactionTypeMatches == null) return true;
            return interactionTypeMatches.Count <= 0;
        }
        public InteractableTag Interactable { get => interactable; set => interactable = value; }
        public ModuleComponentList ModuleComponentList { get => interactable.ModuleComponentList; }
        public ObservableList<InteractionType> InteractionTypeMatches { get => interactionTypeMatches; }
        public UnityEvent OnUndetected { get => onUndetected; }
        public UnityEvent<DetectedInteractable> OnInteractableInteractionTypesChanged { get => onInteractableInteractionTypesChanged; }
        public ObservableList<ADetector> DetectingDetectors { get => detectingDetectors; }
    }
}