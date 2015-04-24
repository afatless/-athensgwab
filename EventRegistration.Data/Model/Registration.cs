using System;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;

namespace EventRegistration.Data.Model
{
	public class Registration : TableEntity
	{
		public Registration()
		{
			PartitionKey = string.Format("Registrations-{0}", DateTime.Now.Year);
			RowKey = Guid.NewGuid().ToString().Split('-').First();
		}

		public string TicketType { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public int ShirtSize { get; set; }
		public bool AllowCommunication { get; set; }
		public string Comments { get; set; }
	}
}
