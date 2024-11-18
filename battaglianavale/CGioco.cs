using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battaglianavale
{
    internal class CGioco
    {
        List<CNave> navi;

        public event EventHandler<DataGridViewCellEventArgs> OnDone;
        public event EventHandler<NaviEventArgs> OnHit;
        public event EventHandler OnSconfitta;

        public bool MossaConsentita { get; set; }

        public CGioco(List<CNave> navi)
        {
            this.navi = navi;

            foreach (CNave nave in navi)
            {
                OnHit += nave.ReplyAttacco;
                nave.OnNaveDistrutta += RimuoviNave;
            }
        }

        public void NewMove(object? sender, DataGridViewCellEventArgs e)
        {
            if (MossaConsentita) 
            {
                foreach (CNave nave in navi)
                {
                    int[,] toCheck = nave.GetAllCoordinates();
                    for (int i = 0; i < toCheck.GetLength(0); i++)
                    {
                        if (toCheck[i, 0] == e.ColumnIndex && toCheck[i, 1] == e.RowIndex)
                        {
                            NaviEventArgs navi = new NaviEventArgs();
                            navi.Id = nave.Id;
                            navi.X = e.ColumnIndex;
                            navi.Y = e.RowIndex;
                            OnHitHandler(navi);
                            OnDone(null, e);
                            return;
                        }
                    }
                }

                OnDone(null, e);
            }
        }

        private void OnHitHandler(NaviEventArgs args)
        {
            EventHandler<NaviEventArgs> handler = OnHit;
            OnHit?.Invoke(this, args);
        }

        private void RimuoviNave(object? sender, NaviEventArgs args)
        {
            foreach (CNave nave  in navi)
            {
                if (nave.Id == args.Id)
                {
                    navi.Remove(nave);
                    break;
                }
            }

            CheckIfWon();
        }

        private void CheckIfWon()
        {
            if (navi.Count == 0)
                OnSconfitta(this, EventArgs.Empty);
        }
    }

    public class NaviEventArgs : EventArgs
    {
        public int Id;
        public int X;
        public int Y;
    }
}