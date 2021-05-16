using System;
using System.Collections.Generic;
using System.IO;
using Optimation_Technical_Coding_Test.Models;

namespace Optimation_Technical_Coding_Test.Services
{
    public class ExtractXmlService
    {
        private string _rawText = "";
        public Event _event { get; set; }
        public List<string> _strResult = new List<string>();
        public List<double> _numResult = new List<double>();

        public ExtractXmlService()
        {
            _event = new Event();
        }

        public void readText(string text)
        {
            _rawText = text;
        }

        public void updateEvent()
        {
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

        public void ExtractXmlData()
        {
            string[] openingTags = _event.openingTags;
            string[] closingTags = _event.closingTags;

            string[] result = new string[openingTags.Length];

            _strResult.Clear();
            _numResult.Clear();

            for (int index = 0; index < openingTags.Length; index++)
            {
                string openingTag = openingTags[index];
                string closingtag = closingTags[index];

                // Now assume tags always exits
                // Need to check the case tag not exits
                int startingIndex = _rawText.IndexOf(openingTag) + openingTag.Length;
                int endingIndex = _rawText.IndexOf(closingtag);
                int infoLength = endingIndex - startingIndex;

                if (startingIndex >= 0 & endingIndex >= 0 & infoLength >= 0)
                {
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
                else
                {
                    if (openingTag == "<total>")
                    {
                        _numResult.Add(Double.NaN);
                    }
                    else
                    {
                        _strResult.Add("UNKNOWN");
                    }
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
            updateEvent();
            return _event;
        }
    }
}