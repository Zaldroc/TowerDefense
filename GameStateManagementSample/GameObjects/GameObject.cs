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
        private Vector2 scale;
        private float rotation;
        private float rotationInDeg;

        public GameObject(Vector2 position, Texture2D texture, Vector2 scale)
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
            if (position == null)
                new Vector2(0, 0);
            return this.position;
        }

        public Texture2D GetTexture()
        {
            return this.texture;
        }

        public Vector2 GetScale()
        {
            return this.scale;
        }

        public void SetRotation(float rotation)
        {
            this.rotation = rotation;

            rotationInDeg = (float)(360 / (2 * Math.PI)) * rotation;
        }

        public void SetRotationInDegrees(float degrees)
        {
            double rad = ((2 * Math.PI) / 360) * degrees;
            rotationInDeg = degrees;

            SetRotation((float)rad);
        }

        public float GetRotationInDegrees()
        {
            return rotationInDeg;
        }

        public float GetRotation()
        {
            return rotation;
        }
    }
}
