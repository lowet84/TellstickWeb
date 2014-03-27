﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TellstickWeb.Helper;
using TellstickWeb.Models;

namespace TellstickWeb.ApiControllers
{
    public class BasicController : ApiController
    {
        
        // GET api/<controller>
        public IEnumerable<TellstickDevice> Get()
        {
            return TellstickHelper.GetDevices();

        }

        // GET api/<controller>/5
        public TellstickDevice Get(int id)
        {
            return TellstickHelper.GetDevices().Where(l => l.id == id).SingleOrDefault();
        }

        // POST api/<controller>
        public void Post(int id)
        {
            TellstickHelper.Turn(id,true);
        }

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            TellstickHelper.Turn(id, false);
        }
    }
}