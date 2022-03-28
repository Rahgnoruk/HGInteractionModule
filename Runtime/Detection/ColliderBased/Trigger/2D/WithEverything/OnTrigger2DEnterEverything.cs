using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnTrigger2DEnterEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> response;

        private void OnTriggerEnter2D(Collider2D other)
        {
            response.Invoke(other.gameObject);
        }

        public UnityEvent<GameObject> Response { get => response; }
    }
}