using MyMauiApp.Data;
using MyMauiApp.Data.Models;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    private readonly AppDbContext _db;
    private User _selectedUser;
    private DateTime selectedDate;

    public MainPage(AppDbContext db)
    {
        InitializeComponent();
        _db = db;
        selectedDate = DateTime.Now;
        LoadUsers();
    }

    private void LoadUsers()
    {
        usersCollection.ItemsSource = _db.Users.OrderBy(u => u.FirstName).ToList();
        _selectedUser = null;
    }

    private void OnSaveUserClicked(object sender, EventArgs e)
    {
        var firstName = firstNameEntry.Text;
        var lastName = lastNameEntry.Text;
        var birthDay = selectedDate;

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            _selectedUser = null;
            DisplayAlert("Validation", "Both names are required.", "OK");
            return;
        }

        if (_selectedUser != null)
        {
            _selectedUser.FirstName = firstName;
            _selectedUser.LastName = lastName;
            _selectedUser.BirthDay = selectedDate.Date;
            _db.Users.Update(_selectedUser);
            _selectedUser = null;
        }
        else
        {
            var user = new User { FirstName = firstName, LastName = lastName, BirthDay = selectedDate };
            _db.Users.Add(user);
        }

        _db.SaveChanges();
        firstNameEntry.Text = lastNameEntry.Text = string.Empty;
        LoadUsers();
    }

    private void OnEditClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var user = button?.CommandParameter as User;
        if (user != null)
        {
            _selectedUser = user;
            firstNameEntry.Text = user.FirstName;
            lastNameEntry.Text = user.LastName;
            
        }
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var user = button?.CommandParameter as User;
        if (user != null)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
            LoadUsers();
        }
    }

    private void OnUserSelected(object sender, SelectionChangedEventArgs e)
    {
        _selectedUser = e.CurrentSelection.FirstOrDefault() as User;
    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        // Get the selected date
        selectedDate = e.NewDate;
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        firstNameEntry.Text = lastNameEntry.Text = string.Empty;
        LoadUsers();

    }

    // Called when "Edit" label is tapped
    private void OnEditTapped(object sender, EventArgs e)
    {
        var label = (Label)sender;
        var user = (User)label.BindingContext;

        if (user != null)
        {
            firstNameEntry.Text = user.FirstName;
            lastNameEntry.Text = user.LastName;
            birthDayDatePicker.Date = user.BirthDay;
            _selectedUser = user;
        }
    }

    // Called when "Delete" label is tapped
    private void OnDeleteTapped(object sender, EventArgs e)
    {
        var label = (Label)sender;
        var user = (User)label.BindingContext;

        if (user != null)
        {
            // Assuming usersCollection.ItemsSource is a List<User>
            var users = (List<User>)usersCollection.ItemsSource;
            _db.Users.Remove(user);
            _db.SaveChanges();
            LoadUsers();
            //usersCollection.ItemsSource = null; // Force refresh
            //usersCollection.ItemsSource = users;
            _selectedUser = null;
        }
    }

    private async void DeleteLabel_Tapped(object sender, EventArgs e)
    {
        var label = (Label)sender;
        var user = (User)label.BindingContext;
        bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete " + user.FullName + " ?", "Yes", "No");
        if (confirm)
        {
            if (user != null)
            {
                var users = (List<User>)usersCollection.ItemsSource;
                _db.Users.Remove(user);
                _db.SaveChanges();
                LoadUsers();
                _selectedUser = null;
            }
        }
    }

    private async void OnRowLoaded(object sender, EventArgs e)
    {
        if (sender is Grid rowGrid)
        {
            rowGrid.Opacity = 0;
            await rowGrid.FadeTo(1, 400, Easing.SinInOut);
        }
    }

}
