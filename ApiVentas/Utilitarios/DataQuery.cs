using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace ApiVentas.Utilitarios
{
    public class DataQuery
    {
        public string? OpcionData {  get; set; }
        public string? DataFirstQuery{  get; set; }
        public string? DataSecondQuery {  get; set; }

        public bool ValidOpcion()
        {
            bool validOption = false;
            if (!this.OpcionData.IsNullOrEmpty())
            {
                EnumOptions enumOptions=new EnumOptions();
                validOption = enumOptions.ValidOptions(this.OpcionData.ToLower());
            }
            return validOption;
        }
        public bool ValidFirst()
        {
            return this.DataFirstQuery.IsNullOrEmpty()?false : true;
        }
        public bool ValidSecond()
        {
            return this.DataSecondQuery.IsNullOrEmpty()?false : true;
        }
        public bool ValidAll()
        {
            return this.ValidFirst() && this.ValidSecond() && this.ValidOpcion();
        }
        public int TryIntDataFirst()
        {
            int.TryParse(this.DataFirstQuery, out int intDataFirstValue);
            return intDataFirstValue;
        }
        public double TryDoubleDataFirst()
        {
            double.TryParse(this.DataFirstQuery, out double doubleDataFirstValue);
            return doubleDataFirstValue;
        }
        public DateTime TryDateTimeDataFirst()
        {
            DateTime.TryParse(this.DataFirstQuery, out DateTime dateTimeDataFirstValue);
            return dateTimeDataFirstValue;
        }
        public int TryIntDataSecond()
        {
            int.TryParse(this.DataSecondQuery,out int intDataSecondValue);
            return intDataSecondValue;
        }
        public double TryDoubleDataSecond()
        {
            double.TryParse(this.DataSecondQuery, out double doubleDataSecondValue);
            return doubleDataSecondValue;
        }
        public DateTime TryDateTimeDataSecond()
        {
            DateTime.TryParse(this.DataSecondQuery, out DateTime dateTimeDataSecondValue);
            return dateTimeDataSecondValue;
        }
        public decimal tryDecimalDataFirst()
        {
            decimal.TryParse(this.DataFirstQuery, out decimal  decimalDataFirstValue);
            return decimalDataFirstValue;
        }
        public decimal tryDecimalDataSecond()
        {
            decimal.TryParse(this.DataSecondQuery, out decimal decimalDataSecondValue);
            return decimalDataSecondValue;
        }
    }
}
