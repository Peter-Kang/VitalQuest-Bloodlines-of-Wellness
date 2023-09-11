using Microsoft.Data.SqlClient;

namespace DataAccessLibary
{
    public class SQLDataAccess
    {
        private const string m_CONNECTIONSTRING = "Data Source=\"localhost, 1433\";User ID=SA;Password='Str0ngPa$w0rd';Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        private SqlConnection m_SQLConnection { get; set; }

        public SQLDataAccess()
        {
            m_SQLConnection = new SqlConnection(m_CONNECTIONSTRING);
        }

        public void init() 
        {
            //initalize the database
            initializeDatabase();
            //initalize the table make sure database is ok before tables
            initializeTables();
        }

        private void initializeDatabase() 
        {
           
            string DB_create_query =
            @"
                IF ((SELECT COUNT(NAME) FROM SYS.DATABASES WHERE NAME ='VITAQUEST' ) = 0)
                BEGIN
                    CREATE DATABASE VITAQUEST
                END
            ";
            SqlCommand DB_create_command = new SqlCommand(DB_create_query, m_SQLConnection);
            m_SQLConnection.Open();
            DB_create_command.ExecuteScalar(); // dont need results for now
            m_SQLConnection.Close();
        }

        private void initializeTables() 
        {
            AppleFitnessTableInit();
            AutoSleepTableInit();
        }

        private void AppleFitnessTableInit() 
        {
            string table_create_query =
            @"
               USE VITAQUEST
               IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'APPLE_FITNESS')
               BEGIN
                   CREATE TABLE APPLE_FITNESS (
                     [ID_APPLE_FITNESS] [int] IDENTITY(0,1) PRIMARY KEY NOT NULL,
                     [DATE_OF_INSTANCE] [date] NOT NULL,
                     [HEART_RATE_MIN] [INT] NOT NULL,
                     [HEART_RATE_MAX] [INT] NOT NULL,
                     [HEART_RATE_AVERAGE] [INT] NOT NULL,
                     [HEART_RATE_RESTING] [INT] NOT NULL,
                     [HEART_RATE_ACTIVE] [INT] NOT NULL,
                     [HEART_RATE_VARIABILITY] [INT] NOT NULL,
                     [STEP_COUNT][INT] NOT NULL,
                     [CALORIES_BURNT][INT] NOT NULL
                    );
               END
            ";
            SqlCommand table_create_command = new SqlCommand(table_create_query, m_SQLConnection);
            m_SQLConnection.Open();
            table_create_command.ExecuteScalar(); // dont need results for now
            m_SQLConnection.Close();
        }

        private void AutoSleepTableInit() 
        {
            string table_create_query =
            @"
                USE VITAQUEST
                IF NOT EXISTS (SELECT 1 FROM SYS.DATABASES WHERE NAME = 'AUTO_SLEEP')
                BEGIN
                    CREATE TABLE AUTO_SLEEP (
                    [ID_AUTO_SLEEP] [int] IDENTITY(0,1) PRIMARY KEY NOT NULL,
                    [DATETIME_OF_INSTANCE] [datetime] NOT NULL,
                    [FROM_DATE] [date],
                    [TO_DATE] [date],
                    [BED_TIME] [datetime],
                    [WAKE_TIME] [datetime],
                    [IN_BED] [time],
                    [AWAKE] [time],
                    [SESSIONS] [int],
                    [ASLEEP] [time],
                    [ASLEEP_AVERAGE][time],
                    [EFFICIENCY][float],
                    [EFFICIENCY_AVERAGE][float],
                    [QUALITY][time],
                    [QUALITY_AVERAGE][TIME],
                    [DEEP][TIME],
                    [DEEP_AVERAGE][TIME],
                    [SLEEP_BPM][float],
                    [SLEEP_BPM_AVERAGE][float],
                    [DAY_BPM][float],
                    [DAY_BPM_AVERAGE][float],
                    [WAKING_BPM][float],
                    [WAKING_BPM_AVERAGE][float],
                    [HRV][float],
                    [HRV_AVERAGE][float],
                    [SLEEP_HRV][INT],
                    [SLEEP_HRV_AVERAGE][float],
                    [SPO2_AVERAGE][float],
                    [SPO2_MIN][INT],
                    [SPO2_MAX][INT]
                    );
                END
            ";
            SqlCommand table_create_command = new SqlCommand(table_create_query, m_SQLConnection);
            m_SQLConnection.Open();
            table_create_command.ExecuteScalar(); // dont need results for now
            m_SQLConnection.Close();
        }
    }
}