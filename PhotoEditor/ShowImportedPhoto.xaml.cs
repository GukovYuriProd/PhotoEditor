using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PhotoEditor.OptimisationDTO;

namespace PhotoEditor;

public partial class ShowImportedPhoto : Page
{
    //18-21 - ширина
    //22-25 - высота
    public ShowImportedPhoto() 
    {
        InitializeComponent();
        drawPictureByPixels();
    }
    private void drawPictureByPixels()
    {
        for (int i = Сache.getPixelMap().Count-1; i >= 0; i-=500)
        {
            StackPanel bufferedLine = new StackPanel
            {
                Opacity = 1,
                Orientation = Orientation.Horizontal,
                Background = new SolidColorBrush(Colors.Transparent)
            };
            for (int j = 499; j >= 0; j--)
            {
                Border pixel = new Border()
                {
                    Width = 1,
                    Height = 1,
                    Opacity = 1,
                    ToolTip = "Это пиксель X = " + (500-j).ToString() + " Y = " + (i/500).ToString() + '\n' + "Цвета:" 
                              + '\n' + "Red -> " + Сache.getPixelMap()[i-j].getRed()
                              + '\n' + "Green -> " + Сache.getPixelMap()[i-j].getGreen()
                              + '\n' + "Blue -> " + Сache.getPixelMap()[i-j].getBlue()
                              + '\n' + "Яркость -> " + Math.Round(Сache.getPixelMap()[i-j].getBrightness(), 2),
                    Background = new SolidColorBrush(Color.FromArgb(255, Сache.getPixelMap()[i-j].getRed(), 
                        Сache.getPixelMap()[i-j].getGreen(), 
                        Сache.getPixelMap()[i-j].getBlue()))
                };
                bufferedLine.Children.Add(pixel);
                pixel.MouseDown += getPixelInfo;
                pixel.MouseEnter += Pixel_OnMouseEnter;
            }
            PixelStorage.Children.Add(bufferedLine);
        }
    }

    private void Pixel_OnMouseEnter(object sender, MouseEventArgs e)
    {
        string[]? splitedPixelInfo = (sender as Border)?.ToolTip.ToString()?.Split(" ");
        TextXCoord.Text = splitedPixelInfo?[4];
        TextYCoord.Text = splitedPixelInfo?[7].Split('\n')[0];
        TextBrigtness.Text = splitedPixelInfo?[15];
        TextColorR.Text = splitedPixelInfo?[9].Split('\n')[0];
        TextColorG.Text = splitedPixelInfo?[11].Split('\n')[0];
        TextColorB.Text = splitedPixelInfo?[13].Split('\n')[0];
    }
    private void getPixelInfo(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(((FrameworkElement)e.OriginalSource).ToolTip.ToString());
    }
    
}