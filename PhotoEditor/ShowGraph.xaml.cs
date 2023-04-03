using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PhotoEditor.OptimisationDTO;

namespace PhotoEditor;

public partial class ShowGraph : Page
{
    public ShowGraph()
    {
        InitializeComponent();
        drawGraph();
    }

    private void drawGraph()
    {
        //GraphColumns - stackpanel for this shit
        List<double> values = new List<double>();
        List<Pixel> pixels = Сache.getPixelMap();
        int[] brightness = new int[256];
        foreach (var t in pixels)
        {
            brightness[(int)Math.Round(t.getBrightness(), 0)]++;
        }
        for (int value = 0; value < 255; value++) values.Add(brightness[value]);
        for (int indexValue = 0; indexValue < 255; indexValue++)
        {
            double MaxValueStatistics = values.Max();
            Border column = new Border
            {
                Height = 260 * (values[indexValue]/MaxValueStatistics),
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
}