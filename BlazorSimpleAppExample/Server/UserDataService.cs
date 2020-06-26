using BlazorSimpleAppExample.Shared;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorSimpleAppExample.Server
{
   public class UserDataService
   {
      /// <summary>
      /// A list of user data managed by this data service.
      /// </summary>
      public List<User> Users { get; set; } = new List<User>();
   }
}
