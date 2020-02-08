using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace PRE.Program
{
    class Validator
    {
        public string ErrorMessage
        {
            get;
            private set;
        }

        private readonly MainWindow MainWindow;
        
        public Validator(MainWindow mainWindow)
        {
            this.ErrorMessage = "";
            this.MainWindow = mainWindow;
            this.Accessible();
            this.Empty();
            this.HasActiveReport();


        }

        private void Empty()
        {
            bool empty = false;

            empty = this.MainWindow.PathIP.Text.Length > 0 ? false : true;
            empty = this.MainWindow.PathOOP.Text.Length > 0 ? false : true;
            empty = this.MainWindow.PathPIO.Text.Length > 0 ? false : true;
            empty = this.MainWindow.PathCalcedIP.Text.Length > 0 ? false : true;
            empty = this.MainWindow.PathSummary.Text.Length > 0 ? false : true;

            if(empty == true)
            {
                this.ErrorMessage = "Please consider all inputs.";
            }
        }

        private void Accessible()
        {
            bool accessible = true;
            
            accessible = Directory.Exists(this.MainWindow.PathIP.Text) ? true : false;
            accessible = Directory.Exists(this.MainWindow.PathOOP.Text) ? true : false;
            accessible = Directory.Exists(this.MainWindow.PathPIO.Text) ? true : false;
            accessible = Directory.Exists(this.MainWindow.PathCalcedIP.Text) ? true : false;
            accessible = Directory.Exists(this.MainWindow.PathSummary.Text) ? true : false;

            if(accessible == false)
            {
                this.ErrorMessage = "Some paths cannot be accessed with your current user rights.";
            }
        }

        private void HasActiveReport()
        {
            if(this.MainWindow.CheckboxIP.IsChecked == false && this.MainWindow.CheckboxOOP.IsChecked == false)
            {
                this.ErrorMessage = "No report was selected for calculation. Please select one.";
            }
        }
    }
}
