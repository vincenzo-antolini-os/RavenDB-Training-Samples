using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RavenSamples.Common;

namespace RavenSamples.Loader
{
	class Program
	{
		static void Main( string[] args )
		{
			var store = new Raven.Client.Document.DocumentStore()
			{
				Url = "http://localhost:8080/",
				DefaultDatabase = "CGN-Samples",
				Credentials = new NetworkCredential( "RavenUser", "P@ssw0rd" )
			}.Initialize();

			using ( var session = store.OpenSession() )
			{
				session.Store( new Person()
				{
					FirstName = "Mauro",
					LastName = "Servienti",
					BornDate = new DateTime( 1973, 01, 10 )
				} );

				session.Store( new Person()
				{
					FirstName = "Manuel",
					LastName = "Scapolan",
				} );

				session.Store( new Person()
				{
					FirstName = "Marco",
					LastName = "Parenzan",
				} );

				session.Store( new Person()
				{
					FirstName = "Andrea",
					LastName = "Saltarello",
				} );

				session.Store( new Company()
				{
					CompanyName = "Managed Designs S.r.l.",
					OpeningDate = new DateTime( 2004, 03, 22 )
				} );

				session.Store( new Company()
				{
					CompanyName = "CGN"
				} );

				session.SaveChanges();
			}
		}
	}
}
