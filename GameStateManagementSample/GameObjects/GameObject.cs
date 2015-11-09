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
        private float scale;

        public GameObject(Vector2 p, Texture2D t, float s)
        {
            position = p;
            texture = t;
            scale = s;
        }

        public void SetPosition(Vector2 p)
        {
            position = p;
        }

        public  Vector2 GetPosition()
        {
            return position;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public float GetScale()
        {
            return scale;
        }
    }
}
