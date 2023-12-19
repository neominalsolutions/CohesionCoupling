using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolidPriciples.Cohesion;
using SolidPriciples.Coupling;

namespace SolidPriciples.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SampleController : ControllerBase
  {
    // IoC tanımlamsı yaptıktan sonraki kısımda ilgili service erişmek için DI kullanıyoruz böylece Application bizim için ilgili sınıfları instance belirlediğimiz scope'a göre otomatik olarak alıyor, Transient (Session Bazlı İşlemler, Event Handling gibi işlemler.),Scoped (Web Request),Singleton (Application Bazlı Tek Instance)

    private readonly HighCohesionAndLowCouplingSample hclc;

    // DI ile otomatik üretilen instance üzerinden çalıştık. (IoC olarak tanımladığı için artık bu şekilde bir yöntem ile farklı sınıfların birbirleri ile çalışmasını sağlayacağız.)
    public SampleController(HighCohesionAndLowCouplingSample hclc)
    {
      this.hclc = hclc;
    }

    [HttpGet("submitOrder")]
    public IActionResult SubmitOrder()
    {
      this.hclc.SubmitOrder(new Order("ALI"), "2342342");

      return Ok();
    }

    [HttpGet]
    public IActionResult Sample()
    {
      LowCohesionSample ls = new LowCohesionSample();
      var order = new Order("ALFKI");
      ls.SubmitOrder(order, "32432432");
      //ls.ApplyPromationCode("23432423");
      //ls.Save(order);
      //ls.SendEmail(order.CustomerId, "Mesaj-1");

      HighCohesionSample hs = new HighCohesionSample();
      hs.SubmitOrder(order, "23423432");

      //hs.discountService.ApplyPromationCode("324324324");
      //hs.orderRepository.sa


      var emailService = new TurkcellEmailService();
      var orderRepo = new DapperOrderRepository();
      var discount = new PromationCodeDiscountService();

      // 1.sample => Dapper, Turkcell Depedencies sample
      HighCohesionAndLowCouplingSample hs1 = new HighCohesionAndLowCouplingSample(emailService, orderRepo, discount);

      hs1.SubmitOrder(new Order("ALI"), "32432432");

      // 2.sample => EF, Vodafone dependency
      var emailService1 = new VodafoneEmailService();
      var orderRepo1 = new EFOrderRepository();
      var discount1=new PromationCodeDiscountService();

      HighCohesionAndLowCouplingSample hs2 = new HighCohesionAndLowCouplingSample(emailService1, orderRepo1, discount1);

      hs2.SubmitOrder(new Order("ALI"), "32432432");


      return Ok();
    }

  }
}
