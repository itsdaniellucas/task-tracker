using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.Misc
{
    public class SignalRHub : Hub
    {
        public async Task SendTaskChanges(int sprintId)
        {
            await Clients.All.SendAsync("ReceiveTaskChanges", sprintId);
        }
    }
}
