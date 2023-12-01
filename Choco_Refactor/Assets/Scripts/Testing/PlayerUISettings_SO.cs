using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerUIPositionSettings")]
public class PlayerUISettings_SO : ScriptableObject
{
    [Header("Name and Screen Resolution")]
    [SerializeField] string UIType;
    [SerializeField] Vector2 screenResolution;
    [Header("Input")]
    [SerializeField] Vector2 anchorPos;
    [SerializeField] Vector2 widthHeightRatio;
    [SerializeField] float widthPercentage; //equal the percentage of the screen width
    [SerializeField] float widthPercentagePositionOffset;
    [SerializeField] float heightPercentage; //equal the percentage of the screen height
    [SerializeField] float heightPercentagePositionOffset;

    [Header("Output")]
    [SerializeField] float actualWidth;
    [SerializeField] float actualHeight;
    [SerializeField] float actualXPos;
    [SerializeField] float actualYPos;

    [ContextMenu("SetUI")]
    public void SetUI(RectTransform passIn)
    {
        screenResolution = new Vector2(Screen.width, Screen.height);

        actualWidth = (widthPercentage * widthHeightRatio.x) * screenResolution.x;
        if (anchorPos.x == 0)
        {
            actualXPos = 0;
        }
        else
        {
            actualXPos = widthPercentagePositionOffset * actualWidth;
        }

        actualHeight = (heightPercentage * widthHeightRatio.y) * screenResolution.y;
        /*
        if (anchorPos.y == 0)
        {
            actualYPos = 0;
        }
        else
        {
            actualYPos = heightPercentagePositionOffset * actualHeight;
        }
        */
        actualYPos = heightPercentagePositionOffset * actualHeight;

        passIn.anchorMin = anchorPos;
        passIn.anchorMax = anchorPos;
        passIn.pivot = anchorPos;

        //set position x and y
        passIn.anchoredPosition = new Vector2(actualXPos, actualYPos);
        //set width and height
        passIn.sizeDelta = new Vector2(actualWidth, actualHeight);
    }

    public void SetPositionFromEditor(RectTransform passIn)
    {
        anchorPos = passIn.anchorMin;
        CalculateAspectRatio(passIn.sizeDelta.x, passIn.sizeDelta.y);
        widthPercentage = passIn.sizeDelta.x / screenResolution.x;
        heightPercentage = passIn.sizeDelta.y / screenResolution.y;
        widthPercentagePositionOffset = passIn.anchoredPosition.x / passIn.sizeDelta.x;
        heightPercentagePositionOffset = passIn.anchoredPosition.y / passIn.sizeDelta.y;
    }

    public void CalculateAspectRatio(float xSize, float ySize)
    {
        if(xSize > ySize)
        {
            widthHeightRatio = new Vector2(1, ySize / xSize);
        }
        else if(xSize < ySize)
        {
            widthHeightRatio = new Vector2(xSize / ySize, 1);
        }
        else
        {
            widthHeightRatio = new Vector2(1, 1);
        }
    }
}