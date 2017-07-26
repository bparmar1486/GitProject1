using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Xml.Linq;
using WebProject.Models;
using System.Xml;

namespace WebProject.Controllers
{
    public class CountryListController : Controller
    {
        // GET: CountryList
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Country(string message)
        {
            const string apiKey = "4kq3ee27yc2559qja7rjcgpf";
            const string Secret = "SYnpbEKY97";

            const string endpoint = "https://api.test.hotelbeds.com/hotel-content-api/1.0/locations/countries?fields=all&language=ENG&from=1&to=100&useSecondaryLanguage=false";

            // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)
            string signature;
            using (var sha = SHA256.Create())
            {
                long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                Console.WriteLine("Timestamp: " + ts);
                var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey + Secret + ts));
                signature = BitConverter.ToString(computedHash).Replace("-", "");
            }

            Console.WriteLine("Signature: " + signature);

            using (var client = new WebClient())
            {
                // Request configuration            
                client.Headers.Add("X-Signature", signature);
                client.Headers.Add("Api-Key", apiKey);
                client.Headers.Add("Accept", "application/xml");

                // Request execution
                string response = client.DownloadString(endpoint);
                //string formattedXml = XElement.Parse(response).ToString();
                //Console.WriteLine(formattedXml);
                ViewBag.Country = response;
                // Debug.WriteLine(response);               
            }


            return View();
        }


        [HttpPost]
        public ActionResult GetCountry()
        {
            string response = "";
            const string apiKey = "4kq3ee27yc2559qja7rjcgpf";
            const string Secret = "SYnpbEKY97";

            const string endpoint = "https://api.test.hotelbeds.com/hotel-content-api/1.0/locations/countries?fields=all&language=ENG&from=1&to=100&useSecondaryLanguage=false";

            // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)
            string signature;
            using (var sha = SHA256.Create())
            {
                long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                Console.WriteLine("Timestamp: " + ts);
                var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey + Secret + ts));
                signature = BitConverter.ToString(computedHash).Replace("-", "");
            }

            Console.WriteLine("Signature: " + signature);

            using (var client = new WebClient())
            {
                // Request configuration            
                client.Headers.Add("X-Signature", signature);
                client.Headers.Add("Api-Key", apiKey);
                client.Headers.Add("Accept", "application/xml");

                // Request XElement.Parse(response).ToString()
                response = client.DownloadString(endpoint);
                string formattedXml = XElement.Parse(response).ToString();
                //Console.WriteLine(formattedXml);
                ViewBag.Country = response;
                // Debug.WriteLine(response);               
            }


            List<CountryModel> country = new List<CountryModel>();

            //XmlDocument xml = new XmlDocument();
            //xml.LoadXml(myXmlString);

            //Load the XML file in XmlDocument.
            //XmlDocument doc1 = new XmlDocument();
            //doc1.Load(Server.MapPath("~/XMLFile1.xml"));

            //XDocument doc2 = XDocument.Load("D:\\XMLFile1.xml"); // Or whatever
            //var allElements = doc2.Descendants();


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response);

            XmlDocument results = new XmlDocument();
            results.LoadXml(response);

            XmlNamespaceManager ns = new XmlNamespaceManager(results.NameTable);
            ns.AddNamespace("ns",
                    "http://www.hotelbeds.com/schemas/messages");

            string Result = results.SelectSingleNode(
            "//ns:countries/ns:country", ns).OuterXml;


            foreach (XmlNode node in results.SelectNodes("//ns:countries/ns:country", ns))
            {
                //Fetch the Node values and assign it to Model.
                country.Add(new CountryModel
                {
                    Name = node["description"].InnerText,
                    CountryCode = node.OuterXml.Split('"')[1]

                });
            }

            //foreach (XmlNode node in results.SelectNodes("//ns:countries", ns))
            //{
            //    //Fetch the Node values and assign it to Model.
            //    country.Add(new CountryModel
            //    {
            //        CountryCode = node["country"].OuterXml.Split('"')[1]

            //    });
            //}


            //Loop through the selected Nodes.
            //XmlNodeList xnList = doc1.SelectNodes("countriesRS/auditData/countries/country");

            //foreach (XmlNode node in doc1.SelectNodes("countries/country"))
            //{
            //    //Fetch the Node values and assign it to Model.
            //    country.Add(new CountryModel
            //    {
            //        Name = node["description"].InnerText
            //        //CountryCode = node["Code"].InnerText

            //    });
            //}

            return View("GetCountry",country);
        }

        [HttpGet]
        public ActionResult GetCountry(string message)
        {
            ViewBag.Country = null;
            return View();
        }

    }
}