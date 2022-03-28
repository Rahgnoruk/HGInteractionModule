using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnCollisionExitEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> response;

        private void OnCollisionExit(Collision collision)
        {
            response.Invoke(collision.gameObject);
        }

        public UnityEvent<GameObject> Response { get => response; }
    }
}