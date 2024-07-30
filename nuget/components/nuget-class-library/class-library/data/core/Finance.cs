using nuget_class_library.class_library.data.enums;
using nuget_class_library.class_library.exception;

namespace nuget_class_library.class_library.data.core
{
    /// <summary>
    /// Holds data regarding the direct debit financial details for a customer object.
    /// </summary>
    /// <remarks>
    /// Initialises a new instance of the <see cref="Finance"/> class.
    /// </remarks>
    /// <param name="deposit">The deposit to set.</param>
    /// <param name="interest">The interest to set.</param>
    /// <param name="apr">The apr to set.</param>
    /// <param name="totalNumberOfInstallments">The total number of installments to set.</param>
    /// <param name="monthlyInstallmentAmount">The monthly installment amount to set.</param>
    /// <param name="totalInstallmentAmount">The total installment amount to set.</param>
    /// <param name="financeProvider">The finance provider to set.</param>
    /// <param name="bankSortCode">The bank sort code to set.</param>
    /// <param name="bankAccountNumber">The bank account number to set.</param>
    public class Finance(
            decimal deposit,
            decimal interest,
            decimal apr,
            int totalNumberOfInstallments,
            decimal monthlyInstallmentAmount,
            decimal totalInstallmentAmount,
            string financeProvider,
            string bankSortCode,
            string bankAccountNumber)
    {
        /// <summary>
        /// Gets the Deposit.
        /// </summary>
        public decimal Deposit { get; private set; } = deposit;

        /// <summary>
        /// Gets the Interest.
        /// </summary>
        public decimal Interest { get; private set; } = interest;

        /// <summary>
        /// Gets the Apr.
        /// </summary>
        public decimal Apr { get; private set; } = apr;

        /// <summary>
        /// Gets the TotalNumberOfInstallments.
        /// </summary>
        public int TotalNumberOfInstallments { get; private set; } = totalNumberOfInstallments;

        /// <summary>
        /// Gets the MonthlyInstallmentAmount.
        /// </summary>
        public decimal MonthlyInstallmentAmount { get; private set; } = monthlyInstallmentAmount;

        /// <summary>
        /// Gets the InstallmentAmount, the total amount paid under installments including interest.
        /// </summary>
        public decimal TotalInstallmentAmount { get; private set; } = totalInstallmentAmount;

        /// <summary>
        /// Gets the FinanceProvider; the name of the finance provider if paid for by installments.
        /// </summary>
        public string? FinanceProvider { get; private set; } = financeProvider;

        /// <summary>
        /// Gets the BankSortCode; the obfuscated sort code bank details.
        /// </summary>
        public string? BankSortCode { get; private set; } = bankSortCode;

        /// <summary>
        /// Gets the BankAccountNumber; the obfuscated account number bank details.
        /// </summary>
        public string? BankAccountNumber { get; private set; } = bankAccountNumber;
    }
}
