using nuget_class_library.class_library.data.core;
using System.Data;

namespace nuget_class_library.class_library.sql
{
    public class PostgreSqlRecordHelper
    {
        public static Address GenerateAddressPropertiesFromDataTable(DataTable resultsDataTable)
        {
            var result = new Address(
                resultsDataTable.Rows[0].Field<string>("houseNameOrNumber"),
                resultsDataTable.Rows[0].Field<string>("addressLine1"),
                resultsDataTable.Rows[0].Field<string>("addressLine2"),
                resultsDataTable.Rows[0].Field<string>("addressLine3"),
                resultsDataTable.Rows[0].Field<string>("addressLine4"),
                resultsDataTable.Rows[0].Field<string>("postcode"));

            return result;
        }

        public static RootCustomer GenerateCustomerPropertiesFromDataTable(DataTable resultsDataTable)
        {
            var result = new RootCustomer(
                resultsDataTable.Rows[0].Field<string>("reference"),
                resultsDataTable.Rows[0].Field<string>("webReference"),
                resultsDataTable.Rows[0].Field<string>("email"),
                resultsDataTable.Rows[0].Field<string>("title"),
                resultsDataTable.Rows[0].Field<string>("firstName"),
                resultsDataTable.Rows[0].Field<string>("surname"),
                resultsDataTable.Rows[0].Field<string>("phone"),
                resultsDataTable.Rows[0].Field<string>("brand"),
                resultsDataTable.Rows[0].Field<bool>("active"),
                resultsDataTable.Rows[0].Field<string>("paymentType"));

            return result;
        }

        public static Finance GenerateFinancePropertiesFromDataTable(DataTable resultsDataTable)
        {
            var result = new Finance(
                resultsDataTable.Rows[0].Field<decimal>("deposit"),
                resultsDataTable.Rows[0].Field<decimal>("interest"),
                resultsDataTable.Rows[0].Field<decimal>("apr"),
                resultsDataTable.Rows[0].Field<int>("totalNumberOfInstallments"),
                resultsDataTable.Rows[0].Field<decimal>("monthlyInstallmentAmount"),
                resultsDataTable.Rows[0].Field<decimal>("TotalInstallmentAmount"),
                resultsDataTable.Rows[0].Field<string>("financeProvider"),
                resultsDataTable.Rows[0].Field<string>("bankSortCode"),
                resultsDataTable.Rows[0].Field<string>("bankAccountNumber"));

            return result;
        }

        public static Premium GeneratePremiumPropertiesFromDataTable(DataTable resultsDataTable)
        {
            var result = new Premium(
                resultsDataTable.Rows[0].Field<decimal>("totalSellingPrice"),
                resultsDataTable.Rows[0].Field<decimal>("nettPremium"),
                resultsDataTable.Rows[0].Field<decimal>("grossPremium"),
                resultsDataTable.Rows[0].Field<decimal>("outstandingBalance"),
                resultsDataTable.Rows[0].Field<string>("cardNumber"));

            return result;
        }
    }
}

