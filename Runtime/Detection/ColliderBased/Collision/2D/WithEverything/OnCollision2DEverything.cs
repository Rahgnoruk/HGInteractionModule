using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnCollision2DEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> collisionEnterResponse;
        [SerializeField] private UnityEvent<GameObject> collisionExitResponse;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collisionEnterResponse.Invoke(collision.gameObject);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            collisionExitResponse.Invoke(collision.gameObject);
        }

        public UnityEvent<GameObject> CollisionEnterResponse { get => collisionEnterResponse; }
        public UnityEvent<GameObject> CollisionExitResponse { get => collisionExitResponse; }
    }
}