using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
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


      //允许使用ServiceFilter 标记特性 ※
      services.AddScoped<CustomActionFilterAttribute>();

      // 实例一个容器
      ContainerBuilder containerbuilder = new ContainerBuilder();
      // 注册服务
      containerbuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance();

      // services 默认的注册服务，还需要处理控制器实例相关的的工作。 
      containerbuilder.Populate(services); // autofac 全权接管了之前这个Service的所有工作

    containerbuilder.RegisterModule<CustomAutofacModule>();
      IContainer container = containerbuilder.Build();
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
