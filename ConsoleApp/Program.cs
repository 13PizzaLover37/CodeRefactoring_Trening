namespace ConsoleApp
{
    using ConsoleApp.ImportedObject;
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new DataReader();
            
            List<string> sourceDataFile = reader.ReadDataFromFile("dataa.csv");
            if (sourceDataFile == null) { Console.WriteLine("Path to file is empty"); return; }

            List<ImportedFile> dataFile = reader.ConvertInfoFromFile(sourceDataFile);
            if (dataFile.Count == 0) { Console.WriteLine("Data from file is empty."); return; }
            
            reader.CorrectData(ref dataFile);
            reader.AssignNumberOfChilren(ref dataFile);
            reader.PrintInfo(ref dataFile);
        }
    }
}
