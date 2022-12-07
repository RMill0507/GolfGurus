namespace GolfGurus.Models
{
    public class GolfCourses
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Distance { get; set; }  
        public int idsavedcourses { get; set; }

        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed {get; set; }

    }
}
