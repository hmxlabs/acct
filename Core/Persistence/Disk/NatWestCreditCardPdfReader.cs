using System;
using System.Globalization;
using System.Text;
using CsvHelper;
using HmxLabs.Acct.Core.Models;

namespace HmxLabs.Acct.Core.Persistence.Disk
{
    public class NatWestCreditCardPdfReader : CsvTransactionReader
    {
        public NatWestCreditCardPdfReader(IAccount account_ = null)
        {
            TransactionAccount = account_;
            TransactionYear = DateTime.Now.Year;
            Delimiter = " ";
        }

        public override TransactionFileType Type => TransactionFileType.NatWestCreditPdf;

        public int TransactionYear { get; set; }

        protected override bool HasHeader => false;

        protected override ITransaction ParseRow(string[] line_)
        {
            var amountIndex = FindAmountIndex(line_);
            if (!_initialBalanceProcessed)
            {
                ProcessBalance(line_, amountIndex);
                _initialBalanceProcessed = true;
                return null;
            }

            if ("NEW".Equals(line_[0]) || "BALANCE".Equals(line_[0]))
            {
                // This is a balance row. Use it as a checkpoint
                ProcessCheckPointBalance(line_, amountIndex);
                return null;
            }

            // Example rows
            // Trans Date    Post Date      Ref Number    Retailer   Amount -- mostly this is true but not always
            // 29 JAN 30 JAN 47845216 FASTHOSTS INTERNET 03330142700 13.19 
            // 30 JAN 31 JAN 54423789 FASTHOSTS INTERNET 03330142700 24.00
            // 02 FEB 02 FEB 00268600 FASTER PAYMENT RECEIVED - THANK YOU 211.63 
            
            var transaction = new Transaction();
            transaction.TransactionDate = CreateDate(line_[0], line_[1]);
            transaction.PostDate = CreateDate(line_[2], line_[3]);

            transaction.Description = CreateDescription(line_, amountIndex);

            // Payments appear to lose the change in sign when copied across so attempt to detect if this is a payment
            // and then don't reverse the sign, else reverse the sign as the statements treat being in debt as positive.
            // Keeps you spending money you don't have that way around!
            var amount = decimal.Parse(line_[amountIndex]);

            if (transaction.Description.Contains("PAYMENT") && transaction.Description.Contains("THANK YOU"))
                transaction.Amount = amount;
            else
                transaction.Amount = amount * -1;


            transaction.Account = TransactionAccount;

            _runningBalance += transaction.Amount;
            transaction.RunningBalance = _runningBalance;

            return transaction;
        }

        private void ProcessBalance(string[] line_, int amountIndex_)
        {
            // First row is like this:
            // BALANCE FROM PREVIOUS STATEMENT £211.63
            // So if the first word isn't BALANCE...
            if (!string.Equals("BALANCE", line_[0]))
                throw new FormatException("Firse line in NatWest Credit Card PDF Text file is not a statrting balance.");

            _startingBalance = decimal.Parse(line_[amountIndex_]) * -1;
            _runningBalance = _startingBalance;
        }

        private void ProcessCheckPointBalance(string[] line_, int amountIndex_)
        {
            var expectedBalance = decimal.Parse(line_[amountIndex_]) * -1;
            var diff = Math.Abs(_runningBalance - expectedBalance);
            if (diff > 0.001M)
                throw new Exception($"Failed checkpoint validation on balance. Expected balance was {expectedBalance} but got {_runningBalance}");
        }

        private int FindAmountIndex(string[] line_)
        {
            // For the amount take the last item as we are space separated and the name of the retailer may have spaced don't which index it will be on
            // Also, need to invert the sign because on credit card statements a negative is actually a payment to the account and positive is a debit
            // from the account which is the inverse to a normal current account and the convention used everywhere else in this application.
            // Note that because the last character on a line a space it means that the last item is an empty entry and the second to last is actually the
            // amount

            // For some odd reason we get a combination of empty strig and nulls at the end of the line
            // so what we need is actually the last non empty and non null index
            for (int index = line_.Length - 1; index >= 0; index--)
            {
                if (!string.IsNullOrEmpty(line_[index]))
                    return index;
            }

            // Hmm.. we seem to have an array of just empty strings and nulls... shouldn't really ever happen... 
            // but we'll assume somehow it did so pretend its just last item in the array
            return line_.Length - 1;
        }

        private string CreateDescription(string[] line_, int amountIndex_)
        {
            // Little sanity check
            if (amountIndex_ <= 5)
                throw new CsvBadDataException("The amount column appears to be earlier than the expected date column");

            // Will start at 5 and then continue to last but one and then tack on the reference to get unique inputs for similar transactions
            var description = new StringBuilder();
            for (int index = 5; index < amountIndex_; index++)
            {
                description.Append(line_[index]);
                description.Append(" ");
            }

            description.Append("- REF: ");
            description.Append(line_[4]);
            return description.ToString();
        }

        private DateTime CreateDate(string day_, string month_)
        {
            int day;
            if (!int.TryParse(day_, out day))
                throw new FormatException($"Unable to interpret {day_} as a day");

            var month = DateTime.ParseExact(month_, "MMM", CultureInfo.InvariantCulture).Month;

            return new DateTime(TransactionYear, month, day);
        }

        private bool _initialBalanceProcessed;
        private decimal _startingBalance;
        private decimal _runningBalance;
    }
}
