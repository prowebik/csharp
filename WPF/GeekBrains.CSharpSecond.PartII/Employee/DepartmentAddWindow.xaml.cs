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

namespace Employee
{
	/// <summary>
	/// Логика взаимодействия для DepartmentAddWindow.xaml
	/// </summary>
	public partial class DepartmentAddWindow : Window
	{
		MainWindow MW;
		private ViewModel viewModel;

		public DepartmentAddWindow()
		{
			InitializeComponent();
		}

		public DepartmentAddWindow(ViewModel viewModel)
		{
			InitializeComponent();
			this.viewModel = viewModel;
			this.MW = Application.Current.Windows
				.Cast<Window>()
				.FirstOrDefault(window => window is MainWindow) as MainWindow;
		}

		/// <summary>
		/// Зажали мышкой часть формы
		/// </summary>
		private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		/// <summary>
		/// Кликнули на кнoпку Close
		/// </summary>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
			MW.ShowDialog();
		}

		/// <summary>
		/// Кликнули на кнопку Done
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			viewModel.AddDep(nameBox.Text);
			this.Close();
			MW.ShowDialog();
		}
	}
}
