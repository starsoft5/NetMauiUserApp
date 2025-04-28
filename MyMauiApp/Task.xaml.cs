using System.Collections.ObjectModel;
using System.Formats.Tar;
using MyMauiApp.Data.Models;

namespace MyMauiApp;

    public partial class Task : ContentPage
    {
    private readonly AppDbContext _db;
    public IList<MyMauiApp.Data.Models.Task> Tasks { get; set; } = [];

        private int nextId = 1;

        public Task(AppDbContext db)
        {
            InitializeComponent();
            BindingContext = this;
            _db = db;
        }

        private void OnAddTaskClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(taskEntry.Text))
            {
                validationLabel.Text = "Task description cannot be empty.";
                validationLabel.IsVisible = true;
                return;
            }

            validationLabel.IsVisible = false;
            

        var newTask = new MyMauiApp.Data.Models.Task
        {
            Id = nextId++,
            TaskDescription = taskEntry.Text.Trim(),
            Completed = 0
        };
            newTask.Id = 0;
        //_db.Tasks.Add(newTask);

        Hotel hotel = new();
        hotel.Id = 0;
        hotel.Name = "Sierraton Hotel";

        _db.Hotels.Add(hotel);
            _db.SaveChanges();
            taskEntry.Text = string.Empty;
    }

        private async void OnEditTaskClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var task = button?.CommandParameter as TaskModel;

            if (task == null) return;

            string result = await DisplayPromptAsync("Edit Task", "Update task description:", initialValue: task.TaskDescription);

            if (!string.IsNullOrWhiteSpace(result))
            {
                task.TaskDescription = result.Trim();
                RefreshTasks();
            }
        }

        private async void OnDeleteTaskClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var task = button?.CommandParameter as TaskModel;

            if (task == null) return;

            bool confirm = await DisplayAlert("Delete Task", $"Are you sure you want to delete \"{task.TaskDescription}\"?", "Yes", "No");

            if (confirm)
            {
                //Tasks.Remove(task);
            }
        }

        private void RefreshTasks()
        {
        // Force CollectionView to refresh
        //var tempList = _db.Tasks.ToList();
        //    Tasks.Clear();
         //   foreach (var item in tempList)
        //    {
        //        Tasks.Add(item);
        //    }
        //taskCollectionView.ItemsSource = Tasks;
        }
        

}

    public class TaskModel
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public bool Completed { get; set; }
    }
 
