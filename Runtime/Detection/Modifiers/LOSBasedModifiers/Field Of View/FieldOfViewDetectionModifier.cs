using HyperGnosys.PhysicsUtilities;
using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    [CreateAssetMenu(fileName ="New FOV Modifier", 
        menuName ="HyperGnosys/Interaction Module/FOV Detection Modifier")]
    public class FieldOfViewDetectionModifier : ALOSBasedModifiers
    {
        [SerializeField] private bool debugging = false;
        [Range(0, 360)]
        [SerializeField] private int viewAngle = 90;
        Vector3 dirToTarget;
        public override bool ModifyDetection(Transform detectorTransform, InteractableTag interactable)
        {
            dirToTarget = (interactable.transform.position - detectorTransform.position).normalized;
            ///Se divide entre dos porque el transform.forward esta a la mitad del ViewAngle
            ///(Osea el view angle se pinta poniendo transform.forward en medio)
            ///Si el target esta en el angulo de vision
            if (Vector3.Angle(detectorTransform.forward, dirToTarget) < viewAngle / 2)
            {
                return RaycastUtilities.HasLineOfSight(detectorTransform, interactable.transform, 
                    ObstacleLayerMask, debugging);
            }
            return false;
        }
    }
}