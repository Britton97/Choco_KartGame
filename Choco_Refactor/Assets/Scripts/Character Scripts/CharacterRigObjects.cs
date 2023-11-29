using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterRigObjects : MonoBehaviour
{
    [SerializeField] private string affecting;
    //[SerializeField] CharacterRigSpecificObject test;
    [SerializeField] List<CharacterRigSpecificObject> rigObjects = new List<CharacterRigSpecificObject>();

    public void ChangeBlendState (float value)
    {
        foreach (CharacterRigSpecificObject rigObject in rigObjects)
        {
            rigObject.SetPos(value);
        }
    }
}
