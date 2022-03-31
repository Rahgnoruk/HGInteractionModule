using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public class TriggerDetector : Detector
    {
        [Tooltip("If true, it will register exiting objects and remove entering ones instead of the other way around")]
        [SerializeField] private bool registerExiting = false;

        private void OnTriggerEnter(Collider other)
        {
            GameObject detectedObject = other.gameObject;
            OnObjectEnter(detectedObject, registerExiting);
        }
        private void OnTriggerExit(Collider other)
        {
            GameObject detectedObject = other.gameObject;
            OnObjectExit(detectedObject, registerExiting);
        }
    }
}