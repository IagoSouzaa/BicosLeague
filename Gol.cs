using System;
using System.Drawing;
using System.Windows.Forms;


public class Gol
{
    Image img = Bitmap.FromFile("./New Piskel (3).png");
    Rectangle rect;

    public Gol(Rectangle rect)
    {
        this.rect = rect;
    }

    public void draw(Graphics g)
    {
        g.DrawImage(img, rect, 0, 704, 33, 33, GraphicsUnit.Pixel); //gol1
    }

    public void draw2(Graphics g)
    {
        g.DrawImage(img, rect, 0, 768, 33, 33, GraphicsUnit.Pixel); //gol2
    }

    public bool touch(Bola bola)
    {
        var pos = new Point(bola.posicaoX + 17, bola.posicaoY + 17);
        return this.rect.Contains(pos);
    }
}