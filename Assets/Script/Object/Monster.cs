using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Utility;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Character
{
    public class Monster : Actor
    {
        private int turnToEvolution = 4;
        public int Level { get;private set; }
        protected int evolutionRemaining;
        
        public override void Initialize(ESpawnable type,int statusGrant)
        {
            MaxHP = 4 * Level;
            HP = MaxHP;
            Attack = 1 * Level;
            evolutionRemaining = turnToEvolution;
            action = EActionType.Battle;
            base.Initialize(type,statusGrant);
        }

        public void Initialize(ESpawnable type,int statusGrant,int turnToEvolution, int level)
        {
            this.Level = level;
            this.turnToEvolution = turnToEvolution;
            Initialize(type,statusGrant);
        }

        public override void EndTurn()
        {
            evolutionRemaining--;
            if (evolutionRemaining == 0 && Level < 4)
            {
                Evolution();
            }
            else if(evolutionRemaining == 0 && Level == 4)
            {
                evolutionRemaining = turnToEvolution;
                if (Random.Range(0, 10) < 5)
                {
                    MaxHP++;
                    HP++;
                }
                else
                {
                    Attack++;
                }
            }
        }

        private void Evolution()
        {
            var actor = this;
            ActionExecuteManager.Execute(EActionType.Evolution, actor);
        }
        
        
    }
}
