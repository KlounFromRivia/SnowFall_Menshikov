using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnowFall_Menshikov
{
    public partial class Form1 : Form
    {
        private IList<Snow> snowFall;
        private readonly Timer timer;
        int n = 0;
        Bitmap fon;
        Bitmap snow;
        Bitmap scene;
        Bitmap Pscene;
        private Graphics gr;
        public Form1()
        {
            InitializeComponent();
            fon = (Bitmap)Properties.Resources.snowPole;
            snow = (Bitmap)Properties.Resources.pngwing_com;
            scene = new Bitmap(fon,
                    ClientRectangle.Width,
                    ClientRectangle.Height);
            Pscene = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            gr = Graphics.FromImage(Pscene);

            snowFall = new List<Snow>();
            AddSnowFall();
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            gr.DrawImage(scene, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));
            foreach (var snowFalls in snowFall)
            {
                snowFalls.Y += snowFalls.Size;
                if (snowFalls.Y > ClientRectangle.Height)
                {
                    snowFalls.Y = -snowFalls.Size;
                }
            }
            DrawScene();
            timer.Start();
        }

        private void DrawScene()
        {
            gr.DrawImage(scene, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));
            foreach (var snowFalls in snowFall)
            {
                if (snowFalls.Y > 0)
                {
                    gr.DrawImage(snow, new Rectangle(
                        snowFalls.X,
                        snowFalls.Y,
                        snowFalls.Size,
                        snowFalls.Size));
                }
            }
            var g = this.CreateGraphics();
            g.DrawImage(Pscene, 0, 0);
        }

        private void AddSnowFall()
        {
            var rnd = new Random();
            for (int i = 0; i < 60; i++)
            {
                snowFall.Add(new Snow
                {
                    X = rnd.Next(ClientRectangle.Width),
                    Y = -rnd.Next(ClientRectangle.Height),
                    Size = rnd.Next(5, 15)
                });
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.BackgroundImage = fon;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (n == 0)
            {
                timer.Start();
                n++;
            }
            else if (n == 1)
            {
                timer.Stop();
                n = 0;
            }
            
        }
    }
}
