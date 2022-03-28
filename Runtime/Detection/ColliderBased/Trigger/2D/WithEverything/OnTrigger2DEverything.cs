using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnTrigger2DEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> triggerEnterResponse;
        [SerializeField] private UnityEvent<GameObject> triggerExitResponse;

        private void OnTriggerEnter2D(Collider2D other)
        {
            triggerEnterResponse.Invoke(other.gameObject);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            triggerExitResponse.Invoke(other.gameObject);
        }

        public UnityEvent<GameObject> TriggerEnterResponse { get => triggerEnterResponse; }
        public UnityEvent<GameObject> TriggerExitResponse { get => triggerExitResponse; }
    }
}