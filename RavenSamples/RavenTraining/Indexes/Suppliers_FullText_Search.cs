//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Raven.Client.Indexes;

//namespace RavenTraining.Indexes
//{
//	public class Suppliers_FullText_Search : AbstractIndexCreationTask<Orders.Supplier>
//	{
//		public Suppliers_FullText_Search()
//		{
//			this.Map = docs => from doc in docs
//							   select new
//							   {
//								   Content = new[]
//								   {
//									   doc.Name, 
//									   doc.Contact.Name,
//									   doc.Address.Line1.Replace("\r\n", " ")
//								   }
//							   };

//			this.Store( "Content", Raven.Abstractions.Indexing.FieldStorage.Yes );
//		}
//	}
//}
