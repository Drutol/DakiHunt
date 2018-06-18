using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakiHunt.Client.Interfaces
{
    public interface IMessageBoxProvider
    {
        Task ShowMessageBox(string title, string content);
    }
}
