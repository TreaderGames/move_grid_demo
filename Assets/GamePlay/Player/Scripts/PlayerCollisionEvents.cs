using UnityEngine;
using System;

public class PlayerCollisionEvents : MonoBehaviour
{
    public Action OnCollidingWithNode;

    [SerializeField] LayerMask nodeLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & nodeLayer) != 0) //Check if the layer mask includes the layer that the collision object is on.
        {
            OnCollidingWithNode?.Invoke();
        }
    }
}
