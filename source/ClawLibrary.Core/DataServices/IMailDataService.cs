using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClawLibrary.Core.DataServices
{
    public interface IMailDataService
    {
        Task<string> GetByName(string name);
    }
}
