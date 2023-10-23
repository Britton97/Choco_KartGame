using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatToTextBeh : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private DataFloat dataFloat;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = dataFloat.DataValue.ToString("F0");
    }
}
