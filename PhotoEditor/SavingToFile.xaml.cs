using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace PhotoEditor;

public partial class SavingToFile : Page
{
    private string? filePath = null;
    public SavingToFile()
    {
        InitializeComponent();
    }

    private void FolderClicked(object sender, MouseButtonEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Text files (*.bmp)|*.bmp"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            this.filePath = openFileDialog.FileName;
            FilePathTextBox.Text = filePath;
        }
    }

    private void SaveEditedPicture_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        byte[] headers = OptimisationDTO.Сache.getBMPHeaders();
        byte[] body = OptimisationDTO.Сache.getEditedBytesMassive();
        byte[] resultedMap = new byte[headers.Length + body.Length];
        Array.Copy(headers, resultedMap, 54);
        Array.Copy(body, 0, resultedMap, 54, body.Length);
        using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            fs.Write(resultedMap, 0, resultedMap.Length);
        }

        MessageBox.Show("Сохранение прошо успешно");
        FilePathTextBox.Text = "";
    }
}