using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerViewport Manager")]
public class PlayerLayerManager_SO : ScriptableObject
{
    [SerializeField] LayerMask player1Mask, player2Mask, player3Mask, player4Mask;

    [SerializeField] bool horizontalSplitScreen = false;
    Vector4 onePlayerViewport = new Vector4(0, 0, 1, 1);
    Vector4[] twoPlayerHorizontalViewports = new Vector4[]
    {
        new Vector4(0,.5f,1,.5f),
        new Vector4(0,0,1,.5f)
    };    
    Vector4[] twoPlayerVerticalViewports = new Vector4[]
    {
        new Vector4(0,0,.5f,1),
        new Vector4(.5f,0,.5f,1)
    };
    Vector4[] threeOrMorePlayersViewPorts = new Vector4[]
    {
        new Vector4(0,.5f,.5f,.5f),
        new Vector4(.5f,.5f,.5f,.5f),
        new Vector4(0,0,.5f,.5f),
        new Vector4(.5f,0,.5f,.5f)
    };


    public Rect GetPlayerViewPort(int whichPlayer, int totalPlayers)
    {
        Rect rectExample = new Rect(0,0,0,0);
        Debug.Log($"whichPlayer: {whichPlayer} // totalPlayers {totalPlayers}");

        if(totalPlayers == 1)
        {
            return new Rect(onePlayerViewport.x, onePlayerViewport.y, onePlayerViewport.z, onePlayerViewport.w);
        }
        else if (totalPlayers == 2)
        {
            if(horizontalSplitScreen)
            {
                return new Rect(twoPlayerHorizontalViewports[whichPlayer].x, twoPlayerHorizontalViewports[whichPlayer].y, twoPlayerHorizontalViewports[whichPlayer].z, twoPlayerHorizontalViewports[whichPlayer].w);
            }
            else
            {
                return new Rect(twoPlayerVerticalViewports[whichPlayer].x, twoPlayerVerticalViewports[whichPlayer].y, twoPlayerVerticalViewports[whichPlayer].z, twoPlayerVerticalViewports[whichPlayer].w);
            }
        }
        else if(totalPlayers >= 3)
        {
            whichPlayer -= 1;
            return new Rect(threeOrMorePlayersViewPorts[whichPlayer].x, threeOrMorePlayersViewPorts[whichPlayer].y, threeOrMorePlayersViewPorts[whichPlayer].z, threeOrMorePlayersViewPorts[whichPlayer].w);
        }
        else
        {
            Debug.LogError("Invalid number of players");
            return rectExample;
        }
    }

    public LayerMask GetPlayerLayerMask(int whichPlayer)
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
}
