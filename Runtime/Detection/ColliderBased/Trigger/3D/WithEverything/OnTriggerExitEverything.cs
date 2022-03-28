using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnTriggerExitEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> response;

        private void OnTriggerExit(Collider other)
        {
            response.Invoke(other.gameObject);
        }

        public UnityEvent<GameObject> Response { get => response; }
    }
}