﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace _002.GetChildren
{
    public class Startup
    {
        // Свойство, хранящее набор значений конфигурации приложения
        public IConfiguration AppConfiguration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Создаем экземпляр класса строителя конфигурации
            var builder = new ConfigurationBuilder();

            // Устанавливаем путь, по которому будет производится поиск файла конфигурации
            builder.SetBasePath(env.ContentRootPath);

            // Задаем имя используемого файла конфигурации
            builder.AddJsonFile("JsonConfig.json");

            // Создаем конфигурацию
            AppConfiguration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            var sections = AppConfiguration.GetSection("Users");
            var sectionsUsers = sections.GetChildren();

            app.Run(async (context) =>
            {
                foreach (var section in sectionsUsers)
                    await context.Response.WriteAsync($"<br>{section.Key}: {section.GetSection("Number").Value}</br>");
            });
        }
    }
}
