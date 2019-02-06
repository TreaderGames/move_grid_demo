using UnityEngine;
using System;

namespace Treader.SwipeInput
{
    [RequireComponent(typeof(SwipeData))]
    public class SwipeObservable : MonoBehaviour
    {
        public Action<SwipeData.Direction> OnDirectionChange;

        SwipeData swipeData;
        Vector2 startPos;

        bool isDragging = false;
        SwipeData.Direction currentDirection = SwipeData.Direction.None;
        SwipeData.Direction previousDirection = SwipeData.Direction.None;

        // Start is called before the first frame update
        void Start()
        {
            startPos = Vector2.zero;
            swipeData = GetComponent<SwipeData>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                DoSwipe();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Reset();
            }
        }

        void DoSwipe()
        {
            if (!isDragging)
            {
                startPos = Input.mousePosition;
                isDragging = true;
            }
            else
            {
                Vector2 swipeVector = (Vector2)Input.mousePosition - startPos;
                if (Vector3.Magnitude(swipeVector) >= swipeData.swipeLength)
                {
                    UpdateDirection(swipeVector);
                    TriggerSwipeEvent();
                    startPos = Input.mousePosition;
                }
            }
        }

        void UpdateDirection(Vector2 swipeVector)
        {
            if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
            {
                if (swipeVector.x > 0)
                {
                    currentDirection = SwipeData.Direction.Right;
                }
                else
                {
                    currentDirection = SwipeData.Direction.Left;
                }
            }
            else
            {
                if (swipeVector.y > 0)
                {
                    currentDirection = SwipeData.Direction.Up;
                }
                else
                {
                    currentDirection = SwipeData.Direction.Down;
                }
            }
        }

        void TriggerSwipeEvent()
        {
            if (currentDirection != SwipeData.Direction.None && currentDirection != previousDirection) 
            {
                previousDirection = currentDirection;
                Debug.Log("Swipe Direction: " + currentDirection.ToString());
                OnDirectionChange?.Invoke(currentDirection);
            }
        }

        void Reset()
        {
            startPos = Vector2.zero;
            isDragging = false;
        }
    }
}
