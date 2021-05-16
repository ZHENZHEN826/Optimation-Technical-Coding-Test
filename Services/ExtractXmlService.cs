using System.IO;
using Optimation_Technical_Coding_Test.Models;

namespace Optimation_Technical_Coding_Test.Services
{
    public class ExtractXmlService
    {
        public ExtractXmlService()
        {
            System.Console.WriteLine("Extract XML Service in progress!");
        }

        public Event GetEvent()
        {
            return new Event("q", "w", "e", "r", "t", 1, 2, 3);
        }
    }
}