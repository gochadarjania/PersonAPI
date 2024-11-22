using PersonAPI.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Contracts.Repositories
{
    public interface IRelatedPersonRepository : IBaseRepository<RelatedPersonEntity>
    {
    }
}
