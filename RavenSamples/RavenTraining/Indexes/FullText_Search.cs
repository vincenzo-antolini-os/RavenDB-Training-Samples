using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Indexes;

namespace RavenTraining.Indexes
{
	public class FullText_Search : AbstractMultiMapIndexCreationTask<FullText_Search.SearchResult>
	{
		public class SearchMap
		{
			public Object[] Content { get; private set; }
		}

		public class SearchResult
		{
			public String Id { get; set; }

			public String Collection { get; set; }
			public String DisplayName { get; private set; }
		}

		public FullText_Search()
		{
			this.AddMap<Person>( persons => from p in persons
											select new
											{
												Content = new[] 
												{
													p.FirstName,
													p.LastName
												},
												DisplayName = p.FirstName + " " + p.LastName,
												Collection = this.MetadataFor( p )[ "Raven-Entity-Name" ],
												Id = p.Id
											} );

			this.AddMap<Company>( companies => from c in companies
											   select new
											   {
												   Content = new[] 
												{
													c.CompanyName
												},
												   DisplayName = c.CompanyName,
												   Collection = this.MetadataFor( c )[ "Raven-Entity-Name" ],
												   Id = c.Id
											   } );
		}
	}
}
