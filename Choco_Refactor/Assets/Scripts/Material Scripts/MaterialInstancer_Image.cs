using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MaterialInstancer_Image : MaterialInstancer_Abstract
{
    private void Awake()
    {
        InstanceMaterial();
    }

    public override void InstanceMaterial()
    {
        Image image = GetComponent<Image>();
        Material mat = Instantiate(image.material);
        image.material = mat;
    }
}
