using System.Runtime.InteropServices;
using System;
using System.Windows;
using System.Windows.Interop;

namespace SIMMS.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
		public MainWindow()
        {
            InitializeComponent();
        }

		private void BtnMinimize_Click(object sender, RoutedEventArgs e)
		{
            WindowState = WindowState.Minimized;
        }

		private void BtnMaximize_Click(object sender, RoutedEventArgs e)
		{
			if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
			else this.WindowState = WindowState.Normal;
		}

		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
            Application.Current.Shutdown();
        }

		private void ControlBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			WindowInteropHelper helper = new WindowInteropHelper(this);
			SendMessage(helper.Handle, 161, 2, 0);
		}

		private void ControlBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
		}
	}
}
