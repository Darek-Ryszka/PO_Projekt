using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Piece : Label //dziedziczenie
    {
        public Piece(int x, int y) //konstruktor, stworzenie węża o kolorze zielonym 
        {
            Location = new Point(x, y);
            Size = new Size(20, 20);
            BackColor = Color.Green;
            Enabled = false;
        }
    }
}