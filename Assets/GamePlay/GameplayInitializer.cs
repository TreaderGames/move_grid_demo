﻿using UnityEngine;

public class GameplayInitializer : MonoBehaviour
{
    [SerializeField] GridInitialBuild gridBuilder;
    [SerializeField] Transform canvas;
    [SerializeField] GameObject player;

    PlayerSpawn playerSpawn = null;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        InitializeGridBuilder();
        InitializePlayerSpawn();
    }

    void InitializeGridBuilder()
    {
        gridBuilder = (GridInitialBuild)GameObject.Instantiate<GridInitialBuild>(gridBuilder, canvas);
        gridBuilder.transform.SetAsFirstSibling();
        gridBuilder.Init();
    }

    void InitializePlayerSpawn()
    {
        PlayerSpawn.SpawnData spawnData;
        spawnData.playerPref = player;
        spawnData.parent = canvas;
        spawnData.gridArea = gridBuilder.pGridArea;

        playerSpawn = gridBuilder.pPlayerSpawn;
        playerSpawn.Spawn(spawnData);
    }
}
