using DataAccessLibary;
using Microsoft.AspNetCore.Http;
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
                        AutoSleepDay day = new AutoSleepDay()
                        {
                            DateTimeOfInstance = Convert.ToDateTime(fields[0]),
                            FromDate = DateOnly.FromDateTime(Convert.ToDateTime(fields[1])),
                            ToDate = DateOnly.FromDateTime(Convert.ToDateTime(fields[2])),
                            BedTime = Convert.ToDateTime(fields[3]),
                            WakeTime = Convert.ToDateTime(fields[4]),
                            InBed = TimeOnly.FromDateTime(Convert.ToDateTime(fields[5])),
                            Awake = TimeOnly.FromDateTime(Convert.ToDateTime(fields[6])),
                            //Skip 7
                            Sessions = Convert.ToInt32(fields[8]),
                            Asleep = TimeOnly.FromDateTime(Convert.ToDateTime(fields[9])),
                            AsleepAverage = TimeOnly.FromDateTime(Convert.ToDateTime(fields[10])),
                            Efficiency = Convert.ToDouble(fields[11]),
                            EfficiencyAverage = Convert.ToDouble(fields[12]),
                            Quality = TimeOnly.FromDateTime(Convert.ToDateTime(fields[13])),
                            QualityAverage = TimeOnly.FromDateTime(Convert.ToDateTime(fields[14])),
                            Deep = TimeOnly.FromDateTime(Convert.ToDateTime(fields[15])),
                            DeepAverage = TimeOnly.FromDateTime(Convert.ToDateTime(fields[16])),
                            SleepBPM = Convert.ToDouble(fields[17]),
                            SleepBPMAverage = Convert.ToDouble(fields[18]),
                            DayBPM = Convert.ToDouble(fields[19]),
                            DayBPMAverage = Convert.ToDouble(fields[20]),
                            WakeingBPM = Convert.ToDouble(fields[21]),
                            WakeingBPMAverage = Convert.ToDouble(fields[22]),
                            HRV = Convert.ToInt32(fields[23]),
                            HRVAverage = Convert.ToInt32(fields[24]),
                            SleepHRV = Convert.ToInt32(fields[25]),
                            SleepHRVAverage = Convert.ToInt32(fields[26]),
                            SPO2Average = Convert.ToDouble(fields[27]),
                            SPO2Min = Convert.ToInt32(fields[28]),
                            SPO2Max = Convert.ToInt32(fields[29])
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
