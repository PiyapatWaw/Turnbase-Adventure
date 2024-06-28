using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface
{
    public interface IPoolMember : IDisposable
    {
        public bool IsInPool { get; set; }
    }
}


