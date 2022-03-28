using UnityEngine;
namespace HyperGnosys.InteractionModule
{
    public class Collision2DDetector : ADetector
    {
        [Tooltip("If true, it will register exiting objects and remove entering ones")]
        [SerializeField] private bool registerExiting = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject detectedObject = collision.gameObject;
            OnObjectEnter(detectedObject, registerExiting);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            GameObject detectedObject = collision.gameObject;
            OnObjectExit(detectedObject, registerExiting);
        }
    }
}