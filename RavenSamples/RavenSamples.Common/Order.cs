using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenSamples.Common
{
	public class Order
	{
		public Order()
		{
			this.Id = "orders/";
		}

		public String Id { get; private set; }
		public String Customer { get; set; }
		public IEnumerable<OrderItem> Items { get; private set; }

		public override string ToString()
		{
			return base.ToString() + ": " + this.Id;
		}
	}

	public class OrderItem
	{
		public String Description { get; set; }
		public Int32 Quantity { get; set; }
		public Decimal UnitPrice { get; set; }
	}
}
