using DataAccessLibary;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using VitaQuest.Data;
using VitaQuest.Pages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VitaQuest.Services
{
    public class CSVDataService
    {
        public CSVDataService() { }

        public async void processAppleFitnessDataAsync( string fileData ) 
        {
            StringReader csvStringReader = new StringReader(fileData);
            // parse the csv
            using(Microsoft.VisualBasic.FileIO.TextFieldParser csvParse = new Microsoft.VisualBasic.FileIO.TextFieldParser(csvStringReader))
            {
                csvParse.TextFieldType = 
                    Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                csvParse.SetDelimiters(",");
                if (!csvParse.EndOfData)
                {// skip first line
                    csvParse.ReadLine();
                }
                List<AppleFitnessDay> appleFitnessDays = new List<AppleFitnessDay>();
                while (!csvParse.EndOfData)
                {
                    //Process row
                    string[]? fields = csvParse.ReadFields();
                    if (fields != null) 
                    {
                        //convert csv data into data structure
                        AppleFitnessDay day = new AppleFitnessDay();
                        day.DateTimeOfInstance = DateTime.Parse(fields[0]);
                        day.HeartRateMin = int.Parse(fields[1]);
                        day.HeartRateMax = int.Parse(fields[2]);
                        day.HeartRateAverage = int.Parse(fields[3]);
                        day.HeartRateResting = int.Parse(fields[4]);
                        day.HeartRateActive = int.Parse(fields[5]);
                        day.HeartRateVariability = int.Parse(fields[6]);
                        day.StepCount = int.Parse(fields[10]);
                        day.CaloriesBurnt = int.Parse(fields[12]);
                        //add it
                        appleFitnessDays.Add(day);
                    }
                }
                if (appleFitnessDays.Count >0) 
                {
                    new AppleFitness().WiteToDB(appleFitnessDays.ToArray());
                }
            }
        }

        public async void processAutoSleepDataAsync( string fileData) 
        {
            StringReader csvStringReader = new StringReader(fileData);
            // parse the csv
            using (Microsoft.VisualBasic.FileIO.TextFieldParser csvParse = new Microsoft.VisualBasic.FileIO.TextFieldParser(csvStringReader))
            {
                csvParse.TextFieldType =
                    Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                csvParse.SetDelimiters(",");
                if (!csvParse.EndOfData)
                {// skip first line
                    csvParse.ReadLine();
                }
                List<AutoSleepDay> autoSleepDays = new List<AutoSleepDay>();
                while (!csvParse.EndOfData)
                {
                    //Process row
                    string[]? fields = csvParse.ReadFields();
                    if (fields != null)
                    {
                        //convert csv data into data structure
                        AutoSleepDay day = new AutoSleepDay();
                        {
                            day.DateTimeOfInstance  = Convert.ToDateTime(fields[0]);
                            day.FromDate            = DateOnly.FromDateTime(Convert.ToDateTime(fields[1]));
                            day.ToDate              = DateOnly.FromDateTime(Convert.ToDateTime(fields[2]));
                            day.BedTime             = Convert.ToDateTime(DataDefaultUtils.StringDefault(fields[3],"0"));
                            day.WakeTime            = Convert.ToDateTime(DataDefaultUtils.StringDefault(fields[4],"0"));
                            day.InBed               = fields[5].IsNullOrEmpty() ? new TimeOnly() : TimeOnly.FromDateTime(Convert.ToDateTime(fields[5]));
                            day.Awake               = fields[6].IsNullOrEmpty() ? new TimeOnly() : TimeOnly.FromDateTime(Convert.ToDateTime(fields[6]));
                            //Skip 7
                            day.Sessions            = Convert.ToInt32(DataDefaultUtils.StringDefault(fields[8], "0"));
                            day.Asleep              = fields[9].IsNullOrEmpty() ? new TimeOnly() : TimeOnly.FromDateTime(Convert.ToDateTime(fields[9]));
                            day.AsleepAverage       = fields[10].IsNullOrEmpty() ? new TimeOnly():TimeOnly.FromDateTime(Convert.ToDateTime(fields[10]));
                            day.Efficiency          = Convert.ToDouble(DataDefaultUtils.StringDefault(fields[11],"0"));
                            day.EfficiencyAverage   = Convert.ToDouble(DataDefaultUtils.StringDefault(fields[12],"0"));
                            day.Quality             = fields[13].IsNullOrEmpty() ? new TimeOnly():TimeOnly.FromDateTime(Convert.ToDateTime(fields[13]));
                            day.QualityAverage      = fields[14].IsNullOrEmpty() ? new TimeOnly():TimeOnly.FromDateTime(Convert.ToDateTime(fields[14]));
                            day.Deep                = fields[15].IsNullOrEmpty() ? new TimeOnly():TimeOnly.FromDateTime(Convert.ToDateTime(fields[15]));
                            day.DeepAverage         = fields[16].IsNullOrEmpty() ? new TimeOnly():TimeOnly.FromDateTime(Convert.ToDateTime(fields[16]));
                            day.SleepBPM            = Convert.ToDouble(DataDefaultUtils.StringDefault(fields[17], "0"));
                            day.SleepBPMAverage     = Convert.ToDouble(DataDefaultUtils.StringDefault(fields[18], "0"));
                            day.DayBPM              = Convert.ToDouble(DataDefaultUtils.StringDefault(fields[19], "0"));
                            day.DayBPMAverage       = Convert.ToDouble(DataDefaultUtils.StringDefault(fields[20], "0"));
                            day.WakeingBPM          = Convert.ToDouble(DataDefaultUtils.StringDefault(fields[21], "0"));
                            day.WakeingBPMAverage   = Convert.ToDouble(DataDefaultUtils.StringDefault(fields[22], "0"));
                            day.HRV                 = Convert.ToInt32(DataDefaultUtils.StringDefault(fields[23], "0"));
                            day.HRVAverage          = Convert.ToInt32(DataDefaultUtils.StringDefault(fields[24], "0"));
                            day.SleepHRV            = Convert.ToInt32(DataDefaultUtils.StringDefault(fields[25], "0"));
                            day.SleepHRVAverage     = Convert.ToInt32(DataDefaultUtils.StringDefault(fields[26], "0"));
                            day.SPO2Average         = Convert.ToDouble(DataDefaultUtils.StringDefault(fields[27], "0"));
                            day.SPO2Min             = Convert.ToInt32(DataDefaultUtils.StringDefault(fields[28], "0"));
                            day.SPO2Max             = Convert.ToInt32(DataDefaultUtils.StringDefault(fields[29], "0"));
                        };
                        //add it
                        autoSleepDays.Add(day);
                    }
                }
                if (autoSleepDays.Count > 0)
                {
                    new AutoSleep().WriteToDB(autoSleepDays.ToArray());
                }
            }
        }
    }
}
