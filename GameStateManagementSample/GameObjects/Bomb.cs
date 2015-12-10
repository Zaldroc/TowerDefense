using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public Bomb(Bomb bomb, Vector2 position, Vector2 direction):base(bomb, position, direction)
        {
            range = bomb.range;
        }

        public Bomb(Vector2 position, Texture2D texture, Vector2 scale, int damage, float speed, Vector2 direction, float range):base(position, texture, scale, damage, speed, direction)
        {
            this.range = range;
        }

        public float GetRange()
        {
            return range;
        }
    }
}
