using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public class ARaycastBasedDetector : ADetector
    {
        [SerializeField] private LayerMask targetLayerMask;
        [SerializeField, HideInInspector] private bool isFirstReset = true;

        public LayerMask TargetLayerMask { get => targetLayerMask; set => targetLayerMask = value; }
    }
}