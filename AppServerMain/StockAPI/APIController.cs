using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using Domain.Models;
using GRPCService.LogicImpl;

namespace AppServerMain.StockAPI;

public class APIController
{
    private readonly string FilePath;
    private readonly string KEY = "DMBU4F0ZU7OHSIF6";
    private Uri query;
    private Dictionary<string, string> stocks;

    public APIController()
    {
        var path = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().GetName().CodeBase);
        path = path.Substring(6);
        path = path.Replace('\\', '/');
        path += "/stocks.json";
        FilePath = path;
        
    }

    public void init()
    {
        stocks = initDictionary();
        updateStocks(stocks);
    }

    public async void updateStocks(string symbol)
    {
        var temp = getStockprices(symbol);

        var aktie = new Aktie
        {
            Navn = symbol,
            Firma = stocks[symbol],
            Pris = temp[0],
            High = temp[1],
            Low = temp[2]
        };

        var aktieLogic = new AktieLogic();
        await aktieLogic.updateAktie(aktie);
    }
    
    

    public void updateStocks(Dictionary<string, string> symbols)
    {
        var keys = new List<string>(symbols.Keys);

        for (var i = 0; i < keys.Count; i++) updateStocks(keys[i]);
    }

    public double[] getStockprices(string SYMBOL)
    {
        query = new Uri(
            $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={SYMBOL}&interval=5min&apikey={KEY}&datatype=json");

        using
            (var
             client = new WebClient()) //Advarsel, nedstående kode er det mindst S.O.L.I.D kode vi nogensinde har skrevet
        {
            var stock_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(client.DownloadString(query));
            var temp = stock_data["Time Series (5min)"];
            var stocksToJson = JsonSerializer.Serialize(temp, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            string temp2 = stocksToJson.ToString();
            var first = "{";
            var second = "}";
            var x = temp2.IndexOf(first) + first.Length;
            var a = temp2.IndexOf(first, x) + first.Length;
            var b = temp2.IndexOf(second);
            var final = temp2.Substring(a, b - a);
            Console.WriteLine(final);

            var array = final.Split(",");
            var numbers = new double[3];

            for (var i = 0; i < numbers.Length; i++)
            {
                var match = Regex.Match(array[i], @"[0-9]+\.[0-9]+");
                if (match.Success)
                {
                    var tempdouble = double.Parse(match.Value);
                    numbers[i] = tempdouble / 10000;
                }
            }

            for (var i = 0; i < numbers.Length; i++) Console.WriteLine(numbers[i]);
            return numbers;
        }
    }


    private Dictionary<string, string> initDictionary()
    {
        var firmaer = new Dictionary<string, string>();
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
          firmaer.Add("NVO", "Novo Nordisk");*/
        return firmaer;
    }
}