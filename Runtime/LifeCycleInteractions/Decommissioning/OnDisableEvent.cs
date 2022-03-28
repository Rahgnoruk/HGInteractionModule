using UnityEngine;
using UnityEngine.Events;
public class OnDisableEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent response;

    private void OnDestroy()
    {
        response.Invoke();
    }

    public UnityEvent Response { get => response; set => response = value; }
}
