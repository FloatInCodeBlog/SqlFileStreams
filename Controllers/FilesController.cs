using SqlFileStreams.Models;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SqlFileStreams.Controllers
{
    public class FilesController : Controller
    {
        private SqlFileStreamsContext db = new SqlFileStreamsContext();

        public ActionResult Index()
        {
            return View(db.FileModels.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileModel fileModel = db.FileModels.Find(id);
            if (fileModel == null)
            {
                return HttpNotFound();
            }
            return View(fileModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create([Bind(Include = "Title,File")] FileViewModel fileModel)
        {
            if (ModelState.IsValid)
            {
                var fileData = new MemoryStream();
                fileModel.File.InputStream.CopyTo(fileData);

                var file = new FileModel { Title = fileModel.Title, File = fileData.ToArray() };
                db.FileModels.Add(file);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(fileModel);
        }

        [HttpPost]
        [ActionName("CreateStreaming")]
        public ActionResult CreateStreaming([Bind(Include = "Title,File")] FileViewModel fileModel)
        {
            if (ModelState.IsValid)
            {

                var file = new FileModel { Title = fileModel.Title };
                db.FileModels.Add(file);
                db.SaveChanges();

                VarbinaryStream blob = new VarbinaryStream(
                                System.Configuration.ConfigurationManager.ConnectionStrings["SqlFileStreamsContext"].ConnectionString,
                                "Files",
                                "File1",
                                "Id",
                                file.Id);

                Debug.WriteLine("Total length: " + fileModel.File.InputStream.Length);
                fileModel.File.InputStream.CopyTo(blob, 16080);

                return RedirectToAction("Index");
            }

            return View(fileModel);
        }
    }
}
