using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnTriggerEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> triggerEnterResponse;
        [SerializeField] private UnityEvent<GameObject> triggerExitResponse;

        private void OnTriggerEnter(Collider other)
        {
            triggerEnterResponse.Invoke(other.gameObject);
        }
        private void OnTriggerExit(Collider other)
        {
            triggerExitResponse.Invoke(other.gameObject);
        }

        public UnityEvent<GameObject> TriggerEnterResponse { get => triggerEnterResponse; }
        public UnityEvent<GameObject> TriggerExitResponse { get => triggerExitResponse; }
    }
}