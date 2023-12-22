using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Path Creator")]
public class PathCreator : ScriptableObject
{
    [SerializeField] private string _name;
    [ReadOnlyInspector]
    public string _filePath = "NULL";
    private void OnValidate()
    {
        //Debug.Log("OnValidate");
        //string filePath = AssetDatabase.GetAssetPath(this);
        //int index = filePath.LastIndexOf('/');

        //filePath = filePath.Substring(0, index);
        //_filePath = $"{filePath}/" + _name;
    }
}