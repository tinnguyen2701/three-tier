using ApplicationTier.Domain.Entities;
using ApplicationTier.Domain.Entities.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationTier.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        #region CRUD normal
        [HttpGet]
        public async Task<IList<Actor>> GetAll()
        {
            return await _actorService.GetAll();
        }

        [HttpPut]
        public async Task Update(Actor actor)
        {
            await _actorService.Update(actor);
        }

        [HttpGet("{id:int}")]
        public async Task<Actor> GetOne([FromRoute] int id)
        {
            return await _actorService.GetOne(id);
        }

        [HttpPost]
        public async Task Add(Actor actor)
        {
            await _actorService.Add(actor);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _actorService.Delete(id);
        }
        #endregion

    }
}
