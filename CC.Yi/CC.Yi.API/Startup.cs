using Autofac;
using Autofac.Extras.DynamicProxy;
using CC.Yi.API.Extension;
using CC.Yi.BLL;
using CC.Yi.Common.Castle;
using CC.Yi.Common.Json;
using CC.Yi.Common.Jwt;
using CC.Yi.DAL;
using CC.Yi.IBLL;
using CC.Yi.IDAL;
using CC.Yi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.API
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //���û��ڲ��Ե���֤
                options.AddPolicy("myadmin", policy =>policy.RequireRole("admin"));

            });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,//�Ƿ���֤Issuer
                           ValidateAudience = true,//�Ƿ���֤Audience
                           ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                           ClockSkew = TimeSpan.FromDays(1),


                           ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                           ValidAudience = JwtConst.Domain,//Audience
                           ValidIssuer = JwtConst.Domain,//Issuer���������ǰ��ǩ��jwt������һ��
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConst.SecurityKey))//�õ�SecurityKey
                       };
                   });

            //ע�������Ķ���
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //���ù�����
            Action<MvcOptions> filters = new Action<MvcOptions>(r =>
            {
                //r.Filters.Add(typeof(DbContextFilter));
            });

            services.AddControllers(filters).AddJsonOptions(options => {

                options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());

            });
            services.AddSwaggerService();


            //�������ݿ�����
            string connection1 = Configuration["ConnectionStringBySQL"];
            string connection2 = Configuration["ConnectionStringByMySQL"];
            string connection3 = Configuration["ConnectionStringBySQLite"];
            string connection4 = Configuration["ConnectionStringByOracle"];

            //var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));//mysql�汾

            services.AddDbContext<DataContext>(options =>
            {
                //options.UseSqlServer(connection1);//sqlserver����
                //options.UseMySql(connection2, serverVersion);//mysql����
                options.UseSqlite(connection3);//sqlite����
                //options.UseOracle(connection4);//oracle����
            });



            services.AddCors(options => options.AddPolicy("CorsPolicy",//�����������
            builder =>
            {
                builder.AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
        }

        //��ʼ��ʹ�ú���
        private void InitData(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var Db = serviceScope.ServiceProvider.GetService<DataContext>();
                var log = serviceScope.ServiceProvider.GetService<Logger<string>>();
                if (Init.InitDb.Init(Db))
                {
                    log.LogInformation("���ݿ��ʼ���ɹ���");
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //���ÿ��ӻ��ӿ�
                app.UseSwaggerService();
            }
            //���þ�̬�ļ�
            app.UseStaticFiles();

            //�����쳣��׽
            app.UseErrorHandling();

            //���ÿ�������
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            //����������֤
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //��ʼ��
            InitData(app.ApplicationServices);
        }
    }
}