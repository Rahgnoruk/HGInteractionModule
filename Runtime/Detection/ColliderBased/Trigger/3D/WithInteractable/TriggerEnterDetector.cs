using UnityEngine;
namespace HyperGnosys.InteractionModule
{
    public class TriggerEnterDetector : Detector
    {
        [Tooltip("If true, will remove objects that enter the collider instead of adding them")]
        [SerializeField] private bool removeFromList = false;
        private void OnTriggerEnter(Collider other)
        {
            GameObject detectedObject = other.gameObject;
            OnObjectEnter(detectedObject, removeFromList);
        }
    }
}