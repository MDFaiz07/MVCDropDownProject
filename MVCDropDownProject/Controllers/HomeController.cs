using MVCDropDownProject.DBContext;
using MVCDropDownProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDropDownProject.Controllers
{
    public class HomeController : Controller
    {
        DropDownEntities dbobj = new DropDownEntities();
        public ActionResult Index()
        {
           
            List<UserModel> mobj = new List<UserModel>();
            var res = dbobj.Users.ToList();
            foreach (var item in res)
            {
                mobj.Add(new UserModel
                {
                    UserId = item.UserId,
                    UFirstName = item.UFirstName,
                    ULastName = item.ULastName,
                    email = item.email,
                    Mobile = item.Mobile,
                    CName=item.Country.CName,
                    SName=item.State.SName
                });
            }

            return View(mobj);
          
        }

        [HttpGet]
        public ActionResult UserForm()
        {
           
            var cityList = dbobj.Countries.ToList();
            ViewBag.CityList = new SelectList(cityList, "CID", "CName");
            var stateList = dbobj.States.ToList();
            ViewBag.StateList = new SelectList(stateList, "SID", "SName");
            //ViewBag.StateList = new SelectList(Enumerable.Empty<SelectListItem>());


            return View();
        }
        [HttpPost]
        public ActionResult UserForm(UserModel mobj)
        {
            
            User tobj = new User();
            tobj.UserId = mobj.UserId;
            tobj.UFirstName = mobj.UFirstName;
            tobj.ULastName = mobj.ULastName;
            tobj.email = mobj.email;
            tobj.Mobile = mobj.Mobile;
            tobj.CID = mobj.CID;
            tobj.SID = mobj.SID;
            if (mobj.UserId == 0)
            {
                dbobj.Users.Add(tobj);
                dbobj.SaveChanges();
            }
            else
            {
                dbobj.Entry(tobj).State = System.Data.Entity.EntityState.Modified;
                dbobj.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult edit(int UserId)
        {
           UserModel mobj = new UserModel();
          
            var eobj = dbobj.Users.Where(m => m.UserId == UserId).First();
            mobj.UserId = eobj.UserId;
            mobj.UFirstName = eobj.UFirstName;
            mobj.ULastName = eobj.ULastName;
            mobj.email = eobj.email;
            mobj.Mobile = eobj.Mobile;
            mobj.CID = eobj.CID;
            mobj.SID = eobj.SID;
           
            var cityList = dbobj.Countries.ToList();
            ViewBag.CityList = new SelectList(cityList, "CID", "CName");
            var stateList = dbobj.States.ToList();

            ViewBag.StateList = new SelectList(stateList, "SID", "SName");
            return View("UserForm", mobj);
        }
      
        public ActionResult delete(int UserId)
        {  
            var ditem = dbobj.Users.Where(m => m.UserId == UserId).First();
            dbobj.Users.Remove(ditem);
            dbobj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        
        public ActionResult FillClass(int CID)
        {
            var classes = new SelectList(dbobj.States.Where(c => c.CID == CID), "SID", "SName");

            return Json(classes, JsonRequestBehavior.AllowGet);
        }


    }

}
