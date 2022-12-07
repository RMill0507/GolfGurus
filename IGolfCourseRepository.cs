using GolfGurus.Models;

namespace GolfGurus
{
    public interface IGolfCourseRepository
    {
        public void InsertCourseToDataBase(GolfCourses courseToInsert);
        public IEnumerable<GolfCourses> GetAllCourses();

        public void DeleteCourse(GolfCourses course);
    }

}
