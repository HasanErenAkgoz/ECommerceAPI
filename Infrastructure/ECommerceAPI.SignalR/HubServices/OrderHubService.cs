﻿using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceAPI.SignalR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderAddedMessageAsync(string massage)
            => await _hubContext.Clients.All.SendAsync(ReciveFunctionNames.OrderAddedMessage,massage);
    }
}
