using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DakiHunt.Api.Controllers
{
    [ApiController]
    [Authorize]
    [EnableCors("GlobalPolicy")]
    [Route("[controller]/[action]")]
    public class HuntController
    {

    }
}
