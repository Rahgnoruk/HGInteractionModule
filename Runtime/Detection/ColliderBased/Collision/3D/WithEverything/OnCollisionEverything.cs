using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnCollisionEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> collisionEnterResponse;
        [SerializeField] private UnityEvent<GameObject> collisionExitResponse;

        private void OnCollisionEnter(Collision collision)
        {
            collisionEnterResponse.Invoke(collision.gameObject);
        }
        private void OnCollisionExit(Collision collision)
        {
            collisionExitResponse.Invoke(collision.gameObject);
        }

        public UnityEvent<GameObject> CollisionEnterResponse { get => collisionEnterResponse; }
        public UnityEvent<GameObject> CollisionExitResponse { get => collisionExitResponse; }
    }
}