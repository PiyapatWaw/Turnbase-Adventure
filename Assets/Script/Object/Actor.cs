using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Interface;
using Game.Utility;
using UnityEngine;

namespace Game.Character
{
    public class Actor : PoolMember, IWorldObject
    {
        [SerializeField] private Animator _animator;
        [field: SerializeField] public int MaxHP { get; protected set; }
        [field: SerializeField] public int HP { get; protected set; }
        [field: SerializeField] public int Attack { get; protected set; }
        public override ESpawnable Type { get; set; }
        public override bool IsInPool { get; set; }
        public Vector2Int coordinate { get; set; }
        public Identifier Identifier;
        public HPBar HpBar;
        protected int statusGrant;
        [field: SerializeField] public EActionType action { get; set; }

        public virtual void Initialize(ESpawnable type,int statusGrant)
        {
            Type = type;
            this.statusGrant = statusGrant;
            GameManager.Singleton.OnEndTurnEvent += EndTurn;
        }
        
        public override void Dispose()
        {
            GameManager.Singleton.OnEndTurnEvent -= EndTurn;
        }

        public async Task AttackEnemy(Actor Target)
        {
            _animator.SetTrigger(AnimatorKey.Attack);
            transform.LookAt(Target.transform);
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorKey.Attack) &&
                   _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                await Task.Yield();
            }

            await Target.TakeDamage(GetDamage());

            await Task.Delay(150);
        }

        protected async Task Death()
        {
            _animator.SetTrigger(AnimatorKey.Death);
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorKey.Death) &&
                   _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                await Task.Yield();
            }

            await Task.Delay(150);
        }

        internal async Task TakeDamage(int damage)
        {
            _animator.SetTrigger(AnimatorKey.Hit);
            while (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorKey.Hit) &&
                   _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                await Task.Yield();
            }
            HP -= damage;
            await HpBar.UpdateHp((float)HP/(float)MaxHP);
            if (HP <= 0)
                await Death();
        }

        protected virtual int GetDamage()
        {
            return Attack;
        }

        public async Task MoveToTile(Tile target)
        {
            var coroutine = MoveTo(target.characterPosition);
            while (coroutine.MoveNext())
            {
                await Task.Yield();
            }

            coordinate = target.coordinate;
            target.worldObject = this;
        }

        private IEnumerator MoveTo(Vector3 target)
        {
            _animator.SetTrigger(AnimatorKey.Walk);
            float duration = 0.5f;
            float time = 0f;
            Vector3 start = transform.position;
            transform.LookAt(target);
            while (time < duration)
            {
                transform.position = Vector3.Lerp(start, target, time / duration);
                yield return null;
                time += Time.deltaTime;
            }

            _animator.SetTrigger(AnimatorKey.Idle);
        }

        public virtual void EndTurn()
        {
            return;
        }
    }
}