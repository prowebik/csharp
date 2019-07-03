using System.Windows;

namespace Employee
{
	public partial class EmployeeClass : Window
	{
		public string FIO;
		public Department department; 
		public EmployeeClass(string _fio, Department _dep)
		{
			this.FIO		   = _fio;
			this.department = _dep;
		}
		public string cell() => $"{FIO}, works with\n  '{this.department.ToString().ToUpper()}'";
	}
}
