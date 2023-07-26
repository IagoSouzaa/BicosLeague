using System;
using System.Drawing;
using System.Windows.Forms;


public class Bola
{
    public const int desacelerar = 5;

    Image img = Bitmap.FromFile("../../../New Piskel (3).png");

    public int posicaoX { get; set; } = 750;
    public int posicaoY { get; set; } = 600;

    public int speedX { get; set; } = -10;
    public int speedY { get; set; } = -80;

    public int desacelerarGravidade { get; set; } = 0;
    
    Rectangle rectangleBola;
    int moveDir = 0;

    public const int gravidade = 5;

    public void posiNova()
    {
        posicaoX += speedX;
        posicaoY += speedY;

        speedX += speedX > 0 ? - desacelerar : 0;
        speedY += speedY > 0 ? - desacelerar : 0;

        speedX += speedX < 0 ? - desacelerar : 0;
        speedY += speedY < 0 ? - desacelerar : 0;

        if (posicaoX < 0)
        {
            posicaoX  = 0;
            speedX *= -1;
        }

        if (posicaoY < 0)
        {
            posicaoY = 0;
            speedY *= -1;
        }

        if (posicaoX > 1920)
        {
            posicaoX = 1920;
            speedX *= -1;
        }

        if (posicaoY > 900)
        {
            desacelerarGravidade = 0;
            posicaoY = 900;
            speedY /= 3;
            speedY *= -1;
        }
        else
        {
            desacelerarGravidade += gravidade;
            speedY += desacelerarGravidade;
        }

    }
}