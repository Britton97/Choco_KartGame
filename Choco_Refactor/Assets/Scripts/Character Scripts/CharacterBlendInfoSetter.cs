using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlendInfoSetter : MonoBehaviour
{
    [SerializeField] CharacterBlendInfo_SO characterBlendInfo;


    public CharacterBlendInfo_SO GetCharacterBlendInfo()
    {
        return characterBlendInfo;
    }

    [ContextMenu("SetPositiveOffset")]
    public void SetPositiveOffset()
    {
        characterBlendInfo.positiveOffset = transform.localPosition;
        characterBlendInfo.positiveRotation = SetRotationValues();
    }
    [ContextMenu("SetNegativeOffset")]
    public void SetNegativeOffset()
    {
        characterBlendInfo.negativeOffset = transform.localPosition;
        characterBlendInfo.negativeRotation = SetRotationValues();
    }
    [ContextMenu("SetStartPos")]
    public void SetStartPos()
    {
        characterBlendInfo.nuetralPosition = transform.localPosition;
        characterBlendInfo.nuetralRotation = SetRotationValues();
    }

    public Vector3 SetRotationValues()
    {
        Vector3 temp = transform.localEulerAngles;

        for (int i = 0; i < 3; i++)
        {
            if (temp[i] > 180)
            {
                temp[i] = temp[i] - 360;
            }
        }
        return temp;
    }

    [ContextMenu("ResetToNuetral")]
    public void ResetToNuetral()
    {
        transform.localPosition = characterBlendInfo.nuetralPosition;
        transform.localEulerAngles = characterBlendInfo.nuetralRotation;
    }
}
