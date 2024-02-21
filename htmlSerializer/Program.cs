
using htmlSerializer;
using System.Text.RegularExpressions;
var html = await Load("https://hebrewbooks.org/beis");

var cleanHtml = new Regex("\\s").Replace(html, "");
var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0);

var htmlElement = htmlLines.FirstOrDefault();
//var attributes = new Regex("([^\\s].*?)=\"(.*?)\"").Matches(htmlElement);

//HtmlHelper html1 = new HtmlHelper();
HtmlElement htmlElement1 = new HtmlElement();
HtmlParser parser = new HtmlParser();
htmlElement1 = parser.ParseHtml(html);
Console.WriteLine("99999999999");
Selector selector = new Selector();
selector = Selector.BuildSel("div p.class-name.claas-sari&ruthy");
Console.WriteLine();
//var htmlElement2 ="div id=\"my-id\" class=\"my-class-2 my-class-1\"";
//var attributes = new Regex("([^\\s].*?)=\"(.*?)\"").Matches(htmlElement);
static async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
