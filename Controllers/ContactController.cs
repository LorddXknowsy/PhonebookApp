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
        private bool IsAuth()
        {
            if (Session["AuthUser"] != null) return true;
            else return false;
        }

        [HttpGet]
        public ActionResult GetContacts()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetContactsList()
        {
            if (!IsAuth()) return new HttpUnauthorizedResult();
            else
            {
                var authUser = (User)Session["AuthUser"];
                var contactRepository = new ContactRepository();
                var contacts = contactRepository.GetContacts(authUser.UserName);
                return Json(contacts, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveContact(Contact contact)
        {
            if (!IsAuth()) return new HttpUnauthorizedResult();
            else
            {
                var authUser = (User)Session["AuthUser"];
                contact.userName = authUser.UserName;
                var contactRepository = new ContactRepository();
                contactRepository.SaveContact(contact);

                return RedirectToAction("GetContactsList");
            }
        }
        [HttpPost]
        public ActionResult DeleteContact(Contact contact)
        {
            if (!IsAuth()) return new HttpUnauthorizedResult();
            else
            {
                var contactRepository = new ContactRepository();
                contactRepository.DeleteContact(contact);
                return Json(contact);
            }
        }
        [HttpPost]
        public ActionResult UpdateContact(Contact contact)
        {
            if (!IsAuth()) return new HttpUnauthorizedResult();
            else
            {
                var contactRepository = new ContactRepository();
                contactRepository.UpdateContact(contact);
                return Json(contact);
            }
        }
    }
}