using Microsoft.AspNetCore.Mvc;
using Models;

[Route("customers")]
[ApiController]
public class CustomerController : ControllerBase
{

    [HttpGet("GetAllCustomer")]
    public ActionResult<List<Customer>> GetAllCustomer()
    {
        List<Customer> CustomerList = Customer.customerList;

        return Ok(CustomerList);
    }


    //Get by ID: retrive a single Customer by id
    [HttpGet("GetById/{id}")]

    public ActionResult<Customer> GetById(int id) //retrive specific Customer by ID in parenthesis -> pass number and retrive specific Customer
    {
        Customer Customer = Customer.FindCustomerById(id);

        return Customer != null ? Ok(Customer) : NotFound(); //if it not null return Customer OK esle NotFound
    }

    [HttpPost("CreateCustomer")]

    public ActionResult<Customer> Create(Customer CreateCustomer)
    {
        Customer Customer = new Customer();

        Customer.InputValidation(CreateCustomer);

        Customer.Name = CreateCustomer.Name;
        Customer.Email = CreateCustomer.Email;
        Customer.UserName = CreateCustomer.UserName;
        Customer.Password = CreateCustomer.Password;
        Customer.RoleId = CreateCustomer.RoleId;

        Customer.Id = Customer.customerList.Count + 1;

        Customer.customerList.Add(Customer);

        return Ok(Customer);
    }

    [HttpPut("UpdateCustomer/{id}")]
    public ActionResult<Customer> Update(int id, Customer updateCustomer) //retrive specific Customer by ID in parenthesis -> pass number and retrive specific Customer
    {
        Customer Customer = new Customer();

        Customer = Customer.customerList[id];

        if (Customer != null)
        {
            Customer.Name = updateCustomer.Name;
            Customer.UserName = updateCustomer.UserName;
            Customer.Password = updateCustomer.Password;
            Customer.RoleId = updateCustomer.RoleId;
            Customer.Email = updateCustomer.Email;
        }
        else
        {
            return NotFound();
        }

        return Ok(Customer);
    }

    [HttpDelete("DeleteCustomer/{id}")]
    public ActionResult Delete(int id) //retrive specific Customer by ID in parenthesis -> pass number and retrive specific Customer
    {
        Customer Customer = Customer.FindCustomerById(id);
        
        if (Customer == null) 
        {
            return NotFound();
        }
        
        Customer.customerList.Remove(Customer);

        return NoContent();
    }
}
