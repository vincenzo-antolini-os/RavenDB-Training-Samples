using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Boost
{
	class Program
	{
		static void Main( string[] args )
		{
			var store = CreateStore();
			using ( var session = store.OpenSession() )
			{
				var data = session.Query<Orders.Category>( "Categories/Search" )
					.Search( c => c.Description, "pr*", boost: 5, escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard )
					.Search( c => c.Name, "pr*", boost: 10, escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard )
					.OrderByScore()
					.ToList();

				foreach ( var item in data )
				{
					Console.WriteLine( "{0}: {1}", item.Name, item.Description );
				}
			}

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
