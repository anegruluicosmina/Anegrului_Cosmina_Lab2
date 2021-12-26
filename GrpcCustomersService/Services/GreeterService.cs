using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LibraryModel.Models;
using System;
using System.Threading.Tasks;
using DataAccess = LibraryModel.Data;
using ModelAccess = LibraryModel.Models;
public class GrpcCrudService : CustomerServiceBase
{

    private DataAccess.LibraryContext db = null;
    public GrpcCrudService(DataAccess.LibraryContext db)
    {
        this.db = db;
    }
    public override Task<CustomerList> GetAll(Empty empty, ServerCallContext
   context)
    {

        CustomerList pl = new CustomerList();
        var query = from cust in db.Customers
                    select new Customer()
                    {
                        CustomerId = cust.CustomerID,
                        Name = cust.Name,
                        Adress = cust.Adress
                    };
        pl.Item.AddRange(query.ToArray());
        return Task.FromResult(pl);
    }
    public override Task<Empty> Insert(Customer requestData, ServerCallContext context)
    {
        db.Customers.Add(new ModelAccess.Customer
        {
            CustomerID = requestData.CustomerID,
            Name = requestData.Name,
            Adress = requestData.Adress,
            BirthDate = requestData.BirthDate
        });
        db.SaveChanges();
        return Task.FromResult(Empty());
    }
}