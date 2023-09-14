using DataAccessLibary;
using System.Collections;
using VitaQuest.Data;

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
                    AppleFitness appleFitness = new AppleFitness();
                    appleFitness.WiteToDB(appleFitnessDays.ToArray());
                }

            }
        }

        public async void processAutoSleepDataAsync( string fileData) 
        {

        }

    }
}
