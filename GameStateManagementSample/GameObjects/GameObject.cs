using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class GameObject
    {
        private Vector2 position;
        private Texture2D texture;

        public GameObject(Vector2 p, Texture2D t)
        {
            position = p;
            texture = t;
        }

        protected void SetPosition(Vector2 p)
        {
            position = p;
        }

        protected Vector2 GetPosition()
        {
            return position;
        }

        protected Texture2D GetTexture()
        {
            return texture;
        }
    }
}
