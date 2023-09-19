using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLibary
{
    public class AutoSleepDay : SQLDataAccess
    {
        public int      AutoSleepID         { get; set; }
        public DateTime DateTimeOfInstance  { get; set; }
        public DateOnly FromDate            { get; set; }
        public DateOnly ToDate              { get; set; }
        public DateTime BedTime             { get; set; }
        public DateTime WakeTime            { get; set; }
        public TimeOnly InBed               { get; set; }
        public TimeOnly Awake               { get; set; }
        public int      Sessions            { get; set; }
        public TimeOnly Asleep              { get; set; }
        public TimeOnly AsleepAverage       { get; set; }
        public double   Efficiency          { get; set; }
        public double   EfficiencyAverage   { get; set; }
        public TimeOnly Quality             { get; set; }
        public TimeOnly QualityAverage      { get; set; }
        public TimeOnly Deep                { get; set; }
        public TimeOnly DeepAverage         { get; set; }
        public double   SleepBPM            { get; set; }
        public double   SleepBPMAverage     { get; set; }
        public double   DayBPM              { get; set; }
        public double   DayBPMAverage       { get; set; }
        public double   WakeingBPM          { get; set; }
        public double   WakeingBPMAverage   { get; set; }
        public int      HRV                 { get; set; }
        public int      HRVAverage          { get; set; }
        public int      SleepHRV            { get; set; }
        public int      SleepHRVAverage     { get; set; }
        public double   SPO2Average         { get; set; }
        public int      SPO2Min             { get; set; }
        public int      SPO2Max             { get; set; }

        public AutoSleepDay() 
        {
            AutoSleepID = -1;
        }

        public AutoSleepDay(in int id): this() 
        {
            InitalizeAutoSleepFromID(id);
        }

        public void InitalizeAutoSleepFromID(in int id) 
        {
            const string get_row_by_id =
               @"USE VITAQUEST
                SELECT TOP 1
                    ID_AUTO_SLEEP,
                    DATETIME_OF_INSTANCE,
                    FROM_DATE,
                    TO_DATE,
                    BED_TIME,
                    WAKE_TIME,
                    IN_BED,
                    AWAKE,
                    SESSIONS,
                    ASLEEP,
                    ASLEEP_AVERAGE,
                    EFFICIENCY,
                    EFFICIENCY_AVERAGE,
                    QUALITY,
                    QUALITY_AVERAGE,
                    DEEP,
                    DEEP_AVERAGE,
                    SLEEP_BPM,
                    SLEEP_BPM_AVERAGE,
                    DAY_BPM,
                    DAY_BPM_AVERAGE,
                    WAKING_BPM,
                    WAKING_BPM_AVERAGE,
                    HRV,
                    HRV_AVERAGE,
                    SLEEP_HRV,
                    SLEEP_HRV_AVERAGE,
                    SPO2_AVERAGE,
                    SPO2_MIN,
                    SPO2_MAX
                FROM AUTO_SLEEP 
                WHERE ID_AUTO_SLEEP = @ID";
            DataTable result = new DataTable();
            SqlCommand init_command
                = new SqlCommand(get_row_by_id, m_SQLConnection);
            init_command.Parameters.AddWithValue("@ID", id);
            init_command.Connection.Open();
            result.Load(init_command.ExecuteReader());
            init_command.Connection.Close();
            if (result.Rows.Count > 0)
            {//read in data
                try
                {
                    AutoSleepID         = (int)     (result.Rows[0]["ID_AUTO_SLEEP"]);
                    DateTimeOfInstance  = (DateTime)(result.Rows[0]["DATETIME_OF_INSTANCE"]);
                    FromDate            = (DateOnly)(result.Rows[0]["FROM_DATE"]);
                    ToDate              = (DateOnly)(result.Rows[0]["TO_DATE"]);
                    BedTime             = (DateTime)(result.Rows[0]["BED_TIME"]);
                    WakeTime            = (DateTime)(result.Rows[0]["WAKE_TIME"]);
                    InBed               = (TimeOnly)(result.Rows[0]["IN_BED"]);
                    Awake               = (TimeOnly)(result.Rows[0]["AWAKE"]);
                    Sessions            = (int)     (result.Rows[0]["SESSIONS"]);
                    Asleep              = (TimeOnly)(result.Rows[0]["ASLEEP"]);
                    AsleepAverage       = (TimeOnly)(result.Rows[0]["ASLEEP_AVERAGE"]);
                    Efficiency          = (double)  (result.Rows[0]["EFFICIENCY"]);
                    EfficiencyAverage   = (double)  (result.Rows[0]["EFFICIENCY_AVERAGE"]);
                    Quality             = (TimeOnly)(result.Rows[0]["QUALITY"]);
                    QualityAverage      = (TimeOnly)(result.Rows[0]["QUALITY_AVERAGE"]);
                    Deep                = (TimeOnly)(result.Rows[0]["DEEP"]);
                    DeepAverage         = (TimeOnly)(result.Rows[0]["DEEP_AVERAGE"]);
                    SleepBPM            = (double)  (result.Rows[0]["SLEEP_BPM"]);
                    SleepBPMAverage     = (double)  (result.Rows[0]["SLEEP_BPM_AVERAGE"]);
                    DayBPM              = (double)  (result.Rows[0]["DAY_BPM"]);
                    DayBPMAverage       = (double)  (result.Rows[0]["DAY_BPM_AVERAGE"]);
                    WakeingBPM          = (double)  (result.Rows[0]["WAKING_BPM"]);
                    WakeingBPMAverage   = (double)  (result.Rows[0]["WAKING_BPM_AVERAGE"]);
                    HRV                 = (int)     (result.Rows[0]["HRV"]);
                    HRVAverage          = (int)     (result.Rows[0]["HRV_AVERAGE"]);
                    SleepHRV            = (int)     (result.Rows[0]["SLEEP_HRV"]);
                    SleepHRVAverage     = (int)     (result.Rows[0]["SLEEP_HRV_AVERAGE"]);
                    SPO2Average         = (double)  (result.Rows[0]["SPO2_AVERAGE"]);
                    SPO2Min             = (int)     (result.Rows[0]["SPO2_MIN"]);
                    SPO2Max             = (int)     (result.Rows[0]["SPO2_MAX"]);
                }catch (Exception ex)
                {
                    Console.WriteLine("Error in InitalizeAutoSleepFromID(in int id) \n" + ex.ToString());
                }
            }
    }

        public AutoSleepDay(in DataRow row) : this() 
        {
            try
            {
                AutoSleepID        = (int)      (row["ID_AUTO_SLEEP"]);
                DateTimeOfInstance = (DateTime) (row["DATETIME_OF_INSTANCE"]);
                FromDate           = (DateOnly) (row["FROM_DATE"]);
                ToDate             = (DateOnly) (row["TO_DATE"]);
                BedTime            = (DateTime) (row["BED_TIME"]);
                WakeTime           = (DateTime) (row["WAKE_TIME"]);
                InBed              = (TimeOnly) (row["IN_BED"]);
                Awake              = (TimeOnly) (row["AWAKE"]);
                Sessions           = (int)      (row["SESSIONS"]);
                Asleep             = (TimeOnly) (row["ASLEEP"]);
                AsleepAverage      = (TimeOnly) (row["ASLEEP_AVERAGE"]);
                Efficiency         = (double)   (row["EFFICIENCY"]);
                EfficiencyAverage  = (double)   (row["EFFICIENCY_AVERAGE"]);
                Quality            = (TimeOnly) (row["QUALITY"]);
                QualityAverage     = (TimeOnly) (row["QUALITY_AVERAGE"]);
                Deep               = (TimeOnly) (row["DEEP"]);
                DeepAverage        = (TimeOnly) (row["DEEP_AVERAGE"]);
                SleepBPM           = (double)   (row["SLEEP_BPM"]);
                SleepBPMAverage    = (double)   (row["SLEEP_BPM_AVERAGE"]);
                DayBPM             = (double)   (row["DAY_BPM"]);
                DayBPMAverage      = (double)   (row["DAY_BPM_AVERAGE"]);
                WakeingBPM         = (double)   (row["WAKING_BPM"]);
                WakeingBPMAverage  = (double)   (row["WAKING_BPM_AVERAGE"]);
                HRV                = (int)      (row["HRV"]);
                HRVAverage         = (int)      (row["HRV_AVERAGE"]);
                SleepHRV           = (int)      (row["SLEEP_HRV"]);
                SleepHRVAverage    = (int)      (row["SLEEP_HRV_AVERAGE"]);
                SPO2Average        = (double)   (row["SPO2_AVERAGE"]);
                SPO2Min            = (int)      (row["SPO2_MIN"]);
                SPO2Max            = (int)      (row["SPO2_MAX"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AutoSleepDay(in DataRow row) \n" + ex.ToString());
            }
        }

    }

    public class AutoSleep : SQLDataAccess
    {
        public AutoSleep() { }
        public void WriteToDB( in AutoSleepDay[] autoSleepDays) 
        {
            using ( SqlCommand cmd = m_SQLConnection.CreateCommand() ) 
            {
                //Inital
                StringBuilder query = new StringBuilder(
                    @"USE VITAQUEST INSERT INTO
                    dbo.AUTO_SLEEP(
                        [DATETIME_OF_INSTANCE],
                        [FROM_DATE],
                        [TO_DATE],
                        [BED_TIME],
                        [WAKE_TIME],
                        [IN_BED],
                        [AWAKE],
                        [SESSIONS],
                        [ASLEEP],
                        [ASLEEP_AVERAGE],
                        [EFFICIENCY],
                        [EFFICIENCY_AVERAGE],
                        [QUALITY],
                        [QUALITY_AVERAGE],
                        [DEEP],
                        [DEEP_AVERAGE],
                        [SLEEP_BPM],
                        [SLEEP_BPM_AVERAGE],
                        [DAY_BPM],
                        [DAY_BPM_AVERAGE],
                        [WAKING_BPM],
                        [WAKING_BPM_AVERAGE],
                        [HRV],
                        [HRV_AVERAGE],
                        [SLEEP_HRV],
                        [SLEEP_HRV_AVERAGE],
                        [SPO2_AVERAGE],
                        [SPO2_MIN],
                        [SPO2_MAX]
                        )
                        VALUES "
                );
                bool first = true;
                //iterative
                foreach (AutoSleepDay day in autoSleepDays) 
                {
                    if ( first )
                    {
                        first = false;
                    }
                    else 
                    {
                        query.Append(',');
                    }
                    /*String formatting info
                     * https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
                     */
                    string line = string.Format("({0},({1}),(2),(3),(4),(5), (6),7,(8),(9),10,11,(12),(13),(14),(15), 16,17,18,19,20,21,22,23,24,25,26,27,28)",
                        day.DateTimeOfInstance  .ToString("yyyy-MM-dd"),//0
                        day.FromDate            .ToString("yyyy-MM-dd"),//1
                        day.ToDate              .ToString("yyyy-MM-dd"),//2
                        day.BedTime             .ToString("o"),         //3
                        day.WakeTime            .ToString("o"),         //4
                        day.InBed               .ToString("o"),         //5
                        day.Awake               .ToString("o"),         //6
                        day.Sessions            .ToString(),            //7
                        day.Asleep              .ToString("o"),         //8
                        day.AsleepAverage       .ToString("o"),         //9
                        day.Efficiency          .ToString(),            //10
                        day.EfficiencyAverage   .ToString(),            //11
                        day.Quality             .ToString("o"),         //12
                        day.QualityAverage      .ToString("o"),         //13
                        day.Deep                .ToString("o"),         //14
                        day.DeepAverage         .ToString("o"),         //15
                        day.SleepBPM            .ToString(),            //16
                        day.SleepBPMAverage     .ToString(),            //17
                        day.DayBPM              .ToString(),            //18
                        day.DayBPMAverage       .ToString(),            //19
                        day.WakeingBPM          .ToString(),            //20
                        day.WakeingBPMAverage   .ToString(),            //21
                        day.HRV                 .ToString(),            //22
                        day.HRVAverage          .ToString(),            //23
                        day.SleepHRV            .ToString(),            //24
                        day.SleepHRVAverage     .ToString(),            //25
                        day.SPO2Average         .ToString(),            //26
                        day.SPO2Min             .ToString(),            //27
                        day.SPO2Max             .ToString()             //28
                        );
                }
                cmd.CommandText = query.ToString();
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
    }
}
