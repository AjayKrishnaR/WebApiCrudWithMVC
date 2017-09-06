using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiCRUDOperations.App_Start;
using WebApiCRUDOperations.Models;

namespace WebApiCRUDOperations.Controllers.Api
{
    public class StudController : ApiController
    {

        [HttpGet]
        [Route("FindStudent")]
        public HttpResponseMessage Get(int id)
        {
            Student student = new Student();
            using (var context = new studentsEntities1())
            {
                student = context.Students.Where(s => s.id == id).FirstOrDefault();
            }
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }


        [HttpGet]
        [Route("CheckStudent")]
        public IHttpActionResult GetStud(int id)
        {
            Student student = new Student();
            using (var context = new studentsEntities1())
            {
                student = context.Students.Where(s => s.id == id).FirstOrDefault();
            }
            if (student == null)
            {
                return new CustomResult("NOT FOUND",Request,HttpStatusCode.NotFound);
            }
            return new CustomResult(student.Name, Request, HttpStatusCode.Found);
        }
    }

}
