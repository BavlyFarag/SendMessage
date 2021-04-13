using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovs_SendingMessage.Controllers
{
    public class MobileController : BaseController
    {
        private static int _countRow = 0;
        private readonly static int _sendMessageSize = 10;
        // GET: Mobile
        public ActionResult ViewMobilesInfo()
        {
            var mobiles = MobileInfoService.GetAllMobileInfo().ToList();
            return View(mobiles);
        }
        public ActionResult SendMessage()
        {
            ViewBag.AlertMessage = string.Format("You will Send Message from {0} to {1} mobile number", (_countRow * _sendMessageSize), (_countRow + 1) * _sendMessageSize);
            return View();
        }
        [HttpPost]
        public ActionResult SendMessage(string messageDetails)
        {
            var mobiles = MobileInfoService.GetAllMobileInfo().OrderBy(c => c.Id).Skip(_countRow * _sendMessageSize).Take(_sendMessageSize).ToList();
            mobiles.ForEach(m =>
            {
                MessageLogService.MessageLogAdd(new Innovs.Core.Data.MessageLog { MessageDetails = messageDetails, IsSend = true, MobileInfoId = m.Id, SendedBy = CurrentUser().Id, SendingDate = DateTime.Now });
            });
            _countRow += 1;
            return RedirectToAction("ViewMobileMessageLog");
        }

        public ActionResult ViewMobileMessageLog()
        {
            var allMessages = MessageLogService.GetAllMessageLog().OrderByDescending(c => c.Id).ToList();
            return View(allMessages);
        }
    }
}