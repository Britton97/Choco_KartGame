using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CharacterBlendState")]
public class CharacterBlendInfo_SO : ScriptableObject
{
    //[SerializeField] public Vector3 positiveOffset, negativeOffset, startPos, startingRot, positiveRot, negativeRot;
    [Header("Starting Info")]
    [SerializeField] public Vector3 nuetralPosition;
    [SerializeField] public Vector3 nuetralRotation;

    [Header("Positive Info")]
    [SerializeField] public Vector3 positiveOffset;
    [SerializeField] public Vector3 positiveRotation;

    [Header("Negative Info")]
    [SerializeField] public Vector3 negativeOffset;
    [SerializeField] public Vector3 negativeRotation;
}
