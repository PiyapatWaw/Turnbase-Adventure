

using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Character
{
    public class Hero : Actor
    {
        public override void Initialize(ESpawnable type,int statusGrant)
        {
            MaxHP = 4;
            HP = MaxHP;
            Attack = 1;
            
            action = EActionType.Party;
            base.Initialize(type,statusGrant);
        }

        public override void EndTurn()
        {
            if (action == EActionType.Over)
            {
                for (int i = 0; i < statusGrant; i++)
                {
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
        }
    }
}
