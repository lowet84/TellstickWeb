using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TellstickCore.Helper;
using TellstickCore.Models;

namespace TellstickWeb.ApiControllers
{
    public class InvokeController : ApiController
    {
        // GET api/<controller>
        public void Get(int id, int method)
        {
            foreach (var item in TellstickHelper.Instance.InputActions.Where(d => d.DeviceId == id))
            {
                TellstickHelper.Instance.InvokeAction(item.ActionId, item.ActionType, item.ActionParameter == -1 ? method : item.ActionParameter);
            }
        }

        // POST api/<controller>
        public void Post(int id, int method)
        {
            foreach (var item in TellstickHelper.Instance.InputActions.Where(d => d.DeviceId == id))
            {
                TellstickHelper.Instance.InvokeAction(item.ActionId, item.ActionType, item.ActionParameter == -1 ? method : item.ActionParameter);
            }
        }
    }
}
