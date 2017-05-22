using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using Model;
using Common;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        private IContactService _contactService;
        public DefaultController(IContactService contactService)
        {
            _contactService = contactService;
        }

       // [Route("ddd/12.html")]
        // GET: Default
        public ActionResult Index()
        {
           //List<Contact> modelList= _contactService.GetModels(new Contact() { ID = 0 });

            PageCriteria Criteria = new PageCriteria();

            Criteria.TableName = "Contact";
            Criteria.Fields = "*";
            Criteria.PrimaryKey = "ContactID";
            Criteria.CurrentPage = 1;
            Criteria.PageSize = 5;
            Criteria.Sort = "ID desc ";
            Criteria.Condition = " ID>0";

            PageDataView<Contact> PageList = _contactService.GetModelsByPage(Criteria);
            List<Contact> modelList = PageList.Items;

            ViewBag.TotalNum = PageList.TotalNum;
            ViewBag.CurrentPage = PageList.CurrentPage;
            ViewBag.TotalPageCount = PageList.TotalPageCount;

            return View(modelList);
        }

        public ActionResult Delete(Guid id)
        {
            _contactService.Delete(new Contact() { ContactID = id});

            return RedirectToAction("Index", "Default");
        }


        public ActionResult Create()
        {
            //if (!string.IsNullOrEmpty(model.Tel))
            //{
            //    model.ContactID = Guid.NewGuid();
            //    _contactService.Insert(model);

            //    Response.Redirect("/Default/Index");
            //}

            return View();
        }

        [HttpPost]
        public ActionResult Create(Contact model)
        {

            //ModelState.AddModelError("", "1111");
            if (ModelState.IsValid)
            {
                //if (!string.IsNullOrEmpty(model.Tel))
                //{
                model.ContactID = Guid.NewGuid();

                //_contactService.Insert(model);

                _contactService.BeginTransactionInsert(model);
                return RedirectToAction("Index");
                //}
            }
            ViewBag.error = "错误提示";
            return View();
        }

        public ActionResult Edit(Guid ID,string Tel)
        {
            Contact mode = _contactService.GetSingleModel(new Contact() { ContactID=ID });
            return View(mode);
        }

        [HttpPost]
        public ActionResult Edit(Contact model)
        {
            if (!string.IsNullOrEmpty(model.Tel))
                _contactService.Update(model);

            return RedirectToAction("Index", "Default");
        }
    }
}