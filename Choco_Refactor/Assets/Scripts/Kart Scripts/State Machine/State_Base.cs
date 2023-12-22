using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// This class is the base class for all states in the kart state machine.
// It contains common functionality and properties that all states share.
public abstract class State_Base : MonoBehaviour
{

    //---State---//
    [Header("State Type")]
    public KartState stateType;
    [Header("Kart Specific Stats")]
    [SerializeField] public Kart_Stats kart_stats;

    [HideInInspector] public Kart_Input input;
    [HideInInspector] public KartController kartController;
    [Header("UI GameObjects")]
    [SerializeField] public Slider _uiBoostSlider;
    //---Kart GameObjects---//
    [HideInInspector]public Rigidbody rb;
    [HideInInspector]public GameObject kartNormal;
    [HideInInspector]public GameObject kartModel;
    [HideInInspector]public GameObject tiltObject;
    [HideInInspector]public Player_Stats player_stats;
    [Header("Gravity Settings")]
    [SerializeField] public DataFloat _gravity;
    [SerializeField] public DataFloat _gravityMultiplier;

    // This method is called when the state is entered.
    // It initializes the state with the necessary components and invokes the onEnterEvent.
    public virtual void OnEnter(Rigidbody passedRB, GameObject pKartModel, GameObject pKartNormal, GameObject pTiltObject, Kart_Input pInput, Kart_Stats pStats, Player_Stats pPlayerStats)
    {
        rb = passedRB;
        kartModel = pKartModel;
        tiltObject = pTiltObject;
        kartNormal = pKartNormal;
        kartController = kartNormal.transform.parent.GetComponent<KartController>(); //get KartController
        input = pInput;
        kart_stats = pStats;
        player_stats = pPlayerStats;

        //Debug.Log($"Tilt object = {pTiltObject.name}");

        //onEnterEvent.Invoke();
    }

    // This method is called when the state is exited.
    // It invokes the onExitEvent and calls OnEnterState() on the new state.
    public virtual void OnExit(KartState passIn)
    {
        //onExitEvent.Invoke();

        kartController.CallOnEnterState(passIn); //call OnEnterState() on the new state
    }

    // This method is called when the A button is pressed.
    public virtual void AButton() { }

    // This method is called when the left stick is moved.
    public virtual void LeftStick() { }

    // This method is called when the kart is moved.
    public virtual void Move() { }

    private float currentTiltX;
    private float currentTiltY;

    // This method is called when the kart is tilted.
    public virtual void Tilt(Vector2 context)
    {
        //Vector2 move = context.ReadValue<Vector2>();
        float tiltX = context.x * 20; //20
        float tiltY = context.y * 20; //10
        currentTiltX = Mathf.Lerp(currentTiltX, tiltX, Time.deltaTime * 8f);
        currentTiltY = Mathf.Lerp(currentTiltY, tiltY, Time.deltaTime * 8f);
        tiltObject.transform.localEulerAngles = new Vector3(currentTiltY, tiltObject.transform.localEulerAngles.y, -currentTiltX);
    }
}
