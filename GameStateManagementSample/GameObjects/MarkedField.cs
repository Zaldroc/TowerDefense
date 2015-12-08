using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class MarkedField:GameObject
    {
        private bool marked;
        private bool ok;
        
        public MarkedField(Vector2 position, Texture2D texture, Vector2 scale):base(position, texture, scale)
        {
            marked = false;
            ok = true;
        }

        public void Mark()
        {
            marked = true;
        }

        public void DeMark()
        {
            marked = false;
        }

        public bool IsMarked()
        {
            return marked;
        }

        public void Ok()
        {
            ok = true;
        }

        public void NotOk()
        {
            ok = false;
        }

        public bool IsOk()
        {
            return ok;
        }

        public Color GetColor()
        {
            if (marked)
            {
                if(ok)
                    return new Color(Color.Green, 0.5f);
                return new Color(Color.Red, 0.5f);
            }
            return Color.White;
        }
    }
}
