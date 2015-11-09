using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    public enum PathBlockEnum
    {
        NORMAL,SPAWN,GOAL
    }

    class PathBlock:GameObject
    {
        private PathBlockEnum type;
        public PathBlock(Vector2 p, Texture2D t,float s,PathBlockEnum type):base(p,t,s)
        {
            this.type = type;
        }

        public PathBlockEnum GetType()
        {
            return this.type;
        }

        public bool IsNormal()
        {
            if (this.type == PathBlockEnum.NORMAL)
                return true;
            return false;
        }

        public bool IsGoal()
        {
            if (this.type == PathBlockEnum.GOAL)
                return true;
            return false;
        }

        public bool IsSpawn()
        {
            if (this.type == PathBlockEnum.SPAWN)
                return true;
            return false;
        }
    }
}
