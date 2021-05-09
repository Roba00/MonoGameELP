using System;
using System.Collections.Generic;
using System.Text;
using MonogameELP.Components;

namespace MonogameELP.Gameobjects
{
    abstract class Enemy : GameObject
    {
        public enum State {IDLE, FOLLOW, ATTACK };
        public abstract State currentState();
        // abstract, extern, patial
        extern Enemy();
        public override Transform transform { get => base.transform; set => base.transform = value; }

        public abstract void LoadContent();

        public abstract void Initialize();

        public abstract void Update();

        public abstract void GetAnimations();

        public abstract State GetState();

        public abstract State SetState();

        public abstract void Idle();

        public abstract void Follow();

        public abstract void Attack();

        public abstract void OnTriggerEnter2d(BoxCollider trigger);
    }
}
