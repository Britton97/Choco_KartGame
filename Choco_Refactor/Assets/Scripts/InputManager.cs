using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private Kart_Input input;
    [SerializeField] private UnityEvent<Vector2> vector2Event;

    #region OnEnable/OnDisable
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    #endregion

    private void Awake()
    {
        input = new Kart_Input();
    }

    public void Update()
    {
        Vector2 move = input.Kart_Controls.Move.ReadValue<Vector2>();
        vector2Event.Invoke(move);
    }
}
