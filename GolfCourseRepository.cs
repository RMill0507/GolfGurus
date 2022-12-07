using Dapper;
using GolfGurus.Models;
using System.Collections;
using System.Data;

namespace GolfGurus
{
    public class GolfCourseRepository : IGolfCourseRepository
    {
        private readonly IDbConnection _conn;

        public GolfCourseRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void InsertCourseToDataBase(GolfCourses courseToInsert)
        {
            courseToInsert.ZipCode = courseToInsert.Address.Split(',')[2].Substring(4);


            _conn.Execute("INSERT INTO savedcourses (Name, ZipCode) VALUES (@name, @zipcode);",
                new { name = courseToInsert.Name, zipcode = courseToInsert.ZipCode });
        }
        public IEnumerable<GolfCourses> GetAllCourses()
        {
            return _conn.Query<GolfCourses>("Select * from savedcourses;");
                
        }

        public void DeleteCourse(GolfCourses course)
        {
              
            _conn.Execute("DELETE FROM savedcourses WHERE idsavedcourses = @id;", new { id = course.idsavedcourses});
        }
    }
}
