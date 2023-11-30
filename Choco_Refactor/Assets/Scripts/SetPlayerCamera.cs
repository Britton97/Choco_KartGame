using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerCamera : MonoBehaviour
{
    [SerializeField] DataInt playerCount;
    [SerializeField] DataGameObject playerOneCam, playerTwoCam, playerThreeCam, playerFourCam;
    [SerializeField] PlayerLayerManager_SO playerLayerManager;
    [SerializeField] KartController kartController;

    private void OnEnable()
    {
        if(playerCount.DataValue == 1)
        {
            playerOneCam.DataValue = gameObject;
        }
        else if(playerCount.DataValue == 2)
        {
            playerTwoCam.DataValue = gameObject;
        }
        else if (playerCount.DataValue == 3)
        {
            playerThreeCam.DataValue = gameObject;
        }
        else if (playerCount.DataValue == 4)
        {
            playerFourCam.DataValue = gameObject;
        }
        else
        {
            Debug.LogError("Player Count is not set correctly");
        }
        SetCam();
        kartController.playerNumber = playerCount.DataValue;
    }

    [ContextMenu("SetCam")]
    public void SetCam()
    {
        gameObject.layer = LayerMask.NameToLayer($"P{playerCount.DataValue} Camera");
        //set the gameobject's layer to int 3

    }
}
