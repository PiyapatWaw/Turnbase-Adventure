using System.Collections.Generic;
using Game.Interface;
using Game.World;
using UnityEngine;

namespace Game
{
    public class Tile : MonoBehaviour
    {
        public GameObject visual { get; set; }
        public IWorldObject worldObject;
        public Vector2Int coordinate;
        public Vector3 characterPosition;
        public Dictionary<ENeighborType, List<Tile>> neighbor { get; set; }

        public void Initialize(Transform parent,Vector2Int coordinate,int size)
        {
            this.coordinate = coordinate;
            transform.parent = parent;
            transform.localPosition = new Vector3(coordinate.x * size * 2, 0, coordinate.y * size * 2);
            transform.localScale = new Vector3(size, size, size);
            characterPosition = new Vector3(transform.position.x, size, transform.position.z);
        }
        
    }
}


