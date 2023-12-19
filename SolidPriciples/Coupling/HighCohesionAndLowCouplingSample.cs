using SolidPriciples.Cohesion;
using SolidPriciples.Coupling;
using System.Xml.Linq;

namespace SolidPriciples.Coupling
{

  

  public interface IRepository<TEntity>
    where TEntity:class,new() // burada TEntity tipinde sınıfı abstract sınıf olamaz, newlenebilir bir sınıf olmalıdır.
  {
    void Save(TEntity entity);
  }

  //public abstract class BaseEntity
  //{
  //  public BaseEntity()
  //  {

  //  }
  //}
  //public class BaseEntityRepository : IRepository<BaseEntity>
  //{
  //  public void Save(BaseEntity entity)
  //  {
  //    throw new NotImplementedException();
  //  }
  //}

  public interface IEmailService
  {
    void Send(string to, string message);
  }

  public interface IDiscount
  {
    void ApplyPromationCode(string promationCode);
  }

  // Dapper Micro ORM ile veritabanına kaydet
  public class DapperOrderRepository : IRepository<Order>
  {
    public void Save(Order entity)
    {
      Console.WriteLine("DapperOrderRepository");
    }
  }

  public class AdoNetOrderRepository : IRepository<Order>
  {
    public void Save(Order entity)
    {
      Console.WriteLine("AdoNetOrderRepository");
    }
  }

  public class EFOrderRepository : IRepository<Order>
  {
    public void Save(Order entity)
    {
      Console.WriteLine("EFOrderRepository");
    }
  }

  public class VodafoneEmailService : IEmailService
  {
    public void Send(string to, string message)
    {
      Console.WriteLine("VodafoneEmailService");
    }
  }

  public class TurkcellEmailService : IEmailService
  {
    public void Send(string to, string message)
    {
      Console.WriteLine("TurkcellEmailService");
    }
  }


  public class PromationCodeDiscountService : IDiscount
  {
    public void ApplyPromationCode(string promationCode)
    {
      Console.WriteLine("PromationCodeDiscountService");
    }
  }
  }


  public class HighCohesionAndLowCouplingSample // Order Service
  {
    //private readonly IEmailService emailService = new VodaPhoneService();
    //private readonly IRepository<Order> repository = new DapperRepository();

  // Interface kullanarak main sistem ile sub sistem sınıflar arasındaki bağlılığı araya soyut bir yapı koyarak azalttık. Yani Low Coupling sağlamak için yaptık. Solid Prensiplerinde bu yaptığımız işlem Dependency Inversion denir.  (DIP) Coupling, (SRP) Cohesion
    private readonly IEmailService _emailService;
    private readonly IRepository<Order> repository;
    private readonly IDiscount discount;
    // Bağımlıkların bu şekilde constructor üzerinden parametre olarak geçilmesi durumuna Dependency Injection diyoruz.
    // Dependency Injection Setter ve Method üzerinde kullanılır. Ama en yaygın yöntem constructor injection tekniğidir. (DI)
    public HighCohesionAndLowCouplingSample(IEmailService emailService, IRepository<Order> repository, IDiscount discount)
    {
      _emailService = emailService;
      this.repository = repository;
      this.discount = discount;
    }

    public void SubmitOrder(Order order,string promationCode)
    {
      this.discount.ApplyPromationCode(promationCode);
      this.repository.Save(order);
      this._emailService.Send(order.CustomerId, "Sipariş Oluştu");
    }

    /// <summary>
    /// Methodun çalışması için gereken bağımlılığı method içine parametre olarak geçme yöntemine method injection adı veriyoruz. Constructor Injection kadar popüler bir kullanım yöntemi değil.
    /// </summary>
    /// <param name="emailService"></param>
    public void MethodInjection(IEmailService emailService)
    {
      emailService.Send("fdsadsa", "sdfsd");
    }

 }
