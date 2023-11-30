using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerNumber;
    private Kart_Input input;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Player_Stats player_stats;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce = 1000f;
    [SerializeField] DataGameObject cinemachineCam;
    [SerializeField] float speed = 10f;
    [SerializeField] private DataFloat forceMultiplier;
    [Range(0,1)]
    [SerializeField] private float moveThreshold = .2f;    
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float applyGravityRayDistance;

    #region OnEnable/OnDisable
    private void OnEnable()
    {
        input.Enable();
        //Debug.Log("Player Controller Enabled");
    }

    private void OnDisable()
    {
        input.Disable();
        //Debug.Log("Player Controller Disabled");
    }

    private void Awake()
    {
        input = new Kart_Input();
    }
    #endregion

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float button = input.Kart_Controls.ActionButton.ReadValue<float>();
        RaycastHit hit;
        if (button == 1)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
        else if (button == 0 && !Physics.Raycast(transform.position, Vector3.down, out hit, applyGravityRayDistance, groundLayer))
        {
            //rb.AddForce(Vector3.up * gravity * gravityMultiplier * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    private void FixedUpdate()
    {
        LeftStick();
        Vector2 move = input.Kart_Controls.Move.ReadValue<Vector2>();

        if(Mathf.Abs(move.y) > moveThreshold)

        rb.AddForce(transform.forward * speed * move.y * forceMultiplier.DataValue);
    }

    private void LateUpdate()
    {
        transform.position = rb.transform.position; //keep this
    }

    public void SetCameraFollow()
    {
        //Debug.Log(t.DataValue);
        CinemachineFreeLook cinemachineFreeLook = cinemachineCam.DataValue.gameObject.GetComponent<CinemachineFreeLook>();
        cinemachineFreeLook.Follow = transform;
        cinemachineFreeLook.LookAt = transform;
        Debug.Log(cinemachineCam.DataValue.gameObject.name);
    }

    float rotate;
    float currentRotate;
    [SerializeField] private float turnSpeed;
    public void LeftStick()
    {
        Vector2 move = input.Kart_Controls.Move.ReadValue<Vector2>();

        rotate = move.x * turnSpeed * Time.deltaTime;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f);
        rotate = 0f;

        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0f, transform.localEulerAngles.y + currentRotate, 0), Time.deltaTime * 4f);

    }
}
