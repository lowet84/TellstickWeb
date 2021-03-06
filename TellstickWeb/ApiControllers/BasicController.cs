﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TellstickCore.Helper;
using TellstickCore.Models;

namespace TellstickWeb.ApiControllers
{
    public class BasicController : ApiController
    {
        
        // GET api/<controller>
        public Options Get()
        {
            return TellstickHelper.Instance.Options;

        }

        // GET api/<controller>/5
        public TellstickDevice Get(int id)
        {
            return TellstickHelper.Instance.Devices.Where(l => l.Id == id).SingleOrDefault();
        }

        // POST api/<controller>
        public void Post(int id)
        {
            TellstickHelper.Instance.Turn((int?)id, true);
        }

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            TellstickHelper.Instance.Turn((int?)id, false);
        }
    }
}