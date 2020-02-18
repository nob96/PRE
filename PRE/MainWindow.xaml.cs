using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using PRE.Program;

namespace PRE
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {    
            InitializeComponent();
        }

        private void RunAnalysis_Click(object sender, RoutedEventArgs e)
        {
            Program.Hand hand = new Program.Hand();
            hand.GetConnectnessLevel("J87");
            Validator validator = new Validator(this);
            
            if(validator.ErrorMessage.Length > 0)
            {
                MessageBox.Show(validator.ErrorMessage);
            }
            else
            {
                RunWindow runWindow = new RunWindow();
                runWindow.Show();
                Analyse analyse = new Analyse();
                analyse.Run(runWindow);
            }
            
        }

        private void BrowseIPDest_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog folderDialog = this.GetFolderDialog();

            if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Config.DestinationFolderCalculated = folderDialog.FileName;
                this.PathCalcedIP.Text = folderDialog.FileName;
            }
        }

        private void BrowseSummaryDest_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog folderDialog = this.GetFolderDialog();

            if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Config.DestinationFolderSummary = folderDialog.FileName;
                this.PathSummary.Text = folderDialog.FileName;
            }
        }

        private CommonOpenFileDialog GetFolderDialog()
        {
            CommonOpenFileDialog folderDialog = new CommonOpenFileDialog();
            folderDialog.IsFolderPicker = true;
            folderDialog.InitialDirectory = @"C:\";

            return folderDialog;
        }

        private void BrowseIP_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = this.GetFileDialog();

            if (fileDialog.ShowDialog() == true)
            {
                Config.PathIP = fileDialog.FileName;
                this.PathIP.Text = fileDialog.FileName;
            }
        }

        private void BrowsePIO_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = this.GetFileDialog();

            if (fileDialog.ShowDialog() == true)
            {
                Config.PathPIO = fileDialog.FileName;
                this.PathPIO.Text = fileDialog.FileName;
            }
        }

        private void BrowseOOP_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = this.GetFileDialog();

            if (fileDialog.ShowDialog() == true)
            {
                Config.PathOOP = fileDialog.FileName;
                this.PathOOP.Text = fileDialog.FileName;
            }
        }

        private Microsoft.Win32.OpenFileDialog GetFileDialog()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "CSF files(*.csv)|*.csv";

            return fileDialog;
        }

        private void SetOopAsActiveReport_Click(object sender, RoutedEventArgs e)
        {
            if(this.CheckboxOOP.IsChecked == true)
            {
                Config.ActiveReport = this.PathOOP.Text;
                this.CheckboxIP.IsEnabled = false;
                Config.InactiveReport = this.PathIP.Text;
            }
            else if (this.CheckboxOOP.IsChecked == false)
            {
                this.CheckboxIP.IsEnabled = false;
            }
        }

        private void SetIPAsActiveReport_Click(object sender, RoutedEventArgs e)
        {
            if (this.CheckboxIP.IsChecked == true)
            {
                Config.ActiveReport = this.PathIP.Text;
                this.CheckboxOOP.IsEnabled = false;
                Config.InactiveReport = this.PathOOP.Text;
            }else if(this.CheckboxIP.IsChecked == false)
            {
                this.CheckboxOOP.IsEnabled = true;
            }
        }
    }
}
