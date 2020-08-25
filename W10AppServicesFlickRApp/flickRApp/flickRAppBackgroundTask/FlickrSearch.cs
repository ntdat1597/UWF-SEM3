using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

internal class FlickrPhotoResult
{
  public FlickrPhotoResult(XElement photo)
  {
    Id = (long)photo.Attribute("id");
    Secret = photo.Attribute("secret").Value;
    Farm = (int)photo.Attribute("farm");
    Server = (int)photo.Attribute("server");
    Title = photo.Attribute("title").Value;
  }
  public long Id { get; private set; }
  public string Secret { get; private set; }
  public int Farm { get; private set; }
  public string Title { get; private set; }
  public int Server { get; private set; }

  public string ImageUrl
  {
    get
    {
      return (string.Format(
        "http://farm{0}.static.flickr.com/{1}/{2}_{3}_b.jpg",
        Farm, Server, Id, Secret));
    }
  }
}
internal static class FlickrSearcher
{
  public static async Task<List<FlickrPhotoResult>> SearchAsync(string searchTerm)
  {
    HttpClient client = new HttpClient();
    FlickrSearchUrl url = new FlickrSearchUrl(searchTerm);
    List<FlickrPhotoResult> list = new List<FlickrPhotoResult>();

    using (HttpResponseMessage response = await client.GetAsync(url.ToString()))
    {
      if (response.IsSuccessStatusCode)
      {
        using (Stream stream = await response.Content.ReadAsStreamAsync())
        {
          XElement xml = XElement.Load(stream);
          list =
              (
                  from p in xml.DescendantsAndSelf("photo")
                  select new FlickrPhotoResult(p)
              ).ToList();
        }
      }
    }
    return (list);
  }
  public static async Task<List<string>> GetHotlistTagsAsync(string userText)
  {
    HttpClient client = new HttpClient();
    string uri = FlickrSearchUrl.hottagsUri;
    List<string> results = new List<string>();

    using (HttpResponseMessage response = await client.GetAsync(uri))
    {
      if (response.IsSuccessStatusCode)
      {
        using (Stream stream = await response.Content.ReadAsStreamAsync())
        {
          XElement xml = XElement.Load(stream);
          results =
            (
              from tag in xml.DescendantsAndSelf("tag")
              where tag.Value.Contains(userText)
              select tag.Value
            ).ToList();
        }
      }
    }
    return (results.GetRange(0, Math.Min(5, results.Count)));
  }

  #region Internal_Class
  internal class FlickrSearchUrl
  {
//You Need to add an api Key here
    static string apiKey = "356f5f70f41e72466e2fa8a9c6615a7f";
    static string serviceUri = "https://api.flickr.com/services/rest/?method=";
    static string baseUri = serviceUri + "flickr.photos.search&";
    public static readonly string hottagsUri = serviceUri + "flickr.tags.getHotList&api_key=" + apiKey;

    public int ContentType { get; set; }
    public int PerPage { get; set; }
    public int Page { get; set; }
    public string SearchTerm { get; set; }

    public FlickrSearchUrl(
        string searchTerm,
        int pageNo = 1,
        int perPage = 1,
        int contentType = 1
    )
    {
      this.SearchTerm = searchTerm;
      this.Page = pageNo;
      this.PerPage = perPage;
      this.ContentType = contentType;
    }
    public override string ToString()
    {
      return (
        string.Format(
          baseUri +
          "api_key={0}&" +
          "safe_search=1&" +
          "text={1}&" +
          "page={2}&" +
          "per_page={3}&" +
          "content_type={4}",
          apiKey, WebUtility.UrlEncode(this.SearchTerm), this.Page, this.PerPage, this.ContentType));
    }
  }
}
 #endregion