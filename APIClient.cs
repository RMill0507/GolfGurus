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
            // Retrieve the latitude and longitude for the given zip code
            var coordinates = ZipCoords.GetCoordinates(zip);  // returns null when there's no data

            // Assign the latitude and longitude values to separate variables
            var latitude = coordinates[0];  // 33.971609
            var longitude = coordinates[1];  // -118.171234

            // Create a new RestClient object and initialize it with the API URL, including the radius, latitude, and longitude values
            var client = new RestClient($"https://golf-course-finder.p.rapidapi.com/courses?radius={radius}&lat={latitude}&lng={longitude}");

            // Read the contents of the "appsettings.json" file and parse the JSON to extract the API key value
            string key = File.ReadAllText("appsettings.json");
            string APIKey = JObject.Parse(key)["ConnectionStrings"]["APIKey"].ToString();

            // Create a new RestRequest object and set the HTTP method to GET
            var request = new RestRequest(Method.GET);

            // Add the API key and hostname as headers to the request
            request.AddHeader("X-RapidAPI-Key", $"{APIKey}");
            request.AddHeader("X-RapidAPI-Host", "golf-course-finder.p.rapidapi.com");

            // Make the request to the API using the RestClient object and store the response
            IRestResponse response = client.Execute(request);

            // Parse the response content as a JSON object
            var json = JObject.Parse(response.Content);

            // Create an enumerable collection of GolfCourses objects and initialize it with the data from the "courses" array in the JSON object
            IEnumerable<GolfCourses> courses = json["courses"].Select(p => new GolfCourses
            {
                Name = (string)p["name"],
                ZipCode = (string)p["zip_code"],
                Distance = (string)p["distance"]
            });

            // Return the courses collection
            return courses;


        }




        public  GolfCourses GolfCourseDetails(string zip, string name)

        {
            // Create a new RestClient object and initialize it with the API URL, including the zip code and name values
            var client = new RestClient($"https://golf-course-finder.p.rapidapi.com/course/details?zip={zip}&name={name}");

            // Create a new RestRequest object and set the HTTP method to GET
            var request = new RestRequest(Method.GET);

            // Read the contents of the "appsettings.json" file and parse the JSON to extract the API key value
            string key = File.ReadAllText("appsettings.json");
            string APIKey = JObject.Parse(key)["ConnectionStrings"]["APIKey"].ToString();

            // Add the API key and hostname as headers to the request
            request.AddHeader("X-RapidAPI-Key", $"{APIKey}");
            request.AddHeader("X-RapidAPI-Host", "golf-course-finder.p.rapidapi.com");

            // Make the request to the API using the RestClient object and store the response
            IRestResponse response = client.Execute(request);

            // Parse the response content as a JSON object and select the "result" property
            var json = JObject.Parse(response.Content)["course_details"]["result"];

            // Call the GetTheWeather method and pass it the zip code. Assign the returned GolfCourses object to the "course" variable.
            var course = GetTheWeather(zip);

            // Assign the values of the "formatted_address", "formatted_phone_number", and "name" properties from the JSON object to the corresponding properties of the "course" object
            course.Address = (string)json["formatted_address"];
            course.PhoneNumber = (string)json["formatted_phone_number"];
            course.Name = (string)json["name"];

            // Return the "course" object
            return course;

        }
        public GolfCourses GetTheWeather(string zip)
        {
            // Reads the contents of the "appsettings.json" file and assigns it to the "key" variable
            string key = File.ReadAllText("appsettings.json");

            // Parses the "key" variable as a JSON object, and gets the value of "APIKeyWeather" from the "ConnectionStrings" object, assigns it to "APIKey" variable
            string APIKey = JObject.Parse(key)["ConnectionStrings"]["APIKeyWeather"].ToString();

            // Create a new instance of the RestClient class, passing in the URL of the OpenWeatherMap API, including the zip code and units, as well as the API key
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/weather?zip={zip}&units=imperial&appid={APIKey}");

            // Create a new GET request using the RestRequest class
            var request = new RestRequest(Method.GET);

            // Execute the request and store the response in the "response" variable
            IRestResponse response = client.Execute(request);

            // parse the json content of the response
            var json = (JObject.Parse(response.Content));

            // create a new instance of the 'GolfCourses' class
            var weather = new GolfCourses();

            // Assign the value of "temp" from the "main" object in the JSON to the "Temp" property of the "weather" object
            weather.Temp = (double)json["main"]["temp"];

            // Assign the value of "feels_like" from the "main" object in the JSON to the "FeelsLike" property of the "weather" object
            weather.FeelsLike = (double)json["main"]["feels_like"];

            // Assign the value of "temp_min" from the "main" object in the JSON to the "MinTemp" property of the "weather" object
            weather.MinTemp = (double)json["main"]["temp_min"];

            // Assign the value of "temp_max" from the "main" object in the JSON to the "MaxTemp" property of the "weather" object
            weather.MaxTemp = (double)json["main"]["temp_max"];

            // Assign the value of "humidity" from the "main" object in the JSON to the "Humidity" property of the "weather" object
            weather.Humidity = (double)json["main"]["humidity"];

            // Assign the value of "speed" from the "wind" object in the JSON to the "WindSpeed" property of the "weather" object
            weather.WindSpeed = (double)json["wind"]["speed"];

            // Return the "weather" object
            return weather;

        }


    }

}
