using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEnterEverything : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> response;

    private void OnCollisionEnter(Collision collision)
    {
        response.Invoke(collision.gameObject);
    }

    public UnityEvent<GameObject> Response { get => response; }
}