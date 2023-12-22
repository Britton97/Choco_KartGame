using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeleteLater : MonoBehaviour
{
    [SerializeField] private Kart_Input _input;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private int playerNumber = -1;
    [SerializeField] private TextMeshProUGUI playerNumberText;
    private string playerNumberString = "Player Number: ";
    [SerializeField] Material mat;
    [SerializeField] Image image;
    private void OnEnable()
    {
        _input = new Kart_Input();
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private Vector2 leftJoystick = Vector2.zero;
    private Vector2 rightJoystick = Vector2.zero;
    private float actionButton = 0;
    public void ReceiveLeftStick(InputAction.CallbackContext context)
    {
        leftJoystick = context.ReadValue<Vector2>();
    }

    public void ReceiveRightStick(InputAction.CallbackContext context)
    {
        rightJoystick = context.ReadValue<Vector2>();
    }

    public void ReceiveActionButton(InputAction.CallbackContext context)
    {
        actionButton = context.ReadValue<float>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the player number
        //playerNumber = _input.PlayerNumber;
        playerNumber = _playerInput.playerIndex;
        playerNumberText.text = $"{playerNumberString}{playerNumber + 1}";
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Player {playerNumber} leftJoystick = {leftJoystick}");
    }

    //make a method to instance a new material on the image

    public void ChangeColor()
    {
        mat = new Material(mat);
        if (playerNumber == 0)
        {
            mat.SetColor("_Color", Color.red);
        }
        else if (playerNumber == 1)
        {
            mat.SetColor("_Color", Color.blue);
        }
        image.material = mat;
    }
}
