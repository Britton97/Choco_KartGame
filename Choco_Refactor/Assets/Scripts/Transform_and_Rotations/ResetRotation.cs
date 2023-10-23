using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour
{
    public void Reset()
    {
        transform.localRotation = Quaternion.identity;
    }
}
