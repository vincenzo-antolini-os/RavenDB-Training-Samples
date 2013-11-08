using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenSamples.Common
{
	public class Company
	{
		public Company()
		{
			this.Id = "companies/";
		}

		public String Id { get; private set; }

		public string CompanyName { get; set; }

		public DateTime? OpeningDate { get; set; }

		public override string ToString()
		{
			return base.ToString() + ": " + this.CompanyName;
		}
	}
}
