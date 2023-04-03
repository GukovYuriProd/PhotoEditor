using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PhotoEditor.OptimisationDTO;

namespace PhotoEditor;

public partial class FileImporting : Page
{
    public FileImporting()
    {
        InitializeComponent();
    }
    
    private void AddImage_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Text files (*.bmp)|*.bmp";
        string filePath = null;
        if (openFileDialog.ShowDialog() == true)
        {
            filePath = openFileDialog.FileName;
            Сache.FillMap(filePath);
        }

        if (filePath != null)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            bitmap.EndInit();
            localImagePreview.Source = bitmap;
            localImagePreview.Width = 170;
            localImagePreview.Height = 170;
            localImagePreview.Stretch = Stretch.Fill;
        }
    }
    
    private void AddImage_OnMouseEnter(object sender, MouseEventArgs e)
    {
        AddImage.Background = new SolidColorBrush(Color.FromRgb(0,130,247));
    }
    private void AddImage_OnMouseLeave(object sender, MouseEventArgs e)
    {
        AddImage.Background = new SolidColorBrush(Color.FromRgb(248,248,250));
    }
}