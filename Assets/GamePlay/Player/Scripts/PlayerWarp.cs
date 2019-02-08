using UnityEngine;
using System;
using Treader.SwipeInput;

public class PlayerWarp : MonoBehaviour
{
    public Action OnWarpBegin;

    [SerializeField] RectTransform playerArea;
    [SerializeField] PlayerChangeDirection changeDirection;

    RectTransform gridArea;
    float halfPlayerHeight;

    public void InitPlayerWarp(RectTransform grid)
    {
        gridArea = grid;
        halfPlayerHeight = (playerArea.rect.height * transform.localScale.y) / 2;

        Debug.Log("Half Player Height" + halfPlayerHeight);
    }

    bool IsTouchingEnd()
    {
        if (gridArea != null)
        {
            float offset = 0;
            float diff = 0;
            if (changeDirection.pNextDirection == SwipeData.Direction.Left || changeDirection.pNextDirection == SwipeData.Direction.Right)
            {
                offset = changeDirection.pNextDirection == SwipeData.Direction.Left ? gridArea.rect.xMin : gridArea.rect.xMax;
                diff = (Mathf.Abs(offset) - Mathf.Abs(transform.localPosition.x));

                return diff <= halfPlayerHeight;
            }
            else
            {
                offset = changeDirection.pNextDirection == SwipeData.Direction.Up ? gridArea.rect.yMax : gridArea.rect.yMin;
                diff = (Mathf.Abs(offset) - Mathf.Abs(transform.localPosition.y));

                return diff <= halfPlayerHeight;
            }
        }

        return false;
    }

    void Update()
    {
        if (IsTouchingEnd())
        {
            Debug.Log(">>>>IsTouchingEnd");
        }
    }
}
