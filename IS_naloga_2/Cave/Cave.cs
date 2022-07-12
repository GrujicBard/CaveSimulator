using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_naloga_2
{
    public class Cave
    {
        public CaveCell[,] CellMap { get; set; }
        private CaveCell[,] oldMap;

        const int width = 48;
        const int height = 48;
        public int Columns { get { return CellMap.GetLength(0); } }
        public int Rows { get { return CellMap.GetLength(1); } }

        Random rnd = new Random();

        const float chanceToStartAlive = 0.4f;
        const float chanceToStartAliveEdge = 0.85f;
        const int numOfSimulations = 20;


        public Cave()
        {
            CellMap = new CaveCell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    CellMap[x, y] = new CaveCell();
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1 )
                    {
                        if (rnd.NextDouble() < chanceToStartAliveEdge)
                        {

                            CellMap[x, y].IsAlive = true;
                        }
                    }
                    else
                    {
                        if (rnd.NextDouble() < chanceToStartAlive)
                        {

                            CellMap[x, y].IsAlive = true;
                        }
                    }

                }
            }
            oldMap = CellMap;
        }

        public void Run()
        {
            for (int i = 0; i < numOfSimulations; i++)
            {
                Simulate();
            }
            
        }

        private void Simulate()
        {
            CountLiveNeighbours();
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    CellMap[i, j].LiveNeighbours = oldMap[i, j].LiveNeighbours;
                }
            }

            foreach (var cell in CellMap)
            {
               cell.DetermineNextState();
            }
        }

        private void CountLiveNeighbours()
        {
            int neighbours = 0;
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    if (i > 0 & j > 0) //-1,1
                    {
                        if (oldMap[i - 1, j - 1].IsAlive) neighbours++;
                    }
                    if (i > 0) //-1,0
                    {
                        if (oldMap[i - 1, j].IsAlive) neighbours++;
                    }
                    if (i > 0 & j < Columns - 1) //-1,1
                    {
                        if (oldMap[i - 1, j + 1].IsAlive) neighbours++;
                    }
                    if (j > 0) //0,-1 
                    {
                        if (oldMap[i, j - 1].IsAlive) neighbours++;
                    }
                    if (j < Columns - 1) //0,1 
                    {
                        if (oldMap[i, j + 1].IsAlive) neighbours++;
                    }
                    if (i < Rows - 1 & j > 0) //1,-1
                    {
                        if (oldMap[i + 1, j - 1].IsAlive) neighbours++;
                    }
                    if (i < Rows - 1) //1,0 
                    {
                        if (oldMap[i + 1, j].IsAlive) neighbours++;
                    }
                    if (i < Rows - 1 & j < Columns - 1) //1,1
                    {
                        if (oldMap[i + 1, j + 1].IsAlive) neighbours++;
                    }
                    oldMap[i, j].LiveNeighbours = neighbours;
                    neighbours = 0;
                }
            }
        }

    }

}
