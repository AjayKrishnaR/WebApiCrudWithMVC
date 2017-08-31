using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiCRUDOperations.Filters;
using WebApiCRUDOperations.Models;

namespace WebApiCRUDOperations.Controllers
{
    public class StudentController : ApiController
    {
        [Log]
        [HttpGet]
       // [Route("List")]
        public IHttpActionResult GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (var context = new studentsEntities1())
            {
                students = context.Students.ToList();
            }

            if (students.Count > 0)
                return Ok(students);
            else
                return Ok("No students found");
        }

        public IHttpActionResult GetStudentById(int id)
        {
            Student student = new Student();
            using (var context = new studentsEntities1())
            {
                student = context.Students.Where(s => s.id == id).FirstOrDefault();
            }
            if (student == null)
                return Ok("No student with given ID found");
            else
                return Ok(student);
        }

        public IHttpActionResult GetStudentsByName(string firstName, string lastName)
        {
            List<Student> students = new List<Student>();
            using (var context = new studentsEntities1())
            {
                students = context.Students.Where(s => s.Name == firstName).ToList();
            }
            if (students.Count > 0)
                return Ok(students);
            else
                return Ok("No students found with the given name");
        }

        [HttpPost]
        public IHttpActionResult PostNewStudent(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid student.");

            using (var context = new studentsEntities1())
            {
                context.Students.Add(new Student()
                {
                    id = student.id,
                    Name = student.Name,
                    Age = student.Age,
                    
                });
                context.SaveChanges();
            }
            return Ok("New Student Added");
        }

        public IHttpActionResult Put(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid student");
            using (var context = new studentsEntities1())
            {
                var existingStudent = context.Students.Where(s => s.id == student.id).FirstOrDefault();
                if (existingStudent == null)
                    return NotFound();
                else
                {
                    existingStudent.Name = student.Name;
                    existingStudent.Age = student.Age;
                    
                    context.SaveChanges();
                }
                return Ok();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            Student student = new Student();
            if (id <= 0)
                return BadRequest("Not a valid student id");
            using (var context = new studentsEntities1())
            {
                student = context.Students.Where(s => s.id == id).FirstOrDefault();
                if (student == null)
                    return NotFound();
                else
                {
                    context.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
