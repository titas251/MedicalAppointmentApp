using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            var user = this.Context.User.Identity.Name;
            if (user == "admin@gmail.com") 
            {
                user = "admin";
            }

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
