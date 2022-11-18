using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AppServerMain.StockAPI;

public class APIController
{ 
   private readonly string KEY = "DMBU4F0ZU7OHSIF6";
   private readonly string FilePath;
   private string SYMBOL = "IBM";
   private Uri query;
   
   public APIController()
   {
      string? path = Path.GetDirectoryName(
           System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        path = path.Substring(6);
        path = path.Replace('\\', '/');
        path += "/stocks.json";
        FilePath = path;
      query = new Uri("https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=IBM&interval=5min&apikey=DMBU4F0ZU7OHSIF6&datatype=json");
   
   init();
   }

  async void init()
   {
      using (WebClient client = new WebClient())
      {
         dynamic? stock_data = JsonSerializer.Deserialize<dynamic>(client.DownloadString(query));
       
        string stocksToJson = JsonSerializer.Serialize(stock_data, new JsonSerializerOptions
        {
           WriteIndented = true
        });
        await File.WriteAllTextAsync(FilePath,stocksToJson);
        StreamReader r = new StreamReader(FilePath);
        dynamic output = r.ReadToEndAsync();
        await Console.WriteLine(output);
      }
      
   }
}  