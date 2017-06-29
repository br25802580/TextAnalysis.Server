using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TextAnalysis.Models
{
    public class ProcessRequest
    {
        public string Text { get; set; }

        public ProcessRequest(string text)
        {
            Text = text;
        }
    }
}