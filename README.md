# Cardano Assignment

## Description
This console application takes an input CSV, enriches it, and outputs the enriched data in a new file in the directory or your choosing, and archives the original file also in another directory of your choosing.

## Clone the repository
You will need to clone the repository to your local machine to run the app or you will need a copy of the executable and artifacts.

## Configuration
To modify the directories and filenames used by the app, you will need to open the appsettings.json file in the root of the project folder.
- `inputFilePath`: the absolute file path (including filename and extension) of the input file the app should look for
- `outputFileDirectory`: the absolute path of the directory for outputing the enriched file (needs to end with `\\`)
- `outputFileName`: the filename and extension of the output file. This filename will be prefixed with a datetime stamp.
- `archiveFileDirectory`: the absolute path of the directory for archiving the original input file (needs to end with `\\`)
- `archiveFileName`: the filename and extension of the original input file once it is moved into the archive directory. This filename will also be prefixed with the same datetime stamp as the output file to show the link between files.

The GLEIF API base URL can also be modified here, but this should not need to be updated.

## Running the app
After configuring the appsettings.json file, ensure the configured input directory has a file with the matching name. The order of the columns in the input file does not matter, however a header row with the following headers is required:
- transaction_uti
- isin
- notional
- notional_currency
- transaction_type
- transaction_datetime
- rate
- lei

Once this is done, open the solution in Visual Studio and run the app or, if you have the executable, run that. If the process runs successfully, the input file will have been moved into the configured archive directory and a new enriched version of the file will exist in the configured output directory. Both files will be prefixed with a matching datetime stamp as a visual link between the files.