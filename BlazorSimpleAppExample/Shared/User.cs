using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorSimpleAppExample.Shared
{
   /// <summary>
   /// Represents the data of a single user.
   /// </summary>
   public class User
   {
      public string FirstName { get; set; } = String.Empty;

      public string LastName { get; set; } = String.Empty;
   }
}
