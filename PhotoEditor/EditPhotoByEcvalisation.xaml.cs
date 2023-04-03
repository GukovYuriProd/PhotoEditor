using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PhotoEditor.OptimisationDTO;

namespace PhotoEditor;

public partial class EditPhotoByEcvalisation : Page
{
    private static bool mode = false;
    public EditPhotoByEcvalisation()
    {
        InitializeComponent();
        LimiterEditorValue.Value = 254;
        BrightnessEditorValue.Value = 50;
    }
    
    private void LimiterEditorValue_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Сache.FillEditedMap(editorLimiter(Сache.getOriginalBytesMassive(), LimiterEditorValue.Value));
        GrapgColumns.Children.Clear();
        drawGraph(mode);
        UpdatePhotoPreview();
    }

    private void BrightnessEditorValue_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Сache.FillEditedMap(editorBrightness(Сache.getOriginalBytesMassive(), BrightnessEditorValue.Value));
        GrapgColumns.Children.Clear();
        drawGraph(mode);
        UpdatePhotoPreview();
    }

    private void AdaptiveBrightness_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Сache.FillEditedMap(graphNormiliser(Сache.getOriginalBytesMassive(), AdaptiveBrightness.Value));
        GrapgColumns.Children.Clear();
        drawGraph(mode);
        UpdatePhotoPreview();
    }
    
    private byte[] graphNormiliser(byte[] originalByteMap, double coeff)
    {
        List<Pixel> origin = OptimisationDTO.Сache.getPixelMap();
        byte[] result = new byte[originalByteMap.Length];
        double resultedCoeff = coeff / 100;

        double averageBrightness = 0;
        for (int i = 0; i < origin.Count; i++) averageBrightness += origin[i].getBrightness();
        averageBrightness = averageBrightness / origin.Count;
        
        for (int value = 0; value < originalByteMap.Length; value+=3)
        {
            Pixel tempPixel = origin[value / 3];

            double tempBrightness = tempPixel.getBrightness();
            double neededBrightness = averageBrightness - tempBrightness;
            neededBrightness *= resultedCoeff;
            
            int B = tempPixel.getBlue();
            int G = tempPixel.getGreen();
            int R = tempPixel.getRed();

            if ((B + neededBrightness < 1)||(G + neededBrightness < 1)||(R + neededBrightness < 1))
            {
                byte minimalValue = Math.Min(Math.Min(originalByteMap[value+0],originalByteMap[value+1]),originalByteMap[value+2]);
                result[value + 0] = (byte)(tempPixel.getBlue() - minimalValue);
                result[value + 1] = (byte)(tempPixel.getGreen() - minimalValue);
                result[value + 2] = (byte)(tempPixel.getRed() - minimalValue);
            } else if ((B + neededBrightness > 255)||(G + neededBrightness > 255)||(R + neededBrightness > 255))
            {
                int maximun = Math.Max(Math.Max(originalByteMap[value+0],originalByteMap[value+1]),originalByteMap[value+2]);
                int delay = 255 - maximun;
                result[value + 0] = (byte)(tempPixel.getBlue() + delay);
                result[value + 1] = (byte)(tempPixel.getGreen() + delay);
                result[value + 2] = (byte)(tempPixel.getRed() + delay);
            }
            else
            {
                result[value + 0] = (byte)(tempPixel.getBlue() + neededBrightness);
                result[value + 1] = (byte)(tempPixel.getGreen() + neededBrightness);
                result[value + 2] = (byte)(tempPixel.getRed() + neededBrightness);
            }
            
        }

        return result;
    }
    
    private void drawGraph(bool mode)
    {
        //GraphColumns - stackpanel for this shit
        List<double> values = new List<double>();
        List<Pixel> pixels = Сache.getTempPixelMap();
        int[] brightness = new int[256];
        foreach (var t in pixels)
        {
            brightness[(int)Math.Round(t.getBrightness(), 0)]++;
        }
        for (int value = 0; value < 256; value++) values.Add(brightness[value]);
        for (int indexValue = 0; indexValue < 256; indexValue++)
        {
            double MaxValueStatistics;
            if (mode)
            {
                MaxValueStatistics = values.Max();
            } else MaxValueStatistics = 2550;
            
            Border column = new Border
            {
                Height = 280 * (values[indexValue]/MaxValueStatistics),
                Width = 2,
                ToolTip = "Всего -> " + values[indexValue].ToString() + '\n' + 
                          "Яркость -> " + indexValue.ToString(),
                VerticalAlignment = VerticalAlignment.Bottom,
                Background = new SolidColorBrush(Colors.Black)
            };
            column.MouseDown += getCoumntInfo;
            column.MouseEnter += Graph_OnMouseEnter;
            column.MouseLeave += Graph_OnMouseLeave;
            GrapgColumns.Children.Add(column);
        }
    }
    
    private byte[] editorBrightness(byte[] originalByteMap, double Brightness)
    {
        byte[] result = new byte[originalByteMap.Length];
        int neededBrightness = (int)(Brightness - 50);
        for (int value = 0; value < originalByteMap.Length; value += 3)
        {
            int B = originalByteMap[value + 0];
            int G = originalByteMap[value + 1];
            int R = originalByteMap[value + 2];

            if ((B + neededBrightness < 1)||(G + neededBrightness < 1)||(R + neededBrightness < 1))
            {
                byte minimalValue = Math.Min(Math.Min(originalByteMap[value+0],originalByteMap[value+1]),originalByteMap[value+2]);
                result[value + 0] = (byte)(originalByteMap[value + 0] - minimalValue);
                result[value + 1] = (byte)(originalByteMap[value + 1] - minimalValue);
                result[value + 2] = (byte)(originalByteMap[value + 2] - minimalValue);
            } else if ((B + neededBrightness > 255)||(G + neededBrightness > 255)||(R + neededBrightness > 255))
            {
                int maximun = Math.Max(Math.Max(originalByteMap[value+0],originalByteMap[value+1]),originalByteMap[value+2]);
                int delay = 255 - maximun;
                result[value + 0] = (byte)(originalByteMap[value + 0] + delay);
                result[value + 1] = (byte)(originalByteMap[value + 1] + delay);
                result[value + 2] = (byte)(originalByteMap[value + 2] + delay);
            }
            else
            {
                result[value + 0] = (byte)(originalByteMap[value + 0] + neededBrightness);
                result[value + 1] = (byte)(originalByteMap[value + 1] + neededBrightness);
                result[value + 2] = (byte)(originalByteMap[value + 2] + neededBrightness);
            }
        }
        return result;
    }
    
   
    
    private byte[] editorLimiter(byte[] originalByteMap, double limiterPorog)
    {
        byte[] result = new byte[originalByteMap.Length];
        for (int value = 0; value < originalByteMap.Length; value += 3)
        {
            double brightness = originalByteMap[value+2] * 0.0721 + originalByteMap[value+1] * 0.7154 + originalByteMap[value] * 0.2125;
            if (brightness < limiterPorog)
            {
                result[value + 0] = originalByteMap[value + 0];
                result[value + 1] = originalByteMap[value + 1];
                result[value + 2] = originalByteMap[value + 2];
            }
            else
            {
                result[value + 0] = 254;
                result[value + 1] = 254;
                result[value + 2] = 254;
            }
        }
        return result;
    }
    
    private void UpdatePhotoPreview()
    {
        byte[] headers = Сache.getBMPHeaders();
        byte[] body = Сache.getEditedBytesMassive();
        byte[] resultedMap = new byte[headers.Length + body.Length];
        Array.Copy(headers, resultedMap, 54);
        Array.Copy(body, 0, resultedMap, 54, body.Length);

        MemoryStream memoryStream = new MemoryStream();
        memoryStream.Write(resultedMap, 0, (int) resultedMap.Length);

        BitmapImage imgsource = new BitmapImage();
        imgsource.BeginInit();
        imgsource.StreamSource = memoryStream;
        imgsource.EndInit();

        EditedImagePreview.Source = imgsource;
        EditedImagePreview.Width = 350;
        EditedImagePreview.Height = 350;
        EditedImagePreview.Stretch = Stretch.Fill;
    }

    private void getCoumntInfo(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(((FrameworkElement)e.OriginalSource).ToolTip.ToString());
    }
    private void Graph_OnMouseEnter(object sender, MouseEventArgs e)
    {
        ((sender as Border)!).BorderThickness = new Thickness(5);
        ((sender as Border)!).BorderBrush = new SolidColorBrush(Colors.Blue);
    }
    private void Graph_OnMouseLeave(object sender, MouseEventArgs e)
    {
        ((sender as Border)!).BorderThickness = new Thickness(0);
        ((sender as Border)!).BorderBrush = new SolidColorBrush(Colors.Transparent);
    }


    private void ButtonMode_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (mode)
        {
            mode = false;
            ButtonMode.Background = new SolidColorBrush(Colors.Red);
        }
        else
        {
            mode = true;
            ButtonMode.Background = new SolidColorBrush(Colors.Green);
        }
        GrapgColumns.Children.Clear();
        drawGraph(mode);
    }

    
}