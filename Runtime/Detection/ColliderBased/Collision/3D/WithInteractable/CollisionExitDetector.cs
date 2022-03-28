using UnityEngine;
namespace HyperGnosys.InteractionModule
{
    public class CollisionExitDetector: ADetector
    {
        [Tooltip("If true, will add objects that exit the collider instead of removing them")]
        [SerializeField] private bool addToList = false;

        private void OnCollisionExit(Collision collision)
        {
            GameObject detectedObject = collision.gameObject;
            OnObjectExit(detectedObject, addToList);
        }
    }
}