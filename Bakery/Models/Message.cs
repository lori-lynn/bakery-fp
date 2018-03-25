using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bakery.Models
{
    public class Message
    {
        public Message()
        {
            this.TitleText = "Result";
        }
        public string MessageText { get; set; }

        public string TitleText { get; set; }
    }
}