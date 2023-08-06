using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using online_selling.Dto;
using online_selling.Interfaces.Orders;
using online_selling.Interfaces.UnitOfWork;
using online_selling.Models;

namespace online_selling.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public static DateTime GetRandomTimeAfterInterval(DateTime startTime, TimeSpan interval)
        {
            Random random = new Random();
            int minutesToAdd = random.Next((int)interval.TotalMinutes, (int)TimeSpan.FromMinutes(120).TotalMinutes + 1);

            return startTime.AddMinutes(minutesToAdd);
        }


        public async Task<ReturnOrderDto> AddOrder(OrderDto orderDto)
        {
            List<int> itemIds = orderDto.ItemIds.Keys.ToList();
            var items = await _unitOfWork.Items.GetItemsById(itemIds);

            List<Item> itemsForOrder = new List<Item>();
            List<OrderItem> orderItems = new List<OrderItem>();


            Dictionary<int, bool> resultDict = new Dictionary<int, bool>();
            Order order = new Order();

            foreach (KeyValuePair<int, int> kvp in orderDto.ItemIds)
            {
                Item item = items.FirstOrDefault(i => i.Id == kvp.Key);
                if (kvp.Value > item.Amount) {
                    resultDict.Add(kvp.Key, false);
                }
                else
                {
                    itemsForOrder.Add(item);
                    resultDict.Add(kvp.Key, true);
                    OrderItem orderItem = new OrderItem
                    {
                        Item = item
                    };
                    if (kvp.Value > 0)
                    {
                        order.Price = kvp.Value * item.Price + 200;
                        orderItems.Add(orderItem);
                    }
                }
                
            }

            order.OrderItems = orderItems;
            order.Address = orderDto.Address;
            order.Comment = orderDto.Comment;
            order.UserId = orderDto.UserId;
            if (orderItems.Count != 0)
            {
                await _unitOfWork.Orders.AddAsync(order);
                //Order result = await _unitOfWork.Orders.GetOrderByUserId(orderDto.UserId);

                foreach (var item in itemsForOrder)
                {
                    item.Amount -= orderDto.ItemIds[item.Id];
                    // item.OrderId = result.Id;
                }
                _unitOfWork.Items.UpdateEntitiesSync(itemsForOrder);
                await _unitOfWork.CompleteAsync();
            }

            ReturnOrderDto returnOrderDto = new ReturnOrderDto();
            returnOrderDto.dict = resultDict;
            DateTime startTime = DateTime.Now; // Set the starting point as the current time
            TimeSpan interval = TimeSpan.FromMinutes(60); // Set the interval as 60 minutes
            returnOrderDto.date = GetRandomTimeAfterInterval(startTime, interval);

            return returnOrderDto;
        }
    }
}
