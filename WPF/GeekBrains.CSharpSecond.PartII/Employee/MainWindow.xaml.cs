using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Employee
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ViewModel viewModel;
		public MainWindow()
		{
			InitializeComponent();
			viewModel = new ViewModel();
			MyGrid.DataContext = viewModel;
		}

		/// <summary>
		/// Зажали мышкой часть формы
		/// </summary>
		private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		/// <summary>
		/// Кликнули на кнопку закрытия
		/// </summary>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}

		/// <summary>
		/// Кликнули на кнопку Add employee
		/// </summary>
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			new EmployeeAddWindow(viewModel).Show();
			this.Visibility				= Visibility.Hidden;
		}

		/// <summary>
		/// Кликнули на кнопку Add Department
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			new DepartmentAddWindow(viewModel).Show();
			this.Visibility				= Visibility.Hidden;
		}

		/// <summary>
		/// Двойной клик на существо из списка
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listEmps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			viewModel.SetIndex(listEmps.SelectedIndex);
			new EmployeeEdit(viewModel).Show();
			this.Visibility				= Visibility.Hidden;
		}
	}
}
