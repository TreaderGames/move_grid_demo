using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// Move the player in the direction it is facing.
    /// </summary>
    void MovePlayer()
    {
        transform.position += transform.up * playerData.speed * Time.fixedDeltaTime;
    }
}
