using UnityEngine;
namespace HyperGnosys.InteractionModule
{
    public class Collision2DEnterDetector : Detector
    {
        [Tooltip("If true, will remove objects that enter the collider instead of adding them")]
        [SerializeField] private bool removeFromList = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject detectedObject = collision.gameObject;
            OnObjectEnter(detectedObject, removeFromList);
        }
    }
}