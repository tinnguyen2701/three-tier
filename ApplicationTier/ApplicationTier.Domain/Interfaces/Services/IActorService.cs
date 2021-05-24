using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTier.Domain.Entities.Services
{
    public interface IActorService
    {
        Task<IList<Actor>> GetAll();
        Task<Actor> GetOne(int id);
        Task Update(Actor actorInput);
        Task Add(Actor actorInput);
        Task Delete(int id);
    }
}
