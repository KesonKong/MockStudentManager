using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudentManager.DBModels;
using StudentManager.IRepository;
using StudentManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace MockStudentManager
{
    public class Startup
    {
        private readonly IConfiguration _configration;

        public Startup(IConfiguration configration)
        {
            //通过IConfiguration 读取appsettings的Key值
            _configration = configration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //添加数据库服务 建议使用AddDbContextPool
            services.AddDbContextPool<AppDbContext>(
                options=>options.UseSqlServer(_configration.GetConnectionString("StudentDbConnection"))
                );

            //添加MVC服务
            //AddXmlSerializerFormatters将XML序列化程序格式化程序添加到MVC中
            services.AddMvc().AddXmlSerializerFormatters();

            //依赖注入绑定接口与实现
            //services.AddSingleton<IStudentRepository, SQLStudentRepository>();
            //services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentRepository, SQLStudentRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment()) //开发环境
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
                developerExceptionPageOptions.SourceCodeLineCount = 20;
                //开发人员报错页面
                app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }
            else if (env.IsProduction() || env.IsStaging() || env.IsEnvironment("UAT")) //开生产环境，预发布环境，UAT环境 
            {
                //错误状态提示页面
                //app.UseStatusCodePages();

                //拦截我们的异常
                app.UseExceptionHandler("/Error");

                //跳转到错误页面,重定向，覆盖正确的StatusCode
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");

                //重定向，返回正确的StatusCode，拦截404找不到页面信息
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            ////添加指定起始页
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("52abp.html");

            ////添加默认文件中间件，添加默认起始页
            ////index.html index.htm default.html default.htm
            ////UseDefaultFiles必须注册在UseStaticFiles 之前才生效
            //app.UseDefaultFiles(defaultFilesOptions);


            //添加静态文件中间件 wwwroot
            app.UseStaticFiles();

            //添加MVC中间件 MVC默认路由，需要添加在UseStaticFiles 后面
            //app.UseMvcWithDefaultRoute();

            //自定义MVC路由信息
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //用属性路由，只需要app.UseMvc()
            //app.UseMvc();


            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("52abp.html");

            //UseFileServer 结合了UseDefaultFiles，UseStaticFiles，UserDirectoryBrowser(浏览网页目录) 中间件的功能
            //app.UseFileServer(fileServerOptions);



            //app.Use(async (context, next) =>
            //{
            //    //处理中文乱码
            //    context.Response.ContentType = "text/plain;charset=utf-8";

            //    logger.LogInformation("M1传入请求");

            //    //await context.Response.WriteAsync("第一个中间件");
            //    await next();

            //    logger.LogInformation("M1传出响应");
            //});


            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("M2传入请求");
            //    await next();
            //    logger.LogInformation("M2传出响应");
            //});


            //Hello World
            //app.Run 是终端中间件，会使整个管道短路，从而不会调用管道中后续的中间件
            //app.Run(async (context) =>
            //{
                //处理中文乱码
                //context.Response.ContentType = "text/plain;charset=utf-8";
                //获取进程名
                //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

                //读取appSettings.json中的值
                //var configVal = _configration["MyKey"];

                //await context.Response.WriteAsync(configVal);

                //throw new Exception("您的请求在管道中发生了一些错误");

                //await context.Response.WriteAsync("Hello World!");

                //logger.LogInformation("M3处理请求并生成响应");

                //await context.Response.WriteAsync("Hosting Enviroment:" + env.EnvironmentName);

            //});

        }
    }
}
