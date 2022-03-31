using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public class Trigger2DDetector : Detector
    {
        [Tooltip("If true, it will register exiting objects and remove entering ones instead of the other way around")]
        [SerializeField] private bool registerExiting = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameObject detectedObject = other.gameObject;
            OnObjectEnter(detectedObject, registerExiting);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            GameObject detectedObject = other.gameObject;
            OnObjectExit(detectedObject, registerExiting);
        }
    }
}
