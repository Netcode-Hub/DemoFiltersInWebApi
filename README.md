# Master .NET Web API🌠: Boost Your Coding Efficiency with Controller Filters – A Complete Guide🔥
 We’re diving into an essential topic for anyone working with .NET Web API – Controller Filters. Whether you're a seasoned developer or just starting out, understanding how to effectively use filters can save you time and make your code cleaner and more maintainable.
![Screenshot 2024-06-22 160953](https://github.com/Netcode-Hub/DemoFiltersInWebApi/assets/110794348/e1a7583e-f6e7-4ded-bf8d-bd3f5e139cd0)

# What Are Controller Filters?
Controller filters in .NET Web API are a powerful feature that allows you to inject logic before or after the execution of an action method. Think of them as the gatekeepers of your API endpoints, handling tasks like logging, authentication, and error handling, without cluttering your controller actions.

# Types of Filters
1. Authorization Filters: These run before the action method and are used to determine if the current user is authorized to execute the action.
2. Action Filters: These can run both before and after the action method, perfect for logging or modifying the result.
3. Exception Filters: These handle exceptions thrown by action methods, providing a centralized error handling mechanism.
4. Result Filters: These run before and after the action results are executed, ideal for modifying the response.

# Use Authorize filter Globally
    builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new AuthorizeFilter());
    });

# Use Authorize filter Locally
    [HttpGet(Name = "GetWeatherForecast")]
    [Authorize]
    public  IActionResult Get()
    {
        return Ok( Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray());
    }

  # Create Custom authorization Filter
       public class CheckAuthFilter : IActionFilter
     {
         bool State;
         public void OnActionExecuted(ActionExecutedContext context)
         {
             context.HttpContext.Response.Headers.Append("AuthenticationState", State.ToString());
         }
    
         public void OnActionExecuting(ActionExecutingContext context)
         {
             var auth = context.HttpContext.Request.Headers.Authorization;
             if (auth.Count != 0)
             {
                 context.HttpContext.Request.Headers["AuthenticationState"] = "true";
                 State = true;
             }
             else
             {
                 context.HttpContext.Request.Headers["AuthenticationState"] = "false";
                 State = false;
             }
         }
     }

  # Apply the Filter Globally
    builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new CheckAuthFilter());
    });

  # Apply the Filter in the Controller
       [HttpGet(Name = "GetWeatherForecast")]
     public  IActionResult Get()
     {
         if (!CheckAuthState()) return Unauthorized();
    
         return Ok( Enumerable.Range(1, 5).Select(index => new WeatherForecast
         {
             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
             TemperatureC = Random.Shared.Next(-20, 55),
             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
         })
         .ToArray());
     }
     private bool CheckAuthState()
     {
         var authState = HttpContext
                         .Request
                         .Headers["AuthenticationState"]
                         .FirstOrDefault();
         return Convert.ToBoolean(authState);
     }

# Apply Filter Locally
    [HttpGet("data")]
    [ServiceFilter(typeof(CheckAuthFilter))]
    public IActionResult GetData()
    {
        if (!CheckAuthState()) return Unauthorized();
        return Ok("New data");
    }

  # Register the Filter
    builder.Services.AddScoped<CheckAuthFilter>();
    
# Here's a follow-up section to encourage engagement and support for Netcode-Hub:
🌟 Get in touch with Netcode-Hub! 📫
1. GitHub: [Explore Repositories](https://github.com/Netcode-Hub/Netcode-Hub) 🌐
2. Twitter: [Stay Updated](https://twitter.com/NetcodeHub) 🐦
3. Facebook: [Connect Here](https://web.facebook.com/NetcodeHub) 📘
4. LinkedIn: [Professional Network](https://www.linkedin.com/in/netcode-hub-90b188258/) 🔗
5. Email: Email: [business.netcodehub@gmail.com](mailto:business.netcodehub@gmail.com) 📧
   
# ☕️ If you've found value in Netcode-Hub's work, consider supporting the channel with a coffee!
1. Buy Me a Coffee: [Support Netcode-Hub](https://www.buymeacoffee.com/NetcodeHub) ☕️
2. Patreon: [Support on Patreon](https://patreon.com/user?u=113091185&utm_medium=unknown&utm_source=join_link&utm_campaign=creatorshare_creator&utm_content=copyLink) 🌟
