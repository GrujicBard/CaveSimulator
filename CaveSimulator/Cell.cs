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
        public State CellState { get; set; }
        private State NextState { get; set; }

        private double _volume; // 0.00 - 100.00
        public int Counter { get; set; }
        public Color Color { get; private set; }

        public Cell()
        {
            CellState = State.EmptyCell;
            Volume = 100.00;
            Counter = 0;
            Color = Color.FromArgb(118, 122, 128);
        }

        public double Volume
        {
            get { return _volume; }
            set
            {
                _volume = value; //_volume = Math.Round(value, 2);
                RefreshColor();
            }
        }
        public State GetState() { return CellState; }
        public void SetNextState(State state)
        {
            Counter = 0;
            Volume = 100.00;
            switch (state)
            {
                case State.EmptyCell:
                    Color = Color.FromArgb(118, 122, 128);
                    NextState = State.EmptyCell;
                    break;
                case State.CaveWall:
                    Color = Color.FromArgb(41, 41, 44);
                    NextState = State.CaveWall;
                    break;
                case State.Wood:
                    Color = Color.FromArgb(81, 56, 40);
                    NextState = State.Wood;
                    break;
                case State.WoodOnFire:
                    Color = Color.FromArgb(71, 11, 4);
                    NextState = State.WoodOnFire;
                    break;
                case State.Water:
                    Color = Color.FromArgb(0, 84, 118);
                    NextState = State.Water;
                    break;
                case State.Fire:
                    Color = Color.FromArgb(215, 53, 2);
                    NextState = State.Fire;
                    break;
                case State.BlackSmoke:
                    Color = Color.FromArgb(74, 74, 74);
                    NextState = State.BlackSmoke;
                    break;
                case State.WhiteSmoke:
                    Color = Color.FromArgb(172, 172, 172);
                    NextState = State.WhiteSmoke;
                    break;
                case State.Sand:
                    Color = Color.FromArgb(194, 178, 128);
                    NextState = State.Sand;
                    break;
            }
        }

        public void Refresh()
        {
            CellState = NextState;
        }

        public void RefreshColor()
        {
            double limit;
            if (Volume >= 0.00)
            {
                if (Volume == 0.00 && Volume < 0.01) limit = 0.00;
                else if (Volume > 0.01 && Volume < 10.00) limit = 0.1f; // better visibility
                else if (Volume >= 100.00) limit = 1.0f;
                else limit = Volume / 100;
                Color = Color.FromArgb((int)(255 * limit), Color.R, Color.G, Color.B);
            }
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
