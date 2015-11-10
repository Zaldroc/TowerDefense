using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class Player
    {
        private int points;

        public void SetPoints(int points)
        {
            this.points = points;
        }

        public int GetPoints()
        {
            return this.points;
        }
    }
}
