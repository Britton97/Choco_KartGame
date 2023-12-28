using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Slider_Beh : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        if (_slider == null)
        {
            _slider = GetComponent<Slider>();
        }
    }

    public void SetSliderValue(float value)
    {
        _slider.value = value;
    }
}
