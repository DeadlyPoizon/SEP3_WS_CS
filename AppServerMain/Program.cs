using AppServerMain.StockAPI;

namespace AppServerMain;

internal class Program
{
    private static void Main(string[] args)
    {
        var apiController = new APIController();
        Console.WriteLine("done");
    }
}