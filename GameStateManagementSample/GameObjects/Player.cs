using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class Player
    {
        private int points;

        public void SetPoints(int p)
        {
            points = p;
        }

        public int GetPoints()
        {
            return points;
        }
    }
}
