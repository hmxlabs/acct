using System.IO;

namespace HmxLabs.Acct.Core.Test.Data.Transactions
{
    public class TransactionFileLocations
    {
        public static readonly string TransactionsDirectoryRelativePath = Path.Combine(".", "Data", "Transactions");
        public static readonly string TransactionsDirectoryAbsolutePath = Path.GetFullPath(TransactionsDirectoryRelativePath);

        public static readonly string TransactionsTestOutputRelativePath = Path.Combine(".", "TestOutput", "Transactions");
        public static readonly string TransactionsTestOutputAbsolutePath = Path.GetFullPath(TransactionsTestOutputRelativePath);

        public const string CaterAllenSampleFilename = "CaterAllen.csv";
        public static readonly string CaterAllenSample = Path.Combine(TransactionsDirectoryAbsolutePath, CaterAllenSampleFilename);

        public const string NatWestCreditCardPdfSampleFilename = "2021-NatWestCreditCardPdf.txt";
        public static readonly string NatWestCreditCardPdfSample = Path.Combine(TransactionsDirectoryAbsolutePath, NatWestCreditCardPdfSampleFilename);

        public const string NatWestCreditCardPdfFailChecksumFilename = "2022-NatWestCreditCardPdf-FailChecksum.txt";
        public static readonly string NatWestCreditCardPdfFailChecksum = Path.Combine(TransactionsDirectoryAbsolutePath, NatWestCreditCardPdfFailChecksumFilename);

        public const string NatWestCreditCardPdfFailYearParseFilename = "FailNameParse.txt";
        public static readonly string NatWestCreditCardPdfFailYearParse = Path.Combine(TransactionsDirectoryAbsolutePath, NatWestCreditCardPdfFailChecksumFilename);

        public const string CaterAllenMultipleSampleFilename = "CaterAllen-Multiple.csv";
        public static readonly string CaterAllenMultipleSample = Path.Combine(TransactionsDirectoryAbsolutePath, CaterAllenMultipleSampleFilename);

        public const string ImportSampleFilename = "ImportSample.csv";
        public static readonly string ImportSample = Path.Combine(TransactionsDirectoryAbsolutePath, ImportSampleFilename);
    }
}
