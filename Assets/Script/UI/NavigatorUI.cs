using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class NavigatorUI : MonoBehaviour
    {
        [SerializeField] private Image up, down, left, right;

        public void UpdateNavigator(Vector2Int direction)
        {
            up.gameObject.SetActive(true);
            down.gameObject.SetActive(true);
            left.gameObject.SetActive(true);
            right.gameObject.SetActive(true);
            if (direction.x == 1 && direction.y == 0)
            {
                left.gameObject.SetActive(false);
            }
            else if (direction.x == 0 && direction.y == 1)
            {
                down.gameObject.SetActive(false);
            }
            else if (direction.x == -1 && direction.y == 0)
            {
                right.gameObject.SetActive(false);
            }
            else if (direction.x == 0 && direction.y == -1)
            {
                up.gameObject.SetActive(false);
            }
        }
    }
}


