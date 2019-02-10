using UnityEngine;

namespace Treader.SwipeInput
{
    public class SwipeData : MonoBehaviour
    {
        //How long the swipe needs to be before it is registred as valid
        public float swipeLength = 0;

        public enum Direction
        {
            None,
            Left,
            Right,
            Up,
            Down
        }
    }
}
