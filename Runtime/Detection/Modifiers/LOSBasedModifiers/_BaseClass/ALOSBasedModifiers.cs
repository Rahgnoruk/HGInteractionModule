using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public abstract class ALOSBasedModifiers : ADetectionModifier
    {
        [Tooltip("All objects in this layer will act as walls." +
            "If set to Nothing it's like an X-Ray super power.\n")]
        [SerializeField] private LayerMask obstacleLayerMask = 1;

        public LayerMask ObstacleLayerMask { get => obstacleLayerMask; set => obstacleLayerMask = value; }
    }
}