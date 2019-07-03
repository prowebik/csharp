using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
	public class Model : INotifyPropertyChanged
	{
		#region Data


		/// <summary>
		/// Коллекция существ
		/// </summary>
		private ObservableCollection<EmployeeClass> Employees;

		public ObservableCollection<EmployeeClass> GetEmp { get => Employees; }

		/// <summary>
		/// Коллекция департаментов
		/// </summary>
		private ObservableCollection<Department> Deps;

		public ObservableCollection<Department> GetDep { get => Deps; }

		/// <summary>
		/// Коллекция сущностей готовых к отображению
		/// </summary>
		private ObservableCollection<string> empList;

		public ObservableCollection<string> EmpList
		{
			get => empList;
			set
			{
				this.empList = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.EmpList)));
			}
		}

		/// <summary>
		/// Позиция выбранного сотрудника
		/// </summary>
		public int _indexOfEmployee;
		private string sql;
		private SqlConnectionStringBuilder connectionStringBuilder;

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Methods

		public Model()
		{
			empList					= new ObservableCollection<string>();
			EmpList					= new ObservableCollection<string>();
			Deps					= new ObservableCollection<Department>();
			Employees				= new ObservableCollection<EmployeeClass>();
			connectionStringBuilder = new SqlConnectionStringBuilder
			{
				DataSource		    = @"(localdb)\MSSQLLocalDB",
				InitialCatalog	    = "Lesson7",
				IntegratedSecurity  = true,
				Pooling			    = true
			};
			LoadData();
		}

		/// <summary>
		/// Добавление нового существа в коллекцию
		/// </summary>
		/// <param name="_name">Имя</param>
		/// <param name="_dep">Департамент</param>
		public void AddEmployee(EmployeeClass x)
		{
			this.Employees.Add(x);
			EmpList.Add(x.cell());
			DBActions(dynEmp:x);
		}

		/// <summary>
		/// Добавление нового существа в коллекцию на указанную позицию
		/// </summary>
		/// <param name="x">Существо</param>
		public void AddEmployeeAt(EmployeeClass x)
		{
			this.Employees.Insert(_indexOfEmployee, x);
			EmpList.Insert(_indexOfEmployee, x.cell());
		}

		/// <summary>
		/// Добавление нового департамента в коллекцию департаментов
		/// </summary>
		/// <param name="_dep"></param>
		public void AddDepartment(Department _dep)
		{
			Deps.Add(_dep);
			DBActions(cmd: "insertDep", _sentDep: _dep.Dep);
		}

		/// <summary>
		/// Удаление существа из коллекции по позиции
		/// </summary>
		/// <param name="_index">Позиция существа</param>
		public void DelEmployeeAt(int _index)
		{
			#region Delete from DB

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
				{
					connection.Open();
					sql = $@"DELETE FROM People WHERE FIO = '{Employees[_index].FIO}'";
					SqlCommand command = new SqlCommand(sql, connection);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				Console.WriteLine("Employee deleting is complete!");
			}

			#endregion

			this.Employees.RemoveAt(_index);
			this.EmpList.RemoveAt(_index);

		}

		/// <summary>
		/// Удаление данного существа из коллекции
		/// </summary>
		/// <param name="emp">Существо</param>
		public void DelEmployee(EmployeeClass emp)
		{
			this.Employees.Remove(emp);
			this.EmpList.Remove(emp.cell());

			#region Delete from DB

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
				{
					connection.Open();
					sql = $@"DELETE FROM People WHERE FIO = '{emp?.FIO}'";
					SqlCommand command = new SqlCommand(sql, connection);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				Console.WriteLine("Employee deleting is complete!");
			}

			#endregion
		}

		/// <summary>
		/// Заполнение начальными данными коллекции
		/// </summary>
		public void LoadData()
		{
			#region Departments loading

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
				{
					connection.Open();
					sql = @"SELECT * FROM Department";
					SqlCommand command = new SqlCommand(sql, connection);
					SqlDataReader sqlData = command.ExecuteReader();
					while (sqlData.Read())
					{
						Department dynDep = new Department($"{sqlData["Department"]}");
						Deps.Add(dynDep);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				Console.WriteLine("Departments loading complete!");
			}

			#endregion

			#region Employees loading

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
				{
					connection.Open();
					sql = @"SELECT * FROM People";
					SqlCommand command = new SqlCommand(sql, connection);
					SqlDataReader sqlData = command.ExecuteReader();
					while (sqlData.Read())
					{
						EmployeeClass dynEmp = new EmployeeClass($"{sqlData["FIO"]}", new Department($"{sqlData["Department"]}"));
						Employees.Add(dynEmp);
						empList.Add(dynEmp.cell());
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				Console.WriteLine("Employees loading complete!");
			}

			#endregion
		}

		/// <summary>
		/// Изменение департамента
		/// </summary>
		/// <param name="_dep">Департамент, на который заменяем</param>
		public void ChangeDep(Department _dep)
		{
			Employees[_indexOfEmployee].department = _dep;
			AddEmployeeAt(Employees[_indexOfEmployee]);
			DelEmployeeAt(_indexOfEmployee + 1);
			DBActions("change", Employees[_indexOfEmployee]);
		}

		/// <summary>
		/// Установка индекса для изменения департамента
		/// </summary>
		/// <param name="_index">Индекс</param>
		public void SetIndex(int _index) => _indexOfEmployee = _index;

		/// <summary>
		/// Взаимодействие с базой данных
		/// </summary>
		/// <param name="cmd">Команда ( change/insertEmp/insertDep )</param>
		/// <param name="dynEmp">Сущность</param>
		/// <param name="_sentDep">Департамент</param>
		private void DBActions(string cmd= "insertEmp", EmployeeClass dynEmp=null, string _sentDep="")
		{
			#region INSERT

			if (cmd == "insertEmp")
			{
				try
				{

					using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
					{
						connection.Open();

						sql = $@"INSERT INTO People (FIO, Department) output 
									 INSERTED.ID VALUES 
									 ('{dynEmp?.FIO}',
									  '{dynEmp?.department.Dep}' );";
						SqlCommand command = new SqlCommand(sql, connection);
						command.ExecuteNonQuery();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			if (cmd == "insertDep")
			{
				try
				{

					using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
					{
						connection.Open();

						sql = $@"INSERT INTO Department (Department) output 
									 INSERTED.ID VALUES 
									 ('{_sentDep}');";
						SqlCommand command = new SqlCommand(sql, connection);
						command.ExecuteNonQuery();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			#endregion

			#region CHANGE

			if (cmd == "change")
			{
				try
				{

					using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
					{
						connection.Open();

						sql = $@"UPDATE People SET
									 Department = '{dynEmp?.department.Dep}' 
									 WHERE FIO = '{dynEmp?.FIO}'";
						SqlCommand command = new SqlCommand(sql, connection);
						command.ExecuteNonQuery();

					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			#endregion 
			Console.WriteLine("Done!");
		}

		#endregion

	}
}
