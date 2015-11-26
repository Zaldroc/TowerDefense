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
        private float rotation;

        public GameObject(Vector2 position, Texture2D texture, float scale)
        {
            this.position = position;
            this.texture = texture;
            this.scale = scale;

            rotation = 0;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 GetPosition()
        {
            return this.position;
        }

        public Texture2D GetTexture()
        {
            return this.texture;
        }

        public float GetScale()
        {
            return this.scale;
        }

        public void SetRotation(float rotation)
        {
            this.rotation = rotation;
        }

        public float GetRotation()
        {
            return rotation;
        }
    }
}
