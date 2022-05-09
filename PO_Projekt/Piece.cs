using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PO_Projekt
{
    class Piece : Label //dziedziczenie
    {
        public Piece(int x, int y) //konstruktor, stworzenie węża o kolorze zielonym 
        {
            Location = new System.Drawing.Point(x, y);
            Size = new System.Drawing.Size(20, 20);
            BackColor = Color.Green;
            Enabled = false;
        }

    }
}
