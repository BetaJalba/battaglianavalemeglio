using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battaglianavale
{
    internal class CNave
    {
        static int count = 1;

        int id,
            grandezza, 
            rotazione, // 0 ^   1 >     2 \/     3 <  
            posX, 
            posY, 
            vita;

        public int Id 
        {
            get { return id; }
        }

        public event EventHandler<NaviEventArgs> OnNaveDistrutta;

        public CNave(int grandezza, int rotazione, int posX, int posY)
        {
            this.grandezza = grandezza;
            this.rotazione = rotazione;
            this.posX = posX;
            this.posY = posY;
            this.vita = grandezza;

            id = count;
        }

        public void ReplyAttacco(object? sender, NaviEventArgs args) 
        {
            if (args.Id == id) 
            {
                vita--;
                CheckVivo();
            }
        }

        private void CheckVivo() 
        {
            if (vita == 0)
            {
                NaviEventArgs args = new NaviEventArgs();
                args.Id = id;
                NaveDistrutta(args);
            }
        }

        public void Placed() 
        {
            count++;
        }

        private void NaveDistrutta(NaviEventArgs args) 
        {
            EventHandler<NaviEventArgs> handler = OnNaveDistrutta;
            handler?.Invoke(this, args);
        }

        public int[,] GetAllCoordinates() 
        {
            int[,] r = new int[grandezza, 2];
            int rX = posX,
                rY = posY;

            for (int i = 0; i < grandezza; i++) 
            {
                r[i, 0] = rX;
                r[i, 1] = rY;

                switch (rotazione) 
                {
                    case 0:
                        rY--;
                        break;
                    case 1:
                        rX++;
                        break;
                    case 2:
                        rY++;
                        break;
                    case 3:
                        rX--;
                        break;
                    default:
                        return r;
                }
            }

            return r;
        }
    }
}
