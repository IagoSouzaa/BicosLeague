using System;
using System.Drawing;
using System.Windows.Forms;

public class Jogador
{
    private const float baseSpeed = 300;
    Image img = Bitmap.FromFile("./New Piskel (3).png");

    public float speedX = 0;
    private float speedY = 0;
    Rectangle rect;
    public int moveDir = 0;
    public float x
    {
        get => rect.X;
        set => rect.X = (int)value;
    }
    public float y
    {
        get => rect.Y;
        set => rect.Y = (int)value;
    }
    bool chute = false;

    public Jogador(Rectangle rect)
    {
        this.rect = rect;
    }

    public void goLeft()
    {
        moveDir = -1;
    }

    public void goRight()
    {
        moveDir = 1;
    }

    public void draw(Graphics g)
    {
        g.DrawImage(img, rect, 0, 0 + 832, 33, 32, GraphicsUnit.Pixel);
    }

    public void draw2(Graphics g)
    {
        g.DrawImage(img, rect, 0, 0 + 1986, 34, 34, GraphicsUnit.Pixel);
    }

    public void move(float dt)
    {
        this.x += speedX * dt;
        this.y += speedY * dt;

        if (moveDir == 1)
            speedX = baseSpeed;
        else if (moveDir == -1)
            speedX = -baseSpeed;
        else speedX *= 0.95f;
    }

    public void stop()
    {
        moveDir = 0;
    }

    public bool touch(Bola bola)
    {
        var pos = new Point(bola.posicaoX + 17, bola.posicaoY + 17);
        return this.rect.Contains(pos);
    }
}
