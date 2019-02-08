using System;
using System.Collections;
using Treader.SwipeInput;
using UnityEngine;

public class PlayerChangeDirection : MonoBehaviour
{
    [SerializeField] PlayerCollisionEvents playerCollisionEvents;
    
    SwipeData.Direction nextDirection;

    public SwipeData.Direction pNextDirection { get => nextDirection; }

    // Start is called before the first frame update
    void Start()
    {
        SubscribeEvents();
        nextDirection = SwipeData.Direction.Up;
    }
    
    void SubscribeEvents()
    {
        playerCollisionEvents.OnCollidingWithNode += HandleNodeCollision;
        SwipeObservable.OnDirectionChange += HandleDirectionChange;
    }

    void HandleDirectionChange(SwipeData.Direction direction)
    {
        nextDirection = direction;
    }

    void HandleNodeCollision()
    {
        ChangeDirection(nextDirection);
    }

    void ChangeDirection(SwipeData.Direction direction)
    {
        switch (direction)
        {
            case SwipeData.Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;

            case SwipeData.Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;

            case SwipeData.Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case SwipeData.Direction.Down:
                transform.rotation = Quaternion.Euler(0,0,180);
                break;

            default:
                Debug.LogError("Invalid Direction Recived");
                break;
        }
    }
}
