using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Optimation_Technical_Coding_Test.Models;
using Optimation_Technical_Coding_Test.Services;

namespace Optimation_Technical_Coding_Test.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public ExtractXmlService _xmlService;
        public Event _event;

        public IndexModel(ILogger<IndexModel> logger, ExtractXmlService xmlService)
        {
            _logger = logger;
            _xmlService = xmlService;
        }

        public void OnGet()
        {
            // _event = _xmlService.GetEvent();
        }
    }
}
