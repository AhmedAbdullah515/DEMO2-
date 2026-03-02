using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DEMO2_
{
    /// <summary>
    /// Interaction logic for management_page.xaml
    /// </summary>
    public partial class management_page : Page
    {
        TaskManagementEntities db=new TaskManagementEntities();
        public management_page()
        {
            InitializeComponent();
            datagrid_cs.ItemsSource = db.Tasks.ToList();
        }

        // add button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(titlebox.Text) || string.IsNullOrWhiteSpace(combotextbox.Text)
                || string.IsNullOrWhiteSpace(taskbox.Text) || string.IsNullOrWhiteSpace(desbox.Text))
            {
                MessageBox.Show("Please enter all data to add task.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Parse Task ID safely
            if (!int.TryParse(taskbox.Text, out int t_id))
            {
                MessageBox.Show("Task ID must be a valid integer.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Prevent duplicate TaskID (if TaskID is key)
            if (db.Tasks.Any(x => x.TaskID == t_id))
            {
                MessageBox.Show("A task with this ID already exists.", "Duplicate", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create and save new task
            var t = new Task
            {
                TaskID = t_id,
                Title = titlebox.Text,
                Status = combotextbox.Text,
                Description = desbox.Text
            };

            db.Tasks.Add(t);
            try
            {
                db.SaveChanges();
                // Refresh grid
                datagrid_cs.ItemsSource = db.Tasks.ToList();
                MessageBox.Show("Add successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // edit button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selected=datagrid_cs.SelectedItem as Task;
            if (selected != null) 
            { 
                
                selected.TaskID = int.Parse(taskbox.Text);
                selected.Title = titlebox.Text;
                selected.Description= desbox.Text;
                selected.Status = combotextbox.Text;
                MessageBox.Show("update successfully");

                db.SaveChanges();

            }
            else
            {
                MessageBox.Show("please select item from data to update");
            }
            datagrid_cs.ItemsSource=db.Tasks.ToList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            var selected = datagrid_cs.SelectedItem as Task;
            if (selected == null) 
            {

                MessageBox.Show("please select item to delete it ");
            }

            else
            {
             db.Tasks.Remove(selected);
                db.SaveChanges();
                datagrid_cs.ItemsSource = db.Tasks.ToList();
            }
        }
    }
}
