using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public abstract class ADetectionModifier : ScriptableObject
    {
        public abstract bool ModifyDetection(Transform detectorTransform, InteractableTag detectedInteractable);
    }
}