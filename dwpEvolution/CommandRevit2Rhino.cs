#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Rhino;
using Rhino.FileIO;
using Rhino.PlugIns;
using System;
using System.IO;
#endregion Namespaces

namespace dwpEvolution
{
    [Transaction(TransactionMode.Manual)]
    public class CommandRevit2Rhino : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp?.ActiveUIDocument;
            Document doc = uidoc?.Document;

            if (doc == null)
            {
                message = "No active Revit document.";
                return Result.Failed;
            }

            // Define the hardcoded file path
            string filePath = @"C:\dwp_Rhino\donut.3dm";

            if (!File.Exists(filePath))
            {
                message = "The specified file does not exist: " + filePath;
                return Result.Failed;
            }

            try
            {
                // Ensure Rhino Inside is initialized by the user
                if (!IsRhinoInsideInitialized())
                {
                    TaskDialog.Show("Rhino Inside Not Started", "Please start Rhino Inside using the 'Start' button in the Revit UI before running this command.");
                    return Result.Cancelled;
                }

                // Proceed with Rhino document conversion
                return ExecuteRhinoConversion(filePath, ref message);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                message = "An error occurred: " + ex.Message;
                return Result.Failed;
            }
        }

        private bool IsRhinoInsideInitialized()
        {
            // Check if RhinoCore is already initialized
            return Rhino.Runtime.HostUtils.RunningInRhino;
        }
        
        private Result ExecuteRhinoConversion(string filePath, ref string message)
        {
            try
            {
                // Retry mechanism to ensure the file opens
                int retries = 3;
                for (int i = 0; i < retries; i++)
                {
                    var rhinoDoc = RhinoDoc.Open(filePath, out bool isOpen);
                    if (isOpen)
                    {
                        // Save the model to wavefront obj
                        var fileObjPath = Path.ChangeExtension(filePath, ".obj");
                        var fowo = new FileObjWriteOptions(new FileWriteOptions())
                        {
                            MeshParameters = Rhino.Geometry.MeshingParameters.Default,
                            ExportMaterialDefinitions = false,
                            MapZtoY = true,
                        };

                        var result = FileObj.Write(fileObjPath, rhinoDoc, fowo);
                        if (result != WriteFileResult.Success)
                        {
                            message = $"Failed to save the OBJ file. Result: {result}";
                            return Result.Failed;
                        }

                        TaskDialog.Show("Conversion Successful", $"OBJ file created at: {fileObjPath}");
                        return Result.Succeeded;
                    }

                    // Wait before retrying
                    System.Threading.Thread.Sleep(2000);
                }

                message = "Failed to open the Rhino document after multiple attempts.";
                return Result.Failed;
            }
            catch (Exception ex)
            {
                message = "An error occurred during conversion: " + ex.Message;
                return Result.Failed;
            }
        }
    }
}