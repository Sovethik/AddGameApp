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

namespace AddGameApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public WindowUser windowUser;
        public static int IdUser;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {

            using (var bd = new AddGameBDEntities())
            {
                if(TxtLogin.Text == "")
                {
                    MessageBox.Show("Заполните логин", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if(TxtPassword.Password == "")
                {
                    MessageBox.Show("Заполните пароль", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var currentUser = bd.Usesrs.AsNoTracking().FirstOrDefault(x => x.login == TxtLogin.Text && x.password == TxtPassword.Password);

                if(currentUser == null)
                {
                    MessageBox.Show("Неверный логин или пароль", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    IdUser = currentUser.id;
                    windowUser = new WindowUser();
                    windowUser.Show();
                    this.Close();
                }

            }

            
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            UserReg userReg = new UserReg();
            userReg.Show();
            this.Close();
        }
    }
}
