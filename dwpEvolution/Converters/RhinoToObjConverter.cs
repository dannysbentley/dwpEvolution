// Converters/RhinoToObjConverter.cs
using Rhino;
using Rhino.FileIO;
using Rhino.Geometry;
using Rhino.PlugIns;
using Rhino.Runtime.InProcess;
using System;

namespace dwpEvolution.Converters
{
    public static class RhinoToObjConverter
    {
        static RhinoToObjConverter()
        {
            RhinoInside.Resolver.Initialize();
        }

        public static void Convert(string filePath)
        {
            try
            {
                using (new RhinoCore(new string[] { "/nosplash" }, Rhino.Runtime.InProcess.WindowStyle.NoWindow))
                {
                    var rhinoDoc = RhinoDoc.Open(filePath, out bool isOpen);
                    if (!isOpen)
                    {
                        throw new Exception("Failed to open the Rhino document.");
                    }

                    var fileObjPath = System.IO.Path.ChangeExtension(filePath, ".obj");
                    var fowo = new FileObjWriteOptions(new FileWriteOptions())
                    {
                        MeshParameters = MeshingParameters.Default,
                        ExportMaterialDefinitions = false,
                        MapZtoY = true,
                    };

                    var result = FileObj.Write(fileObjPath, rhinoDoc, fowo);
                    if (result != WriteFileResult.Success)
                    {
                        throw new Exception($"Failed to save the OBJ file. Result: {result}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during conversion: {ex.Message}", ex);
            }
        }
    }
}
