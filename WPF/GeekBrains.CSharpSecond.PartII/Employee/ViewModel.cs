using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
	public class ViewModel
	{
		/// <summary>
		/// Модель
		/// </summary>
		private Model _model;

		/// <summary>
		/// Коллекция, готовая к выводу
		/// </summary>
		public ObservableCollection<string> OutCollection => _model.EmpList;

		public ViewModel() {
			_model = new Model();
		}

		#region Methods


		/// <summary>
		/// Передача индекса выбранной сущности
		/// </summary>
		/// <param name="index">Индекс</param>
		public void SetIndex(int index)=> _model.SetIndex(index);

		/// <summary>
		/// Получение индекса выбранной сущности
		/// </summary>
		/// <returns>Индекс</returns>
		public int GetIndex() => _model._indexOfEmployee;

		/// <summary>
		/// Изменение Департамента у выбранной сущности
		/// </summary>
		/// <param name="nameOfDep">Название департамента</param>
		public void Edit(Department nameOfDep)=> _model.ChangeDep(nameOfDep);

		/// <summary>
		/// Добавление сущности в коллекцию сущностей
		/// </summary>
		/// <param name="EmpName">Имя сущности</param>
		/// <param name="DepName">Департамент</param>
		public void AddEmp(string EmpName, Department DepName)=>
			 _model.AddEmployee(new EmployeeClass(EmpName, DepName));

		/// <summary>
		/// Добавление департамента в коллекцию департаментов
		/// </summary>
		/// <param name="DepName">Название департамента</param>
		public void AddDep(string DepName)=> 
			 _model.AddDepartment(new Department(DepName));

		/// <summary>
		/// Получение коллекции сущностей
		/// </summary>
		/// <returns>Коллекция сущностей не готовая к выводу</returns>
		public ObservableCollection<EmployeeClass> GetEmpList() => _model.GetEmp;

		/// <summary>
		/// Получение коллекции департаментов
		/// </summary>
		/// <returns>Коллекция департаментов</returns>
		public ObservableCollection<Department> GetDepList() => _model.GetDep;

		#endregion
	}
}
