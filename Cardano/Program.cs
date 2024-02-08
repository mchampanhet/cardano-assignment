using Cardano.Services;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        Run().Wait();
    }

    private static async Task Run()
    {
        try
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build(); ;
            var fileService = new FileService(config["fileService:inputfilePath"], config["fileService:outputFileDirectory"], config["fileService:outputFilename"], config["fileService:archiveFileDirectory"], config["fileService:archiveFilename"]);
            var transactions = fileService.GetTransactions();

            if (transactions == null)
            {
                LoggerService.LogError("No transactions found");
                return;
            }

            var leiRecords = await new GleifService(config["gleifService:baseUrl"]).GetLeiRecords(transactions);
            EnrichmentService.EnrichTransactions(transactions, leiRecords);
            fileService.CreateEnrichedFile(transactions);
        }
        catch (Exception ex)
        {
            LoggerService.LogError("There was an exception during the enrichment process", ex);
            throw;
        }
    }
}