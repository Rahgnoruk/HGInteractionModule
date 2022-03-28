using HyperGnosys.Core;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    [Serializable]
    public class DetectedInteractableEvent
    {
        [SerializeField] private UnityEvent<BodyTag> raisedBodyTag = new UnityEvent<BodyTag>();
        [SerializeField] private UnityEvent<DetectedInteractable> raisedDetectedInteractable = new UnityEvent<DetectedInteractable>();
        public void Invoke(DetectedInteractable detectedInteractable)
        {
            raisedBodyTag.Invoke(detectedInteractable.Interactable);
            raisedDetectedInteractable.Invoke(detectedInteractable);
        }
    }
}