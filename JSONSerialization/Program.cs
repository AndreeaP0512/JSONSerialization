using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JSONSerialization
{
  internal class Program
  {

    public class Youtuber
    {
      public string Name;
      public string Channel;
      public bool Active;
      public int Age;
      public List<string> Members;
    }

    static async Task Main(string[] args)
    {
      //string json = @"{
      //  'Name': 'Jose Luis',
      //  'Channel': 'ParametricCamp',
      //  'Active': true,
      //  'Age': 3,
      //  'Members': [
      //    'Richard',
      //    'Tim',
      //    'Victor',
      //    'Chandra',
      //    'Andres',
      //    'Nicholas'
      //  ]
      //}";

      // json from file
      string json = System.IO.File.ReadAllText(@"D:\learning\JSONSerialization\stuff.json");

      Youtuber deserialized = JsonConvert.DeserializeObject<Youtuber>(json);
      Console.WriteLine(deserialized.Name);
      Console.WriteLine(deserialized.Channel);
      Console.WriteLine(deserialized.Active);
      Console.WriteLine(deserialized.Age);
      foreach(string member in deserialized.Members) 
      {
        Console.WriteLine("Member: " + member);
      }

      string serialized = JsonConvert.SerializeObject(deserialized);
      Console.WriteLine(serialized);

      //json from url
      string url = "https://my-json-server.typicode.com/typicode/demo/posts";
      //string url with parameter
      //string url = "https://my-json-server.typicode.com/typicode/demo/posts?var=12345";
      HttpClient httpClient = new HttpClient();
      try
      {
        var httpResponseMessage = await httpClient.GetAsync(url);
        string jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
        Console.WriteLine(jsonResponse);

        Post[] myPosts = JsonConvert.DeserializeObject<Post[]>(jsonResponse);
        foreach (Post post in myPosts)
        {
          Console.WriteLine("{0} {1}", post.Id, post.Title);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        httpClient.Dispose();
      }

      Console.ReadKey();
    }
  }
}
