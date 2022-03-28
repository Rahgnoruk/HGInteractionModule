using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnTriggerEnterEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> response;

        private void OnTriggerEnter(Collider other)
        {
            response.Invoke(other.gameObject);
        }

        public UnityEvent<GameObject> Response { get => response; }
    }
}