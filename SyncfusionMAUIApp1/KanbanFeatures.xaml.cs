namespace SyncfusionMAUIApp1;
    public partial class KanbanFeatures : ContentPage
    {
        public KanbanFeatures()
        {
            InitializeComponent();
            column1.Categories = new List<object> { "Open" };
            column2.Categories = new List<object> { "In Progress" };
            column3.Categories = new List<object> { "Code Review" };
            column4.Categories = new List<object> { "Closed" };             
        }
    }
