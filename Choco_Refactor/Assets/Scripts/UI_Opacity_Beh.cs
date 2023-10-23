using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Opacity_Beh : MonoBehaviour
{
    [SerializeField] DataFloat boostValue;
    [SerializeField] private Image image;
    private Kart_Input input;

    #region OnEnable/OnDisable
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    #endregion

    private void Awake()
    {
        input = new Kart_Input();
    }

    // Update is called once per frame
    void Update()
    {
        var imageAlpha = image.color;
        imageAlpha.a = boostValue.DataValue;
        image.color = imageAlpha;

        float button = input.Kart_Controls.ActionButton.ReadValue<float>(); //read value of A button
        if (button == 1) //if 'A' button held down execute below
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
        }
    }
}
