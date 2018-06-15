using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;

namespace DakiHunt.Client.Utils
{
    public static class HttpUtils
    {
        public static async Task<T> ReadContent<T>(this HttpResponseMessage response)
        {
            return JsonUtil.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
