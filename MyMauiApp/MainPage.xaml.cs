using MyMauiApp.Data;
using MyMauiApp.Data.Models;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    private readonly AppDbContext _db;
    private User _selectedUser;

    public MainPage(AppDbContext db)
    {
        InitializeComponent();
        _db = db;
        LoadUsers();
    }

    private void LoadUsers()
    {
        usersCollection.ItemsSource = _db.Users.ToList();
    }

    private void OnSaveUserClicked(object sender, EventArgs e)
    {
        var firstName = firstNameEntry.Text;
        var lastName = lastNameEntry.Text;

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            DisplayAlert("Validation", "Both names are required.", "OK");
            return;
        }

        if (_selectedUser != null)
        {
            _selectedUser.FirstName = firstName;
            _selectedUser.LastName = lastName;
            _db.Users.Update(_selectedUser);
            _selectedUser = null;
        }
        else
        {
            var user = new User { FirstName = firstName, LastName = lastName };
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
}
