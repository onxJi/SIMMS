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

namespace SIMMS.ControlStyles
{
    /// <summary>
    /// Lógica de interacción para TextBoxPasswordRounded.xaml
    /// </summary>
    public partial class TextBoxPasswordRounded : UserControl
    {
        public static readonly DependencyProperty PassProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(TextBoxPasswordRounded));
        public string Password
        {
            get
            {
                return (string)GetValue(PassProperty);
            }
            set { SetValue(PassProperty, value); }
        }
        public TextBoxPasswordRounded()
        {
            InitializeComponent();
            tb_password.PasswordChanged += OnPassChanged;
        }

        private void OnPassChanged(object sender, RoutedEventArgs e)
        {
            Password = tb_password.Password;
        }
        
    }
}
