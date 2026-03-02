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
    /// Interaction logic for descriptions.xaml
    /// </summary>
    public partial class descriptions : Page
    {
      TaskManagementEntities db=new TaskManagementEntities();
        public descriptions(User1 user)
        {
            InitializeComponent();

            string username = user.Name;
            WelcomeTB.Text = username;


            DataContext = this;
            grid1();
            grid2();


        }



        public void grid1()
        {
            var task1 = db.Tasks.Where(u => u.Status == "In Progress" || u.Status == "Pending").ToList();
            DGPendingInProgress.ItemsSource = task1;
        }

        public void grid2()
        {

            var task2 = db.Tasks.Where(u => u.Status == "Completed").ToList();

            DGCompleted.ItemsSource = task2;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

           int taskid=int.Parse(TaskIDTB.Text);
            var task=db.Tasks.FirstOrDefault(u=>u.TaskID==taskid);
            if (task != null) 
            {
                task.Status=StatusCB.Text;
                db.SaveChanges();
                grid1();
                grid2();
            }
            else
            {
                MessageBox.Show("gggggggggg");
            }
        }
    }
}
