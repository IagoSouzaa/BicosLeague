using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Logging;

ApplicationConfiguration.Initialize();

Game futebol = new Game();
futebol.go();

TimeSpan duration = TimeSpan.FromMinutes(3);
DateTime start = DateTime.Now;

public class Game
{
    public bool pular { get; set; } = false;
    public bool voltar { get; set; } = false;
    public Bola bolaa { get; set; } = new Bola();

    public void go()
    {
        var form = new Form();
        
        PictureBox pb = new PictureBox();

        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.None;
        form.BackColor = Color.Gray;

        var bg = Image.FromFile("../../../pixil.png");
        Image img = Bitmap.FromFile("../../../New Piskel (3).png");
        Image placarImg = Image.FromFile("../../../Placar.png");

        Point cursor = Point.Empty;
        Graphics g = null;
        Bitmap bmp = null;
        GraphicsUnit units = GraphicsUnit.Pixel;


        Rectangle placar = new Rectangle(700, -100, 500, 500);

        var jogador1 = new Jogador(new Rectangle(200, 680, 200, 300));
        var jogador2 = new Jogador(new Rectangle(1530, 710, 200, 300));


        var gol1 = new Gol(new Rectangle(-150, 590, 200, 400));
        var gol2 = new Gol(new Rectangle(1870, 590, 200, 400));

        

        Rectangle time1 = new Rectangle(715, 70, 150, 150);
        Rectangle time2 = new Rectangle(1060, 70, 150, 150);

        // Rectangle tempo = new Rectangle(880, 150, 150, 150);


        pb.Dock = DockStyle.Fill;
        form.Controls.Add(pb);
        pb.SizeMode = PictureBoxSizeMode.StretchImage;


        var y = 832;

        var tm = new Timer();

        tm.Interval = 20;

        // var locX = 0;
        // var locY = 0;

        var width = form.Width;
        var heigh = form.Height; 

        var qntgol1 = 0;
        var qntgol2 = 0;

        int size = 25; 

        pb.Width = size;
        pb.Height = size;

        //var cronometro = datetime.now;
        //var cronometro1 = datetime.now;


        Tempo.tempo = DateTime.Now;

        tm.Tick += delegate
        {
            var drawFont = new Font("arial", 30);
            var now = DateTime.Now;

            bolaa.posiNova();
            
            Rectangle bola = new Rectangle(bolaa.posicaoX, bolaa.posicaoY, 100, 100);

            PointF drawPoint = new PointF(750 + 155 , 230);
            PointF Pointplacar1 = new PointF(750 + 125, 83);
            PointF Pointplacar2 = new PointF(750 + 225, 83);


            jogador1.move(0.02f);

            //if (pular && (now - cronometro).Milliseconds > 100)
            //{
            //    jogador1.y = 620;
            //    this.voltar = true;
            //    this.pular = false;
            //    cronometro = DateTime.Now;
            //}

            //if (voltar && (now - cronometro1).Milliseconds > 600)
            //{
            //    jogador1.y = 590;
            //    this.voltar = false;
            //    cronometro1 = DateTime.Now;
            //}


            g.DrawImage(bg, new Rectangle(0, 0, form.Width, form.Height), 0, 0, 640, 480, units); //fundo

            g.DrawImage(placarImg, placar, 0, 0, 300, 200, units); //placar

            g.DrawString($"{(int)(120 - Tempo.segundos())}", drawFont, Brushes.Black, drawPoint);

            g.DrawString($"{qntgol2}", drawFont ,Brushes.Black, Pointplacar1);
            g.DrawString($"{qntgol1}", drawFont, Brushes.Black, Pointplacar2);

            jogador1.draw(g); //jogador1
            jogador2.draw2(g); //jogador2

            g.DrawImage(img, bola, 0, 387, 33, 33, units); //bola

            gol1.draw(g); //gol1
            gol2.draw2(g); //gol2

            g.DrawImage(img, time1, 0, 3, 33, 33, units); //time1
            g.DrawImage(img, time2, 0, 259, 33, 33, units); //time2

            if (gol1.touch(bolaa))
            {
                qntgol1 ++;
                bolaa.posicaoX = 780;
                bolaa.posicaoY = 600;
                jogador1.x = 200;
                jogador1.y = 680;
                jogador2.x = 1530;
                jogador2.y = 710;
            }

            if (gol2.touch(bolaa))
            {
                qntgol2 ++;
                bolaa.posicaoX = 780;
                bolaa.posicaoY = 600;
                jogador1.x = 200;
                jogador1.y = 680;
                jogador2.x = 1530;
                jogador2.y = 710;

            }

            if (jogador1.touch(bolaa))
            {
                bolaa.speedX += 30;
                bolaa.speedY -= 15;
            }

            if (Tempo.segundos() > 120) //tempo
            {
                Application.Exit();
            }

            pb.Refresh();
            g.Clear(Color.Transparent);
        };

        form.Load += delegate
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            bg = bg.GetThumbnailImage(640, 480, null, nint.Zero);
            pb.Image = bmp;
            g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            tm.Start();
        };

        form.KeyUp += (s, e) =>
        {
            if (e.KeyCode == Keys.W)
                this.pular = false;

            if (e.KeyCode == Keys.D)
            {
                jogador1.stop();
            }

            if (e.KeyCode == Keys.A)
            {
                jogador1.stop();
            }
        };

        form.KeyDown += (s, e) =>
        {
            var x = jogador1.x;
            var y = jogador1.y;

            if (e.KeyCode == Keys.D)
            {
                // if (jogador1.x >= width)
                // {
                //     jogador1.speedX = 0;
                // }          
                jogador1.goRight();
            }

            if (e.KeyCode == Keys.A)
            {
                // if (jogador1.x <= width)
                // {
                //     jogador1.speedX = 0;
                // }
                jogador1.goLeft();
            }

            if (e.KeyCode == Keys.W)
            {
                this.pular = true;
            }

            if (e.KeyCode == Keys.Space)
            {
                // this.chute = true;

            }
        };

        form.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        };

        Application.Run(form);
    }
}

