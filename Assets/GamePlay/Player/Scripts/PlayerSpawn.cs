using UnityEngine;
using System;

/// <summary>
/// Instantiates the player at its current location and initializes the screen warp if applicable
/// </summary>
public class PlayerSpawn : MonoBehaviour
{
    [Serializable]
    public struct SpawnData
    {
        public GameObject playerPref;
        public Transform parent;
        public RectTransform gridArea;
    }
    const string PATH = "Asset/Resources/Player";

    SpawnData spawn;
    GameObject player;
    public void Spawn(SpawnData spawnData)
    {
        this.spawn = spawnData;
        InitializePlayer();
        InitPlayerWarp();
    }

    void InitializePlayer()
    {
        player = (GameObject)GameObject.Instantiate(spawn.playerPref, spawn.parent);
        player.transform.position = transform.position;
        player.transform.SetSiblingIndex(1);
    }

    void InitPlayerWarp()
    {
        PlayerWarp playerWarp = player.GetComponent<PlayerWarp>();
        if (playerWarp != null)
        {
            playerWarp.InitPlayerWarp(spawn.gridArea);
        }
    }
}
