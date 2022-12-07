using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using Domain.Models;
using Json.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AppServerMain.StockAPI;

public class APIController
{ 
   private readonly string KEY = "DMBU4F0ZU7OHSIF6";
   private readonly string FilePath;
   private Uri query;
   
   public APIController()
   {
      string? path = Path.GetDirectoryName(
           System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        path = path.Substring(6);
        path = path.Replace('\\', '/');
        path += "/stocks.json";
        FilePath = path;
   }

   public void updateStocks(string symbol)
   {
      
   }
   

   public double[] getStockprices(string SYMBOL)
   {
      query = new Uri($"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={SYMBOL}&interval=5min&apikey={KEY}&datatype=json");
    
      using (WebClient client = new WebClient()) //Advarsel, nedstående kode er det mindst S.O.L.I.D kode vi nogensinde har skrevet
      {
         Dictionary<string, dynamic> stock_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(query));
         dynamic temp = stock_data["Time Series (5min)"];
         dynamic stocksToJson = JsonSerializer.Serialize(temp, new JsonSerializerOptions
          {
             WriteIndented = true
          });
         string temp2 = stocksToJson.ToString();
         
         /*string[] tempArray = temp2.Split(',');
         for (int i = 0; i < tempArray.Length; i++)
         {
            if (tempArray[i].Contains("1. open"))
            {
               Console.WriteLine(tempArray[i]);
            }
         }*/
         string first = "{";
         string second = "}";
         int x = temp2.IndexOf(first) + first.Length;
         int a = temp2.IndexOf(first, x) + first.Length;
         int b = temp2.IndexOf(second);
         string final = temp2.Substring(a, b - a);
         Console.WriteLine(final);

         string[] array = final.Split(",");
         double[] numbers = new double[3];

         for (int i = 0; i < numbers.Length; i++)
         {
            var match = Regex.Match(array[i], @"[0-9]+\.[0-9]+");
            if (match.Success)
            {
               double tempdouble = double.Parse(match.Value);
               tempdouble = tempdouble / 10000;
               numbers[i] = tempdouble;
            }
         }

         for (int i = 0; i < numbers.Length; i++)
         {
            Console.WriteLine(numbers[i]);
         }
         return numbers;
         /*
          using (StreamReader r = new StreamReader(FilePath))
          {
             string json = r.ReadToEnd();
            // Console.WriteLine(json);
             List<Aktie> aktier = new List<Aktie>();
  */
         // dynamic array = JsonConvert.DeserializeObject(json);


         /*
         foreach (var index in array)
         {
            aktier.Add(new Aktie()
            {
               Firma = array{1}
            });
         }*/
         //await File.WriteAllTextAsync(FilePath,stocksToJson);
         //StreamReader r = new StreamReader(FilePath);
         //dynamic output = r.ReadToEndAsync();
      }
   }


   private Dictionary<string, string> initDictionary()
   {
      Dictionary<string, string> firmaer = new Dictionary<string, string>();
      firmaer.Add("IBM", "International Business Machines");
      firmaer.Add("MSFT", "Microsoft");
      firmaer.Add("AAPL", "Apple Inc.");
      firmaer.Add("AMZN", "Amazon.com Inc.");
      firmaer.Add("TRMD", "TORM plc. A/S");
    /*  firmaer.Add("GOOG", "Alphabet");
      firmaer.Add("TSLA", "Tesla Inc."); 
      firmaer.Add("NVDA", "Nvidia");
      firmaer.Add("WMT", "Wallmart");
      firmaer.Add("PEP", "PepsiCo");
      firmaer.Add("NVO", "Novo Nordisk");
      firmaer.Add("TSM", "Taiwan Semiconductor Manufacturing Company");*/
      return firmaer;
   }
}  

