using System;
using UnityEngine;

public class Gameplay : UnitySingleton<Gameplay>
{
    public Transform spawnPoint;
    public float respawnTime = 1;
    public GameObject playerPrefab;

    private void Start()
    {
        Player.OnDie.AddListener(RespawnRoutine);
        Instantiate(playerPrefab, spawnPoint.position,spawnPoint.rotation);
    }

    public async void RespawnRoutine()
    {
        await new WaitForSeconds(respawnTime);
        Instantiate(playerPrefab, spawnPoint.position,spawnPoint.rotation);
    }
}