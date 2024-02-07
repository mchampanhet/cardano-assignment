using Cardano.Models;
using Cardano.Service;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        Run().Wait();
    }

    private static async Task Run()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();;

        var fileService = new FileService(config["fileService:inputfilePath"], config["fileService:outputFileDirectory"], config["fileService:outputFilename"], config["fileService:archiveFilePath"]);
        var transactions = fileService.GetFile();

        if (transactions == null)
        {
            Console.WriteLine("No input file found");
            return;
        }

        var leiRecords = await new GleifService(config["gleifService:baseUrl"]).GetLeiRecords(transactions);
        new EnrichmentService().EnrichTransactions(transactions, leiRecords);
        fileService.WriteFile(transactions);
    }
}