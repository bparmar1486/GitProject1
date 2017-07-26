using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic;

namespace WebProject.Controllers
{
    public class CountryController : ApiController
    {
        public IEnumerable<CountryList> GetCountriesWithXML()
        {
            using (WebTestEntities context = new WebTestEntities())
            {
                return context.CountryLists.ToList();
            }
        }
    }
}
