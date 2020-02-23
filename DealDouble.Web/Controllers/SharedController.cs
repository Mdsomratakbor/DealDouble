using DealDouble.Entities;
using DealDouble.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared

        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            List<object> pictuerJSON = new List<object>();
            try
            {
                var pictures = Request.Files;
                for (var i = 0; i < pictures.Count; i++)
                {
                    var picture = pictures[i];
                    var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                 
                    picture.SaveAs(path);
                    var dbPictuer = new Picture();
                    dbPictuer.URL = fileName;
                    int pictureID =  SharedService.Instance.SavePicture(dbPictuer);
                    pictuerJSON.Add(new {ID = pictureID, pictureURL = dbPictuer.URL });
                }
                result.Data = new { Success = true, pictuerJSON };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;
        }
    }
}