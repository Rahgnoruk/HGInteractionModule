using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnCollision2DEnterEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> response;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            response.Invoke(collision.gameObject);
        }

        public UnityEvent<GameObject> Response { get => response; }
    }
}