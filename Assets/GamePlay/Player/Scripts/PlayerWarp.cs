using UnityEngine;
using System;
using Treader.SwipeInput;

/// <summary>
/// Responsible for the effect of the player exiting one end of the screen and entering from another
/// </summary>
public class PlayerWarp : MonoBehaviour
{
    public Action OnWarpBegin;

    [SerializeField] RectTransform playerArea;
    [SerializeField] PlayerChangeDirection changeDirection;
    [SerializeField] GameObject ghostPlayer;
    [SerializeField] PlayerCollisionEvents playerCollisionEvents;
    [SerializeField] PlayerMovment playerMovment;

    RectTransform gridArea;
    float halfPlayerHeight;
    bool ghostLoaded = false;
    bool canInitGhost = true;

    public void InitPlayerWarp(RectTransform grid)
    {
        gridArea = grid;
        halfPlayerHeight = (playerArea.rect.height * transform.localScale.y) / 2;
        playerCollisionEvents.OnCollidingWithNode += HandleCollidingWithNode;
    }

    private void HandleCollidingWithNode()
    {
        canInitGhost = true;
    }

    /// <summary>
    /// Checks whether the player is touching the end of the screen but is still visible in it.
    /// </summary>
    /// <returns></returns>
    bool IsTouchingEnd()
    {
        if (gridArea != null)
        {
            float offset = 0;
            float diff = 0;

            offset = GetOffsetByDirection(changeDirection.pCurrentDirection);
            if (IsHorzontal())
            {
                diff = (Mathf.Abs(offset) - Mathf.Abs(transform.localPosition.x));

                return diff <= halfPlayerHeight;
            }
            else
            {
                diff = (Mathf.Abs(offset) - Mathf.Abs(transform.localPosition.y));

                return diff <= halfPlayerHeight;
            }
        }

        return false;
    }

    /// <summary>
    /// Check is player has compleatly left the grid area
    /// </summary>
    /// <returns></returns>
    bool IsOutOfGridArea()
    {
        if (gridArea != null)
        {
            float offset = 0;
            float sum = 0;

            offset = GetOffsetByDirection(changeDirection.pCurrentDirection);
            sum = (Mathf.Abs(offset) + Mathf.Abs(halfPlayerHeight));
            if (IsHorzontal())
            {
                return sum <= Mathf.Abs(transform.localPosition.x);
            }
            else
            {
                return sum <= Mathf.Abs(transform.localPosition.y);
            }
        }

        return false;
    }

    /// <summary>
    /// Returns the left right top and bottom positions of the grid based on direction
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    float GetOffsetByDirection(SwipeData.Direction direction)
    {
        float offset = 0;
        switch (direction)
        {
            case SwipeData.Direction.Left:
                offset = gridArea.rect.xMin;
                break;
            case SwipeData.Direction.Right:
                offset = gridArea.rect.xMax;
                break;
            case SwipeData.Direction.Up:
                offset = gridArea.rect.yMax;
                break;
            case SwipeData.Direction.Down:
                offset = gridArea.rect.yMin;
                break;
        }

        return offset;
    }

    bool IsHorzontal()
    {
        return (changeDirection.pCurrentDirection == SwipeData.Direction.Left || changeDirection.pCurrentDirection == SwipeData.Direction.Right);
    }

    /// <summary>
    /// Creates a ghost player on the other end of the grid area to create the effect of player 
    /// entering from the other side even before the player has compleatly exited the screen.
    /// </summary>
    void InitGhost()
    {
        if (!ghostLoaded)
        {
            ghostPlayer = GameObject.Instantiate(ghostPlayer, transform.parent);
        }

        ghostPlayer.transform.rotation = transform.rotation;
        ghostPlayer.transform.SetSiblingIndex(1);

        PositionGhost();

        ghostLoaded = true;
        canInitGhost = false;
        ghostPlayer.SetActive(true);
    }

    /// <summary>
    /// Position the ghost player to the other end of the screen
    /// </summary>
    void PositionGhost()
    {
        SwipeData.Direction ghostOffsetDirection;

        float ghostHeight = halfPlayerHeight;
        float offset = 0;

        if (IsHorzontal())
        {
            ghostOffsetDirection = changeDirection.pCurrentDirection == SwipeData.Direction.Left ? SwipeData.Direction.Right : SwipeData.Direction.Left;
            offset = GetOffsetByDirection(ghostOffsetDirection);

            if (offset < 0)
            {
                ghostHeight *= -1;
            }

            ghostPlayer.transform.localPosition = new Vector3(GetOffsetByDirection(ghostOffsetDirection) + ghostHeight, transform.localPosition.y, 0);
        }
        else
        {
            ghostOffsetDirection = changeDirection.pCurrentDirection == SwipeData.Direction.Up ? SwipeData.Direction.Down : SwipeData.Direction.Up;
            offset = GetOffsetByDirection(ghostOffsetDirection);

            if (offset < 0)
            {
                ghostHeight *= -1;
            }

            ghostPlayer.transform.localPosition = new Vector3(transform.localPosition.x, GetOffsetByDirection(ghostOffsetDirection) + ghostHeight, 0);
        }
    }

    /// <summary>
    /// Place the player at the ghosts position and disable the ghost 
    /// </summary>
    void WarpPlayer()
    {
        transform.localPosition = ghostPlayer.transform.localPosition;
        ghostPlayer.SetActive(false);
    }

    void Update()
    {
        if (canInitGhost && IsTouchingEnd())
        {
            InitGhost();
        }

        if (ghostPlayer.activeInHierarchy && IsOutOfGridArea())
        {
            WarpPlayer();
        }
    }
}
