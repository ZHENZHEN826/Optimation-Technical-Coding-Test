using System;
using System.Collections.Generic;
using System.IO;
using Optimation_Technical_Coding_Test.Models;

namespace Optimation_Technical_Coding_Test.Services
{
    public class ExtractXmlService
    {
        private string _rawText = @"Hi Yvaine,
        Please create an expense claim for the below.Relevant details are marked up as requested…
        <expense>
        <cost_centre>DEV002</cost_centre>
        <total>1024.01</total>
        <payment_method>personal card</payment_method>
        </expense>
        From: Ivan Castle Sent: Friday, 16 February 2018 10:32 AM
        To: Antoine Lloyd <Antoine.Lloyd @example.com>
        Subject: test
        Hi Antoine,
        Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our
        <description>development team’s project end celebration dinner</description> on <date>Tuesday
        27 April 2017</date>. We expect to arrive around 7.15pm.Approximately 12 people but I’ll
        confirm exact numbers closer to the day.
        Regards,
        Ivan";

        private string _rawText2 = @"Hi<cost_centre>DEV002</cost_centre><total>1024.01</total>n";

        private string _rawText3 = @"<total>1024.01</total>";

        public Event _event { get; set; }
        public List<string> _strResult = new List<string>();
        public List<double> _numResult = new List<double>();

        public ExtractXmlService()
        {
            //System.Console.WriteLine(_rawText);
            _event = new Event();
            ExtractXmlData();
            if (_numResult.Count == 0)
            {
                // reject the message
            }
            else
            {
                // calculate gst, excluding gst
                CalculateGst();
            }
            _event.SetFields(_strResult, _numResult);
        }

        public void readText(string text)
        {
            _rawText = text;
        }

        public void ExtractXmlData()
        {
            string[] openingTags = _event.openingTags;
            string[] closingTags = _event.closingTags;

            string[] result = new string[openingTags.Length];

            for (int index = 0; index < openingTags.Length; index++)
            {
                string openingTag = openingTags[index];
                string closingtag = closingTags[index];

                // Now assume tags always exits
                // Need to check the case tag not exits
                int startingIndex = _rawText.IndexOf(openingTag) + openingTag.Length;
                int endingIndex = _rawText.IndexOf(closingtag);
                int infoLength = endingIndex - startingIndex;

                string info = _rawText.Substring(startingIndex, infoLength);

                System.Console.WriteLine(info);

                if (openingTag == "<total>")
                {
                    _numResult.Add(Convert.ToDouble(info));
                }
                else
                {
                    _strResult.Add(info);
                }
            }
        }

        public void CalculateGst()
        {
            double total = _numResult[0];
            double gst = Math.Round(total * 0.15, 2);
            double total_excluding_gst = total - gst;
            _numResult.Add(gst);
            _numResult.Add(total_excluding_gst);
        }

        public Event GetEvent()
        {
            return _event;
        }
    }
}