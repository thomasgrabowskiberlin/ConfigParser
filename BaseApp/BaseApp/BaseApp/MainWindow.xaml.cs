using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;


namespace BaseApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataCore DATACore;
        private ConfigParser MainConfigParser;
        public MainWindow()
        {
            InitializeComponent();
            DATACore = DataCore.getInstance();
            if (DATACore == null)
            {     
                System.Windows.MessageBox.Show("DataCore nulltr exception!");
                this.Close();
            }
            MainConfigParser = new ConfigParser("config.cfg");


            
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            // Datei oeffnen Dialog hier
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Label1.Content = File.ReadAllText(openFileDialog.FileName);

        }

            private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

        

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show(DATACore.Config.conf);
        }
    }
}
