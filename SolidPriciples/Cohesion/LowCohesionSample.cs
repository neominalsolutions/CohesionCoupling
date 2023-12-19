namespace SolidPriciples.Cohesion
{
  // Sipariş işlemi (SubmitOrder)
  // Promosyon Kodu uygulama (İndirim)
  // Siparişi veren müşteriye mail atma
  // Sipariş Kodu oluşturma
  // Siparişin veritabanına kaydolması

  public class LowCohesionSample // OrderService
  {
    // Bir sınıfın birden fazla sorumluluğu olması cohesion düşük olması anlamına gelir. 
    // bu sınıf submit order dışında farklı sorumluklarıda yönetme zorunda kaldığı için sorumlulukların ayrıştırılmasını iyi yapmadığından cohesion düşük olur. 
    // SRP => Bir sınıfın değişmesi için tek bir sebebin bir sorumluluğun bulunması gerekir. 
    public void SubmitOrder(Order order, string promationCode)
    {
      Console.WriteLine("Order Submit Edildi");
      ApplyPromationCode(promationCode);
      Save(order);
      SendEmail(order.CustomerId, "Sipariş oluştu");
    }

    // AdoNet => Postgres Driver
    private void Save(Order order)
    {
      Console.WriteLine("Sipariş veritabanına kaydoldu");
    }

    // Discount entitysinden promosyon koduna göre indirim oranlarını buldum.
    private void ApplyPromationCode(string promationCode)
    {
      Console.WriteLine("Promosyon kodu uygulandı");
    }

    // SendGrid => Vodaphone geçiş
    private void SendEmail(string toEmail, string message)
    {
      Console.WriteLine($"{toEmail} {message} gönderildi");
    }

  }
}
