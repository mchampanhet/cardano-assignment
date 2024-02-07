using Cardano.Maps;
using Cardano.Models;
using CsvHelper;
using System.Globalization;

namespace Cardano.Service
{
    internal class FileService
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private readonly string _outputFileName;
        private readonly string _archiveFilePath;

        public FileService(string inputFilePath, string outputFilePath, string outputFileName, string archiveFilePath)
        {
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;
            _outputFileName = outputFileName;
            _archiveFilePath = archiveFilePath;
        }

        public List<Transaction> GetFile()
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
                return null;
            }
        }

        public void WriteFile(IEnumerable<Transaction> transactions)
        {
            if (!Directory.Exists(_outputFilePath))
            {
                Directory.CreateDirectory(_outputFilePath);
            }
            var newFilePath = _outputFilePath + _outputFileName + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + ".csv";
            using (var writer = new StreamWriter(newFilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<TransactionFileWriteMap>();
                csv.WriteRecords(transactions);
            }
            Console.WriteLine("Enriched file created: " + newFilePath.Replace("//", "/"));

            if (!Directory.Exists(_archiveFilePath))
            {
                Directory.CreateDirectory(_archiveFilePath);
            }

            File.Move(_inputFilePath, _archiveFilePath);
        }
    }
}
