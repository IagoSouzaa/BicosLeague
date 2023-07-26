using System;
using System.Drawing;
using System.Windows.Forms;

public static class Tempo
{
    public static DateTime tempo { get; set; }
    
    public static  double segundos()
    {
        double tempoAtt = (DateTime.Now - tempo).TotalSeconds;

        return tempoAtt;
    } 
};