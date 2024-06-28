using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public interface IWorldCanvas 
    {
        public Canvas canvas { get; set; }
        public void LookAtCamera();
        public Task SetToWorldSpace();
    }
}


