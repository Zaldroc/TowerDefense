using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class Tower:GameObject
    {
        private float range;
        private int costs;
        private int shootingInterval;
        private int elapsedTime=9999;

        public bool isIdle;

        private int level;
        private int upgradeCosts;

        private Tower levelZero;

        private Projectile projectileType;

        public Tower(Vector2 position, Texture2D texture, Vector2 scale, float range, int costs, int shootingInterval, Projectile projectileType):base(position,texture,scale)
        {
            this.range = range;
            this.costs = costs;
            this.shootingInterval = shootingInterval;
            this.projectileType = projectileType;

            isIdle = true;
            level = 0;
        }

        public Tower(Tower t):this(t.GetPosition(),t.GetTexture(),t.GetScale(),t.range,t.costs,t.shootingInterval,t.projectileType)
        {
            
        }

        public void Buy()
        {
            level = 1;
            upgradeCosts = (int)(costs * 0.5f);

            levelZero = new Tower(this);
        }

        public void Upgrade()
        {
            switch(level)
            {
                case 1:
                    shootingInterval = (int)(shootingInterval * 0.75f);
                    level++;
                    upgradeCosts = costs * 1;
                    range = range * 1.25f;
                    projectileType.SetDamage((int)(projectileType.GetDamage() * 1.5f));
                    projectileType.SetSpeed((int)(projectileType.GetSpeed() * 1.5f));
                    break;
                case 2:
                    shootingInterval = (int)(levelZero.shootingInterval * 0.5f);
                    level++;
                    upgradeCosts = costs * 2;
                    range = levelZero.range * 1.5f;
                    projectileType.SetDamage((int)(levelZero.projectileType.GetDamage() * 2f));
                    projectileType.SetSpeed((int)(levelZero.projectileType.GetSpeed() * 2f));
                    break;
            }
        }

        public bool UpgradeAvailable()
        {
            return level <= 2;
        }

        public int GetUpgradeCosts()
        {
            return upgradeCosts;
        }

        public float GetRange()
        {
            return range;
        }

        public Projectile Shoot(int millisElapsed, List<Enemy> enemies)
        {
            elapsedTime += millisElapsed;
            if (elapsedTime>=shootingInterval)
            {
                Enemy target = null;
                double bestDistance=0;
                double direction=0;
                Vector2 v = new Vector2(0,0);

                foreach (Enemy e in enemies)
                {
                    double distance = Math.Sqrt(Math.Pow(GetPosition().X-e.GetPosition().X, 2) + Math.Pow(GetPosition().Y - e.GetPosition().Y, 2));
                    if (range >= distance && (distance<bestDistance||bestDistance==0))
                    {
                        target = e;
                        bestDistance = distance;
                        v = GetPosition() - e.GetPosition();
                        v.Normalize();
                        direction = Math.Atan(v.Y/v.X);
                        direction = direction * (180 / Math.PI);

                        direction = direction + 270;

                        if(v.X<0)
                            direction = direction + 180;

                        this.SetRotationInDegrees((float)direction);
                    }
                }


                if (target != null)
                {
                    elapsedTime = 0;
                    isIdle = false;
                    return new Projectile(projectileType, GetPosition(), v*(-1));
                }
                if(elapsedTime>1000)
                    isIdle = true;
            }
            return null;            
        }

        public void Move()
        {
            if(isIdle)
                SetRotationInDegrees(GetRotationInDegrees() + 0.5f);
        }

        public int GetCosts()
        {
            return costs;
        }
    }
}
