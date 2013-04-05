﻿using System.Collections.Generic;
using System.Web.Http;

namespace Ewk.BandWebsite.Web.API.Controllers
{
    public class DummyController : ApiController
    {
        // GET api/dummy
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/dummy/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/dummy
        public void Post([FromBody]string value)
        {
        }

        // PUT api/dummy/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/dummy/5
        public void Delete(int id)
        {
        }
    }
}