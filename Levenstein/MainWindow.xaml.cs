using System.Data.Entity;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace Levenstein
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (MarathonSystemEntities context = new MarathonSystemEntities())
            {
                context.User.Load();
                usersDataGrid.ItemsSource = context.User.Local;
            }
        }

        private void searchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            using (MarathonSystemEntities context = new MarathonSystemEntities())
            {
                List<User> users = new List<User>();
                foreach (User user in context.User)
                {
                    int diff1 = Levenshtein.LevenshteinDistance(searchBox.Text, user.FirstName);
                    int diff2 = Levenshtein.LevenshteinDistance(searchBox.Text, user.LastName);
                    int diff3 = Levenshtein.LevenshteinDistance(searchBox.Text, user.Email);
                    if ((diff1 <= 3) || (diff2 <= 3) || (diff3 <= 3))
                    {
                        users.Add(user);
                        usersDataGrid.ItemsSource = users.ToList();
                    }   
                }
                           
            }

        }
    }
}