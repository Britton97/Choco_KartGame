using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterRigSpecificObject
{
    public GameObject characterObject;
    [SerializeField] CharacterBlendInfo_SO characterBlendInfo;
    public bool invertOffset = false;
    //[Range(-1, 1)] public float stateBlend;

    public void SetPos(float passIn)
    {
        if(passIn<0)
        {
            passIn = Mathf.Abs(passIn);
            if(invertOffset)
            {
                characterObject.transform.localPosition = Vector3.Lerp(characterBlendInfo.nuetralPosition, characterBlendInfo.positiveOffset, passIn);
                characterObject.transform.localEulerAngles = Vector3.Lerp(characterBlendInfo.nuetralRotation, characterBlendInfo.positiveRotation, passIn);
            }
            else
            {
                characterObject.transform.localPosition = Vector3.Lerp(characterBlendInfo.nuetralPosition, characterBlendInfo.negativeOffset, passIn);
                characterObject.transform.localEulerAngles = Vector3.Lerp(characterBlendInfo.nuetralRotation, characterBlendInfo.negativeRotation, passIn);
            }
        }
        else if(passIn>0)
        {
            if(invertOffset)
            {
                characterObject.transform.localPosition = Vector3.Lerp(characterBlendInfo.nuetralPosition, characterBlendInfo.negativeOffset, passIn);
                characterObject.transform.localEulerAngles = Vector3.Lerp(characterBlendInfo.nuetralRotation, characterBlendInfo.negativeRotation, passIn);
            }
            else
            {
                characterObject.transform.localPosition = Vector3.Lerp(characterBlendInfo.nuetralPosition, characterBlendInfo.positiveOffset, passIn);
                characterObject.transform.localEulerAngles = Vector3.Lerp(characterBlendInfo.nuetralRotation, characterBlendInfo.positiveRotation, passIn);
            }
        }
        else
        {
            characterObject.transform.localPosition = characterBlendInfo.nuetralPosition;
        }
    }
}
