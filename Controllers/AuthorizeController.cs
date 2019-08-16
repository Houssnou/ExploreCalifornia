using ExploreCalifornia.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExploreCalifornia.Controllers
{
    public class AuthorizeController : ApiController
    {
        public IHttpActionResult Post(AuthorizeRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}
