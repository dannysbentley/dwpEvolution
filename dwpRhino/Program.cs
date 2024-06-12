using Rhino;
using Rhino.Runtime.InProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dwpEvolution
{
    internal class Program
    {
        //Open the model in Rhino
        static Program()
        {
            RhinoInside.Resolver.Initialize();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("file path .3dm to .obj");
            var filePath = Console.ReadLine();
            //Open Rhino, Loading assemblies
            using(new RhinoCore(new string[] { "/nosplash" }))
            {
                //Rhino is open, can access Rhino funtions              
                var rhinoDoc = RhinoDoc.Open(filePath, out bool isOpen);

                //Save the model to waveformat obj
                var fileObjPath = System.IO.Path.ChangeExtension(filePath, ".obj");
                var fowo = new Rhino.FileIO.FileObjWriteOptions(
                    new Rhino.FileIO.FileWriteOptions())
                {
                    MeshParameters = Rhino.Geometry.MeshingParameters.Default,
                    ExportMaterialDefinitions = false,
                    MapZtoY = true,
                };

                var result = Rhino.FileIO.FileObj.Write(fileObjPath, rhinoDoc, fowo);
                Console.WriteLine($"File {result.ToString()}");
            }
        }        
    }
}
