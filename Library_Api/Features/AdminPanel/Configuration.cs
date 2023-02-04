namespace Library_Api.Features.AdminPanel
{
    public class Configuration
    {
        private static Configuration _instance = null;
        private static object obj = new object();
        public int RentDays { get; set; } = 14;//Default setting
        public double Latefee { get; set; } = 0.10;//Default setting

        private Configuration()
        {
        }

        public static Configuration GetInstance()
        {
            lock (obj)
            {
                if (_instance == null)
                {
                    _instance = new Configuration();
                }
            }
            return _instance;
        }
    }
}