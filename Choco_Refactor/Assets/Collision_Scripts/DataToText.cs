using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataToText : MonoBehaviour
{
    [SerializeField] private TextMesh _text;
    public void ToText(float passIn)
    {
        _text.text = passIn.ToString();
    }
}
