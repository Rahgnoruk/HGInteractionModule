using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public class CollisionDetector : ADetector
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
            OnObjectExit(detectedObject, registerExiting);
        }
    }
}