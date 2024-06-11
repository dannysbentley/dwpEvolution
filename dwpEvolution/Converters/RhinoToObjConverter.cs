// Converters/RhinoToObjConverter.cs
using Rhino;
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
            using (new RhinoCore(new string[] { "/nosplash" }))
            {
                var rhinoDoc = RhinoDoc.Open(filePath, out bool isOpen);

                var fileObjPath = System.IO.Path.ChangeExtension(filePath, ".obj");
                var fowo = new Rhino.FileIO.FileObjWriteOptions(
                    new Rhino.FileIO.FileWriteOptions())
                {
                    MeshParameters = Rhino.Geometry.MeshingParameters.Default,
                    ExportMaterialDefinitions = false,
                    MapZtoY = true,
                };

                var result = Rhino.FileIO.FileObj.Write(fileObjPath, rhinoDoc, fowo);
                Console.WriteLine($"File {result}");
            }
        }
    }
}
