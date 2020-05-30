using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFacAop
{
  /// <summary>
  /// 定义AOP  继承IInterceptor
  /// </summary>
  public class CustomAutofacAOP : IInterceptor
  {
    public void Intercept(IInvocation invocation)
    {
      Console.WriteLine($"Aop 调用方法之前执行 {invocation.Method.Name}");

      invocation.Proceed();// 表示继续执行，就去应该执行的动作了

      Console.WriteLine("Aop 调用方法之后执行==============");
    }
  }

  public interface ITestMathed
  {
    void Show();
  }

  /// <summary>
  /// 特性放在实例上
  /// </summary>
  [Intercept(typeof(CustomAutofacAOP))]
  public class TestMathed : ITestMathed
  {
    private readonly ILogger<TestMathed> _logger = null;
     

    public TestMathed(ILogger<TestMathed> factory)
    {
      _logger = factory; 

    }
    public void Show()
    {
      _logger.LogInformation("autofac -aop 打印");
      Console.WriteLine("autofac -aop 打印");
    }
  }
}
