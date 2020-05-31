using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoFacAop
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    //public void ConfigureServices(IServiceCollection services)
    //{
    //  services.AddControllers();
    //}

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });
      services.AddSession();
      services.AddMvc(o =>
      {
        o.Filters.Add(typeof(CustomGlobalActionFilterAttribute));//全局
        o.Filters.Add(typeof(CustomExceptionFilterAttribute));// 这里就是全局注册Filter

      }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
      // 实例一个容器
      ContainerBuilder containerbuilder = new ContainerBuilder();

      #region 依赖于配置文件配置服务 
      //// 实例化
      //IConfigurationBuilder config = new ConfigurationBuilder();
      ////指定配置文件  这里的默认配置文件的路径在根目录下，课根据实际情况调整
      //config.AddJsonFile("autofac.json");
      //// Register the ConfigurationModule with Autofac. 
      //IConfigurationRoot configBuild = config.Build();
      ////读取配置文件里配置需要注册的服务
      //var module = new ConfigurationModule(configBuild);
      //containerbuilder.RegisterModule(module);
      //// 测试内容放在Home/Index 下
      #endregion


      //允许使用ServiceFilter 标记特性 ※
      services.AddScoped<CustomActionFilterAttribute>();

      
      // 注册服务
      containerbuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance();

      // services 默认的注册服务，还需要处理控制器实例相关的的工作。 
      containerbuilder.Populate(services); // autofac 全权接管了之前这个Service的所有工作

      containerbuilder.RegisterModule<CustomAutofacModule>();
      IContainer container = containerbuilder.Build();

      #region  一个就接口多个实现
      // 表示当前autofac 获取服务的时候允许 获取多个接口的实现
      //containerbuilder.RegisterAssemblyTypes(typeof(Startup).Assembly).AsImplementedInterfaces();
      /////添加两个接口的实现  这里全部注册到容器中来
      //containerbuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance();
      //containerbuilder.RegisterType<TestServiceA_1>().As<ITestServiceA>().SingleInstance();
      //获取单个服务，已最后一个注册为准
      //ITestServiceA testServiceA = container.Resolve<ITestServiceA>();
      //testServiceA.Show();

      //// 如果注册了同一个接口的多个实现，那么如果需要获取所有的实现，需要通过一个迭代器去获取服务
      //IEnumerable<ITestServiceA> testServiceds = container.Resolve<IEnumerable<ITestServiceA>>();
      //foreach (var serviceD in testServiceds)
      //{
      //  serviceD.Show();
      //} 
      #endregion


      return new AutofacServiceProvider(container);
    }



    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory factory)
    {
      factory.AddLog4Net();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
