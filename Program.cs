using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoFacAop
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging((context, LoggingBuilder) =>
            {
              LoggingBuilder.AddFilter("System", LogLevel.Warning); // 忽略系统的其他日志
              LoggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
              LoggingBuilder.AddLog4Net();
            })
            .UseStartup<Startup>();
    }
  }
}

