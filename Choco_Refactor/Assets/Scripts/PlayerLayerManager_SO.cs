using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerViewport Manager")]
public class PlayerLayerManager_SO : ScriptableObject
{
    [SerializeField] LayerMask player1Mask, player2Mask, player3Mask, player4Mask;

    public LayerMask GetCamerCullingMasks(int whichPlayer)
    {
        if(whichPlayer == 0)
        {
            return player1Mask;
        }
        else if(whichPlayer == 1)
        {
            return player2Mask;
        }
        else if(whichPlayer == 2)
        {
            return player3Mask;
        }
        else if(whichPlayer == 3)
        {
            return player4Mask;
        }
        else
        {
            Debug.LogError("Invalid player number");
            return player1Mask;
        }
    }

    public LayerMask SetLayer(int whichPlayer)
    {
        if (whichPlayer == 0)
        {
            return LayerMask.NameToLayer("P1 Camera");
        }
        else if (whichPlayer == 1)
        {
            return LayerMask.NameToLayer("P2 Camera");
        }
        else if (whichPlayer == 2)
        {
            return LayerMask.NameToLayer("P3 Camera");
        }
        else if (whichPlayer == 3)
        {
            return LayerMask.NameToLayer("P4 Camera");
        }
        else
        {
            Debug.LogError("Invalid player number");
            return LayerMask.NameToLayer("P1 Camera");
        }
    }
}
