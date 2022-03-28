using UnityEngine;
using UnityEngine.Events;

public class OnStartEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent response;

    private void Start()
    {
        response.Invoke();
    }

    public UnityEvent Response { get => response; set => response = value; }
}
