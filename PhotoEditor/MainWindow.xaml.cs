using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace PhotoEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void InputPicture_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            WorkArea.NavigationService.Navigate(new FileImporting());
        }
        
        private void Graph_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            WorkArea.NavigationService.Navigate(new ShowGraph());
        }
        
        private void ShowPicture_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            WorkArea.NavigationService.Navigate(new ShowImportedPhoto());
        }

        private void Settings_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            WorkArea.NavigationService.Navigate(new EditPhotoByEcvalisation());
        }

        private void Edited_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Save_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            WorkArea.NavigationService.Navigate(new SavingToFile());
        }
        private void Graph_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Graph.Background = new SolidColorBrush(Color.FromRgb(0,130,247));
        }
        private void Graph_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Graph.Background = new SolidColorBrush(Color.FromRgb(248,248,250));
        }
        private void Save_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Save.Background = new SolidColorBrush(Color.FromRgb(0,130,247));
        }
        private void Save_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Save.Background = new SolidColorBrush(Color.FromRgb(248,248,250));
        }
        private void Edited_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Edited.Background = new SolidColorBrush(Color.FromRgb(0,130,247));
        }
        private void Edited_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Edited.Background = new SolidColorBrush(Color.FromRgb(248,248,250));
        }
        private void Settings_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Settings.Background = new SolidColorBrush(Color.FromRgb(0,130,247));
        }
        private void Settings_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Settings.Background = new SolidColorBrush(Color.FromRgb(248,248,250));
        }
        private void InputPicture_OnMouseEnter(object sender, MouseEventArgs e)
        {
            InputPicture.Background = new SolidColorBrush(Color.FromRgb(0,130,247));
        }
        private void InputPicture_OnMouseLeave(object sender, MouseEventArgs e)
        {
            InputPicture.Background = new SolidColorBrush(Color.FromRgb(248,248,250));
        }
        private void ShowPicture_OnMouseEnter(object sender, MouseEventArgs e)
        {
            ShowPicture.Background = new SolidColorBrush(Color.FromRgb(0,130,247));
        }
        private void ShowPicture_OnMouseLeave(object sender, MouseEventArgs e)
        {
            ShowPicture.Background = new SolidColorBrush(Color.FromRgb(248,248,250));
        }

    }
}