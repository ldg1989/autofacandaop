
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoFacAop.Controllers
{
  [TypeFilter(typeof(CustomControllerActionFilterAttribute), Order = -1)]
  [ApiController]
  [Route("[controller]")]
  public class HomeController : Controller
  {

    private readonly ILogger<HomeController> _logger = null;

    private ITestServiceA _testServiceA = null;


    private ITestMathed _a = null;

    public HomeController(ILogger<HomeController> factory, ITestServiceA testServiceA,ITestMathed a) {
      _logger = factory;
      _testServiceA = testServiceA;
      _a = a;
    }

    

    /// <summary>
    /// 新特性注入，因有构造函数，用ServiceFilter  注入，order排序
    /// </summary>
    /// <returns></returns>

    [HttpGet(nameof(GetData))]
    [ServiceFilter(typeof(CustomActionFilterAttribute), Order = -2)]
    public ActionResult GetData() {
      _logger.LogInformation("======HomeController Get()======");

      _a.Show();
      _testServiceA.Show();
      return Json(new
      {
        success = true,
        token = "12312"
      });
    }


    [HttpGet(nameof(GetData1))]
    public ActionResult GetData1()
    {
      _logger.LogInformation("======HomeController GetData1()======");


      return Json(new
      {
        success = true,
        token = "12312"
      });
    }

    //// GET: api/<HomeController>
    //[HttpGet]
    //public IEnumerable<string> Get()
    //{
    //  return new string[] { "value1", "value2" };
    //}

    //// GET api/<HomeController>/5
    //[HttpGet("{id}")]
    //public string Get(int id)
    //{
    //  return "value";
    //}

    //// POST api/<HomeController>
    //[HttpPost]
    //public void Post([FromBody] string value)
    //{
    //}

    //// PUT api/<HomeController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<HomeController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
  }
}
