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
        o.Filters.Add(typeof(CustomGlobalActionFilterAttribute));//ȫ��
        o.Filters.Add(typeof(CustomExceptionFilterAttribute));// �������ȫ��ע��Filter

      }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
      // ʵ��һ������
      ContainerBuilder containerbuilder = new ContainerBuilder();

      #region �����������ļ����÷��� 
      //// ʵ����
      //IConfigurationBuilder config = new ConfigurationBuilder();
      ////ָ�������ļ�  �����Ĭ�������ļ���·���ڸ�Ŀ¼�£��θ���ʵ���������
      //config.AddJsonFile("autofac.json");
      //// Register the ConfigurationModule with Autofac. 
      //IConfigurationRoot configBuild = config.Build();
      ////��ȡ�����ļ���������Ҫע��ķ���
      //var module = new ConfigurationModule(configBuild);
      //containerbuilder.RegisterModule(module);
      //// �������ݷ���Home/Index ��
      #endregion


      //����ʹ��ServiceFilter ������� ��
      services.AddScoped<CustomActionFilterAttribute>();

      
      // ע�����
      containerbuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance();

      // services Ĭ�ϵ�ע����񣬻���Ҫ���������ʵ����صĵĹ����� 
      containerbuilder.Populate(services); // autofac ȫȨ�ӹ���֮ǰ���Service�����й���

      containerbuilder.RegisterModule<CustomAutofacModule>();
      IContainer container = containerbuilder.Build();

      #region  һ���ͽӿڶ��ʵ��
      // ��ʾ��ǰautofac ��ȡ�����ʱ������ ��ȡ����ӿڵ�ʵ��
      //containerbuilder.RegisterAssemblyTypes(typeof(Startup).Assembly).AsImplementedInterfaces();
      /////��������ӿڵ�ʵ��  ����ȫ��ע�ᵽ��������
      //containerbuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance();
      //containerbuilder.RegisterType<TestServiceA_1>().As<ITestServiceA>().SingleInstance();
      //��ȡ�������������һ��ע��Ϊ׼
      //ITestServiceA testServiceA = container.Resolve<ITestServiceA>();
      //testServiceA.Show();

      //// ���ע����ͬһ���ӿڵĶ��ʵ�֣���ô�����Ҫ��ȡ���е�ʵ�֣���Ҫͨ��һ��������ȥ��ȡ����
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
