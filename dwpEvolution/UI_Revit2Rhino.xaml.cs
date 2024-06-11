using System;
using System.Windows;
using Microsoft.Win32;
using dwpEvolution.Converters;

namespace dwpEvolution
{
    public partial class UI_Rhino2Revit : Window
    {
        public UI_Rhino2Revit()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Rhino Files (*.3dm)|*.3dm";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please select a file first.");
                return;
            }

            try
            {
                RhinoToObjConverter.Convert(filePath);
                MessageBox.Show("Conversion successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during conversion: {ex.Message}");
            }
        }
    }
}
