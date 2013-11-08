using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Raven.Abstractions.Data;
using Raven.Json.Linq;
using Raven.Client.Linq;
using Raven.Client;
using RavenSamples.Common;
using Raven.Client.Indexes;

namespace RavenSamples
{
	class Program
	{
		static void Main( string[] args )
		{
			//Uri uri = new Uri( "http://localhost:8080/" );
			//ICredentials credentials = CredentialCache.DefaultCredentials;
			//NetworkCredential credential = credentials.GetCredential( uri, "Basic" );

			var store = new Raven.Client.Document.DocumentStore()
			{
				Url = "http://localhost:8080/",
				DefaultDatabase = "Samples",
				//Credentials = credential
			}.Initialize();

			//store.Changes()
			//	.ForDocumentsStartingWith( "orders/" );

			//store.Changes()
			//	.ForIndex( "myIndex/Name" );

			using ( var session = store.OpenSession() )
			{
				var p = new Person() { FirstName = "Someone", LastName = "Foo" };
				session.Store( p );
				session.SaveChanges();
			}

			using ( var session = store.OpenSession() )
			{
				var query = session.Query<Person>()
					.Where( p => p.FirstName.StartsWith( "S" ) )
					.ToList();
			}

			//var dbCmd = store.DatabaseCommands;
			//dbCmd.Patch( "orders/2", new[] { new PatchRequest
			//{
			//	Type = PatchCommandType.Set,
			//	Name = "Customer",
			//	Value = RavenJToken.Parse( "new customer name" )
			//} } );
		}
	}

	class My_Index : AbstractIndexCreationTask<Person> 
	{
		public My_Index()
		{
			this.Map = docs => from doc in docs
							   select new
							   {
								   doc.FirstName,
								   doc.LastName
							   };
		}
	}
}