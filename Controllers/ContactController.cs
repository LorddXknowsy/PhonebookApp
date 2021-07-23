using ContactApp.Models;
using ContactApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactApp.Controllers
{
    public class ContactController : Controller
    {
        bool hasID = false;
        [HttpGet]
        public ActionResult GetContacts()
        {
            
            return View();
        }

        [HttpPost]
        public JsonResult GetContactsList()
        {
            var authUser = (User)Session["AuthUser"];
            var contactRepository = new ContactRepository();
            var contacts = contactRepository.GetContacts(authUser.UserName);
            return Json(contacts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveContact(Contact contact)
        {
            hasID = false;
            var authUser = (User)Session["AuthUser"];
            contact.userName = authUser.UserName;
            var contactRepository = new ContactRepository();
            contactRepository.SaveContact(contact);
            return RedirectToAction("GetContactsList");
        }
        [HttpPost]
        public JsonResult DeleteContact(Contact contact)
        {
            var contactRepository = new ContactRepository();
            contactRepository.DeleteContact(contact);
            return Json(contact);
        }
        [HttpPost]
        public JsonResult UpdateContact(Contact contact)
        {
            hasID = true;
            var contactRepository = new ContactRepository();
            contactRepository.UpdateContact(contact);
            return Json(contact);
        }
    }
}