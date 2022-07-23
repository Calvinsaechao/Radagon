namespace Radagon
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows.Media.Imaging;
    using Autodesk.Revit.UI;

    public class Main : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            /*if (DateTime.Compare(DateTime.Now.ToUniversalTime(), new DateTime(2022, 7, 4, 3, 20, 5, DateTimeKind.Utc)) <= 0)*/
            if (true)
            {
                string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string imagesFolder = assemblyFolder + "\\res\\";
                string tabName = "Radagon";
                string generalPanelName = "General";

                //Create Radagon tab
                application.CreateRibbonTab(tabName);

                //Create panel under Radagon tab
                var generalPanel = application.CreateRibbonPanel(tabName, generalPanelName);

                //Create buttons
                var CopyViewsBtnData = new PushButtonData("CopyViewsBtnData", "Copy Over\nView Plans", Assembly.GetExecutingAssembly().Location, "Radagon.Commands.CopyViewsCommand")
                {
                    ToolTipImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_320x320.png")),
                    LargeImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_32x32.png")),
                    ToolTip = "Copy selected view plans from one document to another"
                };
                var DuplicateViewsBtnData = new PushButtonData("DuplicateViewsBtnData", "Duplicate\nView Plans", Assembly.GetExecutingAssembly().Location, "Radagon.Commands.DuplicateViewsCommand")
                {
                    ToolTipImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_320x320.png")),
                    LargeImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_32x32.png")),
                    ToolTip = "Duplicate selected view plans into the current document"
                };
                var EditSelectedViewportLocationsCommandBtnData = new PushButtonData("EditSelectedViewportLocationsCommandBtnData", "Select & Edit\nViewport Locations", Assembly.GetExecutingAssembly().Location, "Radagon.Commands.EditSelectedViewportLocationsCommand")
                {
                    ToolTipImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_320x320.png")),
                    LargeImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_32x32.png")),
                    ToolTip = "Edit viewport locations in each selected view sheet"
                };

                //Add buttons to panel
                var CopyViewsBtn = generalPanel.AddItem(CopyViewsBtnData) as PushButton;
                var DuplicateViewsBtn = generalPanel.AddItem(DuplicateViewsBtnData) as PushButton;
                var EditSelectedViewportLocationsBtn = generalPanel.AddItem(EditSelectedViewportLocationsCommandBtnData) as PushButton;

                //Upload Excel File Panel
                string uploadExcelFilePanelName = "Excel File";

                //Create Panel under Radagon
                var uploadExcelFilePanel = application.CreateRibbonPanel(tabName, uploadExcelFilePanelName);

                var CreateSheetsBtnData = new PushButtonData("CreateSheetsBtnData", "Create Sheets\nExcel File", Assembly.GetExecutingAssembly().Location, "Radagon.Commands.CreateSheetsCommand")
                {
                    ToolTipImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_320x320.png")),
                    LargeImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_32x32.png")),
                    ToolTip = "Create Sheets in the current document from a xlsx file"
                };
                var EditViewportLocationsCommandBtnData = new PushButtonData("EditViewportLocationsCommandBtnData", "Edit Viewports\nExcel File", Assembly.GetExecutingAssembly().Location, "Radagon.Commands.EditViewportLocationsCommand")
                {
                    ToolTipImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_320x320.png")),
                    LargeImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_32x32.png")),
                    ToolTip = "Edit viewport locations selected from the current document with a xlsx file"
                };

                //Add buttons to panel
                var CreateSheetsBtn = uploadExcelFilePanel.AddItem(CreateSheetsBtnData) as PushButton;
                var EditViewportLocationsBtn = uploadExcelFilePanel.AddItem(EditViewportLocationsCommandBtnData) as PushButton;

                //Developer Panel
                /*string panelDevName = "Developer Tools";
                var panelDev = application.CreateRibbonPanel(tabName, panelDevName);
                var GetSelectedBtnData = new PushButtonData("GetSelectedBtn", "Select Elements", Assembly.GetExecutingAssembly().Location, "Radagon.Commands.GetSelectedIdsCommand")
                {
                    ToolTip = "Returns the ElementId of the selected elements.",
                    LargeImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_32x32.png")),
                };
                var TestFormBtnData = new PushButtonData("TestFormBtn", "Test Form", Assembly.GetExecutingAssembly().Location, "Radagon.Commands.TestFormCommand")
                {
                    ToolTip = "Shows a form loaded for testing purposes.",
                    LargeImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_32x32.png")),
                };
                var PlaceGroupBtnData = new PushButtonData("PlaceGroupBtnData", "Place Group", Assembly.GetExecutingAssembly().Location, "Radagon.Commands.PlaceGroupCommand")
                {
                    ToolTipImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_320x320.png")),
                    ToolTip = "Select a group to copy then select a location to place the group.",
                    LargeImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_32x32.png")),
                };
                var ProjectParameterGuidsCommandBtnData = new PushButtonData("ProjectParameterGuidsBtnData", "Project Parameters", Assembly.GetExecutingAssembly().Location, "Radagon.Commands.ProjectParameterGuidsCommand")
                {
                    ToolTip = "Returns the Project Parameters",
                    LargeImage = new BitmapImage(new Uri(imagesFolder + "placegroup_img_32x32.png")),
                };
                var GetSelectedBtn = panelDev.AddItem(GetSelectedBtnData) as PushButton;
                var TestFormBtn = panelDev.AddItem(TestFormBtnData) as PushButton;
                var PlaceGroupBtn = panelDev.AddItem(PlaceGroupBtnData) as PushButton;
                var ProjectParameterGuidsBtn = panelDev.AddItem(ProjectParameterGuidsCommandBtnData) as PushButton;*/

                return Result.Succeeded;
            }
            TaskDialog.Show("Radagon", "License has expired!");
            return Result.Failed;
        }
        
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
