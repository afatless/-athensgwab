using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventRegistration.Data.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace EventRegistration.Data
{
    public class RegistrationRepository
    {
		private const string TableName = "messagetable";
		private readonly CloudTable _table;

	    public RegistrationRepository(string connectionString)
	    {
			var storageAccount = CloudStorageAccount.Parse(connectionString);
			var tableClient = storageAccount.CreateCloudTableClient();
			_table = tableClient.GetTableReference(TableName);
			_table.CreateIfNotExists();		    
	    }

	    public async Task<Registration> AddRegistrationAsync(Registration registration)
	    {
			var response = await _table.ExecuteAsync(TableOperation.InsertOrReplace(registration));

		    if (response.HttpStatusCode == 200)
		    {
			    return registration;
		    }

		    return null;
	    }

	    public int GetTotalRegistrations()
	    {
		    var query = new TableQuery<Registration>();
				
			var filter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,
			    string.Format("Registrations-{0}", DateTime.Now.Year));
		    
			var result = _table.ExecuteQuery(query.Where(filter));

		    return result.Count();
	    }
    }
}
