using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;
using Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthentication(); //Authenticatin service
builder.Services.AddAuthorization();  //Authorization service

var app = builder.Build();

app.UseAuthentication(); //use feature that we built earlier 
app.UseAuthorization();  //use feature that we built earlier
app.MapControllers();

//run conditionally use COPILOT to find this: context.Request.Path.StartsWithSegments("/customers"),
app.UseWhen(
    context => context.Request.Method == "POST" && context.Request.Path.StartsWithSegments("/customers/CreateCustomer"),
    //if this was not a GET method, we are going to call this code that happens in here
    appBuilder => appBuilder.Use(async (context, next) => {
        Customer customer = new Customer();

        string username = context.Request.Headers["username"];
        string password = context.Request.Headers["password"];

        customer = Customer.FindCustomerByUsername(username);

        if (customer != null)
        {
            if (password == customer.Password)
            {
                await next.Invoke();
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid password");
            }
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid username");
        }
    })
);

//run conditionally
app.UseWhen(
    //context.Request.Path.StartsWithSegments("/books") -> FIND WITH COPILOT
    context => context.Request.Method == "GET" && context.Request.Path.StartsWithSegments("/customers"),
    //if this was not a GET method, we are going to call this code that happens in here
    appBuilder => appBuilder.Use(async (context, next) => {

        string username = context.Request.Headers["username"];
        string password = context.Request.Headers["password"];

        Customer customer = Customer.FindCustomerByUsername(username);

        if (customer != null)
        {
            if (password == customer.Password)
            {
                await next.Invoke();
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid password");
            }
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid username");
        }
    })
);

//run conditionally
app.UseWhen(
    //context.Request.Path.StartsWithSegments("/books") -> FIND WITH COPILOT
    context => context.Request.Method == "POST" && context.Request.Path.StartsWithSegments("/books"),
    //if this was not a GET method, we are going to call this code that happens in here
    appBuilder => appBuilder.Use(async (context, next) => {

        string username = context.Request.Headers["username"];
        string password = context.Request.Headers["password"];

        Customer customer = Customer.FindCustomerByUsername(username);

        if (customer != null && customer.RoleId == 1)
        {
            if (password == customer.Password)
            {
                await next.Invoke();
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid password");
            }
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid username and role");
        }
    })
);

app.Use(
    async (context, next) => 
    {
        BookController controller = new BookController();

        List<Book> bookList = controller.GetAllBook();

        foreach (Book book in bookList)
        {
            Console.WriteLine(book.Title);
        }

        await next();
    });

app.Run();