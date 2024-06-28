using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Character;
using Game.Data;
using UnityEngine;

namespace Game
{
    public class Party
    {
        private ObjectSpawner spawner;
        private WorldData world;
        private List<Actor> allActors;

        public Transform container;

        public Actor Head => allActors.First();
        public Actor Tail => allActors.Last();

        public List<Actor> Member => allActors;

        public Party(WorldData world,ObjectSpawner spawner)
        {
            allActors = new List<Actor>();
            this.world = world;
            this.spawner = spawner;
            container = new GameObject("Party").transform;
        }
        
        public void Add(Actor target)
        {
            target.transform.parent = container;
            target.action = EActionType.Over;
            allActors.Add(target);
            spawner.SpawnWorldCanvas(ESpawnable.HPBar,target);
            UpdateIdentifier();
        }

        public void Remove(Actor target)
        {
            spawner.Return(target.HpBar);
            target.HpBar = null;
            allActors.Remove(target);
            if(allActors.Count > 0)
                UpdateIdentifier();
        }

        public void UpdateIdentifier()
        {
            allActors[0].Identifier.UpdateIdentifier( allActors[0],true);
            for (int i = 1; i < allActors.Count; i++)
            {
                allActors[i].Identifier.UpdateIdentifier( allActors[i],false);
            }
        }
        
        public async Task UpdatePartyPosition(Tile newHead)
        {
            List<Task> alltask = new List<Task>();
            var headTile = world.GetTileFromCoordinate(allActors[0].coordinate);
            if (headTile != null)
            {
                headTile.worldObject = null;
                var task = allActors[0].MoveToTile(newHead);
                alltask.Add(task);
                for (int i = 1; i < allActors.Count; i++)
                {
                    var current = world.GetTileFromCoordinate(allActors[i].coordinate);
                    var next = world.GetTileFromCoordinate(allActors[i - 1].coordinate);
                    current.worldObject = null;
                    task = allActors[i].MoveToTile(next);
                    alltask.Add(task);
                }
            }

            await Task.WhenAll(alltask);
        }

        public bool IsPartyEmpty()
        {
            return allActors.Count == 0;
        }

        public List<Actor> GetBattleParty(int count)
        {
            List<Actor> result = new List<Actor>();
            for (int i = 0; i < count; i++)
            {
                if (allActors.Count > i)
                {
                    result.Add(allActors[i]);
                }
            }

            return result;
        }

        public Vector2Int GetWalkableDirection()
        {
            Vector2Int result = new Vector2Int(0, 0);

            if (allActors.Count > 1)
            {
                result = Head.coordinate - allActors[1].coordinate;
            }
            
            return result;
        }

        public void ReplaceMembers(List<Actor> members)
        {
            allActors = members;
        }
    }
}

