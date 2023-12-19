namespace SolidPriciples.Cohesion
{
  public class OrderItem
  {
    public Guid OrderCode { get; init; }
    public int Quantity { get; init; }
    public decimal ListPrice { get; init; }
    public Guid ProductId { get; init; }


    public OrderItem(Guid orderCode, int quantity, Guid productId, decimal listPrice)
    {
      OrderCode = orderCode;
      Quantity = quantity;
      ProductId = productId;
      ListPrice = listPrice;
    }




  }
}
