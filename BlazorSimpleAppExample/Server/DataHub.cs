using BlazorSimpleAppExample.Shared;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSimpleAppExample.Server
{
   public class DataHub : Hub
   {
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="userDataService">Dependency injected user data backend</param>
      public DataHub(UserDataService userDataService)
      {
         UserDataManagerRef = userDataService;
      }

      /// <summary>
      /// Adds a new user to the system.
      /// </summary>
      /// <param name="newUser">A copy of user data.</param>
      /// <returns>True if the user was added, false if there was an error.</returns>
      public async Task<bool> AddNewUser(User newUser)
      {
         // Always double check your data input. Never trust the client. Ever. No really. Don't.
         if (newUser != null  && !String.IsNullOrEmpty(newUser.FirstName) && !String.IsNullOrEmpty(newUser.LastName))
         {
            User existingUser = UserDataManagerRef.Users.FirstOrDefault(u => u.FirstName.Equals(newUser.FirstName, 
                     StringComparison.OrdinalIgnoreCase) && u.LastName.Equals(newUser.LastName, StringComparison.OrdinalIgnoreCase));

            if (existingUser == null)
            {
               UserDataManagerRef.Users.Add(newUser);
               await Clients.All.SendAsync("UserDataUpdate", JsonConvert.SerializeObject(UserDataManagerRef.Users));
               return true;
            }
         }

         return false;
      }

      /// <summary>
      /// Handles deleting a single user from the manager.
      /// </summary>
      /// <param name="userToDelete">The user to remove.</param>
      public async Task DeleteUser(User userToDelete)
      {
         if (userToDelete != null)
         {
            User existingUser = UserDataManagerRef.Users.FirstOrDefault(u => u.FirstName.Equals(userToDelete.FirstName,
                     StringComparison.OrdinalIgnoreCase) && u.LastName.Equals(userToDelete.LastName, StringComparison.OrdinalIgnoreCase));
            if (existingUser != null)
            {
               UserDataManagerRef.Users.Remove(existingUser);
               await Clients.All.SendAsync("UserDataUpdate", JsonConvert.SerializeObject(UserDataManagerRef.Users));
            }
         }
      }

      /// <summary>
      /// Keep an internal reference to the data back-end service. 
      /// </summary>
      private UserDataService UserDataManagerRef { get; set; }
   }
}
