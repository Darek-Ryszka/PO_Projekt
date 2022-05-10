using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Snake : Form
    {
        int cols = 50, rows = 25, score = 0, dx = 0, dy = 0, front = 0, back = 0;
        Piece[] snake = new Piece[1250];
        List<int> available = new List<int>();
        bool[,] visit;

        Random rand = new Random();

        Timer timer = new Timer();

        public Snake()
        {
            InitializeComponent();
            intial();
            launchTimer();
        }

        private void launchTimer() //metoda odpalająca za czas i interwały czasowe
        {
            timer.Interval = 50;
            timer.Tick += move;
            timer.Start();
        }

        private void Snake_KeyDown(object sender, KeyEventArgs e) //metoda pobierająca informacje o przycisku
        {
            dx = dy = 0;
            switch(e.KeyCode)
            {
                case Keys.Right:
                    dx = 20;
                    break;
                case Keys.Left:
                    dx = -20;
                    break;
                case Keys.Up:
                    dy = -20;
                    break;
                case Keys.Down:
                    dy = 20;
                    break;
            }
        }

        private void move(object sender, EventArgs e) //metoda opowiadające za logikę gry, poruszanie się weża, game over i kolizję z jedzeniem
        {
            int x = snake[front].Location.X, y = snake[front].Location.Y;
            if (dx == 0 && dy == 0) return;
            if (game_over(x + dx, y + dy)) //wykrycie kolizji węża z brzegiem okna 
            {
                timer.Stop();
                MessageBox.Show("Game Over"); //game over
                return;
            }
            if (collisionFood(x + dx, y + dy)) //wykrycie kolizji węża z jedzeniem
            {
                score += 1;
                lblScore.Text = "Score: " + score.ToString(); //podbicie wyniku o 1 punkt
                if (hits((y + dy) / 20, (x + dx) / 20)) return;
                Piece head = new Piece(x + dx, y + dy);
                front = (front - 1 + 1250) % 1250;
                snake[front] = head;
                visit[head.Location.Y / 20, head.Location.X / 20] = true;
                Controls.Add(head);
                randomFood(); //logika odpowiadająca za podbicie wyniku o 1 w momencie kolizji węża z jedzeniem, generowanie nowego jedzenia na planszy i dodanie kolejnego segmentu węża
            }
            else
            {
                if (hits((y + dy) / 20, (x + dx) / 20)) return;
                visit[snake[back].Location.Y / 20, snake[back].Location.X / 20] = false;
                front = (front - 1 + 1250) % 1250;
                snake[front] = snake[back];
                snake[front].Location = new Point(x + dx, y + dy);
                back = (back - 1 + 1250) % 1250;
                visit[(y + dy) / 20, (x + dx) / 20] = true; //zjedzenie części włąsnego ciało przez węża
            }
        }

        private void randomFood() //metoda która czyści miejsce pojawienia się jedzenia
        {
            available.Clear(); //czyśczenie okna z jedzenia
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (!visit[i, j]) available.Add(i * cols + j);
            int idx = rand.Next(available.Count) % available.Count;
            lblFood.Left = (available[idx] * 20) % Width;
            lblFood.Top = (available[idx] * 20) / Width * 20; //losowanie nowego miejsca pojawienia się jedzeni na mapie
        }

        private bool hits(int x, int y) //metoda sprawdzająca czy wąż ugryzł własne ciało, jeśli tak gra się zatrzymuje i wyświetlany jest kominikat
        {
           if (visit[x, y])
            {
                timer.Stop();
                MessageBox.Show("Snake Hit his Body");
                return true;
            }
            return false;
        }

        private bool collisionFood(int x, int y) //metoda wykrywająca kolizję z jedzeniem
        {
            return x == lblFood.Location.X && y == lblFood.Location.Y;
        }

        private bool game_over(int x, int y) //metoda wykrywająca kolizję węża z brzegami okna
        {
            return x < 0 || y < 0 || x > 980 || y > 480;
        }

        private void intial() //metoda inicjująca/rozpoczynająca, losująca startowe pozycje węża i jedzenia
        {
            visit = new bool[rows, cols];
            Piece head 
                = new Piece((rand.Next() % cols) * 20, (rand.Next() % rows) * 20); //randomowe tworzenie położenia węża
            lblFood.Location 
                = new Point((rand.Next() % cols) * 20, (rand.Next() % rows) * 20); //randomowe tworzenie położenia jedzonka dla weza
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    visit[i, j] = false;
                    available.Add(i * cols + j);
                }
            visit[head.Location.Y / 20, head.Location.X / 20] = true;
            available.Remove(head.Location.Y / 20 * cols + head.Location.X / 20);
            Controls.Add(head); snake[front] = head;
        }
    }
}
