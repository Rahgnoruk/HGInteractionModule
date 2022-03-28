using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnTrigger2DExitEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> response;

        private void OnTriggerExit2D(Collider2D other)
        {
            response.Invoke(other.gameObject);
        }

        public UnityEvent<GameObject> Response { get => response; }
    }
}