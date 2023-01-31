using Library_Api.Entity;
using Library_Api.Features.AdminPanel;

namespace Library_Api.Features.RentService.Command.Helper
{
    public class CalculateFee
    {
        private static readonly Configuration _configuration=Configuration.GetInstance();
        public static double Calculate(Rent rent)
        {
            if (DateTime.Now.CompareTo(rent.Ends) <= 0)
            {
                return 0;
            }
            if (rent.ReturnDate == null)
            {
                var time = DateTime.Now - rent.Ends;
                var days = (int)time.Days;
                return days * _configuration.Latefee; 
            }
            TimeSpan span = (TimeSpan)(rent.ReturnDate - rent.Ends);
            var spandays = (int)span.Days;
            return (double)(spandays * _configuration.Latefee);
        }
    }
}