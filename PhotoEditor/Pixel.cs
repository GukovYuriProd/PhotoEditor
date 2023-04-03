using System;

namespace PhotoEditor;

public class Pixel
{
    private byte colorR;
    private byte colorG;
    private byte colorB;
    private double brightness;
    
    public Pixel(byte R, byte G, byte B)
    {
        //BGR
        this.colorR = R;
        this.colorG = G;
        this.colorB = B;
        this.brightness = B*0.0721 + G*0.7154 + R*0.2125;
    }

    public byte getRed()
    {
        return this.colorR;
    }

    public byte getGreen()
    {
        return this.colorG;
    }

    public byte getBlue()
    {
        return this.colorB;
    }

    public double getBrightness()
    {
        return this.brightness;
    }
}