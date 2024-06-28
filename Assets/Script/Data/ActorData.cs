using Game.Character;
using UnityEngine;

namespace Game.Data
{
    public struct ActorData
    {
        public ActorData(/*Actor actor,*/ Vector3 position, Vector3 eulerAngles, Vector2Int coordinate, Tile tile)
        {
            /*Actor = actor;*/
            this.position = position;
            this.eulerAngles = eulerAngles;
            this.coordinate = coordinate;
            this.tile = tile;
        }
        
        /*public Actor Actor;*/
        public Vector3 position;
        public Vector3 eulerAngles;
        public Vector2Int coordinate;
        public Tile tile;
    }
}