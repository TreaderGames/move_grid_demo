using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        transform.position += transform.up * playerData.speed * Time.fixedDeltaTime;
    }
}
