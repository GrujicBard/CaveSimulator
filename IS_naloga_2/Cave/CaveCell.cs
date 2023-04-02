using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_naloga_2
{
    public class CaveCell
    {
        const int deathLimit = 3;
        const int birtLimit = 4;
        public bool IsAlive { get; set; }
        public int LiveNeighbours { get; set; }

        public CaveCell()
        {
            IsAlive = false;
            LiveNeighbours = 0;
        }

        public void SwitchState()
        {
            IsAlive = !IsAlive;
        }

        public void DetermineNextState() // Rules to determine next state
        {
            if (IsAlive) 
            {
                if (LiveNeighbours < deathLimit)
                {
                    IsAlive = false;
                }
                else
                {
                    IsAlive = true;
                }
            }
            else
            {
                if (LiveNeighbours > birtLimit)
                {
                    IsAlive = true;
                }
                else
                {
                    IsAlive = false;
                }
            }
                
        }
    }
}
