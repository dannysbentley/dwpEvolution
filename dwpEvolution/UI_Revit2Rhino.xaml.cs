using System.Windows;
using Microsoft.Win32;
using Rhino;
using Rhino.Runtime.InProcess;

namespace dwpEvolution
{
    

    public partial class UI_Rhino2Revit : Window
    {     
        public UI_Rhino2Revit()
        {
            InitializeComponent();
        }

        internal string SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
                return FilePathTextBox.Text;
            }
            return null;
        }
    }
}
