using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace lab2
{
    public sealed class Cat
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Breed { get; set; }
        public bool Vaccination { get; set; }

        public static List<Cat> ReadCatFromCSV(string filePath)
        {
            List<Cat> dataList = new List<Cat>();

            using (StreamReader reader = new StreamReader(filePath))
            using (CsvReader csvReader = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                csvReader.Context.RegisterClassMap<CatCsvMap>();
                
                while (csvReader.Read())
                {
                    Cat cat = csvReader.GetRecord<Cat>();
                    dataList.Add(cat);
                }
            }

            return dataList;
        }

        public void WriteCatToCSV(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            using (CsvWriter csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(new List<Cat> { this });
            }
        }
    }

    public sealed class CatCsvMap : ClassMap<Cat>
    {
        public CatCsvMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.Age).Name("Age");
            Map(m => m.Gender).Name("Gender");
            Map(m => m.Breed).Name("Breed");
            Map(m => m.Vaccination).Name("Vaccination");
        }
    }
}