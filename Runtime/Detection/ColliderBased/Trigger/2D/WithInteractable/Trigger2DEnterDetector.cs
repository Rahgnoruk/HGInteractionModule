using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public class Trigger2DEnterDetector : Detector
    {
        [Tooltip("If true, will remove objects that enter the collider instead of adding them")]
        [SerializeField] private bool removeFromList = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameObject detectedObject = other.gameObject;
            OnObjectEnter(detectedObject, removeFromList);
        }
    }
}
