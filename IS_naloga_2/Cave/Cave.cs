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
        private CaveCell[,] OldMap;

        const int WIDTH = 48;
        const int HEIGHT = 48;
        public int Columns { get { return CellMap.GetLength(0); } }
        public int Rows { get { return CellMap.GetLength(1); } }

        Random rnd = new Random();

        const float CHANCE_TO_START_ALIVE = 0.4f;//Chance for cells to be alive
        const float CHANCE_TO_START_ALIVE_EDGE = 0.85f; //Chance for cells on edge to be alive
        const int NUM_OF_SIMULATIONS = 20; //Number of iterations that rules are executed


        public Cave()
        {
            CellMap = new CaveCell[WIDTH, HEIGHT];

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    CellMap[x, y] = new CaveCell();
                    if (x == 0 || y == 0 || x == WIDTH - 1 || y == HEIGHT - 1) //Edges
                    {
                        if (rnd.NextDouble() < CHANCE_TO_START_ALIVE_EDGE) //Higher chances for cells on edge to be alive
                        {

                            CellMap[x, y].IsAlive = true;
                        }
                    }
                    else
                    {
                        if (rnd.NextDouble() < CHANCE_TO_START_ALIVE)
                        {

                            CellMap[x, y].IsAlive = true;
                        }
                    }

                }
            }
            OldMap = CellMap;
        }

        public void Run()
        {
            for (int i = 0; i < NUM_OF_SIMULATIONS; i++)
            {
                Simulate();
            }

        }

        private void Simulate()
        {
            //First count live neighbours
            CountLiveNeighbours();
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    CellMap[i, j].LiveNeighbours = OldMap[i, j].LiveNeighbours;
                }
            }

            //Then determine next state based on rules in CaveCell.cs
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
                        if (OldMap[i - 1, j - 1].IsAlive) neighbours++;
                    }
                    if (i > 0) //-1,0
                    {
                        if (OldMap[i - 1, j].IsAlive) neighbours++;
                    }
                    if (i > 0 & j < Columns - 1) //-1,1
                    {
                        if (OldMap[i - 1, j + 1].IsAlive) neighbours++;
                    }
                    if (j > 0) //0,-1 
                    {
                        if (OldMap[i, j - 1].IsAlive) neighbours++;
                    }
                    if (j < Columns - 1) //0,1 
                    {
                        if (OldMap[i, j + 1].IsAlive) neighbours++;
                    }
                    if (i < Rows - 1 & j > 0) //1,-1
                    {
                        if (OldMap[i + 1, j - 1].IsAlive) neighbours++;
                    }
                    if (i < Rows - 1) //1,0 
                    {
                        if (OldMap[i + 1, j].IsAlive) neighbours++;
                    }
                    if (i < Rows - 1 & j < Columns - 1) //1,1
                    {
                        if (OldMap[i + 1, j + 1].IsAlive) neighbours++;
                    }


                    OldMap[i, j].LiveNeighbours = neighbours;
                    neighbours = 0;
                }
            }
        }

    }

}
