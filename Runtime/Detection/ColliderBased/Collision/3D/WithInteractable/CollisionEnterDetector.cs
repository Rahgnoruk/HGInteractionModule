using UnityEngine;
namespace HyperGnosys.InteractionModule
{
    public class CollisionEnterDetector : Detector
    {
        [Tooltip("If true, will remove objects that enter the collider instead of adding them")]
        [SerializeField] private bool removeFromList = false;

        private void OnCollisionEnter(Collision collision)
        {
            GameObject detectedObject = collision.gameObject;
            OnObjectEnter(detectedObject, removeFromList);
        }
    }
}