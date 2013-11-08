using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenSamples.Common
{
	public class Person
	{
		public Person()
		{
			
		}

		public String Id { get; private set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime? BornDate { get; set; }

		public override string ToString()
		{
			return base.ToString() +": " + this.FirstName + " " + this.LastName;
		}
	}
}
