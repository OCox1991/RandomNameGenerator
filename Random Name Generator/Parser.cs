using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Random_Name_Generator
{
    internal class Parser
    {
        List<String> startParts;
        List<String> endParts;

        internal Parser(String fileLoc)
        {
            XElement doc = XElement.Load(fileLoc);
            //Need namespace since it is included in the XName that is used to check
            //equality by the .Descendants() method.
            XNamespace ns = "http://tempuri.org/NamePartsSchema.xsd";
            IEnumerable<XElement> elements = doc.Descendants(ns + "part");
            startParts = new List<string>();
            endParts = new List<string>();
            foreach (var element in elements)
            {
                if (element.Parent.Name == ns + "startpart")
                {
                    startParts.Add(element.Value);
                }
                else if(element.Parent.Name == ns + "endpart")
                {
                    endParts.Add(element.Value);
                }
            }
        }

        internal List<String> getStartParts()
        {
            return startParts;
        }

        internal List<String> getEndParts()
        {
            return endParts;
        }
    }
}
