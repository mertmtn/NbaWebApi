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

        /// <summary>
        /// Bütün kayıtları getirir.
        /// </summary>
        /// <returns><see cref="Teams"/> tipinde bir takım listesini döner.</returns>
        [HttpGet]
        public ActionResult<List<Teams>> Get() =>  _nbaService.GetAll();

        /// <summary>
        /// Verilen id değerindeki kaydı getirir.
        /// </summary>
        /// <param name="id">Takımın id değeri</param>
        /// <returns><see cref="Teams"/> tipinde takım döner.</returns>
        [HttpGet("{id:length(24)}")]
        public ActionResult<Teams> Get(string id) => _nbaService.GetSingle(id);

        /// <summary>
        /// Yeni takım oluşturur.
        /// </summary>
        /// <param name="team">Yeni bir takım nesnesi</param> 
        [HttpPost]
        public ActionResult<Teams> Create(Teams team) => _nbaService.Create(team);

        /// <summary>
        /// Verilen id değerindeki kaydı günceller.
        /// </summary>
        /// <param name="id">Takımın id değeri</param>
        /// <param name="currentTeam">Güncellenecek kayıt</param> 
        /// <returns>Kayıt yoksa <see cref="NotFoundResult"/> döner. Başarılıysa <see cref="OkResult"/> döner.</returns>
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

        /// <summary>
        /// Verilen id değerindeki kaydı siler.
        /// </summary>
        /// <param name="id">Takımın id değeri</param>
        /// <returns>Kayıt yoksa <see cref="NotFoundResult"/> döner. Başarılıysa <see cref="OkResult"/> döner.</returns>
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