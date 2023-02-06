namespace Library_Api.Features.AdminPanel
{
    public class ApiConfiguration
    {
        private static ApiConfiguration _instance = null;
        private static object obj = new object();
        public int RentDays { get; set; } = 14;//Default setting
        public double Latefee { get; set; } = 0.10;//Default setting

        private ApiConfiguration()
        {
        }

        public static ApiConfiguration GetInstance()
        {
            lock (obj)
            {
                if (_instance == null)
                {
                    _instance = new ApiConfiguration();
                }
            }
            return _instance;
        }
    }
}