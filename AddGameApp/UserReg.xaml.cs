using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AddGameApp
{
    /// <summary>
    /// Логика взаимодействия для UserReg.xaml
    /// </summary>
    public partial class UserReg : Window
    {
        AddGameBDEntities contextBD = new AddGameBDEntities();
        Usesrs usesrs = new Usesrs();


        public UserReg()
        {
            InitializeComponent();
            DataContext = usesrs;

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            if (TxtFirstName.Text == "" || TxtlastName.Text == "" || TxtEmail.Text == "" || TxtLogin.Text == "" || TxtPassword.Text == "")
            {
                MessageBox.Show("Заполните обязательные поля!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var log = contextBD.Usesrs.FirstOrDefault(x => x.login == TxtLogin.Text);


            if(log != null)
            {
                MessageBox.Show("Такой логин уже существует!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(TxtLogin.Text.Length < 4)
            {
                MessageBox.Show("Логин должен содержать не менее 4 символов!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(TxtPassword.Text.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(TxtPassword.Text != TxtRepeatPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            


            string userName = TxtEmail.Text;
            Regex regex = new Regex(@"^([a-zA-Z0-9_\.]+)@(gmail.com|mail.ru)$");
            bool isValid = regex.IsMatch(userName);

            if(isValid == false)
            {
                MessageBox.Show("Неверный формат почты!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var BDemail = contextBD.Usesrs.FirstOrDefault(x => x.email == TxtEmail.Text);

            if(BDemail != null)
            {
                MessageBox.Show("Введенная почта уже используется!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (usesrs.id == 0)
                contextBD.Usesrs.Add(usesrs);

            try
            {
                contextBD.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно", "Поздравляем!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
