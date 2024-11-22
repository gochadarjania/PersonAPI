using AutoMapper;
using PersonAPI.Core.Contracts.Repositories;
using PersonAPI.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Infrastructure.Repositories
{
    public class RelatedPersonRepository : BaseRepository<RelatedPersonEntity>, IRelatedPersonRepository
    {
        public RelatedPersonRepository(PersonDbContext db, IMapper mapper) : base(db, mapper)
        {
        }
    }
}
