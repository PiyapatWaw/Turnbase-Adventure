using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Game.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Identifier : WorldCanvas
    {
        [SerializeField] private Image icon;
        [SerializeField] private Sprite normalSprite, headSprite;
        private Canvas _canvas;
        private Color _heroColor;
        private Color _monsterColor;

        public override void Initialize(Actor actor)
        {
            actor.Identifier = this;
            Type = ESpawnable.Identifier;
            icon.sprite = normalSprite;
            SetColorFromActor(actor);
            SetToWorldSpace();
        }

        public void UpdateIdentifier(Actor actor, bool head = false)
        {
            if (head)
            {
                icon.color = Color.white;
                icon.sprite = headSprite;
            }
            else
            {
                icon.sprite = normalSprite;
                SetColorFromActor(actor);
            }
        }

        private void SetColorFromActor(Actor actor)
        {
            if (actor is Hero)
            {
                icon.color = heroColor;
            }
            else if (actor is Monster)
            {
                icon.color = monsterColor;
            }
        }


        public override ESpawnable Type { get; set; }
        public override bool IsInPool { get; set; }

        public override void Dispose()
        {
        }
    }
}