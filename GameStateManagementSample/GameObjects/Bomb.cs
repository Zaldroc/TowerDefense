using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class Bomb:Projectile
    {
        float range;

        public Bomb(Projectile projectile,float range):base(projectile,projectile.GetPosition(),projectile.GetDirection())
        {
            this.range = range;
        }

        public Bomb(Bomb bomb):this((Projectile)bomb,bomb.range)
        {

        }

        public Bomb(Bomb bomb, Vector2 position, Vector2 direction):this(bomb)
        {
            SetPosition(position);
            SetDirection(direction);
        }

        public float GetRange()
        {
            return range;
        }
    }
}
