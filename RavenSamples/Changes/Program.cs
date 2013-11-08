using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Changes
{
	class Program
	{
		static void Main( string[] args )
		{
			var store = CreateStore();
			store.Changes()
				.ForAllDocuments()
				.Subscribe( change => 
				{
					Console.WriteLine("Document '{0}' changed, change type is {1}", change.Id, change.Type);
				} );

			//using ( var foo = store.AggressivelyCacheFor( TimeSpan.FromMinutes( 10 ) ) ) 
			//{
			using ( var session = store.OpenSession() )
			{
				session.Query<Object>()
					.Customize( c =>
					{
						c.NoTracking();
					} );
			}
			//}

			

			Console.Read();
		}

		static IDocumentStore CreateStore()
		{
			var store = new DocumentStore()
			{
				Url = "http://localhost:8080",
				DefaultDatabase = "Northwind"
			}.Initialize();

			IndexCreation.CreateIndexes( Assembly.GetExecutingAssembly(), store );

			return store;
		}
	}
}
