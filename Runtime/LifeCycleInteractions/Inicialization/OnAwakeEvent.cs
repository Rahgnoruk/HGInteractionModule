using UnityEngine;
using UnityEngine.Events;
public class OnAwakeEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent response;

    private void Awake()
    {
        response.Invoke();
    }

    public UnityEvent Response { get => response; set => response = value; }
}
