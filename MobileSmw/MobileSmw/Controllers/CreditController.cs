using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using Model.Dtos;

namespace MobileSmw.Controllers
{
    public class CreditController : BaseController
    {
        private readonly ICreditService _creditService;

        public CreditController(ICreditService creditService)
        {
            _creditService = creditService;
        }

        public ActionResult CreditApply()
        {
            return View();
        }

        /// <summary>
        /// 申请信用购
        /// </summary>
        /// <param name="model" />
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreditApply(CreditApplyModifyModel model)
        {
            model.Uid = CMemberInfo.MemberId;
             _creditService.InsertCreditApply(model);
            return RedirectToAction("Index", "Home");
        }
    }
}