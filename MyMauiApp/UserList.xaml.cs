// Filename: MainPage.xaml.cs
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;
using MyMauiApp.Data.Models;

namespace MyMauiApp;

public partial class UserList : ContentPage
{
    private readonly AppDbContext _db;

    private IList<User> _users = [];
    bool isFirstNameAscending = true;
    bool isLastNameAscending = true;
    bool isBirthDayAscending = true;

    public UserList(AppDbContext db)
    {
        InitializeComponent();
        _db = db;
        LoadUsers();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // This will always be executed when the page appears
        LoadUsers();
    }


    void LoadUsers()
    {
        _users = _db.Users.OrderBy(u => u.FirstName).ToList();
        usersCollection.ItemsSource = _users;
    }

    void OnSortByFirstName(object sender, EventArgs e)
    {
        var sorted = isFirstNameAscending
            ? _users.OrderBy(u => u.FirstName).ToList()
            : _users.OrderByDescending(u => u.FirstName).ToList();

        isFirstNameAscending = !isFirstNameAscending;
        UpdateHeaderArrow(FirstNameHeader, "First Name", isFirstNameAscending);
        ResetOtherHeaders(nameof(FirstNameHeader));
        UpdateUserCollection(sorted);
    }

    void OnSortByLastName(object sender, EventArgs e)
    {
        var sorted = isLastNameAscending
            ? _users.OrderBy(u => u.LastName).ToList()
            : _users.OrderByDescending(u => u.LastName).ToList();

        isLastNameAscending = !isLastNameAscending;
        UpdateHeaderArrow(LastNameHeader, "Last Name", isLastNameAscending);
        ResetOtherHeaders(nameof(LastNameHeader));
        UpdateUserCollection(sorted);
    }

    void OnSortByBirthDay(object sender, EventArgs e)
    {
        var sorted = isBirthDayAscending
            ? _users.OrderBy(u => u.BirthDay).ToList()
            : _users.OrderByDescending(u => u.BirthDay).ToList();

        isBirthDayAscending = !isBirthDayAscending;
        UpdateHeaderArrow(BirthDayHeader, "BirthDay", isBirthDayAscending);
        ResetOtherHeaders(nameof(BirthDayHeader));
        UpdateUserCollection(sorted);
    }

    void UpdateUserCollection(List<User> sortedList)
    {
        _users.Clear();
        foreach (var user in sortedList)
        {
            _users.Add(user);
        }
        usersCollection.ItemsSource = null;
        usersCollection.ItemsSource = _users;
    }

    void UpdateHeaderArrow(Label header, string title, bool ascending)
    {
        header.Text = ascending ? $"{title} Asc" : $"{title} Desc";
    }

    void ResetOtherHeaders(string exceptName)
    {
        if (exceptName != nameof(FirstNameHeader)) FirstNameHeader.Text = "First Name";
        if (exceptName != nameof(LastNameHeader)) LastNameHeader.Text = "Last Name";
        if (exceptName != nameof(BirthDayHeader)) BirthDayHeader.Text = "BirthDay";
    }

    // Placeholder event handlers
    void OnUserSelected(object sender, SelectionChangedEventArgs e) { usersCollection.ItemsSource = _users; }
    void OnEditTapped(object sender, EventArgs e) { /* ... */ }
    void OnDeleteTapped(object sender, EventArgs e) { /* ... */ }
}
