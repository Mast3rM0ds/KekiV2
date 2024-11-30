using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KekiEditor.GameProject
{
    /// <summary>
    /// Interaction logic for ProjectBrowser.xaml
    /// </summary>
    public partial class ProjectBrowser : Window
    {
        public ProjectBrowser()
        {
            InitializeComponent();
        }

        private void OnToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender == openKekiButton)
            {
                if(newKekiButton.IsChecked == true)
                {
                    newKekiButton.IsChecked = false;
                    browserContent.Margin = new Thickness(0);
                }
                openKekiButton.IsChecked = true;
            }
            else
            {
                if (openKekiButton.IsChecked == true)
                {
                    openKekiButton.IsChecked = false;
                    browserContent.Margin = new Thickness(-800,0,0,0);
                }
                newKekiButton.IsChecked = true;
            }
        }
    }
}
