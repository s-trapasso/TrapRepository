using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Maui.Kanban;


namespace SyncfusionMAUIApp1
{
    internal class KanbanViewModel
 {
     public ObservableCollection<KanbanModel> Cards { get; set; }

     public KanbanViewModel()
     {

         Cards = new ObservableCollection<KanbanModel>();

         Cards.Add(
             new KanbanModel()
             {
                 ID = 1,
                 Title = "iOS - 1",
                 ImageURL = "People_circle1.png",
                 Category = "Open",
                 Description = "Analyze customer requirements",
                 IndicatorFill = SolidColorBrush.Red,
                 Tags = new List<string> { "Bug", "Customer", "Release Bug" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 6,
                 Title = "Xamarin - 6",
                 ImageURL = "People_Circle2.png",
                 Category = "Open",
                 Description = "Show the retrived data from the server in grid control",
                 IndicatorFill = SolidColorBrush.Red,
                 Tags = new List<string> { "Bug", "Customer", "Breaking Issue" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 3,
                 Title = "iOS - 3",
                 ImageURL = "People_Circle3.png",
                 Category = "Open",
                 Description = "Fix the filtering issues reported in safari",
                 IndicatorFill = SolidColorBrush.Red,
                 Tags = new List<string> { "Bug", "Customer", "Breaking Issue" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 11,
                 Title = "iOS - 11",
                 ImageURL = "People_Circle4.png",
                 Category = "Open",
                 Description = "Add input validation for editing",
                 IndicatorFill = SolidColorBrush.Red,
                 Tags = new List<string> { "Bug", "Customer", "Breaking Issue" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 15,
                 Title = "Android - 15",
                 Category = "Open",
                 ImageURL = "People_Circle5.png",
                 Description = "Arrange web meeting for cutomer requirement",
                 IndicatorFill = SolidColorBrush.Red,
                 Tags = new List<string> { "Story", "Kanban" }
             });

         Cards.Add(
             new KanbanModel()
             {
                 ID = 3,
                 Title = "Android - 3",
                 Category = "Code Review",
                 ImageURL = "People_Circle6.png",
                 Description = "API Improvements",
                 IndicatorFill = SolidColorBrush.Purple,
                 Tags = new List<string> { "Bug", "Customer" }
             });

         Cards.Add(
             new KanbanModel()
             {
                 ID = 4,
                 Title = "UWP - 4",
                 ImageURL = "People_Circle7.png",
                 Category = "Code Review",
                 Description = "Enhance editing functionality",
                 IndicatorFill = SolidColorBrush.Brown,
                 Tags = new List<string> { "Story", "Kanban" }
             });

         Cards.Add(
             new KanbanModel()
             {
                 ID = 9,
                 Title = "Xamarin - 9",
                 ImageURL = "People_Circle8.png",
                 Category = "Code Review",
                 Description = "Improve application performance",
                 IndicatorFill = SolidColorBrush.Orange,
                 Tags = new List<string> { "Story", "Kanban" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 13,
                 Title = "UWP - 13",
                 ImageURL = "People_Circle9.png",
                 Category = "In Progress",
                 Description = "Add responsive support to applicaton",
                 IndicatorFill = SolidColorBrush.Brown,
                 Tags = new List<string> { "Story", "Kanban" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 17,
                 Title = "Xamarin - 17",
                 Category = "In Progress",
                 ImageURL = "People_Circle10.png",
                 Description = "Fix the issues reported in IE browser",
                 IndicatorFill = SolidColorBrush.Brown,
                 Tags = new List<string> { "Bug", "Customer" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 21,
                 Title = "Xamarin - 21",
                 Category = "In Progress",
                 ImageURL = "People_Circle11.png",
                 Description = "Improve performance of editing functionality",
                 IndicatorFill = SolidColorBrush.Purple,
                 Tags = new List<string> { "Bug", "Customer" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 19,
                 Title = "iOS - 19",
                 Category = "In Progress",
                 ImageURL = "People_Circle12.png",
                 Description = "Fix the issues reported by the customer",
                 IndicatorFill = SolidColorBrush.Purple,
                 Tags = new List<string> { "Bug" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 8,
                 Title = "Android",
                 Category = "Code Review",
                 ImageURL = "People_Circle13.png",
                 Description = "Check login page validation",
                 IndicatorFill = SolidColorBrush.Brown,
                 Tags = new List<string> { "Feature" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 24,
                 Title = "UWP - 24",
                 ImageURL = "People_Circle14.png",
                 Category = "In Progress",
                 Description = "Test editing functionality",
                 IndicatorFill = SolidColorBrush.Orange,
                 Tags = new List<string> { "Feature", "Customer", "Release" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 20,
                 Title = "iOS - 20",
                 Category = "In Progress",
                 ImageURL = "People_Circle15.png",
                 Description = "Fix the issues reported in data binding",
                 IndicatorFill = SolidColorBrush.Red,
                 Tags = new List<string> { "Feature", "Release" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 12,
                 Title = "Xamarin - 12",
                 Category = "In Progress",
                 ImageURL = "People_Circle16.png",
                 Description = "Test editing functionality",
                 IndicatorFill = SolidColorBrush.Red,
                 Tags = new List<string> { "Feature", "Release" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 11,
                 Title = "iOS - 11",
                 Category = "In Progress",
                 ImageURL = "People_Circle17.png",
                 Description = "Check filtering validation",
                 IndicatorFill = SolidColorBrush.Red,
                 Tags = new List<string> { "Feature", "Release" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 13,
                 Title = "UWP - 13",
                 ImageURL = "People_Circle18.png",
                 Category = "Closed",
                 Description = "Fix cannot open user's default database sql error",
                 IndicatorFill = SolidColorBrush.Purple,
                 Tags = new List<string> { "Bug", "Internal", "Release" }
             });

         Cards.Add(
             new KanbanModel()
             {
                 ID = 14,
                 Title = "Android - 14",
                 Category = "Closed",
                 ImageURL = "People_Circle19.png",
                 Description = "Arrange web meeting with customer to get login page requirement",
                 IndicatorFill = SolidColorBrush.Red,
                 Tags = new List<string> { "Feature" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 15,
                 Title = "Xamarin - 15",
                 Category = "Closed",
                 ImageURL = "People_Circle20.png",
                 Description = "Login page validation",
                 IndicatorFill = SolidColorBrush.Red,
                 Tags = new List<string> { "Bug" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 16,
                 Title = "Xamarin - 16",
                 ImageURL = "People_Circle21.png",
                 Category = "Closed",
                 Description = "Test the application in IE browser",
                 IndicatorFill = SolidColorBrush.Purple,
                 Tags = new List<string> { "Bug" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 20,
                 Title = "UWP - 20",
                 ImageURL = "People_Circle22.png",
                 Category = "Closed",
                 Description = "Analyze stored procedure",
                 IndicatorFill = SolidColorBrush.Brown,
                 Tags = new List<string> { "CustomSample", "Customer", "Incident" }
             }
         );

         Cards.Add(
             new KanbanModel()
             {
                 ID = 21,
                 Title = "Android - 21",
                 Category = "Closed",
                 ImageURL = "People_Circle23.png",
                 Description = "Arrange web meeting with customer to get editing requirements",
                 IndicatorFill = SolidColorBrush.Orange,
                 Tags = new List<string> { "Story", "Improvement" }
             }
         );

     }
 }
}
