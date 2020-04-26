using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nba.Core;

namespace Nba.WebApi.Controllers
{
    [Route("Nba/api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        readonly NbaService _nbaService;

        public TeamsController(IDbSettings settings)
        {
            _nbaService = new NbaService(settings);
        }


        [HttpGet]
        public ActionResult<List<Teams>> Get() =>  _nbaService.GetAll();

        [HttpGet("{id:length(24)}")]
        public ActionResult<Teams> Get(string id) => _nbaService.GetSingle(id);


        [HttpPost]
        public ActionResult<Teams> Create(Teams team) => _nbaService.Create(team);

        [HttpPut("{id:length(24)}")]

        public ActionResult Update(string id, Teams currentTeam)
        {
            var team = _nbaService.GetSingle(id);
            if (team==null)
            {
                return NotFound();
            }
            else
            {
                _nbaService.Update(id, currentTeam);
                return Ok();
            }
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id )
        {
            var team = _nbaService.GetSingle(id);
            if (team == null)
            {
                return NotFound();
            }
            else
            {
                _nbaService.Delete(id);
                return Ok();
            }
        }

    }
}