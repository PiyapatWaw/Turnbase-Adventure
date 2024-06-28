using System;
using System.Collections.Generic;
using System.Linq;
using Game.Character;
using Game.Data;
using Game.Interface;
using Game.Setting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class ObjectSpawner
    {
        private SpawnSetting _spawnSetting;
        private Dictionary<ESpawnable, Pooler> Poolers = new Dictionary<ESpawnable, Pooler>();
        private WorldData world;
        private int[] yEulers = new[] { 0, 90, 180, 270 };
        public ObjectSpawner(SpawnSetting spawnSetting,WorldData world)
        {
            this.world = world;
            _spawnSetting = spawnSetting;
            Poolers = new Dictionary<ESpawnable, Pooler>();
            var Pooler = new GameObject("Pooler").transform;
            foreach (var hero in _spawnSetting.AllHero)
            {
                Poolers.Add(hero.Type,new Pooler(hero.Character,Pooler));
            }
            foreach (var monster in _spawnSetting.AllMonster)
            {
                Poolers.Add(monster.Type,new Pooler(monster.Character,Pooler));
            }
            Poolers.Add(ESpawnable.HPBar,new Pooler(_spawnSetting.HpBar,Pooler));
            Poolers.Add(ESpawnable.Identifier,new Pooler(_spawnSetting.Identifier,Pooler));
        }

        public Actor SpawnHero(Transform parent)
        {
            var spawnAbleType = new[] { ESpawnable.Hero_Barbarian,ESpawnable.Hero_Knight,ESpawnable.Hero_Mage,ESpawnable.Hero_Rogue };
            var spawnType = spawnAbleType[Random.Range(0, spawnAbleType.Length)];
            var result = Spawn(spawnType,world.GetEmptyRandomTile(),parent);
            if (result is Actor valid)
            {
                valid.Initialize(spawnType,_spawnSetting.Statuspoint);
                SpawnWorldCanvas(ESpawnable.Identifier,valid);
                return valid;
            }
            return null;
        }
        
        public Actor SpawnMonster(Transform parent,int level)
        {
            return SpawnMonster(parent,level,world.GetEmptyRandomTile());
        }
        
        public Actor SpawnMonster(Transform parent,int level,Tile tile)
        {
            var spawnAbleType = new[] { ESpawnable.Monster_Skeleton_Minion,ESpawnable.Monster_Skeleton_Rogue,ESpawnable.Monster_Skeleton_Warrior,ESpawnable.Monster_Skeleton_Mage };
            var spawnType = spawnAbleType[level-1];
            var result = Spawn(spawnType,tile,parent);
            if (result is Monster valid)
            {
                valid.Initialize(spawnType,_spawnSetting.Statuspoint,_spawnSetting.TurnToEvolution,level);
                SpawnWorldCanvas(ESpawnable.Identifier,valid);
                return valid;
            }
            return null;
        }
        
        public void Return(Actor actor)
        {
            var tile = world.GetTileFromCoordinate(actor.coordinate);
            if (tile != null)
            {
                tile.worldObject = null;
            }
            
            Poolers[ESpawnable.Identifier].Return(actor.Identifier);
            Poolers[actor.Type].Return(actor);
        }
        
        public void Return(PoolMember member)
        {
            Poolers[member.Type].Return(member);
        }
        
        private PoolMember Spawn(ESpawnable type,Tile tile,Transform parent)
        {
            if (tile == null)
            {
                return null;
            }
            
            var newObject = Poolers[type].Get();
            newObject.transform.parent = parent;
            newObject.transform.position = tile.characterPosition;
            newObject.transform.eulerAngles = new Vector3(0,yEulers[Random.Range(0, yEulers.Length)],0);

            if (newObject is IWorldObject worldObject)
            {
                worldObject.coordinate = tile.coordinate;
                tile.worldObject = worldObject;
            }
                
            
            return newObject;
        }

        public void SpawnWorldCanvas(ESpawnable type,Actor actor)
        {
            var hpBar = Poolers[type].Get();
            if (hpBar is WorldCanvas valid)
            {
                valid.Initialize(actor);
                valid.transform.parent = actor.transform;
            }
        }
    }
}