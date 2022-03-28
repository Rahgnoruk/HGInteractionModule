using UnityEngine;
using UnityEngine.Events;

public class OnEnableEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent response;

    private void OnEnable()
    {
        response.Invoke();
    }

    public UnityEvent Response { get => response; set => response = value; }
}
