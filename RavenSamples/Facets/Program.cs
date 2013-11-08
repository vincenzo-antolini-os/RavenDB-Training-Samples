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

namespace ConsoleApplication1
{
	class Program
	{
		static void Main( string[] args )
		{
			var store = CreateStore();
			SetupFacets( "my/facet", store );

			using ( var session = store.OpenSession() )
			{
				var query = session.Query<Orders.Product, Products_Search>()
					.Where( p => !p.Discontinued );

				var results = query.ToFacets( "my/facet" );

				foreach ( var r in results.Results )
				{
					Console.WriteLine( r.Key );
					foreach ( var i in r.Value.Values )
					{
						Console.WriteLine( "\t" + i.Range + "\t-> " + i.Hits );
					}

					//Console.WriteLine( "{0}: {1}", r.Key, r.Value );
				}
			}

			Console.Read();
		}

		private static void SetupFacets( String id, IDocumentStore store )
		{
			var facet = new FacetSetup()
			{
				Id = id,
				Facets = 
				{
					new Facet<Orders.Product>()
					{
						Name = p=>p.Supplier
					},
					new Facet<Orders.Product>()
					{
						Name = p=>p.PricePerUser,
						Ranges = 
						{
							p=>p.PricePerUser <= 50,
							p=>p.PricePerUser > 50 && p.PricePerUser <= 100,
							p=>p.PricePerUser > 100 && p.PricePerUser <= 200,
							p=>p.PricePerUser > 200,
						}
					},
				}
			};

			using ( var session = store.OpenSession() )
			{
				session.Store( facet );
				session.SaveChanges();
			}
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
