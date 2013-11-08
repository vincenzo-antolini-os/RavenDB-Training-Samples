using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenTraining.Views
{
	public class PersonView
	{
		public string FirstName { get; internal set; }

		public string LastName { get; internal set; }

		public Country Country { get; internal set; }
	}

	public class Country 
	{
		public String Id { get; internal set; }
		public String Name { get; internal set; }
	}
}
