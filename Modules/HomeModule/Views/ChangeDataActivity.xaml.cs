using Prism.Events;
using SIMMS.Models;
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
using System.Windows.Shapes;

namespace SIMMS.Modules.HomeModule.Views
{
	/// <summary>
	/// Lógica de interacción para ChangeDataActivity.xaml
	/// </summary>
	public partial class ChangeDataActivity : Window
	{
		IEventAggregator _ea;
		public ChangeDataActivity()
		{
			InitializeComponent();
			
		}

		private void BtnMinimize_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
        }

		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }
}
