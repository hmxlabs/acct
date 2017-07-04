namespace HmxLabs.AcctCommandLine
{
    public interface ICommandLineParams
    {
        string ConfigFile { get; }
        ulong? InvoiceNumber { get; }

        bool Read(string[] args_);
    }
}
