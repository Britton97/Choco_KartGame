using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PlayerIndividualUISetter : MonoBehaviour
{
    [SerializeField] PlayerUISettings_SO onePlayerSettings, twoPlayerHorizontalSettings, twoPlayerVerticalSettings ,threeOrMorePlayerSettings;
    [SerializeField] int testPlayerCount;
    [SerializeField] bool testVerticalScreen;
    
    public void SetUI(int playerCount, bool verticalScreen)
    {
        verticalScreen = !verticalScreen;
        if(playerCount == 1 && onePlayerSettings != null)
        {
            onePlayerSettings.SetUI(GetComponent<RectTransform>());
        }
        else if(playerCount == 2 && twoPlayerHorizontalSettings != null && twoPlayerVerticalSettings != null)
        {
            if(verticalScreen)
            {
                twoPlayerVerticalSettings.SetUI(GetComponent<RectTransform>());
            }
            else
            {
                twoPlayerHorizontalSettings.SetUI(GetComponent<RectTransform>());
            }
        }
        else if(playerCount >= 3 && threeOrMorePlayerSettings != null)
        {
            threeOrMorePlayerSettings.SetUI(GetComponent<RectTransform>());
        }
        //uiSettings.SetUI(GetComponent<RectTransform>());
    }

    [ContextMenu("TestUI")]
    public void TestUI()
    {
        SetUI(testPlayerCount, testVerticalScreen);
    }
}
