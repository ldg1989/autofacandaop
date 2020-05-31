using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFacAop
{
  public class CustomAutofacModule : Module
  { /// <summary>
    ///  当前这Module 专用做服务注册
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance();

      builder.Register(a => new CustomAutofacAOP()); //autofac 允许使用Aop
      // 允许当前注册的这个服务实例使用Aop
      builder.RegisterType<TestMathed>().As<ITestMathed>().EnableInterfaceInterceptors();
    }
  }

  public interface ITestServiceA
  {
    void Show();
  }


  public class TestServiceA_1 : ITestServiceA
  {
    private readonly ILogger<TestServiceA> _logger = null;
    public TestServiceA_1(ILogger<TestServiceA> logger)
    {
      _logger = logger;
    }

    public void Show()
    {
      _logger.LogInformation("这是TestServiceA打印的log");
      Console.WriteLine("这是TestServiceA打印的log");
    }
  }


  public class TestServiceA : ITestServiceA
  {
    private readonly ILogger<TestServiceA> _logger = null;
    public TestServiceA(ILogger<TestServiceA> logger)
    {
      _logger = logger;
    }

    public void Show()
    {
      _logger.LogInformation("这是TestServiceA打印的log");
      Console.WriteLine("这是TestServiceA打印的log");
    }
  }
}
