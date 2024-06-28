using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Game.Interface;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class Pooler : IPooler , IDisposable
    {
        private Transform _container;
        private PoolMember _asset;

        public Pooler(PoolMember asset , Transform containerParent)
        {
            _asset = asset;
            _container = new GameObject($"Pool [{asset.name}]").transform;
            _container.SetParent(containerParent);
            Members = new List<PoolMember>();
        }


        PoolMember IPooler.asset
        {
            get => _asset;
            set => _asset = value;
        }

        Transform IPooler.container
        {
            get => _container;
            set => _container = value;
        }

        public List<PoolMember> Members { get; set; }
        public PoolMember Get()
        {
            PoolMember member = null;
            if (Members.Count > 0)
            {
                member = Members.First(f => f.IsInPool);
                Members.Remove(member);
            }
            else
            {
                member = Creat();
            }

            member.IsInPool = false;
            member.gameObject.SetActive(true);
            return member;
        }

        public void Return(PoolMember member)
        {
            member.Dispose();
            member.IsInPool = true;
            member.gameObject.SetActive(false);
            member.transform.SetParent(_container);
            Members.Add(member);
        }

        public PoolMember Creat()
        {
            var newMember = Object.Instantiate(_asset, _container);
            return newMember;
        }


        public void Dispose()
        {
            foreach (var member in Members)
            {
                member.Dispose();
            }
        }
    }
}


