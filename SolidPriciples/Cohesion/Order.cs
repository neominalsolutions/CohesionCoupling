namespace SolidPriciples.Cohesion
{
  public class Order
  {
    public Guid OrderCode { get; init; }
    public DateTime OrderDate { get; init; }
    public string CustomerId { get; init; }

    private List<OrderItem> items = new List<OrderItem>();
    public IReadOnlyList<OrderItem> Items => items;

    public Order()
    {

    }

    public Order(string customerId)
    {
      CustomerId = customerId;
      OrderDate = DateTime.Now;
      OrderCode = Guid.NewGuid();
    }

    public void AddItem(int quantity, Guid ProductId, decimal listPrice)
    {
      var item = new OrderItem(OrderCode, quantity, ProductId, listPrice);
      items.Add(item);
    }


  }
}
