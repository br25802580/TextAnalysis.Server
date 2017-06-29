using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TextAnalysis.BL;
using TextAnalysis.Models;

namespace TextAnalysis.Controllers
{
    public class ProcessController : ApiController
    {
        /// <summary>
        /// Divide text into sentences, using separators like a dot
        /// Rest url: POST api/process
        /// </summary>
        /// <param name="request">Request model contains input string</param>
        /// <returns>Sentences generated from text processing</returns>
        public IHttpActionResult Post([FromBody] ProcessRequest request)
        {
            IList<string> senteceList = new ProcessBL().Process(request.Text);

            return Ok(senteceList);
        }
    }
}
