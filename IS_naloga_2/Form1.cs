using System.Reflection;
using System.Diagnostics;
using System.Data.Common;

namespace IS_naloga_2
{
    public partial class Form1 : Form
    {
        private Color[,] cellColors;
        private int columns;
        private int rows;
        private Color currentColor;
        private readonly Color color_empty = Color.FromArgb(118, 122, 128);
        private readonly Color color_caveWall = Color.FromArgb(41, 41, 44);
        private readonly Color color_water = Color.FromArgb(255, 0, 84, 118);
        private readonly Color color_fire = Color.FromArgb(215, 53, 2);
        private readonly Color color_wood = Color.FromArgb(81, 56, 40);
        private readonly Color color_sand = Color.FromArgb(194, 178, 128);
        private Button btn_empty;
        private Button btn_caveWall;
        private Button btn_wood;
        private Button btn_fire;
        private Button btn_sand;
        private Button btn_water;
        private bool show_volume;

        private Button btn_inspect;

        bool[,] generatedCave;

        Cave cave;
        Game game;

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
            columns = tableLayoutPanel1.ColumnCount;
            rows = tableLayoutPanel1.RowCount;
            cellColors = new Color[columns, rows];
            SetDoubleBuffered(tableLayoutPanel1);
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            btn_start.FlatAppearance.BorderColor = SystemColors.ControlDark;
            btn_clear.FlatAppearance.BorderColor = SystemColors.ControlDark;
            btn_generate.FlatAppearance.BorderColor = SystemColors.ControlDark;
            tableLayoutPanel1.BackColor = color_empty;
            FillTableLayoutPanel2();
            show_volume = false;

        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            if (btn_start.Text == "Start")
            {
                timer1.Start();
                btn_start.Text = "Pause";
            }
            else
            {
                timer1.Stop();
                btn_start.Text = "Start";
            }
            //GameStart();
        }
        public void GameStart()
        {
            if (game != null)
            {
                game.Advance();
                for (int x = 0; x < columns; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        cellColors[x, y] = game.Cells[x, y].Color;
                    }
                }

                tableLayoutPanel1.Refresh();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            GameStart();
        }

        private void Tb_speed_ValueChanged(object sender, EventArgs e)
        {
            if (tb_speed.Value == 1) timer1.Interval = 1000;
            if (tb_speed.Value == 2) timer1.Interval = 900;
            if (tb_speed.Value == 3) timer1.Interval = 800;
            if (tb_speed.Value == 4) timer1.Interval = 700;
            if (tb_speed.Value == 5) timer1.Interval = 600;
            if (tb_speed.Value == 6) timer1.Interval = 500;
            if (tb_speed.Value == 7) timer1.Interval = 400;
            if (tb_speed.Value == 8) timer1.Interval = 300;
            if (tb_speed.Value == 9) timer1.Interval = 200;
            if (tb_speed.Value == 10) timer1.Interval = 100;
        }

        private void Btn_pause_Click(object sender, EventArgs e)
        {

        }

        public static void SetDoubleBuffered(Control c)
        {
            if (SystemInformation.TerminalServerSession)
                return;
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void TableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            using Font font1 = new("Arial", 5, FontStyle.Regular, GraphicsUnit.Point);
            if (cellColors != null)
            {
                var color = cellColors[e.Column, e.Row];
                e.Graphics.FillRectangle(new SolidBrush(color), e.CellBounds);

                /* Show water volume */
                if (game != null && show_volume)
                {
                    if (game.Cells[e.Column, e.Row].GetState() == Cell.State.Water)
                    {
                        var volume = Math.Round(game.Cells[e.Column, e.Row].Volume, 2);
                        e.Graphics.DrawString(volume.ToString(), font1, Brushes.White, e.CellBounds);
                    }
                }
                /* Show water volume */
            }
        }

        private void TableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            int row = 0;
            int verticalOffset = 0;
            foreach (int h in tableLayoutPanel1.GetRowHeights())
            {
                int column = 0;
                int horizontalOffset = 0;
                foreach (int w in tableLayoutPanel1.GetColumnWidths())
                {
                    Rectangle rectangle = new Rectangle(horizontalOffset, verticalOffset, w, h);
                    if (rectangle.Contains(e.Location))
                    {
                        if (game != null)
                        {
                            if (currentColor == color_empty)
                            {
                                game.Cells[column, row].SetNextState(Cell.State.EmptyCell);
                            }
                            if (cellColors[column, row] != color_caveWall)
                            {
                                if (currentColor == color_empty)
                                {
                                    game.Cells[column, row].SetNextState(Cell.State.EmptyCell);
                                }
                                else if (currentColor == color_caveWall)
                                {
                                    game.Cells[column, row].SetNextState(Cell.State.CaveWall);
                                }
                                else if (currentColor == color_wood)
                                {
                                    game.Cells[column, row].SetNextState(Cell.State.Wood);
                                }
                                else if (currentColor == color_fire)
                                {
                                    game.Cells[column, row].SetNextState(Cell.State.Fire);
                                }
                                else if (currentColor == color_water)
                                {
                                    game.Cells[column, row].SetNextState(Cell.State.Water);
                                }
                                else if (currentColor == color_sand)
                                {
                                    game.Cells[column, row].SetNextState(Cell.State.Sand);
                                }
                            }
                            if (currentColor != Color.Empty)
                            {
                                cellColors[column, row] = currentColor;
                            }
                            if (currentColor == Color.Empty)
                            {
                                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ": Volume: " + game.Cells[column, row].Volume);
                                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ": " + game.Cells[column, row].Color);
                                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ": State: " + game.Cells[column, row].GetState());
                                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ": x, y: " + column + "," + row);
                                Debug.WriteLine("-");
                            }
                        }
                        tableLayoutPanel1.Refresh();
                        return;
                    }
                    horizontalOffset += w;
                    column++;
                }
                verticalOffset += h;
                row++;
            }
        }

        private void FillTableLayoutPanel2()
        {
            tableLayoutPanel2.Controls.Add(btn_empty = new Button() { BackColor = color_empty, FlatStyle = FlatStyle.Flat, ForeColor = SystemColors.ControlDarkDark, Dock = DockStyle.Fill });
            tableLayoutPanel2.Controls.Add(new Label() { Text = "Empty Block", Font = new Font("Segoe UI", 12) });
            tableLayoutPanel2.Controls.Add(btn_caveWall = new Button() { BackColor = color_caveWall, FlatStyle = FlatStyle.Flat, ForeColor = SystemColors.ControlDarkDark, Dock = DockStyle.Fill });
            tableLayoutPanel2.Controls.Add(new Label() { Text = "Cave Wall", Font = new Font("Segoe UI", 12) });
            tableLayoutPanel2.Controls.Add(btn_wood = new Button() { BackColor = color_wood, FlatStyle = FlatStyle.Flat, ForeColor = SystemColors.ControlDarkDark, Dock = DockStyle.Fill });
            tableLayoutPanel2.Controls.Add(new Label() { Text = "Wood", Font = new Font("Segoe UI", 12) });
            tableLayoutPanel2.Controls.Add(btn_water = new Button() { BackColor = color_water, FlatStyle = FlatStyle.Flat, ForeColor = SystemColors.ControlDarkDark, Dock = DockStyle.Fill });
            tableLayoutPanel2.Controls.Add(new Label() { Text = "Water", Font = new Font("Segoe UI", 12) });
            tableLayoutPanel2.Controls.Add(btn_fire = new Button() { BackColor = color_fire, FlatStyle = FlatStyle.Flat, ForeColor = SystemColors.ControlDarkDark, Dock = DockStyle.Fill });
            tableLayoutPanel2.Controls.Add(new Label() { Text = "Fire", Font = new Font("Segoe UI", 12) });
            tableLayoutPanel2.Controls.Add(btn_sand = new Button() { BackColor = color_sand, FlatStyle = FlatStyle.Flat, ForeColor = SystemColors.ControlDarkDark, Dock = DockStyle.Fill });
            tableLayoutPanel2.Controls.Add(new Label() { Text = "Sand", Font = new Font("Segoe UI", 12) });

            tableLayoutPanel2.Controls.Add(btn_inspect = new Button() { BackColor = Color.Empty, FlatStyle = FlatStyle.Flat, ForeColor = SystemColors.ControlDarkDark, Dock = DockStyle.Fill });
            tableLayoutPanel2.Controls.Add(new Label() { Text = "Inspect", Font = new Font("Segoe UI", 12) });

            //Empty Block
            btn_empty.Click += (o, e) =>
            {
                btn_empty.ForeColor = Color.FromArgb(117, 213, 175);
                currentColor = color_empty;
            };
            btn_empty.LostFocus += (o, e) => { btn_empty.ForeColor = SystemColors.ControlDarkDark; };

            //Cave Wall
            btn_caveWall.Click += (o, e) =>
            {
                btn_caveWall.ForeColor = Color.FromArgb(117, 213, 175);
                currentColor = color_caveWall;
            };
            btn_caveWall.LostFocus += (o, e) => { btn_caveWall.ForeColor = SystemColors.ControlDarkDark; };

            //Wood
            btn_wood.Click += (o, e) =>
            {
                btn_wood.ForeColor = Color.FromArgb(117, 213, 175);
                currentColor = color_wood;
            };
            btn_wood.LostFocus += (o, e) => { btn_wood.ForeColor = SystemColors.ControlDarkDark; };

            //Water
            btn_water.Click += (o, e) =>
            {
                btn_water.ForeColor = Color.FromArgb(117, 213, 175);
                currentColor = color_water;
            };
            btn_water.LostFocus += (o, e) => { btn_water.ForeColor = SystemColors.ControlDarkDark; };

            //Fire
            btn_fire.Click += (o, e) =>
            {
                btn_fire.ForeColor = Color.FromArgb(117, 213, 175);
                currentColor = color_fire;
            };
            btn_fire.LostFocus += (o, e) => { btn_fire.ForeColor = SystemColors.ControlDarkDark; };

            //Sand
            btn_sand.Click += (o, e) =>
            {
                btn_sand.ForeColor = Color.FromArgb(117, 213, 175);
                currentColor = color_sand;
            };
            btn_sand.LostFocus += (o, e) => { btn_sand.ForeColor = SystemColors.ControlDarkDark; };

            //Inspect
            btn_inspect.Click += (o, e) =>
            {
                btn_inspect.ForeColor = Color.FromArgb(117, 213, 175);
                currentColor = Color.Empty;
            };
            btn_inspect.LostFocus += (o, e) => { btn_sand.ForeColor = SystemColors.ControlDarkDark; };

        }

        private bool[,] GenerateCave()
        {
            bool[,] caveMap = new bool[columns, rows];

            cave.Run();

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {

                    if (i == 0 || j == 0 || i == columns - 1 || j == rows - 1 || (i == 1 & j == 1) || (i == 1 & j == rows - 2) || (i == columns - 2 & j == 1) || (i == columns - 2 & j == rows - 2))
                    {
                        caveMap[i, j] = true;
                    }
                    else
                    {
                        caveMap[i, j] = false;
                    }
                }
            }

            for (int x = 0; x < cave.Columns; x++)
            {
                for (int y = 0; y < cave.Rows; y++)
                {
                    if (!((x == 0 & y == 0) || (x == 0 & y == cave.Rows - 1) || (x == cave.Columns - 1 & y == 0) || (x == cave.Columns - 1 & y == cave.Rows - 1)))
                    {
                        if (cave.CellMap[x, y].IsAlive)
                        {
                            caveMap[x + 1, y + 1] = true;
                        }
                        else
                        {
                            caveMap[x + 1, y + 1] = false;
                        }
                    }
                }
            }
            return caveMap;
        }

        private void Btn_generate_Click(object sender, EventArgs e)
        {
            cave = new Cave();
            generatedCave = GenerateCave();
            game = new Game(generatedCave);
            GenerateMap();
            tableLayoutPanel1.Refresh();
        }

        private void GenerateMap()
        {
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    cellColors[x, y] = game.Cells[x, y].Color;
                }
            }
        }

        private void Btn_clear_Click(object sender, EventArgs e)
        {
            if (generatedCave != null)
            {
                game = new Game(generatedCave);
                GenerateMap();
                tableLayoutPanel1.Refresh();
                timer1.Stop();
                btn_start.Text = "Start";
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                btn_start.PerformClick();
                btn_start.Focus();
            }
            if (e.KeyCode == Keys.C)
            {
                btn_clear.PerformClick();
                btn_clear.Focus();
            }
            else if (e.KeyCode == Keys.G)
            {
                btn_generate.PerformClick();
                btn_generate.Focus();
            }
            else if (e.KeyCode == Keys.E)
            {
                btn_empty.PerformClick();
                btn_empty.Focus();
            }
            else if (e.KeyCode == Keys.W)
            {
                btn_water.PerformClick();
                btn_water.Focus();
            }
            else if (e.KeyCode == Keys.D)
            {
                btn_wood.PerformClick();
                btn_wood.Focus();
            }
            else if (e.KeyCode == Keys.F)
            {
                btn_fire.PerformClick();
                btn_fire.Focus();
            }
            else if (e.KeyCode == Keys.S)
            {
                btn_sand.PerformClick();
                btn_sand.Focus();
            }
            else if (e.KeyCode == Keys.Q)
            {
                btn_inspect.PerformClick();
                btn_inspect.Focus();
            }
            else if (e.KeyCode == Keys.A)
            {
                btn_caveWall.PerformClick();
                btn_caveWall.Focus();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            show_volume = !show_volume;
        }
    }
}