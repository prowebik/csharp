using System.ComponentModel;

namespace Employee
{
	public class Department
	{
		private string department;

		/// <summary>
		/// Название департамента
		/// </summary>
		public string Dep
		{
			get => this.department;
			set
			{
				this.department = value;
			}
		}

		public Department(string _title)
		{
			Dep = _title;
		}

		public override string ToString()
		{
			return Dep;
		}
	}
}
