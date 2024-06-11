// Program.cs
using System;
using dwpEvolution.Converters;

namespace dwpEvolution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the file path for the .3dm file to convert to .obj:");
            var filePath = Console.ReadLine();

            try
            {
                RhinoToObjConverter.Convert(filePath);
                Console.WriteLine("Conversion successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during conversion: {ex.Message}");
            }
        }
    }
}
