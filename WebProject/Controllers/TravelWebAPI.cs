using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic;

namespace WebProject.Controllers
{
    public class TravelWebAPI : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public string HelloWorld()
        {
            return "HelloWorld";
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public IEnumerable<Country> GetCountriesWithXML()
        {
            using (WebTestEntities context = new WebTestEntities())
            {
                return context.Countries.ToList();
            }
        }
    }
}