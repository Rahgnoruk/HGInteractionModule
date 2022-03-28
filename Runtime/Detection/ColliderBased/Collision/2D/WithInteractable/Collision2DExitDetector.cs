using UnityEngine;
namespace HyperGnosys.InteractionModule
{
    public class Collision2DExitDetector : ADetector
    {
        [Tooltip("If true, will add objects that exit the collider instead of removing them")]
        [SerializeField] private bool addToList = false;

        private void OnCollisionExit2D(Collision2D collision)
        {
            GameObject detectedObject = collision.gameObject;
            OnObjectExit(detectedObject, addToList);
        }
    }
}