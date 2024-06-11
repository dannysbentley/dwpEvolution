#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
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


            return Result.Succeeded;
        }
    }
}
