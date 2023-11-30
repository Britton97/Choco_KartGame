using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSetting : MonoBehaviour
{
    [SerializeField] PlayerLayerManager_SO playerLayerManager;
    [SerializeField] List<Camera> playerCameras = new List<Camera>();
    [SerializeField] public int playerCount = 0;



    [ContextMenu("SetPlayerCameras")]
    public void SetPlayerCameras()
    {
        GetActiveCameras();
        int i = 0;
        foreach(Camera playerCam in playerCameras)
        {
            playerCam.cullingMask = playerLayerManager.GetPlayerLayerMask(i);
            playerCam.rect = playerLayerManager.GetPlayerViewPort(i, playerCount);
            i++;
        }
        //playerOne.cullingMask = playerLayerManager.GetPlayerLayerMask(1);
        //Rect rectExample = playerLayerManager.GetPlayerViewPort(1, 1);
        //playerCam.rect = rectExample;
        //make a rect example variable
    }

    public void GetActiveCameras()
    {
        //foreach through each child and if the child is active ++playerCount
        playerCount = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                playerCount++;
            }
        }
    }
}