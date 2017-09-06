using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streaming_inż.Models;

namespace streaming_inż.Common
{
    interface IUserRepository
    {
        void UpdateSettings(ApplicationUser user);
    }
}
