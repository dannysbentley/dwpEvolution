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
            try
            {
                UIApplication uiapp = commandData.Application;
                UIDocument uidoc = uiapp?.ActiveUIDocument;
                Document doc = uidoc?.Document;

                UI_Rhino2Revit UI_revit2Rhino = new UI_Rhino2Revit();

                var ui = UI_revit2Rhino.ShowDialog();
                if (!(ui.HasValue && ui.Value)) return Result.Cancelled;

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                message = "An error occurred: " + ex.Message;
                return Result.Failed;
            }
        }
    }
}
