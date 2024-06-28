using System;
using System.Collections;
using System.Threading.Tasks;
using Game.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HPBar : WorldCanvas
    { 
        [SerializeField] private Image fill;
        public override void Initialize(Actor actor)
        {
            actor.HpBar = this;
            Type = ESpawnable.HPBar;
            if (actor is Hero)
            {
                fill.color = heroColor;
            }
            else if(actor is Monster)
            {
                fill.color = monsterColor;
            }

            fill.fillAmount = ((float)actor.HP / (float)actor.MaxHP);

            SetToWorldSpace();
        }
        
        public async Task UpdateHp(float value)
        {
            var coroutine = LerpFill(value == 0? 0 : value);
            while (coroutine.MoveNext())
            {
                await Task.Yield();
            }
        }

        IEnumerator LerpFill(float value)
        {
            float duration = 0.3f;
            float time = 0;
            float start = fill.fillAmount;
            while (time < duration)
            {
                fill.fillAmount = Mathf.Lerp(start, value, time / duration);
                yield return null;
                time += Time.deltaTime;
            }
        }

        public override ESpawnable Type { get; set; }
        public override bool IsInPool { get; set; }
        public override void Dispose()
        {
            
        }
    }
}

