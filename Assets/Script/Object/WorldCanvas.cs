using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Game.Character;
using UnityEngine;

namespace Game
{
    public abstract class WorldCanvas : PoolMember , IWorldCanvas
    {
        [field: SerializeField] public Canvas canvas { get; set; }
        [SerializeField] protected Vector3 worldPosition;
        [SerializeField] protected Color heroColor;
        [SerializeField] protected Color monsterColor;

        public abstract void Initialize(Actor actor);
        
        private void FixedUpdate()
        {
            LookatCamera();
        }

        public void LookAtCamera()
        {
            throw new NotImplementedException();
        }

        public async Task SetToWorldSpace()
        {
            canvas.worldCamera = Camera.main;
            await Task.Delay(10);
            transform.localPosition = worldPosition;
        } 
        
        public void LookatCamera()
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                Camera.main.transform.rotation * Vector3.up);
        }


    }
}


