using System.Linq;
using System.Text.RegularExpressions;
namespace htmlSerializer
{

    public class HtmlParser
    {
        private HtmlHelper htmlHelper;

        public HtmlParser()
        {
            htmlHelper = HtmlHelper.Instance;
        }

        public HtmlElement ParseHtml(string htmlString)
        {
            string id,cla;
            var cleanHtml = new Regex("\\s+").Replace(htmlString, " ");
            var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0);
            HtmlElement rootElement = new HtmlElement();
            HtmlElement currentElement = rootElement;

            //  List<string> htmlLines = htmlString.Split('\n').Select(line => line.Trim()).ToList();

            foreach (string line in htmlLines)
            {
                string[] tagName = line.Split(" ");
                if (line.StartsWith("/html"))
                {
                    // End of HTML
                    break;
                }
                else if (line.StartsWith("/"))
                {
                    // Closing tag
                    //if (currentElement.Parent != null)
                    currentElement = currentElement.Parent;
                }

                else


                    if (htmlHelper.LablesWithClosure.Contains(tagName[0]) || htmlHelper.LablesWithoutClosure.Contains(tagName[0]))
                {
                    HtmlElement htmlElement = new HtmlElement();
                    htmlElement.Name = tagName[0];
                    currentElement.Children.Add(htmlElement);
                    htmlElement.Parent = currentElement;
                    if (line.IndexOf(" ", 0) != -1)
                    {
                        int index;
                        string attribute;
                        attribute = line.Substring(line.IndexOf(" ", 0));

                        var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(attribute);
                        string result="";
                        foreach (var att in attributes)
                        {
                           foreach(var attr in att.ToString())
                            {
                                if (!(attr.Equals('\\') || attr.Equals('"')))
                                    result += attr;
                            }
                            currentElement.Attributes.Add(result);
                           
                            //if (att.ToString().IndexOf("id=", 0) != -1)
                            //{
                            //    index = att.ToString().IndexOf("id=", 0);
                            //    currentElement.Id = att.ToString().Substring(index+3);
                            //}
                            //if (att.ToString().IndexOf("class=", 0) != -1)
                            //{

                            //}

                        }
                        if (currentElement.Attributes.Count > 0)
                        {
                            id = currentElement.Attributes.Find((f) => f.Contains("id="));
                            if (id != null)
                                currentElement.Id = id.Substring(3);
                            cla=currentElement.Attributes.Find((f) => f.Contains("class="));
                            if(cla!= null)
                            {
                                cla = cla.Substring(6);
                                string[] s = cla.Split(" ");
                                foreach(string s2 in s)
                                {
                                    currentElement.Classes.Add(s2);
                                }
                            }

                        }

                    }
                    if (!(line.EndsWith("/") && htmlHelper.LablesWithoutClosure.Contains(line)))
                    {
                        currentElement = htmlElement;
                    }
                }



                else
                {
                    currentElement.InnerHtml = line;

                }





























                //    if (IsValidTag(line))
                //{
                //    string[] tagParts = Regex.Split(line, @"\s+");
                //    string tagName = tagParts[0];
                //    Console.WriteLine("line");
                //    HtmlElement newElement = new HtmlElement
                //    {
                //        Name = tagName,
                //        Attributes = tagParts.Skip(1).ToList(),
                //        Parent = currentElement
                //    };
                //    if (line.StartsWith("/html"))
                //    {
                //        // End of HTML
                //        break;
                //    }
                //    else if (line.StartsWith("/"))
                //    {
                //        // Closing tag
                //        currentElement = currentElement.Parent;
                //    }
                //    if (htmlHelper.LablesWithClosure.Contains(tagName) && !line.EndsWith("/"))
                //    {
                //        // Tag requires closure and is not self-closing
                //        currentElement.Children.Add(newElement);
                //        currentElement = newElement;
                //    }
                //    else
                //    {
                //        // Self-closing tag or tag does not require closure
                //        currentElement.Children.Add(newElement);
                //    }

                //    // Check for InnerHtml
                //    if (!line.EndsWith("/") && !htmlHelper.LablesWithoutClosure.Contains(tagName))
                //    {
                //        // Non-self-closing tag, parse InnerHtml
                //        newElement.InnerHtml = ParseInnerHtml(line);
                //    }
                //}
            }

            return rootElement;
        }

        private bool IsValidTag(string line)
        {
            // Check if the line starts with a valid HTML tag
            string[] tagParts = Regex.Split(line, @"\s+");
            string tagName = tagParts[0].Trim('/');

            return htmlHelper.LablesWithClosure.Contains(tagName) || htmlHelper.LablesWithoutClosure.Contains(tagName);
        }

        private string ParseInnerHtml(string line)
        {
            // Extract the content between the opening and closing tags
            int startIndex = line.IndexOf('>') + 1;
            int endIndex = line.LastIndexOf('<');

            return startIndex >= 0 && endIndex > startIndex ? line.Substring(startIndex, endIndex - startIndex) : "";
        }
    }




}
