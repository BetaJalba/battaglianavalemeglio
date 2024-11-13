using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battaglianavale
{
    internal class CGioco
    {
        //int[,] grid;
        List<CNave> navi;

        public event EventHandler OnMiss;
        public event EventHandler<NaviEventArgs> OnHit;
        public event EventHandler OnSconfitta;

        public CGioco(List<CNave> navi)
        {
            //this.grid = grid;
            this.navi = navi;

            foreach(CNave nave in navi) 
            {
                OnHit += nave.ReplyAttacco;
                nave.OnNaveDistrutta += RimuoviNave;
            }

            //TO DO
            //il forms deve iscriversi agli eventi OnHit e OnMiss
        }

        private void RimuoviNave(object? sender, NaviEventArgs args) 
        {
            foreach(CNave nave in navi) 
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
    }
}
