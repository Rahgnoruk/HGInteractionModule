using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public class Trigger2DExitDetector : Detector
    {
        [Tooltip("If true, will add objects that exit the collider instead of removing them")]
        [SerializeField] private bool addToList = false;

        private void OnTriggerExit2D(Collider2D other)
        {
            GameObject detectedObject = other.gameObject;
            OnObjectExit(detectedObject, addToList);
        }
    }
}
