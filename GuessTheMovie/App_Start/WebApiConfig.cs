using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GuessTheMovie
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Добавляем поддержку CORS
            var cors = new EnableCorsAttribute(origins: "*", headers: "API-KEY", methods: "*");

            config.EnableCors(cors);

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "FilteredFilm",
               routeTemplate: "api/{controller}/{years}/{genres}",
               defaults: new
               {
                   years = RouteParameter.Optional,
                   genres = RouteParameter.Optional,
               }
            );

            config.Routes.MapHttpRoute(
               name: "FilteredFilms",
               routeTemplate: "api/{controller}/{id}/{years}/{genres}",
               defaults: new
               {
                   id = RouteParameter.Optional,
                   years = RouteParameter.Optional,
                   genres = RouteParameter.Optional,
               }
           );

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //config.Formatters.JsonFormatter.SupportedMediaTypes
            //.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
