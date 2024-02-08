using Cardano.Maps;
using Cardano.Models;
using CsvHelper;
using System.Globalization;

namespace Cardano.Services
{
    internal class FileService
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private readonly string _outputFileName;
        private readonly string _archiveFileDirectory;
        private readonly string _archiveFilename;

        public FileService(string inputFilePath, string outputFilePath, string outputFileName, string archiveFileDirectory, string archiveFilename)
        {
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;
            _outputFileName = outputFileName;
            _archiveFileDirectory = archiveFileDirectory;
            _archiveFilename = archiveFilename;
        }

        public List<Transaction> GetTransactions()
        {
            try
            {
                using (var reader = new StreamReader(_inputFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    //csv.HeaderRecord
                    csv.Context.RegisterClassMap<TransactionFileReadMap>();
                    var records = csv.GetRecords<Transaction>().ToList();
                    return records;
                }
            }
            catch (FileNotFoundException)
            {
                LoggerService.LogError("No file found matching inputFilePath");
                return null;
            }
        }

        public void CreateEnrichedFile(IEnumerable<Transaction> transactions)
        {
            // make sure output directory exists
            if (!Directory.Exists(_outputFilePath))
            {
                Directory.CreateDirectory(_outputFilePath);
            }

            // I could use a Guid to stamp the enriched file and the original file, showing that they are linked
            // but a datetime seems more human-readable than a Guid
            var fileId = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var newFilePath = _outputFilePath + fileId + _outputFileName;

            // Create a new file with the enriched data, so as to not alter/destroy the original input file
            using (var writer = new StreamWriter(newFilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<TransactionFileWriteMap>();
                csv.WriteRecords(transactions);
            }

            LoggerService.LogInfo("Enriched file created: " + newFilePath.Replace("//", "/"));

            if (!Directory.Exists(_archiveFileDirectory))
            {
                Directory.CreateDirectory(_archiveFileDirectory);
            }
            
            // move the original input file to an archive directory in case there's ever a need to troubleshoot issues
            File.Move(_inputFilePath, _archiveFileDirectory + fileId + _archiveFilename);
        }
    }
}
