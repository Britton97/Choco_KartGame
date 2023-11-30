using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<GameObject> spawnPoints;

    private void Awake()
    {
        //TurnOffMeshRenders();
        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        GameObject randSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject player = Instantiate(playerPrefab, randSpawnPoint.transform.position, randSpawnPoint.transform.rotation);
    }

    private void TurnOffMeshRenders()
    {
        foreach (GameObject spawnPoint in spawnPoints)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
