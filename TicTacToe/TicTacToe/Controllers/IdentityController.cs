using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        DBAccess db = new DBAccess();
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public void Post([FromBody]TTTPlayersDB player)
        {
            db.AddToDb(player);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [AccessTokenAuthorize]
        public void Put([FromBody]Token AccessToken)
        {
            db.StartGameWith(AccessToken);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
