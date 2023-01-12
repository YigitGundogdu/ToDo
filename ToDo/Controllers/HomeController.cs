using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using ToDo.Models;
using static System.Net.WebRequestMethods;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {   
        

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult show_list()
        {
            todoEntities db = new todoEntities();
            ViewBag.List = db.List;
            return View();
        }
        
        public ActionResult show_done()
        {
            todoEntities db = new todoEntities();
            ViewBag.Done = db.Done;
            return View();

        }


        public ActionResult show_in_progress()
        {
            todoEntities db = new todoEntities();
            ViewBag.Progress = db.Progress;
            return View();
        }



        [HttpPost]
        public ActionResult Save(List liste)
        {
            todoEntities db = new todoEntities ();
            db.List.Add(liste);
            db.SaveChanges();
            return RedirectToAction("show_list");
        }




        [HttpPost]
        public ActionResult Edit(List edit_liste)
        {
            todoEntities db = new todoEntities();
            List Saved_db = db.List.FirstOrDefault(x => x.id == edit_liste.id);
            Saved_db.title=edit_liste.title;
            Saved_db.description = edit_liste.description;

            db.SaveChanges();
            return RedirectToAction("show_list");
        }




        [HttpPost]
        public ActionResult Progress_Edit(List edit_liste)
        {
            todoEntities db = new todoEntities();
            var Saved_db = db.Progress.FirstOrDefault(x => x.id == edit_liste.id);
            Saved_db.title = edit_liste.title;
            Saved_db.description = edit_liste.description;

            db.SaveChanges();
            return RedirectToAction("show_in_progress");
        }

        [HttpPost]
        public ActionResult Done_Edit(List edit_liste)
        {
            todoEntities db = new todoEntities();
            var Saved_db = db.Done.FirstOrDefault(x => x.id == edit_liste.id);
            Saved_db.title = edit_liste.title;
            Saved_db.description = edit_liste.description;

            db.SaveChanges();
            return RedirectToAction("show_done");
        }
        public ActionResult Done_Remove(int id)
        {
            todoEntities db = new todoEntities();

            var model = db.Done.FirstOrDefault(x => x.id == id);
            db.Done.Remove(model);
            db.SaveChanges();
            return RedirectToAction("show_done");
        }



        public ActionResult Remove(int id)
        {
            todoEntities db = new todoEntities();
            List Removed_data=db.List.FirstOrDefault(x => x.id == id);
            db.List.Remove(Removed_data);
            db.SaveChanges();
            return RedirectToAction("show_list");
        }

        public ActionResult Progress_Remove(int id)
        {
            todoEntities db = new todoEntities();

            var model = db.Progress.FirstOrDefault(x => x.id == id);
            db.Progress.Remove(model);
            db.SaveChanges();
            return RedirectToAction("show_in_progress");
        }

        public ActionResult Send_In_Progress(int id)
        {
            todoEntities db = new todoEntities();
            List Send_data = db.List.FirstOrDefault(x => x.id == id);
            var model = new Progress { description = Send_data.description, id = Send_data.id, title = Send_data.title };
            db.Progress.Add(model);
            db.List.Remove(Send_data);
            db.SaveChanges();
            return RedirectToAction("show_list");
        }

        public ActionResult Send_do_to_done(int id)
        {
            todoEntities db = new todoEntities();
            List Send_data = db.List.FirstOrDefault(x => x.id == id);
            var model = new Done { description = Send_data.description, id = Send_data.id, title = Send_data.title };
            db.Done.Add(model);
            db.List.Remove(Send_data);
            db.SaveChanges();
            return RedirectToAction("show_list");
        }

        public ActionResult Send_Done(int id)
        {
            todoEntities db = new todoEntities();
            var model = db.Progress.FirstOrDefault(x => x.id == id);
            var done_model=new Done {id=model.id,title=model.title,description=model.description};
            db.Done.Add(done_model);
            db.Progress.Remove(model);
            db.SaveChanges();
            return RedirectToAction("show_in_progress");
        }
        public ActionResult News()
        {
            /*
            string Baseurl = "http://comp.eng.ankara.edu.tr/";
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("category/duyuru/");
                //Checking the response is successful or not which is sent using HttpClient

                    //Storing the response details recieved from web api
                    var New = Res.Content.ReadAsStringAsync().Result.ToString();
                    var jsonstring = new JavaScriptSerializer().Serialize(New);
                //Deserializing the response recieved from web api and storing into the Employee list
                //var NewInfo = JsonConvert.DeserializeObject(New);

                //returning the employee list to view
                return View(jsonstring);
            
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://comp.eng.ankara.edu.tr/category/duyuru/");
            string data = await response.Content.ReadAsStringAsync();
            var doc = XDocument.Parse(data);
            var href = doc.Element("meta").Attribute("property").Value;
            return View(href);

            */


            /*
            string link = "http://comp.eng.ankara.edu.tr/2022/12/27";  


            Uri url = new Uri(link); //Uri tipinde değişeken linkimizi veriyoruz.

            WebClient client = new WebClient(); 
            client.Encoding = Encoding.UTF8; 

            string html = client.DownloadString(url);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument(); 
            document.LoadHtml(html);//documunt değişkeninin html ine çektiğimiz htmli veriyoruz

            //var secilenhtml = @"//*[@id=""posts-container""]"; 
            //veriyi atacağımız stringbuilder 
            var divs = document.DocumentNode
                  .SelectNodes("//posts-container");

            return View();
            */
            //*[@id="posts-container"]
            /// html / body / div[1] / div[2] / main / div / section / div / div[1] / article[10] / div[2] / h2 / a
            ///  return View();
            ///  
            var url = "https://newsapi.org/v2/everything?q=cybersecurity&sortBy=popularity&apiKey=97da344f557e483f8e64c69bc0b256e2";

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string json = client.DownloadString(url);

            var deserialized = JsonConvert.DeserializeObject<RootJson>(json);
        
            return View(deserialized);
        }
 

    }
}