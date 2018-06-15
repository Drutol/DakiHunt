using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;

namespace DakiHunt.Client.Utils
{
    public class JsonHttpContent : StringContent
    {
        public JsonHttpContent(object content) : base(JsonUtil.Serialize(content),Encoding.UTF8,"application/json")
        {

        }
    }
}
