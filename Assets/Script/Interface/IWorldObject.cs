using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface
{
    public interface IWorldObject 
    {
        public Vector2Int coordinate { get; set; }
        public EActionType action { get; set; }
    }

}

