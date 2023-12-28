using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameObject List", fileName = "Data GameObject List")]
public class DataGameObjectList : ScriptableObject
{
    [SerializeField] private List<GameObject> gameObjectlist = new List<GameObject>();
    public int Count { get { return gameObjectlist.Count; } }

    public GameObject GetGameObjectFromList(int arrayPos)
    {
        //if arrayPos is out of range, return null
        if (arrayPos < 0 || arrayPos > gameObjectlist.Count - 1)
        {
            return null;
        }
        else
        {
            return gameObjectlist[arrayPos];
        }
    }
}
