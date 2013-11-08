using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Indexes;

namespace ConsoleApplication1
{
	public class Products_Search : AbstractIndexCreationTask<Orders.Product>
	{
		public Products_Search()
		{
			this.Map = docs => from doc in docs
							   select new
							   {
								   doc.Name,
								   doc.Discontinued,
								   doc.Supplier,
								   doc.PricePerUser
							   };
		}
	}
}
