using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Indexes;
using Raven.Client;

namespace RavenTraining
{
	class Sample { public string Foo { get; set; } }

	class Program
	{
		static void Main( string[] args )
		{
			var store = new Raven.Client.Document.DocumentStore()
			{
				Url = "http://localhost:8080/",
				DefaultDatabase = "Samples"
			}.Initialize();

			//IndexCreation.CreateIndexes( Assembly.GetExecutingAssembly(), store );

			//var id = "";

			//using ( var session = store.OpenSession() )
			//{
			//	var person = new Person()
			//	{
			//		FirstName = "Omar",
			//		LastName = "Damiani"
			//	};

			//	session.Store( person );
			//	id = person.Id;

			//	session.SaveChanges();
			//}

			using ( var session = store.OpenSession() )
			{
				session.Store( new
				{
					Id = "settings/sample",
					Settings = new Dictionary<String, Object>()
					{
						{ "kkk", new Sample(){ Foo = "bar" } },
						{ "yyy", 12 }
					}
				} );

				session.SaveChanges();
			}

			using ( var session = store.OpenSession() )
			{
				var t = session.Load<dynamic>( "settings/sample");
			}

			//using ( var session = store.OpenSession() )
			//{
			//	//session.Query<Person>()
			//	//	.Customize( c => 
			//	//	{
			//	//		c.WaitForNonStaleResults();
			//	//	} )
			//	//	.Where( p=> p.Id == id )
			//	//	.Single();

			//	//var data = session.Query<Person, Indexes.Person_ByCountry>()
			//	//	.Where( p => p.FirstName.StartsWith( "l*" ) )
			//	//	.ProjectFromIndexFieldsInto<Views.PersonView>();

			//	//var results = data.ToList();



			//	var query = session.Query<Orders.Employee>()
			//		.Where( e => e.FirstName.StartsWith( "A" ) );

			//	foreach ( var emp in query )
			//	{
			//		Console.WriteLine( emp.FirstName );
			//	}

			//	//session.Query<Orders.Employee>()
			//	//	.Where( e => e.LastName.EndsWith( "a" ) )
			//	//	.ToList().ForEach( i =>
			//	//	{
			//	//		Console.WriteLine( i.LastName );
			//	//	} );
			//}
		}

		//	static void Main( string[] args )
		//	{
		//		//Uri uri = new Uri( "http://localhost:8080/" );
		//		//ICredentials credentials = CredentialCache.DefaultCredentials;
		//		//NetworkCredential credential = credentials.GetCredential( uri, "Basic" );

		//		var store = new Raven.Client.Document.DocumentStore()
		//		{
		//			Url = "http://localhost:8080/",
		//			DefaultDatabase = "Sample",
		//			//Credentials = credential
		//		}.Initialize();

		//		//var original = store.Conventions.FindClrType;
		//		//store.Conventions.FindClrType = ( c, doc, meta ) =>
		//		//{
		//		//	if ( c == "Countries" )
		//		//	{
		//		//		return typeof( Country ).AssemblyQualifiedName;
		//		//	}

		//		//	return original( c, doc, meta );
		//		//};

		//		using ( var session = store.OpenSession() )
		//		{
		//			//var data = session
		//			//	.Include<Person>( p => p.BirthCountry )
		//			//	.Load<Person>( "people/1" );
		//			//var country = session.Load<Country>( data.BirthCountry );
		//			//var country2 = session.Load<Country>( "countries/2" );

		//			var p = new Person() 
		//			{
		//				FirstName = "Liborio Igor",
		//				LastName = "Damiani"
		//			};

		//			session.Store( p );
		//			session.SaveChanges();
		//		}
		//	}
		//}
	}

	public class Person
	{
		public String Id { get; private set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string BirthCountry { get; set; }
	}

	public class Company
	{
		public String Id { get; private set; }

		public string CompanyName { get; set; }
	}

	public class Country
	{
		public String Id { get; internal set; }

		public string Name { get; internal set; }
	}
}
