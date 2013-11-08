//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Raven.Imports.Newtonsoft.Json;

//namespace RavenSamples
//{
//	class Mauro
//	{
//		public String Id { get; private set; }

//		public Inner InnerProperty { get; set; }

//		//[JsonConstructor]
//		public Mauro()
//		{
//			this.Id = "mauros/";
//		}

//		[JsonProperty(PropertyName = "Name")]
//		public String CompanyName { get; set; }
//	}

//	class Inner
//	{
//		public String Id { get; private set; }

//		public Inner()
//		{
//			this.Id = "inners/";
//		}
//	}
//}
