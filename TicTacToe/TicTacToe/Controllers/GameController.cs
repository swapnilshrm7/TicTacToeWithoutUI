using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        DBAccess db = new DBAccess();
        // GET: api/Game
        [HttpGet]
        public string Get()
        {
            string status=db.GetStatus();
            return status;
        }

        // GET: api/Game/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Game
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Game/5
        [HttpPut]
        public void Put([FromBody]Token AccessToken)
        {
            db.StartGameWith(AccessToken);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    }
}
