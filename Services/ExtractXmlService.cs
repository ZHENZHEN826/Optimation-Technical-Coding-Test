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
            ExtractXmlData(); // update _strResult, _numResult
            ErrorHandling(); // check if request is valid
            CalculateGst(); // calculate gst, excluding gst
            _event.SetFields(_strResult, _numResult); // update _event
        }

        public void ExtractXmlData()
        {
            string[] openingTags = _event.openingTags;
            string[] closingTags = _event.closingTags;

            _strResult.Clear();
            _numResult.Clear();
            _event.IsValid = true;

            for (int index = 0; index < openingTags.Length; index++)
            {
                string openingTag = openingTags[index];
                string closingtag = closingTags[index];

                int start = _rawText.IndexOf(openingTag);
                int startingIndex = start + openingTag.Length;
                int endingIndex = _rawText.IndexOf(closingtag);
                int infoLength = endingIndex - startingIndex;

                if ((start >= 0 & endingIndex < 0) | (start < 0 & endingIndex >= 0))
                {
                    // opening/closing tags not match
                    _event.IsValid = false;
                    _event.ValidityMessage = "Error: Opening and Closing tags should match.";
                }

                if (startingIndex >= 0 & endingIndex >= 0 & infoLength >= 0)
                {
                    string info = _rawText.Substring(startingIndex, infoLength);
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

        public void ErrorHandling()
        {
            if (Double.IsNaN(_numResult[0]))
            {
                // <Total> tag is not found, reject the message
                _event.IsValid = false;
                _event.ValidityMessage = "Error: Missing <total> tag.";
            }

            if (_event.IsValid)
            {
                _event.ValidityMessage = "Success!";
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