using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class KartController : MonoBehaviour, ICollisionHandlerable
{
    //---Player Info Getter---//
    private PlayerInfoGetter playerInfoGetter;


    //---Input---//
    private Kart_Input _input;
    //private PlayerInput _input;
    [SerializeField] public int playerNumber = -1;
    [SerializeField] CinemachineInputProvider cinemachineInputProvider;
    [SerializeField] Camera playerCamera;
    [SerializeField] PlayerLayerManager_SO playerLayerManager_SO;
    //---Kart Objects---//
    [SerializeField] private GameObject _kartNormal;
    [SerializeField] private GameObject _modelHolder;
    [SerializeField] private GameObject _tiltObject;
    [SerializeField] private GameObject _lookAtObject;
    //---Kart Rigidbody---//
    private Rigidbody colliderBall;

    //---State Machine---//
    [SerializeField] private State_Base _currentState;
    [SerializeField] private List<State_Base> _dataStates;
    //[SerializeField] private Dictionary<KartState, State_Base> _stateDictionary = new Dictionary<KartState, State_Base>();

    //---Kart Stats---//
    [SerializeField] Kart_Stats kart_stats;
    [SerializeField] Player_Stats player_stats;

    //---Events---//
    public UnityEvent onAwake;
    public UnityEvent onLeaveKart;
    public UnityEvent onEnterKart;
    [SerializeField] DataGameObject cinemachineCam;

    public void TestingInput(InputAction.CallbackContext context)
    {
        float button = context.ReadValue<float>();
        Debug.Log($"Player {playerNumber} button = {button}");
    }

    #region OnEnable/OnDisable Functions
    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
    #endregion
    #region Awake Function
    private void Awake()
    {
        _input = new Kart_Input();
        onAwake.Invoke();
        player_stats.ResetStats();
        playerNumber = GetComponent<PlayerInput>().playerIndex;
        cinemachineInputProvider.PlayerIndex = playerNumber;
        playerCamera.cullingMask = playerLayerManager_SO.GetCamerCullingMasks(playerNumber);
        playerCamera.gameObject.layer = playerLayerManager_SO.SetLayer(playerNumber);
        cinemachineInputProvider.gameObject.layer = playerLayerManager_SO.SetLayer(playerNumber);

        try
        {
            playerInfoGetter = transform.parent.GetComponent<PlayerInfoGetter>();
        } catch { Debug.LogError("Was not able to get PlayerInfoGetter"); }

        if (playerInfoGetter != null)
        {
            colliderBall = playerInfoGetter.GetColliderBall();
        }
        else
        {
            onLeaveKart.Invoke();
        }

        _currentState.OnEnter(colliderBall, _modelHolder, _kartNormal, _tiltObject, _input, kart_stats, player_stats);
    }
    #endregion
    #region LateUpdate Function
    private void LateUpdate()
    {
        transform.position = colliderBall.transform.position; //keep this. It will match the location of the collider ball      
    }
    #endregion
    #region CallOnEnterState Function
    public void CallOnEnterState(KartState passIn)
    {
        _currentState.enabled = false;

        foreach (State_Base state in _dataStates)
        {
            if (state.stateType == passIn)
            {
                _currentState = state;
            }
        }

        _currentState.enabled = true;
        _currentState.OnEnter(colliderBall, _modelHolder, _kartNormal, _tiltObject, _input, kart_stats, player_stats);
    }
    #endregion

    public void SetCameraFollow()
    {
        CinemachineFreeLook cinemachineFreeLook = cinemachineCam.DataValue.gameObject.GetComponent<CinemachineFreeLook>();
        cinemachineFreeLook.Follow = _modelHolder.transform;
        cinemachineFreeLook.LookAt = _lookAtObject.transform;
    }

    public void FindPlayerRigidbody()
    {
        StartCoroutine(WaitFrame());
    }

    private IEnumerator WaitFrame()
    {
        yield return new WaitForEndOfFrame();
        Transform parent = transform.parent;
        Debug.Log("Parent is " + parent.name);
        playerInfoGetter = transform.parent.GetComponent<PlayerInfoGetter>();
        player_stats = playerInfoGetter.GetPlayerStats();
        playerInfoGetter.kartController = this;

        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == "Player Collider")
            {
                Debug.Log("Found the player collider");
                colliderBall = child.gameObject.GetComponent<Rigidbody>();
            }
        }
    }

    public void OnLeaveKartEvent()
    {
        onLeaveKart.Invoke();
        if(playerInfoGetter != null)
        {
            playerInfoGetter.PlayerExitKart();
        }
    }

    public void CollisionHandler(GameObject hitObject, GameObject hittingObject)
    {
        this.enabled = true;

        if(hitObject.transform.parent.GetComponent<PlayerInfoGetter>())
        {
        Debug.LogError("Called collision handler");

        }
        playerInfoGetter = hitObject.transform.parent.GetComponent<PlayerInfoGetter>();
        colliderBall = hitObject.GetComponent<Rigidbody>();
        CallOnEnterState(KartState.Ground);
        onEnterKart.Invoke();
    }
}
