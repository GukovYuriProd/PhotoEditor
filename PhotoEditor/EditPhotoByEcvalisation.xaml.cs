using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PhotoEditor.OptimisationDTO;

namespace PhotoEditor;

public partial class EditPhotoByEcvalisation : Page
{
    public EditPhotoByEcvalisation()
    {
        InitializeComponent();
        LimiterEditorValue.Value = 254;
        BrightnessEditorValue.Value = 50;
    }
    
    private void LimiterEditorValue_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Сache.FillEditedMap(editorLimiter(Сache.getOriginalBytesMassive(), LimiterEditorValue.Value));
        UpdatePhotoPreview();
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
        Сache.FillEditedMap(result);
        return result;
    }

    
    private void BrightnessEditorValue_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Сache.FillEditedMap(editorBrightness(Сache.getOriginalBytesMassive(), BrightnessEditorValue.Value));
        //OptimisationDTO.Сache.FillEditedMap(editorBrightness(OptimisationDTO.Сache.getOriginalBytesMassive(), 200));
        UpdatePhotoPreview();
    }
    
    private byte[] editorBrightness(byte[] originalByteMap, double Brightness)
    {
        byte[] result = new byte[originalByteMap.Length];
        int neededBrightness = (int)(Brightness - 50)*1;
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
                int maximun = Math.Max(Math.Min(originalByteMap[value+0],originalByteMap[value+1]),originalByteMap[value+2]);
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
        Сache.FillEditedMap(result);
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
    
    private void UpdatePhotoPreviewWithCmyk()
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
}