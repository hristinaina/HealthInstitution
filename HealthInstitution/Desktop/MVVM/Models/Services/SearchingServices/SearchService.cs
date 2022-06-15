using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services.SearchingServices
{
    class SearchService
    {
        protected bool isMatching(string first, string second)
        {
            return second != "" && first.ToLower().Contains(second.ToLower());
        }
    }
}
