using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using dwpEvolution.Converters;
using Rhino;
using Rhino.Runtime.InProcess;
using System.IO;

namespace dwpEvolution
{
    public partial class UI_Rhino2Revit : Window
    {
        private RhinoCore _rhinoCore;
        private Task _initializationTask;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public UI_Rhino2Revit()
        {
            InitializeComponent();
            InitializeRhinoCoreAsync();
        }

        private void InitializeRhinoCoreAsync()
        {
            _initializationTask = Task.Run(() =>
            {
                try
                {
                    RhinoInside.Resolver.Initialize();
                    _rhinoCore = new RhinoCore(new string[] { "/nosplash" });
                }
                catch (FileNotFoundException ex)
                {
                    Dispatcher.Invoke(() =>
                        MessageBox.Show($"Failed to load RhinoCommon or one of its dependencies: {ex.Message}", "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error));
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() =>
                        MessageBox.Show($"Failed to initialize Rhino: {ex.Message}", "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error));
                }
            }, _cts.Token);
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Rhino Files (*.3dm)|*.3dm"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private async void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please select a file first.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            await _initializationTask;

            try
            {
                RhinoToObjConverter.Convert(filePath);
                MessageBox.Show("Conversion successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during conversion: {ex.Message}", "Conversion Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _cts.Cancel();
            _rhinoCore?.Dispose();
            base.OnClosed(e);
        }
    }
}
