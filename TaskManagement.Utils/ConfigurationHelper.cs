namespace TaskManagement.Utils
{
    public class ConfigurationHelper
    {
        private const int DefaultItemsPerPage = 100;

        public static int GetItemsPerPage()
        {
            string quantityStr = Environment.GetEnvironmentVariable("QuantityOfItensByPageReturn");

            if (!string.IsNullOrWhiteSpace(quantityStr) && int.TryParse(quantityStr, out int quantity))
            {
                return quantity;
            }

            return DefaultItemsPerPage;
        }
    }
}
