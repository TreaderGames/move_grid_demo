using UnityEngine;

namespace Treader.SwipeInput
{
    public class SwipeData : MonoBehaviour
    {
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
