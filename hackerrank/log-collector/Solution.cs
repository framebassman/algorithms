namespace log_collector;

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

public class LogCollector : IDisposable
{
    private readonly ILogsDirectoryLister _directoryLister;
    private readonly ILogFileReader _logFileReader;
    private readonly ILogFileParsingResultPublisher _publisher;
    
    private HashSet<ParsedLogEntry> _existingEntries = new HashSet<ParsedLogEntry>();
    
    public LogCollector(
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

    }
    
    public void Dispose()
    {
        _existingEntries.Clear();
    }
}

public class ParsedLogEntry : IEquatable<ParsedLogEntry>
{
    public string TimeStamp { get; set; }

    public string LogLevel { get; set; }

    public string Message { get; set; }
    
    public bool Equals(ParsedLogEntry other)
    {
        return other?.TimeStamp == this.TimeStamp
            && other?.LogLevel == this.LogLevel
            && other?.Message == this.Message;
    }
    
    public override bool Equals(object o) => o is ParsedLogEntry e && this.Equals(e);
}

public class LogFileParsingResult
{
    public string FileName { get; set; }

    public long TotalLogEntries { get; set; }

    public IDictionary<string, int> CountsPerLogLevel { get; set; }

    public IEnumerable<ParsedLogEntry> Entries { get; set; }
}

public interface ILogsDirectoryLister
{
    /// <summary>
    /// Listing all log files in specified directory
    /// </summary>
    Task<(string fileName, string filesPaths)[]> ListLogFilesAsync(string directoryPath, CancellationToken cancellationToken);
}

public interface ILogFileReader
{
    /// <summary>
    /// Reads all lines from specified log file async
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<string>> ReadLogFileLinesAsync(string logFilePath, CancellationToken cancellationToken);
}


public interface ILogFileParsingResultPublisher
{
    /// <summary>
    /// Publishes a log file parsing result
    /// </summary>
    Task PublishLogFileParsingResult(LogFileParsingResult resultToPublish, CancellationToken cancellationToken);
}