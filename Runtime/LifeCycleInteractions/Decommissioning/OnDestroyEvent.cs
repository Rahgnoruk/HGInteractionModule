using UnityEngine;
using UnityEngine.Events;
public class OnDestroyEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent response;
    private void OnDestroy()
    {
        response.Invoke();
    }
    public UnityEvent Response { get => response; set => response = value; }
}
