using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace DakiHunt.ApiUtils
{
    public static class HtmlAgilityPackExtensions
    {
        public static bool HasAttribute(this HtmlNode node, string attribute, string value)
        {
            return node.Attributes.Contains(attribute) && node.Attributes[attribute].Value == value;
        }
    }
}
