using System.CodeDom;

namespace battaglianavale
{
    public partial class Form1 : Form
    {
        public Form1()
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
        }

        bool inGame;
        bool turno; // true - giocatore 1; false - giocatore 2;
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
                                giocatore2 = new CGioco(navi2);
                                LoadGrid(dataGridView1, revealed2);
                                SwapGrids(dataGridView1, dataGridView2);
                                inGame = true;

                                dataGridView1.CellClick += giocatore1.NewMove;

                                giocatore1.OnHit += HitHandler;
                                giocatore1.OnDone += DoneHandler;
                                giocatore2.OnHit += HitHandler;
                                giocatore2.OnDone += DoneHandler;

                                LoadGrid(concealed2, dataGridView1);
                            }

                            //rimasto a: cercare di capire perché non entra nell'else
                        }
                    }           
                }
            } else if (inGame) 
            {
                if (turno) 
                {
                    //per il prossimo turno
                    dataGridView1.CellClick += giocatore2.NewMove;
                    dataGridView1.CellClick -= giocatore1.NewMove;
                    turno = false;
                }
                else 
                {
                    //per il prossimo turno
                    dataGridView1.CellClick += giocatore1.NewMove;
                    dataGridView1.CellClick -= giocatore2.NewMove;
                    turno = false;
                }
            }

            label2.Text = $"X: {e.ColumnIndex}; Y: {e.RowIndex}";
        }

        private void HitHandler(object? sender, NaviEventArgs e) 
        {
            hit = true;
        }

        private void DoneHandler(object? sender, DataGridViewCellEventArgs e) 
        {
            if (hit) 
            {
                dataGridView1[e.RowIndex, e.ColumnIndex].Style.BackColor = Color.Red;
                dataGridView1[e.RowIndex, e.ColumnIndex].Value = "hit";
                if (sender == giocatore1) 
                {
                    
                }
                else 
                {

                }
            } else 
            {
                dataGridView1[e.RowIndex, e.ColumnIndex].Style.BackColor = Color.Gray;
                dataGridView1[e.RowIndex, e.ColumnIndex].Value = "miss";
                if (sender == giocatore1) 
                {
                    
                }
                else 
                {
                    concealed1[e.RowIndex, e.ColumnIndex].Style.BackColor = Color.Gray;
                    concealed1[e.RowIndex, e.ColumnIndex].Value = "miss";
                }
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
            loadTo = loadFrom;
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
                if (navi1.Count == 6)
                    return true;
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
