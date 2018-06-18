using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.Client.Interfaces;
using DakiHunt.Client.Shared.Bases;

namespace DakiHunt.Client.BL
{
    public class MessageBoxProvider : IMessageBoxProvider
    {
        public Task ShowMessageBox(string title, string content)
        {
            MessageBoxBase.Instance.Title = title;
            MessageBoxBase.Instance.Content = content;
            return MessageBoxBase.Instance.Show();
        }
    }
}
