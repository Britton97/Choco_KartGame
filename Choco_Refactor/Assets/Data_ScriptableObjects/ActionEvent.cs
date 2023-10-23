using UnityEngine;
using UnityEngine.Events;
public class ActionEvent : MonoBehaviour
{
    [SerializeField] private GameAction gameAction;
    [TextArea]
    public string description;
    [SerializeField] private UnityEvent unityEvent;

    private void OnEnable()
    {
        gameAction.action += EventInvoker;
    }

    private void OnDisable()
    {
        gameAction.action -= EventInvoker;
    }

    public void EventInvoker()
    {
        unityEvent.Invoke();
    }
}
