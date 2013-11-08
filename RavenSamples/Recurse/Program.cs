using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Recurse
{
	class Program
	{
		static void Main( string[] args )
		{
		}

		static IDocumentStore CreateStore()
		{
			var store = new DocumentStore()
			{
				Url = "http://localhost:8080",
				DefaultDatabase = "Recurse"
			}.Initialize();

			IndexCreation.CreateIndexes( Assembly.GetExecutingAssembly(), store );

			return store;
		}
	}
}
