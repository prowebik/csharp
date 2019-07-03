﻿using System;
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
using System.Windows.Shapes;

namespace Employee
{
	/// <summary>
	/// Логика взаимодействия для EmployeeAddWindow.xaml
	/// </summary>
	public partial class EmployeeAddWindow : Window
	{
		private ViewModel viewModel;
		private MainWindow MW;

		public EmployeeAddWindow()
		{
			InitializeComponent();
		}

		public EmployeeAddWindow(ViewModel viewModel)
		{
			InitializeComponent();
			this.viewModel = viewModel;
			this.MW = Application.Current.Windows
				.Cast<Window>()
				.FirstOrDefault(window => window is MainWindow) as MainWindow;
			LoadData();
		}

		/// <summary>
		/// Заполнение формы необходимой информацией
		/// </summary>
		private void LoadData()
		{
			depList.ItemsSource = viewModel.GetDepList();
			depList.SelectedIndex = 0;
		}

		/// <summary>
		/// Зажали мышкой часть формы
		/// </summary>
		private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		/// <summary>
		/// Кликнули на кнопку Close
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
			viewModel.AddEmp(nameBox.Text, viewModel.GetDepList()[depList.SelectedIndex]);
			this.Close();
			MW.ShowDialog();
		}
	}
}
