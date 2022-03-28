using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class OnCollision2DExitEverything : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> response;

        private void OnCollisionExit2D(Collision2D collision)
        {
            response.Invoke(collision.gameObject);
        }

        public UnityEvent<GameObject> Response { get => response; }
    }
}