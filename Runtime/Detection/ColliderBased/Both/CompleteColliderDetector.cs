using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public class CompleteColliderDetector : ADetector
    {
        [Tooltip("If true, it will register exiting objects and remove entering ones instead of the other way around")]
        [SerializeField] private bool registerExiting = false;

        private void OnCollisionEnter(Collision collision)
        {
            GameObject detectedObject = collision.gameObject;
            OnObjectEnter(detectedObject, registerExiting);
        }
        private void OnCollisionExit(Collision collision)
        {
            GameObject detectedObject = collision.gameObject;
            OnObjectEnter(detectedObject, registerExiting);
        }

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