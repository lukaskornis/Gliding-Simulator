using System;
using UnityEngine;

/// <summary>
/// Manages the state of one run. Resets on death
/// Respawns player, scripted events, generate terrain
/// </summary>
public class Gameplay : UnitySingleton<Gameplay>
{
    public Transform spawnPoint;
    public float respawnTime = 1;
    public GameObject playerPrefab;

    private void Awake()
    {
        Player.OnCrash.AddListener(RespawnRoutine);
        Instantiate(playerPrefab, spawnPoint.position,spawnPoint.rotation);
    }

    public async void RespawnRoutine()
    {
        await new WaitForSeconds(respawnTime);
        Instantiate(playerPrefab, spawnPoint.position,spawnPoint.rotation);
    }
}