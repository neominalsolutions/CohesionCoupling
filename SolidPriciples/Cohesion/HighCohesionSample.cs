namespace SolidPriciples.Cohesion
{
  public class EmailSettings
  {
    public string ProviderName { get; set; }

  }
  public class EmailService // Turkcell
  {
    public readonly EmailSettings emailSettings = new EmailSettings();
    public void SendEmail(string to,string message)
    {
      Console.WriteLine("Mesaj gönderildi");
    }
  }

  public class DiscountService // Indirimleri hesaplama servisi
  {
    public void ApplyPromationCode(string promationCode)
    {
      Console.WriteLine("Promosyon kod uygulandı");
    }
  }

  public class OrderRepository // AdoNet
  {
    
    public void Save(Order order)
    {
      Console.WriteLine("Sipariş Kaydoldu");
    }
  }

  // Avantajlar servislere ayrıdığımız için her bir servis için unitTest yazabiliriz.
  // Servislerin referans aldığı tüm yerlerde bu unit testler geçerli hale geldi.
  // Reusability => Servisileri merkezileştirdiğim için başka sınıflarda aynı servisleri defalarca kullanabilirim. (DRY) Dont Repeat yourself
  // Management (Maintainability) => Merkezi olarak servis üzerinden yönetim yaptığımdan herhangi bir alt yapı güncellemesinde sadece ilgili servisin kaynak kodunu değişirmek yeterli olucak referans alan tüm sınıfların kaynak kodu güncellenecek.
  // Sorumlulukları dağıtarak, high cohesion (yüksek uyumlu) bir geliştirme yaptık. SRP single responsibity'e uygun bir geliştirme yaptık.


  // main system, main service
  public class HighCohesionSample // BestPracticeOrderService
  {
    // subsytem, sub services
    private readonly EmailService emailService = new EmailService();
    private readonly DiscountService discountService = new DiscountService();
    private readonly OrderRepository orderRepository = new OrderRepository();

    // Bir sınıfın içerisindeki alt sistemlerin sınıf düzeyin encapsulate edilip, kod karmaşıklığını azalması Facade tasarım deseni ile oluyor.

    // kodun 

    public void SubmitOrder(Order order,string promationCode)
    {
      //emailService.emailSettings = new EmailSettings();
      
      discountService.ApplyPromationCode(promationCode);
      orderRepository.Save(order);
      emailService.SendEmail(order.CustomerId, "Sipariş oluştu");
    }
  }
}
