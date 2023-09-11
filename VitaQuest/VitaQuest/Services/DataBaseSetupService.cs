using DataAccessLibary;

namespace VitaQuest.Services
{
    public class DataBaseSetupService
    {
        public DataBaseSetupService() { }

        public void Setup() 
        {
            SQLDataAccess sql = new SQLDataAccess();
            sql.init();
        }
    }
}
