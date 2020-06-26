using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSimpleAppExample.Server
{
   [Route("example")]
   public class ExampleController : Controller
   {
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="userDataService">Dependency-injected UserDataService reference.</param>
      public ExampleController(UserDataService userDataService)
      {
         _userDataService = userDataService;
      }

      /// <summary>
      /// A simple example of how to use a controller endpoint. This method can be invoked
      /// by using the Http client instance in the Blazor front-end to access example/usercount.
      /// The response will include the total count of users in the system.
      /// </summary>
      [HttpGet]
      [Route("usercount")]
      public ActionResult GetNumberOfUsers()
      {
         // Return a 200 status to indicate success. A separate object result can be added as well.
         // This will be the count of the Users list on the data service.
         return StatusCode(StatusCodes.Status200OK, _userDataService.Users.Count.ToString());
      }

      /// <summary>
      /// Implementation of base Controller method. Boilerplate.
      /// </summary>
      public IActionResult Index()
      {
         return View();
      }

      // An reference to the user data service.
      private UserDataService _userDataService { get; set; }
   }
}
