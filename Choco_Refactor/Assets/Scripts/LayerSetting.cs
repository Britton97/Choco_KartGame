using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LayerSetting : MonoBehaviour
{
    [SerializeField] PlayerLayerManager_SO playerLayerManager;
    [SerializeField] List<Camera> playerCameras = new List<Camera>();
    [SerializeField] public int playerCount = 0;
    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] DataInt playerCountData;
    [SerializeField] DataGameObject playerOneCam, playerTwoCam, playerThreeCam, playerFourCam;

    private void Awake()
    {
        UpdatePlayerCount();
    }

    [ContextMenu("SetPlayerCameras")]
    public void SetPlayerCameras()
    {
        int i = 0;
        Debug.Log($"Player Count: {playerCount} heree");
        foreach(Camera playerCam in playerCameras)
        {
            if (i <= playerCount - 1)
            {
                playerCam.cullingMask = playerLayerManager.GetPlayerLayerMask(i);
                playerCam.rect = playerLayerManager.GetPlayerViewPort(i, playerCount);
            }
            i++;
        }
        //Debug.Log($"Player Count: {playerInputManager.playerCount} and {i}");
    }

    public void UpdatePlayerCount()
    {
        playerCount = playerInputManager.playerCount;
        playerCountData.DataValue = playerCount;
        //Debug.Log("Player Count: " + playerCount);
    }

    public void UpdateCameraActiveStates()
    {
        playerCount = playerInputManager.playerCount;
        switch (playerCount)
        {
            case 1:
                playerCameras[0].gameObject.SetActive(true);
                playerCameras[1].gameObject.SetActive(false);
                playerCameras[2].gameObject.SetActive(false);
                playerCameras[3].gameObject.SetActive(false);
                break;
            case 2:
                playerCameras[0].gameObject.SetActive(true);
                playerCameras[1].gameObject.SetActive(true);
                playerCameras[2].gameObject.SetActive(false);
                playerCameras[3].gameObject.SetActive(false);
                break;
            case 3:
                playerCameras[0].gameObject.SetActive(true);
                playerCameras[1].gameObject.SetActive(true);
                playerCameras[2].gameObject.SetActive(true);
                playerCameras[3].gameObject.SetActive(false);
                break;
            case 4:
                playerCameras[0].gameObject.SetActive(true);
                playerCameras[1].gameObject.SetActive(true);
                playerCameras[2].gameObject.SetActive(true);
                playerCameras[3].gameObject.SetActive(true);
                break;
            default:
                Debug.LogError("Invalid player count");
                break;
        }
    }

    private void OnEnable()
    {
        playerOneCam.DataValue = playerCameras[0].gameObject;
        playerTwoCam.DataValue = playerCameras[1].gameObject;
        playerThreeCam.DataValue = playerCameras[2].gameObject;
        playerFourCam.DataValue = playerCameras[3].gameObject;
    }

    private void OnDisable()
    {
        playerOneCam.DataValue = null;
        playerTwoCam.DataValue = null;
        playerThreeCam.DataValue = null;
        playerFourCam.DataValue = null;
    }
}