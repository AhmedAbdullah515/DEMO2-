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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        User1 u=new User1();
        public login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (TaskManagementEntities task = new TaskManagementEntities()) 
            {
                if (string.IsNullOrEmpty(passwordtextbox.Password)||string.IsNullOrEmpty(emailtextbox.Text)) 
                {

                    MessageBox.Show("please enter email and password");
                }
               
                string email =emailtextbox.Text; 
                string password2 =passwordtextbox.Password;

                var t=task.User1.FirstOrDefault(x => x.Email == email&&x.Password==password2);
                if (t != null)
                {
                    MessageBox.Show("login successfull!!");

                    if (t.Role == "Employee")
                    {
                        this.NavigationService.Navigate(new descriptions(t));
                    }

                    else if (t.Role == "Manager")
                    {
                        this.NavigationService.Navigate(new management_page());
                    }
                }


                else
                {
                    MessageBox.Show("invalid email or password");
                    return;
                }
               
            }
        }
    }
}
