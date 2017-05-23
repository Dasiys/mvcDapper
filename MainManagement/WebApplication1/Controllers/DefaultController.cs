using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IBLL;
using Model;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IContactService _contactService;
        public DefaultController(IContactService contactService)
        {
            _contactService = contactService;
        }

       // [Route("ddd/12.html")]
        // GET: Default
        public ActionResult Index()
        {
           //List<Contact> modelList= _contactService.GetModels(new Contact() { ID = 0 });

            PageCriteria criteria = new PageCriteria();

            criteria.TableName = "Contact";
            criteria.Fields = "*";
            criteria.PrimaryKey = "ContactID";
            criteria.CurrentPage = 1;
            criteria.PageSize = 5;
            criteria.Sort = "ID desc ";
            criteria.Condition = " ID>0";

            PageDataView<Contact> pageList = _contactService.GetModelsByPage(criteria);
            List<Contact> modelList = pageList.Items;

            ViewBag.TotalNum = pageList.TotalNum;
            ViewBag.CurrentPage = pageList.CurrentPage;
            ViewBag.TotalPageCount = pageList.TotalPageCount;

            return View(modelList);
        }

        public ActionResult Delete(int id)
        {
            _contactService.Delete(new Contact() { ID = id});

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