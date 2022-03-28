using HyperGnosys.Core;
using HyperGnosys.PhysicsUtilities;
using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    [CreateAssetMenu(fileName ="New LOS Modifier", menuName ="HyperGnosys/Interaction Module/LOS Detection Modifier")]
    public class LineOfSightDetectionModifier : ALOSBasedModifiers
    {
        [SerializeField] private bool debugging = false;
        
        public override bool ModifyDetection(Transform detectorTransform, 
            InteractableTag detectedInteractable)
        {
            if (detectorTransform == null || detectedInteractable == null) return false;
            return RaycastUtilities.HasLineOfSight(detectorTransform, 
                detectedInteractable.transform, ObstacleLayerMask, debugging);
        }
    }
}