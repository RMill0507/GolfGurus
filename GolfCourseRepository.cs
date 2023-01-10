using Dapper;
using GolfGurus.Models;
using System.Collections;
using System.Data;

namespace GolfGurus
{
    public class GolfCourseRepository : IGolfCourseRepository
    {
        // private field to hold an instance of IDbConnection which will be used to interact with the database
        private readonly IDbConnection _conn;

        // constructor that takes an IDbConnection as a parameter and assigns it to the _conn field
        public GolfCourseRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        // method that inserts an object of 'GolfCourses' class into the database
        public void InsertCourseToDataBase(GolfCourses courseToInsert)
        {
            // extract the zip code from the Address property of the courseToInsert object
            courseToInsert.ZipCode = courseToInsert.Address.Split(',')[2].Substring(4);

            // use the _conn object to execute an INSERT statement
            // passing in the Name and ZipCode properties of the courseToInsert object as parameters
            _conn.Execute("INSERT INTO savedcourses (Name, ZipCode) VALUES (@name, @zipcode);",
                new { name = courseToInsert.Name, zipcode = courseToInsert.ZipCode });
        }
        // method that retrieves all 'GolfCourses' objects from the database
        public IEnumerable<GolfCourses> GetAllCourses()
        {
            // use the _conn object to execute a SELECT statement
            // and retrieve all records from the savedcourses table
            return _conn.Query<GolfCourses>("Select * from savedcourses;");
                
        }

        // method that takes an object of 'GolfCourses' and deletes it from the database
        public void DeleteCourse(GolfCourses course)
        {
            // use the _conn object to execute a DELETE statement
            // passing in the idsavedcourses property of the course object as a parameter
            _conn.Execute("DELETE FROM savedcourses WHERE idsavedcourses = @id;", new { id = course.idsavedcourses});
        }
    }
}
