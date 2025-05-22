using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SyncfusionMAUIApp1;
///<summary>
///SchedulerFeatures class
///</summary>
public partial class SchedulerFeatures : ContentPage
{
    ///<summary>
    ///SchedulerFeatures class constructor
    ///</summary>
    public SchedulerFeatures()
    {
        InitializeComponent();
    }
}
    ///<summary>
    ///ViewModel class for the SchedulerFeatures
    ///</summary>
public class SchedulerViewModel
{
    public SchedulerViewModel()
    {
        this.GenerateAppointments();
        this.DisplayDate = DateTime.Now.Date.AddHours(8).AddMinutes(50);
    }
    public DateTime DisplayDate { get; set; }
    public ObservableCollection<SchedulerAppointment> Events { get; set; }
    private void GenerateAppointments()
    {
        this.Events = new ObservableCollection<SchedulerAppointment>();
        //Adding the schedule appointments in the schedule appointment collection.
        this.Events.Add(new SchedulerAppointment
        {
            StartTime = DateTime.Now.Date.AddHours(10),
            EndTime = DateTime.Now.Date.AddHours(11),
            Subject = "Client Meeting",
            Background = new SolidColorBrush(Color.FromArgb("#FF8B1FA9")),
        });
        this.Events.Add(new SchedulerAppointment
        {
            StartTime = DateTime.Now.Date.AddDays(1).AddHours(13),
            EndTime = DateTime.Now.Date.AddDays(1).AddHours(14),
            Subject = "GoToMeeting",
            Background = new SolidColorBrush(Color.FromArgb("#FFD20100")),
        });
    }
}
