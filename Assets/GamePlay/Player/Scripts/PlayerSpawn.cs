using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{ 
    const string PATH = "Asset/Resources/Player";

    public void Spawn(Transform parent)
    {
        Debug.Log("LoadPlayer");
        GameObject player = (GameObject)GameObject.Instantiate(Resources.Load("Player", typeof(GameObject)), parent);
        player.transform.position = transform.position;
    }
}
