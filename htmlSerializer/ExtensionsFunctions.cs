using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlSerializer
{
    public static class ExtensionsFunctions
    {
        public static List<HtmlElement> MatchSelector(this HtmlElement htmlElement,
            Selector selector, List<HtmlElement> result)
        {
            if (selector.Child == null)
            {
                result.Add(htmlElement);
                return result;
            }
            bool isEqualClasses = true;
            List<HtmlElement> list = new List<HtmlElement>();
            list = htmlElement.Descendants().ToList();
            foreach (HtmlElement element in list)
            {
                if (selector.Id == element.Id && selector.TagName == element.Name)
                {
                    if (selector.Classes.Count == element.Classes.Count)
                    {
                        for (int i = 0; i < selector.Classes.Count && isEqualClasses; i++)
                        {
                            if (selector.Classes[i] != element.Classes[i])
                                isEqualClasses = false;
                        }
                    }
                    if (isEqualClasses)
                    {
                        result.Add(element);
                        for (int i = 0; i < element.Children.Count; i++)
                          return  MatchSelector(element.Children[i], selector.Child, result);
                    }
                }
            }
        }
    }
}
