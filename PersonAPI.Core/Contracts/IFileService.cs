using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Contracts
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(MemoryStream image);
    }
}
