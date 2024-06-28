using System;
using System.Collections;
using System.Collections.Generic;
using Game.Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.World
{
    public class Environment : MonoBehaviour,IWorldObject
    {
        public List<Transform> childs;
        public EShape shape;
        public Vector2Int coordinate { get; set; }
        public EActionType action { get; set; }

        private void Start()
        {
            action = EActionType.RemoveHead;
        }
    }
}


