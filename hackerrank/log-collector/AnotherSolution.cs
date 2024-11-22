// The LogCollector class is a service that performs the following tasks:
// - Continuously monitors the specified directory for log files
// - When a new log file appears, it does it's parsing independently of others and publishes the parsing result
// - When termination is requested by the calling code, it stops gracefully, finishing any parsing tasks that have already started

// Parsing of an individual log file:
// - Each line of a log file is a log entry with the format: "[Timestamp] [LogLevel] [Message]"
// - Assume that correct format is guaranteed for every log entry

// The parsing result for a log file includes:
// - File name
// - Total number of log entries
// - Counts of messages per log level
// - All parsed log entries (timestamp, log level, message)

// Task: you need to complete an implementation of LogCollector. Due to the interview time limitations, ideal solution is not required.
// Bonus task: can we address a situation when new log files may appear frequently?

using log_collector;

public class LogCollector1 : IDisposable
{
    private readonly ILogsDirectoryLister _directoryLister;
    private readonly ILogFileReader _logFileReader;
    private readonly ILogFileParsingResultPublisher _publisher;
    
    private HashSet<ParsedLogEntry> _existingEntries = new HashSet<ParsedLogEntry>();
    
    public LogCollector1(
        ILogsDirectoryLister directoryLister,
        ILogFileReader logFileReader,
        ILogFileParsingResultPublisher publisher
    )
    {
        _directoryLister = directoryLister;
        _logFileReader = logFileReader;
        _publisher = publisher;
    }

    /// <summary>
    /// Monitors the specified directory and processes new log files as they appear.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to monitor.</param>
    /// <param name="cancellationToken">Token used to terminate processing gracefully.</param>
    public async Task ProcessDirectoryAsync(string directoryPath, CancellationToken cancellationToken)
    {
        /*
         * - Get list of log files
         * - For each file
         *      - read all lines
         *      - get diff from existent
         *      - format answer
         *      - publish answer
         */ 
        
        
        
        while (true)
        {
            var logFiles = await _directoryLister.ListLogFilesAsync(directoryPath, cancellationToken);
            if (logFiles?.Any() == false) return;
            
            var logDescriptors = logFiles
                .Select(x => new
                {
                    Path = Path.Combine(x.filesPaths, x.fileName),
                    Result = new LogFileParsingResult
                    {
                        FileName = x.fileName,
                    }
                })
                .ToList();
            foreach (var logDescriptor in logDescriptors)
            {
                var logLines = await _logFileReader.ReadLogFileLinesAsync(logDescriptor.Path, cancellationToken);
                if (logLines?.Any() == false)
                {
                    continue;
                }
                
                var logEntries = logLines.Select(x => x.Split(" "))
                    .Select(x => new ParsedLogEntry
                    {
                        TimeStamp = x[0],
                        LogLevel = x[1],
                        Message = x[2]
                    })
                    .ToList();
                var newEntries = logEntries.Select(x => new
                {
                    Entry = x,
                    IsExisting = _existingEntries.Contains(x)
                })
                .Where(x => !x.IsExisting)
                .Select(x => x.Entry)
                .ToList();
                
                
                var countsPerLogLevel = newEntries
                    .GroupBy(x => x.LogLevel)
                    .ToDictionary(x => x.Key, x => x.Count());

                var some = newEntries
                    .GroupBy(x => x.LogLevel);
                
                logDescriptor.Result.Entries = newEntries;
                logDescriptor.Result.CountsPerLogLevel = countsPerLogLevel;
                logDescriptor.Result.TotalLogEntries = newEntries.Count;
                
                await _publisher.PublishLogFileParsingResult(logDescriptor.Result, cancellationToken);
            }
            
            await Task.Delay(1000);
            
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }
        }
    }
    
    public void Dispose()
    {
        _existingEntries.Clear();
    }
}
