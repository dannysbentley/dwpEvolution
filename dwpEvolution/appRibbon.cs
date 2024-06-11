using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;
using System;
using System.IO;

namespace dwpEvolution
{
    internal class appRibbon : IExternalApplication
    {       

        public Result OnStartup(UIControlledApplication a)
        {
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string assemblyDir = Path.GetDirectoryName(assemblyPath);

            try
            {
                // Create a new tab named "dwp Tools"
                a.CreateRibbonTab("dwp Rhino");

                // Create first panel "dwp | BIM Help"
                RibbonPanel dwpHelpPanel = a.CreateRibbonPanel("dwp Rhino", "dwp | Rhino Tools");

                // Add buttons to the first panel
                CreateButton(dwpHelpPanel, "Health", "BIM Health", assemblyPath, "dwpBeyond.CommandHealth", "dwpBIMHealth.png", assemblyDir);
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
                return Result.Failed;
            }

            return Result.Succeeded;
        }

        private void CreateButton(RibbonPanel panel, string name, string text, string assemblyPath, string className, string imageName, string imagePath)
        {
            PushButtonData buttonData = new PushButtonData(name, text, assemblyPath, className);
            PushButton button = panel.AddItem(buttonData) as PushButton;
            button.ToolTip = text;
            button.LargeImage = new BitmapImage(new Uri(Path.Combine(imagePath, imageName)));
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
