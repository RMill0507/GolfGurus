using GolfGurus.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Xml.Linq;

namespace GolfGurus
{
    public class APIClient
    {
        public APIClient()
        {

        }
        public  IEnumerable<GolfCourses> GolfCourses(string zip, string radius)
        {
           
            var coordinates = ZipCoords.GetCoordinates(zip);  // returns null when there's no data

            var latitude = coordinates[0];  // 33.971609
            var longitude = coordinates[1];  // -118.171234
            var client = new RestClient($"https://golf-course-finder.p.rapidapi.com/courses?radius={radius}&lat={latitude}&lng={longitude}");
            string key = File.ReadAllText("appsettings.json");
            string APIKey = JObject.Parse(key)["ConnectionStrings"]["APIKey"].ToString();
            var request = new RestRequest(Method.GET);//Calling api 
            request.AddHeader("X-RapidAPI-Key", $"{APIKey}");//adding headers to request
            request.AddHeader("X-RapidAPI-Host", "golf-course-finder.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            var json = JObject.Parse(response.Content);//creating an object
            IEnumerable<GolfCourses> courses = json["courses"].Select(p => new GolfCourses//making a list out of the object and looping through the json array
            {
                Name = (string)p["name"],
                ZipCode = (string)p["zip_code"],
                Distance = (string)p["distance"]


            });
            return courses;

        }

        

        
        public  GolfCourses GolfCourseDetails(string zip, string name)

        {
            
            var client = new RestClient($"https://golf-course-finder.p.rapidapi.com/course/details?zip={zip}&name={name}");
            var request = new RestRequest(Method.GET);
            string key = File.ReadAllText("appsettings.json");
            string APIKey = JObject.Parse(key)["ConnectionStrings"]["APIKey"].ToString();

            request.AddHeader("X-RapidAPI-Key", $"{APIKey}");
            request.AddHeader("X-RapidAPI-Host", "golf-course-finder.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            var json = JObject.Parse(response.Content)["course_details"]["result"];
            var course = GetTheWeather(zip);
            course.Address = (string)json["formatted_address"];
            course.PhoneNumber = (string)json["formatted_phone_number"];
            course.Name = (string)json["name"];
            





            return course;
        }
        public GolfCourses GetTheWeather(string zip)
        {
            string key = File.ReadAllText("appsettings.json");
            string APIKey = JObject.Parse(key)["ConnectionStrings"]["APIKeyWeather"].ToString();
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/weather?zip={zip}&units=imperial&appid={APIKey}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var json = (JObject.Parse(response.Content));
            var weather = new GolfCourses();
            weather.Temp = (double)json["main"]["temp"];
            weather.FeelsLike = (double)json["main"]["feels_like"];
            weather.MinTemp = (double)json["main"]["temp_min"];
            weather.MaxTemp = (double)json["main"]["temp_max"];
            weather.Humidity = (double)json["main"]["humidity"];
            weather.WindSpeed = (double)json["wind"]["speed"];

            return weather;

        }


    }

}
