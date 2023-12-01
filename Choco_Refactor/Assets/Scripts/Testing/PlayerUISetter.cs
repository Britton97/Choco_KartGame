using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUISetter : MonoBehaviour
{
    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] PlayerLayerManager_SO playerLayerManager;
    [SerializeField] private bool overridePlayerCount;
    [SerializeField] int playerCount;

    [Header("Player One UI Setters")]
    [SerializeField] GameObject playerOneUIHolder;
    [SerializeField] List<PlayerIndividualUISetter> playerOneUIList;

    [Header("Player Two UI Setters")]
    [SerializeField] GameObject playerTwoUIHolder;
    [SerializeField] List<PlayerIndividualUISetter> playerTwoUIList;

    [Header("Player Three UI Setters")]
    [SerializeField] GameObject playerThreeUIHolder;
    [SerializeField] List<PlayerIndividualUISetter> playerThreeUIList;

    [Header("Player Four UI Setters")]
    [SerializeField] GameObject playerFourUIHolder;
    [SerializeField] List<PlayerIndividualUISetter> playerFourUIList;

    [ContextMenu("PositionandScaleUI")]
    public void PositionandScaleUI()
    {
        if (!overridePlayerCount)
        {
            playerCount = playerInputManager.playerCount;
        }
        //playerUISettings.SetUI(GetComponent<RectTransform>());
        if (playerCount == 1)
        {
            playerOneUIHolder.SetActive(true);
            playerTwoUIHolder.SetActive(false);
            playerThreeUIHolder.SetActive(false);
            playerFourUIHolder.SetActive(false);
            foreach (PlayerIndividualUISetter playerOneUI in playerOneUIList)
            {
                playerOneUI.SetUI(playerCount, playerLayerManager.horizontalSplitScreen);
            }
        }
        else if(playerCount == 2)
        {
            playerOneUIHolder.SetActive(true);
            playerTwoUIHolder.SetActive(true);
            playerThreeUIHolder.SetActive(false);
            playerFourUIHolder.SetActive(false);
            foreach (PlayerIndividualUISetter playerOneUI in playerOneUIList)
            {
                playerOneUI.SetUI(playerCount, playerLayerManager.horizontalSplitScreen);
            }
            foreach (PlayerIndividualUISetter playerTwoUI in playerTwoUIList)
            {
                playerTwoUI.SetUI(playerCount, playerLayerManager.horizontalSplitScreen);
            }
        }
        else if(playerCount == 3)
        {
            playerOneUIHolder.SetActive(true);
            playerTwoUIHolder.SetActive(true);
            playerThreeUIHolder.SetActive(true);
            playerFourUIHolder.SetActive(false);
            foreach (PlayerIndividualUISetter playerOneUI in playerOneUIList)
            {
                playerOneUI.SetUI(playerCount, playerLayerManager.horizontalSplitScreen);
            }
            foreach (PlayerIndividualUISetter playerTwoUI in playerTwoUIList)
            {
                playerTwoUI.SetUI(playerCount, playerLayerManager.horizontalSplitScreen);
            }
            foreach (PlayerIndividualUISetter playerThreeUI in playerThreeUIList)
            {
                playerThreeUI.SetUI(playerCount, playerLayerManager.horizontalSplitScreen);
            }
        }
        else if(playerCount == 4)
        {
            playerOneUIHolder.SetActive(true);
            playerTwoUIHolder.SetActive(true);
            playerThreeUIHolder.SetActive(true);
            playerFourUIHolder.SetActive(true);
            foreach (PlayerIndividualUISetter playerOneUI in playerOneUIList)
            {
                playerOneUI.SetUI(playerCount, playerLayerManager.horizontalSplitScreen);
            }
            foreach (PlayerIndividualUISetter playerTwoUI in playerTwoUIList)
            {
                playerTwoUI.SetUI(playerCount, playerLayerManager.horizontalSplitScreen);
            }
            foreach (PlayerIndividualUISetter playerThreeUI in playerThreeUIList)
            {
                playerThreeUI.SetUI(playerCount, playerLayerManager.horizontalSplitScreen);
            }
            foreach (PlayerIndividualUISetter playerFourUI in playerFourUIList)
            {
                playerFourUI.SetUI(playerCount, playerLayerManager.horizontalSplitScreen);
            }
        }
        else
        {
            playerOneUIHolder.SetActive(false);
            playerTwoUIHolder.SetActive(false);
            playerThreeUIHolder.SetActive(false);
            playerFourUIHolder.SetActive(false);
        }
    }
}
