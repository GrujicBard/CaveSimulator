using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_naloga_2
{
    public class Cell
    {
        public State state { get; set; }
        private State nextState { get; set; }

        private double volume; // 0.00 - 100.0
        public int Counter { get; set; }
        public Color Color { get; private set; }

        public Cell()
        {
            state = State.EmptyCell;
            Volume = 100.00;
            Counter = 0;
            Color = Color.FromArgb(118, 122, 128);
        }

        public double Volume
        {
            get { return volume; }
            set
            {
                volume = Math.Round(value, 2);
                RefreshColor();
            }
        }
        public State GetState() { return state; }
        public void SetNextState(State state)
        {
            Counter = 0;
            Volume = 100.00;
            switch (state)
            {
                case State.EmptyCell:
                    Color = Color.FromArgb(118, 122, 128);
                    nextState = State.EmptyCell;
                    break;
                case State.CaveWall:
                    Color = Color.FromArgb(41, 41, 44);
                    nextState = State.CaveWall;
                    break;
                case State.Wood:
                    Color = Color.FromArgb(81, 56, 40);
                    nextState = State.Wood;
                    break;
                case State.WoodOnFire:
                    Color = Color.FromArgb(71, 11, 4);
                    nextState = State.WoodOnFire;
                    break;
                case State.Water:
                    Color = Color.FromArgb(0, 84, 118);
                    nextState = State.Water;
                    break;
                case State.Fire:
                    Color = Color.FromArgb(215, 53, 2);
                    nextState = State.Fire;
                    break;
                case State.BlackSmoke:
                    Color = Color.FromArgb(74, 74, 74);
                    nextState = State.BlackSmoke;
                    break;
                case State.WhiteSmoke:
                    Color = Color.FromArgb(172, 172, 172);
                    nextState = State.WhiteSmoke;
                    break;
                case State.Sand:
                    Color = Color.FromArgb(194, 178, 128);
                    nextState = State.Sand;
                    break;
            }
        }

        public void Refresh()
        {
            state = nextState;
        }

        public void RefreshColor()
        {
            double limit;
            if (Volume < 10.00) limit = 0.2f; // better visibility
            else if (Volume > 100.00) limit = 1.0f;
            else limit = Volume/100;
            Color = Color.FromArgb((int)(255 * limit), Color.R, Color.G, Color.B);
        }

        public enum State
        {
            EmptyCell,
            CaveWall,
            Wood,
            WoodOnFire,
            Water,
            Fire,
            BlackSmoke,
            WhiteSmoke,
            Sand
        }
    }


}
