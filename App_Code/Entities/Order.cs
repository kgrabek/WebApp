using System;

namespace Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public bool OrderSent { get; set; }

        public Order(int id, string client, string productName, int amount, double price, DateTime date, bool orderSent)
        {
            Id = id;
            Client = client;
            ProductName = productName;
            Amount = amount;
            Price = price;
            Date = date;
            OrderSent = orderSent;
        }

        public Order(string client, string productName, int amount, double price, DateTime date, bool orderSent)
        {
            Client = client;
            ProductName = productName;
            Amount = amount;
            Price = price;
            Date = date;
            OrderSent = orderSent;
        }
    }
}