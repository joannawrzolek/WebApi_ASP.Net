using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace CountingKs
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.Routes.MapHttpRoute(
          name: "Food",
          routeTemplate: "api/nutrition/foods/{foodid}",
          defaults: new { controller = "foods", foodid = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "Measures",
          routeTemplate: "api/nutrition/foods/{foodid}/measures/{id}",
          defaults: new { controller = "measures", id = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "Diaries",
          routeTemplate: "api/user/diaries/{diaryid}",
          defaults: new { controller = "diaries", diaryid = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "DiaryEntries",
          routeTemplate: "api/user/diaries/{diaryid}/entries/{id}",
          defaults: new { controller = "diaryentries", id = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "DiarySummary",
          routeTemplate: "api/user/diaries/{diaryid}/summary",
          defaults: new { controller = "diarysummary" }
      );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            config.EnableQuerySupport();

            config.Formatters.Clear(); //because there are defaults of XML..
            config.Formatters.Add(new JsonMediaTypeFormatter());
        
            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
           
        }

        static void CreateMediaTypes(JsonMediaTypeFormatter jsonFormatter)
        {
            var mediaTypes = new string[]
            {
        "application/vnd.countingks.food.v1+json",
        "application/vnd.countingks.measure.v1+json",
        "application/vnd.countingks.measure.v2+json",
        "application/vnd.countingks.diary.v1+json",
        "application/vnd.countingks.diaryEntry.v1+json",
            };

            foreach (var mediaType in mediaTypes)
            {
                jsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(mediaType));
                jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            }

        }
    }
}