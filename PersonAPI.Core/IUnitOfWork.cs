using PersonAPI.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        IRelatedPersonRepository RelatedPersonRepository { get; }
        Task<int> SaveAsync();
    }
}
