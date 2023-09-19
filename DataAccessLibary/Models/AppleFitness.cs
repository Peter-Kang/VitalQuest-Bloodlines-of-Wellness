using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace DataAccessLibary
{
    public class AppleFitnessDay: SQLDataAccess
    {
        public int AppleFitnessID { get; set; }
        public DateTime DateTimeOfInstance { get; set; }
        public int HeartRateMin { get; set; }
        public int HeartRateMax { get; set; }
        public int HeartRateAverage { get; set; }
        public int HeartRateResting { get; set; }
        public int HeartRateActive { get; set; }
        public int HeartRateVariability { get; set; }
        public int StepCount { get; set; }
        public int CaloriesBurnt { get; set; }

        public AppleFitnessDay() // calls SQLDataAccess' base empty
        { 
            AppleFitnessID = -1; 
        }
        public AppleFitnessDay(in int id): this() // current constructor
        {
            InitializeAppleFitnessFromID(id);
        }
        public void InitializeAppleFitnessFromID(in int id) 
        {
            const string get_row_by_id =
                @"USE VITAQUEST
                SELECT TOP 1
                    ID_APPLE_FITNESS,
                    DATE_OF_INSTANCE,
                    HEART_RATE_MIN,
                    HEART_RATE_MAX,
                    HEART_RATE_AVERAGE,
                    HEART_RATE_RESTING,
                    HEART_RATE_ACTIVE,
                    HEART_RATE_VARIABILITY,
                    STEP_COUNT,
                    CALORIES_BURNT
                FROM APPLE_FITNESS 
                WHERE ID_APPLE_FITNESS = @ID";
            DataTable result = new DataTable();
            SqlCommand init_command 
                = new SqlCommand(get_row_by_id, m_SQLConnection);
            init_command.Parameters.AddWithValue("@ID", id);
            init_command.Connection.Open();
            result.Load(init_command.ExecuteReader());
            init_command.Connection.Close();
            if (result.Rows.Count > 0 ) 
            {//read in data
                try
                {
                    AppleFitnessID = (int)(result.Rows[0]["ID_APPLE_FITNESS"]);
                    DateTimeOfInstance = (DateTime)(result.Rows[0]["DATE_OF_INSTANCE"]);
                    HeartRateMin = (int)(result.Rows[0]["HEART_RATE_MIN"]);
                    HeartRateMax = (int)(result.Rows[0]["HEART_RATE_MAX"]);
                    HeartRateAverage = (int)(result.Rows[0]["HEART_RATE_AVERAGE"]);
                    HeartRateResting = (int)(result.Rows[0]["HEART_RATE_RESTING"]);
                    HeartRateActive = (int)(result.Rows[0]["HEART_RATE_ACTIVE"]);
                    HeartRateVariability = (int)(result.Rows[0]["HEART_RATE_VARIABILITY"]);
                    StepCount = (int)(result.Rows[0]["STEP_COUNT"]);
                    CaloriesBurnt = (int)(result.Rows[0]["CALORIES_BURNT"]);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Error in InitializeAppleFitnessFromID(in int id)\n" +ex.ToString());
                }
            }
        }

        public AppleFitnessDay(in DataRow row) : this() 
        {
            try
            {
                AppleFitnessID          = (int)     (row["ID_APPLE_FITNESS"]);
                DateTimeOfInstance      = (DateTime)(row["DATE_OF_INSTANCE"]);
                HeartRateMin            = (int)     (row["HEART_RATE_MIN"]);
                HeartRateMax            = (int)     (row["HEART_RATE_MAX"]);
                HeartRateAverage        = (int)     (row["HEART_RATE_AVERAGE"]);
                HeartRateResting        = (int)     (row["HEART_RATE_RESTING"]);
                HeartRateActive         = (int)     (row["HEART_RATE_ACTIVE"]);
                HeartRateVariability    = (int)     (row["HEART_RATE_VARIABILITY"]);
                StepCount               = (int)     (row["STEP_COUNT"]);
                CaloriesBurnt           = (int)     (row["CALORIES_BURNT"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AppleFitnessDay(in DataRow row)\n" + ex.ToString());
            }
        }
    }

    public class AppleFitness :SQLDataAccess
    {
        public AppleFitness() { } // automatically calls base

        public void WiteToDB( in AppleFitnessDay[] appleFitnessDays) 
        {
            using (SqlCommand cmd = m_SQLConnection.CreateCommand())
            {
                //Inital
                StringBuilder query = new StringBuilder("USE VITAQUEST INSERT INTO dbo.APPLE_FITNESS (DATE_OF_INSTANCE, HEART_RATE_MIN,HEART_RATE_MAX,HEART_RATE_AVERAGE,HEART_RATE_RESTING,HEART_RATE_ACTIVE,HEART_RATE_VARIABILITY,STEP_COUNT, CALORIES_BURNT) VALUES ");
                bool first = true;
                //iterative
                foreach (AppleFitnessDay day in appleFitnessDays) 
                {
                    if (first)
                    {
                        first = false;
                    }
                    else 
                    {
                        query.Append(',');
                    }
                    string line = string.Format("(('{0}'),{1},{2},{3},{4},{5},{6},{7},{8})", 
                        day.DateTimeOfInstance.ToString("yyyy-MM-dd"), 
                        day.HeartRateMin.ToString(), 
                        day.HeartRateMax.ToString(), 
                        day.HeartRateAverage.ToString(), 
                        day.HeartRateResting.ToString(), 
                        day.HeartRateActive.ToString(), 
                        day.HeartRateVariability.ToString(), 
                        day.StepCount.ToString(), 
                        day.CaloriesBurnt.ToString());
                    query.Append(line);
                }
                cmd.CommandText = query.ToString();
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public DataTable CreateDataTableFromAppleFitnessDay(AppleFitnessDay[] appleFitnessDays) 
        {
            //Create the table
            DataTable results = new DataTable();
            results.Columns.Add("DATE_OF_INSTANCE", typeof(DateTime));
            results.Columns.Add("HEART_RATE_MIN", typeof(int));
            results.Columns.Add("HEART_RATE_MAX", typeof(int));
            results.Columns.Add("HEART_RATE_AVERAGE", typeof(int));
            results.Columns.Add("HEART_RATE_RESTING", typeof(int));
            results.Columns.Add("HEART_RATE_ACTIVE", typeof(int));
            results.Columns.Add("HEART_RATE_VARIABILITY", typeof(int));
            results.Columns.Add("STEP_COUNT ", typeof(int));
            results.Columns.Add("CALORIES_BURNT", typeof(int));

            foreach (AppleFitnessDay day in appleFitnessDays)
            {
                //fill in the data
                DataRow row = results.NewRow();
                row["DATE_OF_INSTANCE"]         = day.DateTimeOfInstance;
                row["HEART_RATE_MIN"]           = day.HeartRateMin;
                row["HEART_RATE_MAX"]           = day.HeartRateMax;
                row["HEART_RATE_AVERAGE"]       = day.HeartRateAverage;
                row["HEART_RATE_RESTING"]       = day.HeartRateResting;
                row["HEART_RATE_ACTIVE"]        = day.HeartRateActive;
                row["HEART_RATE_VARIABILITY"]   = day.HeartRateVariability;
                row["STEP_COUNT "]              = day.StepCount;
                row["CALORIES_BURNT"]           = day.CaloriesBurnt;
                results.Rows.Add(row);
            }

            return results;
        }
    }

}
