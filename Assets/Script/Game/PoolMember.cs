using System.Collections;
using System.Collections.Generic;
using Game.Interface;
using UnityEngine;

namespace Game
{
    public abstract class PoolMember : MonoBehaviour , IPoolMember
    {
        public abstract ESpawnable Type { get; set; }
        public abstract bool IsInPool { get; set; }
        public abstract void Dispose();
    }
}

