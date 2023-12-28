using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MaterialInstancer_Mesh : MaterialInstancer_Abstract
{
    private void Awake()
    {
        InstanceMaterial();
    }

    public override void InstanceMaterial()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material mat = Instantiate(meshRenderer.material);
        meshRenderer.material = mat;
    }
}