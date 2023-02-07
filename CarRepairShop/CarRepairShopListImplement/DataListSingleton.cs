using CarRepairShopListImplement.Models;

namespace CarRepairShopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton? _instance;
        public List<Detail> Details { get; set; }
        public List<Order> Orders { get; set; }
        public List<Car> Cars { get; set; }
        private DataListSingleton()
        {
            Details = new List<Detail>();
            Orders = new List<Order>();
            Cars = new List<Car>();
        }
        public static DataListSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataListSingleton();
            }
            return _instance;
        }
    }
}