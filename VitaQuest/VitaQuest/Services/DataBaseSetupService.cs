using DataAccessLibary;

namespace VitaQuest.Services
{
    public class DataBaseSetup
    {
        public DataBaseSetup() { }

        public void Setup() 
        {
            new SQLDataAccess().init();
        }
    }
}
