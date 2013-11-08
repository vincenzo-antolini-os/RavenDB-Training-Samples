using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace DynamicFields
{
	class Program
	{
		static void Main( string[] args )
		{
			var store = CreateStore();

			#region setup dei dati

			using ( var session = store.OpenSession() )
			{
				session.Store( new Product()
				{
					Name = "Camicia",
					Attributes = new List<Attribute>()
					{
						new Attribute()
						{
							Name = "Taglia",
							Value = new AttributeValue<int>()
							{ 
								Value = 40
							}
						},
						new Attribute()
						{
							Name = "Colore",
							Value = new AttributeValue<String>()
							{ 
								Value = "Azzurra"
							}
						}
					}
				} );

				session.Store( new Product()
				{
					Name = "Camicia",
					Attributes = new List<Attribute>()
					{
						new Attribute()
						{
							Name = "Taglia",
							Value = new AttributeValue<int>()
							{ 
								Value = 54
							}
						},
						new Attribute()
						{
							Name = "Colore",
							Value = new AttributeValue<String>()
							{ 
								Value = "Bianca"
							}
						}
					}
				} );

				session.Store( new Product()
				{
					Name = "Camicia",
					Attributes = new List<Attribute>()
					{
						new Attribute()
						{
							Name = "Taglia",
							Value = new AttributeValue<int>()
							{ 
								Value = 40
							}
						},
						new Attribute()
						{
							Name = "Colore",
							Value = new AttributeValue<String>()
							{ 
								Value = "Bianca"
							}
						}
					}
				} );

				session.Store( new Product()
				{
					Name = "Camicia",
					Attributes = new List<Attribute>()
					{
						new Attribute()
						{
							Name = "Taglia",
							Value = new AttributeValue<int>()
							{ 
								Value = 2
							}
						},
						new Attribute()
						{
							Name = "Colore",
							Value = new AttributeValue<String>()
							{ 
								Value = "Bianca"
							}
						}
					}
				} );

				session.Store( new Product()
				{
					Name = "Camicia",
					Attributes = new List<Attribute>()
					{
						new Attribute()
						{
							Name = "Taglia",
							Value = new AttributeValue<int>()
							{ 
								Value = 1
							}
						},
						new Attribute()
						{
							Name = "Colore",
							Value = new AttributeValue<String>()
							{ 
								Value = "Bianca"
							}
						}
					}
				} );

				session.Store( new Product()
				{
					Name = "Camicia",
					Attributes = new List<Attribute>()
					{
						new Attribute()
						{
							Name = "Taglia",
							Value = new AttributeValue<int>()
							{ 
								Value = 10
							}
						},
						new Attribute()
						{
							Name = "Colore",
							Value = new AttributeValue<String>()
							{ 
								Value = "Bianca"
							}
						}
					}
				} );

				session.Store( new Product()
				{
					Name = "Camicia",
					Attributes = new List<Attribute>()
					{
						new Attribute()
						{
							Name = "Taglia",
							Value = new AttributeValue<int>()
							{ 
								Value = 50
							}
						},
						new Attribute()
						{
							Name = "Colore",
							Value = new AttributeValue<String>()
							{ 
								Value = "Bianca"
							}
						}
					}
				} );

				session.SaveChanges();
			}

			#endregion

			//Taglia: [40 TO 44]

			using ( var session = store.OpenSession() )
			{
				var query = session.Advanced.LuceneQuery<Product, Product_Search>()
					.Where( "Taglia: [40 TO 44]" )
					.AddOrder( "Taglia_Range", true, typeof( int ) );

				foreach ( var item in query )
				{
					Console.WriteLine( "Prodotto: {0} -> {1}", item.Name, item.Attributes.Single( a => a.Name == "Taglia" ).Value );
				}
			}

			Console.Read();
		}

		static IDocumentStore CreateStore()
		{
			var store = new DocumentStore()
			{
				Url = "http://localhost:8080",
				DefaultDatabase = "DynamicFields"
			}.Initialize();

			IndexCreation.CreateIndexes( Assembly.GetExecutingAssembly(), store );

			return store;
		}
	}
}
