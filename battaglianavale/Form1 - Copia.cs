using System.CodeDom;

namespace battaglianavale
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            inGame = false;
            turno = true;
            hit = false;
            rotazione = 0;
            lunghezza = 0;
            placingNave = false;
            navi1 = new List<CNave>();
            navi2 = new List<CNave>();
            revealed1 = new DataGridView();
            revealed2 = new DataGridView();
            concealed1 = new DataGridView();
            concealed2 = new DataGridView();
            concealed1.RowCount = 10;
            concealed1.ColumnCount = 10;
            concealed2.RowCount = 10;
            concealed2.ColumnCount = 10;
            revealed1.RowCount = 10;
            revealed1.ColumnCount = 10;
            revealed2.RowCount = 10;
            revealed2.ColumnCount = 10;
            direzione = -1;
            botX = -1;
            botY = -1;
            consecutiveHits = 0;
        }

        bool inGame;
        bool turno; // true - giocatore 1; false - bot;
        bool hit;
        int rotazione;
        int lunghezza;
        bool placingNave;
        List<CNave> navi1;
        List<CNave> navi2;
        CGioco giocatore1;
        CGioco giocatore2;
        DataGridView revealed1;
        DataGridView revealed2;
        DataGridView concealed1;
        DataGridView concealed2;
        int direzione, botX, botY;
        int consecutiveHits;

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.MultiSelect = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowCount = 10;
            dataGridView1.ColumnCount = 10;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.SelectionChanged += SelectionChanged;
            dataGridView1.ClearSelection();
            dataGridView1.CellClick += CellSelected;

            foreach (DataGridViewTextBoxColumn column in dataGridView1.Columns)
            {
                column.Width = 40;
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 40;
            }

            dataGridView2.MultiSelect = false;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.RowCount = 10;
            dataGridView2.ColumnCount = 10;
            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.ScrollBars = ScrollBars.None;
            dataGridView2.SelectionChanged += SelectionChanged;
            dataGridView2.ClearSelection();

            foreach (DataGridViewTextBoxColumn column in dataGridView2.Columns)
            {
                column.Width = 20;
            }
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                row.Height = 20;
            }
        }

        private void CellSelected(object? sender, DataGridViewCellEventArgs e)
        {
            if (placingNave && lunghezza > 0) //piazzamento navi
            {
                if (CheckPlaceable(e.ColumnIndex, e.RowIndex, lunghezza, rotazione, (sender as DataGridView).ColumnCount - 1, (sender as DataGridView).RowCount - 1))
                {
                    //possibile nave
                    CNave toAdd = new CNave(lunghezza, rotazione, e.ColumnIndex, e.RowIndex);
                    int[,] toDraw = toAdd.GetAllCoordinates();

                    if (CheckOtherBoats(sender as DataGridView, toDraw, lunghezza))
                    {
                        //nave 100% possibile
                        toAdd.Placed();
                        if (turno)
                            navi1.Add(toAdd);
                        else
                            navi2.Add(toAdd);

                        for (int i = 0; i < lunghezza; i++)
                        {
                            (sender as DataGridView)[toDraw[i, 0], toDraw[i, 1]].Style.BackColor = Color.RebeccaPurple;
                            if (turno)
                                (sender as DataGridView)[toDraw[i, 0], toDraw[i, 1]].Value = navi1.Last().Id;
                            else
                                (sender as DataGridView)[toDraw[i, 0], toDraw[i, 1]].Value = navi2.Last().Id;
                        }

                        switch (lunghezza)
                        {
                            case 1:
                                DecreaseButtonValue(btnNave1);
                                break;
                            case 2:
                                DecreaseButtonValue(btnNave2);
                                break;
                            case 3:
                                DecreaseButtonValue(btnNave3);
                                break;
                            case 4:
                                DecreaseButtonValue(btnNave4);
                                break;
                        }

                        if (CheckIfReady())
                        {
                            if (turno)
                            {
                                giocatore1 = new CGioco(navi1);
                                LoadGrid(dataGridView1, revealed1);
                                SwapGrids(dataGridView1, dataGridView2);
                                ResetButtons();
                                turno = false;
                            }
                            else
                            {
                                //inizia gioco

                                giocatore2 = new CGioco(navi2);
                                LoadGrid(dataGridView1, revealed2); // carica nel revealed
                                turno = true;
                                inGame = true;

                                dataGridView1.CellClick -= CellSelected;
                                dataGridView1.CellClick += giocatore2.NewMove;

                                giocatore1.OnHit += HitHandler;
                                giocatore2.OnHit += HitHandler;
                                giocatore1.OnDone += DoneHandler;
                                giocatore2.OnDone += DoneHandler;
                                giocatore1.OnSconfitta += SconfittaHandler;
                                giocatore2.OnSconfitta += SconfittaHandler;
                                giocatore2.OnHit += CalcolaMossaDopoHit;

                                CalcolaMossaRandom();
                                ProssimoTurno();
                            }
                        }
                    }
                }
            }

            label2.Text = $"X: {e.ColumnIndex}; Y: {e.RowIndex}";
        }

        private void CalcolaMossaDopoHit(object? sender, NaviEventArgs e) 
        {
            Random rand = new Random();

            consecutiveHits++;

            botX = e.X;
            botY = e.Y;

            if (consecutiveHits < 2) 
            {
                switch (rand.Next(4))
                {
                    case 0:
                        botY--;
                        if (botY < 0) 
                            botY++;
                        rotazione = 0;
                        break;
                    case 1:
                        botX++;
                        if (botX > 9)
                            botX--;
                        rotazione = 1;
                        break;
                    case 2:
                        if (botY > 9) 
                            botY--;
                        botY++;
                        rotazione = 2;
                        break;
                    case 3:
                        botX--;
                        if (botX < 0)
                            botX++;
                        rotazione = 3;
                        break;
                }
            } else //se azzecca 2 volte di fila continua nella stessa direzione
            {
                switch (rotazione) 
                {
                    case 0:
                        botY--;
                        if (botY < 0)
                            botY++;
                        rotazione = 0;
                        break;
                    case 1:
                        botX++;
                        if (botX > 9)
                            botX--;
                        rotazione = 1;
                        break;
                    case 2:
                        if (botY > 9)
                            botY--;
                        botY++;
                        rotazione = 2;
                        break;
                    case 3:
                        botX--;
                        if (botX < 0)
                            botX++;
                        rotazione = 3;
                        break;
                }
            }

            if (botX == e.X) 
            {
                CalcolaMossaRandom();
                consecutiveHits = 0;
            }
        }

        private void CalcolaMossaRandom() 
        {
            Random rand = new Random();

            do 
            {
                botX = rand.Next(10);
                botY = rand.Next(10);
            } while (concealed1[botX, botY].Value == "hit" || concealed1[botX, botY].Value == "miss");
            //placement nave
        }

        private void SconfittaHandler(object? sender, EventArgs e) 
        {
            if (sender == giocatore1)
                MessageBox.Show("Giocatore 2 ha vinto!");
            else
                MessageBox.Show("Giocatore 1 ha vinto!");
        }

        private void HitHandler(object? sender, NaviEventArgs e)
        {
            hit = true;
        }

        private void DoneHandler(object? sender, DataGridViewCellEventArgs e)
        {
            if (hit)
            {
                if (turno)
                {
                    concealed2[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                    concealed2[e.ColumnIndex, e.RowIndex].Value = "hit";
                    revealed2[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                    revealed2[e.ColumnIndex, e.RowIndex].Value = "hit";
                    //LoadGrid(dataGridView1, concealed2); //giocatore 1 guarda la griglia del gioc 2 nascosta
                }
                else
                {
                    concealed1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                    concealed1[e.ColumnIndex, e.RowIndex].Value = "hit";
                    revealed1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                    revealed1[e.ColumnIndex, e.RowIndex].Value = "hit";
                    //LoadGrid(dataGridView1, concealed1); //giocatore 1 guarda la griglia del gioc 2 nascosta
                }

                hit = false;
            }
            else
            {
                if (turno)
                {
                    concealed2[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Gray;
                    concealed2[e.ColumnIndex, e.RowIndex].Value = "miss";
                    revealed2[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Gray;
                    revealed2[e.ColumnIndex, e.RowIndex].Value = "miss";
                    //LoadGrid(dataGridView1, concealed2); //giocatore 1 guarda la griglia del gioc 2 nascosta
                    consecutiveHits = 0;
                    CalcolaMossaRandom();
                }
                else
                {
                    concealed1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Gray;
                    concealed1[e.ColumnIndex, e.RowIndex].Value = "miss";
                    revealed1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Gray;
                    revealed1[e.ColumnIndex, e.RowIndex].Value = "miss";
                    //LoadGrid(dataGridView1, concealed1);
                }
            }

            ProssimoTurno();
        }

        private void ProssimoTurno()
        {
            if (inGame)
            {
                if (turno) // se va al contrario funziona non so perché
                {
                    // per il prossimo turno
                    LoadGrid(concealed1, dataGridView1); // carica in dgv1 lo schermo nascosto del pl1
                    LoadGrid(revealed2, dataGridView2); // carica in dgv2 lo schermo delle navi del pl2

                    dataGridView1.CellClick -= giocatore2.NewMove;
                    dataGridView1.CellClick += giocatore1.NewMove;
                    turno = false;

                    MessageBox.Show("Turno P1");
                }
                else
                {
                    // per il prossimo turno
                    LoadGrid(concealed2, dataGridView1);
                    LoadGrid(revealed1, dataGridView2);

                    dataGridView1.CellClick -= giocatore1.NewMove;
                    dataGridView1.CellClick += giocatore2.NewMove;
                    turno = true;

                    MessageBox.Show("Turno P2");

                    //bot
                    DataGridViewCellEventArgs args = new DataGridViewCellEventArgs(botX, botY);
                    giocatore2.NewMove(this, args);
                }

                //todo vedere perché tutto è al contrarios
            }
        }

        private void ResetButtons()
        {
            btnNave1.Enabled = true;
            btnNave2.Enabled = true;
            btnNave3.Enabled = true;
            btnNave4.Enabled = true;

            btnNave1.Text = "1x Nave da 1";
            btnNave2.Text = "2x Navi da 2";
            btnNave3.Text = "2x Navi da 3";
            btnNave4.Text = "1x Nave da 4";
        }

        private void LoadGrid(DataGridView loadFrom, DataGridView loadTo)
        {
            for (int y = 0; y < loadTo.Rows.Count; y++)
            {
                for (int x = 0; x < loadTo.Rows.Count; x++)
                {
                    loadTo[x, y].Value = loadFrom[x, y].Value;
                    loadTo[x, y].Style.BackColor = loadFrom[x, y].Style.BackColor;
                }
            }
        }

        private void SwapGrids(DataGridView grid1, DataGridView grid2)
        {
            DataGridView temp = new DataGridView();

            temp.RowCount = 10;
            temp.ColumnCount = 10;

            for (int y = 0; y < temp.Rows.Count; y++)
            {
                for (int x = 0; x < temp.Rows.Count; x++)
                {
                    temp[x, y].Value = grid1[x, y].Value;
                    temp[x, y].Style.BackColor = grid1[x, y].Style.BackColor;
                }
            }

            for (int y = 0; y < grid1.Rows.Count; y++)
            {
                for (int x = 0; x < grid1.Rows.Count; x++)
                {
                    grid1[x, y].Value = grid2[x, y].Value;
                    grid1[x, y].Style.BackColor = grid2[x, y].Style.BackColor;
                }
            }

            for (int y = 0; y < grid2.Rows.Count; y++)
            {
                for (int x = 0; x < grid2.Rows.Count; x++)
                {
                    grid2[x, y].Value = temp[x, y].Value;
                    grid2[x, y].Style.BackColor = temp[x, y].Style.BackColor;
                }
            }
        }

        private bool CheckIfReady()
        {
            if (turno)
            {
                if (navi1.Count == 6)
                    return true;
            }
            else
                if (navi2.Count == 6)
                return true;
            return false;
        }

        private void DecreaseButtonValue(Button button)
        {
            char[] toReplace = button.Text.ToCharArray();

            if (button.Text.Substring(0, 1) == "2")
            {
                toReplace[0] = '1';
                toReplace[6] = 'e';
            }
            else if (button.Text.Substring(0, 1) == "1")
            {
                toReplace[0] = '0';
                toReplace[6] = 'i';
                button.Enabled = false;
                lunghezza = 0;
            }

            button.Text = new string(toReplace);
        }

        private bool CheckPlaceable(int x, int y, int len, int rot, int xMax, int yMax)
        {
            for (int i = 0; i < len; i++)
            {
                if (x > xMax || y > yMax || x < 0 || y < 0)
                    return false;
                switch (rot)
                {
                    case 0: //sopra
                        y--;
                        break;
                    case 1: //destra
                        x++;
                        break;
                    case 2: //sotto
                        y++;
                        break;
                    case 3: //sinistra
                        x--;
                        break;
                    default:
                        return false;
                }
            }

            return true;
        }

        private bool CheckOtherBoats(DataGridView grid, int[,] coords, int len)
        {
            for (int i = 0; i < len; i++)
                if (grid[coords[i, 0], coords[i, 1]].Style.BackColor == Color.RebeccaPurple)
                    return false;
            return true;
        }

        private void SelectionChanged(object? sender, EventArgs e)
        {
            (sender as DataGridView).ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rotazione = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rotazione = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rotazione = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rotazione = 3;
        }

        private void btnNave1_Click(object sender, EventArgs e)
        {
            lunghezza = 1;
        }

        private void btnNave2_Click(object sender, EventArgs e)
        {
            lunghezza = 2;
        }

        private void btnNave3_Click(object sender, EventArgs e)
        {
            lunghezza = 3;
        }

        private void btnNave4_Click(object sender, EventArgs e)
        {
            lunghezza = 4;
        }

        private void btnPiazza_Click(object sender, EventArgs e)
        {
            if (placingNave)
                placingNave = false;
            else placingNave = true;
        }
    }
}
