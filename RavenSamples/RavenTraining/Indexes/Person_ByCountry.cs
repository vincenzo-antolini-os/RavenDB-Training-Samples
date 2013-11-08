using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace RavenTraining.Indexes
{
	public class Person_ByCountry : AbstractIndexCreationTask<Person>
	{
		public Person_ByCountry()
		{
			this.Map = docs => from doc in docs
							   let country = this.LoadDocument<Country>( doc.BirthCountry )
							   select new Views.PersonView()
							   {
								   FirstName = doc.FirstName,
								   LastName= doc.LastName,
								   Country = new Views.Country()
								   {
									   Id = country.Id,
									   Name = country.Name
								   }
							   };

			this.Store( "FirstName", FieldStorage.Yes );
			this.Store( "LastName", FieldStorage.Yes );
			this.Store( "Country", FieldStorage.Yes );
		}
	}
}
