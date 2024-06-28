using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface
{
    public interface IPooler
    {
        protected PoolMember asset { get; set; }
        protected Transform container { get; set; }
        public List<PoolMember> Members { get; set; }
        
        public PoolMember Get();
        public void Return(PoolMember member);
        public PoolMember Creat();
    }
}


