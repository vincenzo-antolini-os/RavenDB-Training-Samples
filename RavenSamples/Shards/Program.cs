using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Shard;

namespace Shards
{
	class Program
	{
		static void Main( string[] args )
		{
			var store = CreateStore();

			using ( var session = store.OpenSession() )
			{
				session.Store( new Person()
				{
					FirstName = "mauro",
					LastName = "servienti"
				} );
				session.Store( new Person()
				{
					FirstName = "alessandro",
					LastName = "melchiori"
				} );

				session.Store( new Order()
				{
					Country = "italy"
				} );

				session.Store( new Order()
				{
					Country = "italy"
				} );

				session.Store( new Order()
				{
					Country = "italy"
				} );

				session.Store( new Order()
				{
					Country = "germany"
				} );

				session.Store( new Order()
				{
					Country = "USA"
				} );

				session.SaveChanges();
			}
		}

		static IDocumentStore CreateStore()
		{
			var s1 = new DocumentStore()
			{
				Url = "http://localhost:8080",
				DefaultDatabase = "S1"
			}.Initialize();

			var s2 = new DocumentStore()
			{
				Url = "http://localhost:8081",
				DefaultDatabase = "S2"
			}.Initialize();

			var strategy = new ShardStrategy( new Dictionary<String, IDocumentStore>() 
			{
				{"S1", s1},
				{"S2", s2}
			} );

			strategy.ShardingOn<Order>( o => o.Country, c =>
			{
				if ( c.Equals( "italy", StringComparison.OrdinalIgnoreCase ) )
				{
					return "S1";
				}

				return "S2";
			} );

			strategy.ShardingOn<Person>();

			var store = new ShardedDocumentStore( strategy )
			{

			}.Initialize();

			IndexCreation.CreateIndexes( Assembly.GetExecutingAssembly(), store );

			return store;
		}
	}
}
