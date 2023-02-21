namespace ConsoleApp
{
    using ConsoleApp.ImportedObject;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class DataReader

    {
        /// <summary>
        /// Method for read date from file
        /// </summary>
        /// <param name="fileToImport">Path to the file from which need to read data</param>
        /// <returns>List of string that collect info from file</returns>
        public List<string> ReadDataFromFile(string fileToImport)
        {
            if (string.IsNullOrWhiteSpace(fileToImport)) { Console.WriteLine("Path to the file is empty"); return null; }
            if (!File.Exists(fileToImport)) { Console.WriteLine("This file is not exist."); return null; }

            List<ImportedFile> ImportedObjects = new List<ImportedFile>();

            StreamReader streamReader = new StreamReader(fileToImport);

            List<string> importedLines = new List<string>();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                importedLines.Add(line);
            }
            return importedLines;

        }

        /// <summary>
        /// Convert information from list of string to list of imported files
        /// </summary>
        /// <param name="importedLines">List of strings from file</param>
        /// <returns>List of imported files</returns>
        public List<ImportedFile> ConvertInfoFromFile(List<string> importedLines)
        {
            if (importedLines.Count == 0) return new List<ImportedFile>();

            List<ImportedFile> ImportedObjects = new List<ImportedFile>();

            for (int i = 0; i < importedLines.Count; i++)
            {
                var importedLine = importedLines[i];

                if (string.IsNullOrWhiteSpace(importedLine)) continue;

                var values = importedLine.Split(';');

                if (values.Length != 7)
                {
                    continue;
                }

                var importedObject = new ImportedFile();

                importedObject.Type = values[0];
                importedObject.Name = values[1];
                importedObject.Schema = values[2];
                importedObject.ParentName = values[3];
                importedObject.ParentType = values[4];
                importedObject.DataType = values[5];
                importedObject.IsNullable = values[6];

                ImportedObjects.Add(importedObject);
            }

            return ImportedObjects;
        }

        /// <summary>
        /// Correct data in list of imported file
        /// </summary>
        /// <param name="importedFiles">List of imported file</param>
        /// <returns>Corrected list of imported file</returns>
        public List<ImportedFile> CorrectData(List<ImportedFile> importedFiles)
        {
            if (importedFiles.Count == 0)
            {
                Console.Error.WriteLine("Error while trying to correct Data: list of files is empty");
                return null;
            }

            foreach (var importedObject in importedFiles)
            {
                importedObject.DataType = importedObject.DataType?.Trim().Replace(Environment.NewLine, "");
                importedObject.Name = importedObject.Name?.Trim().Replace(Environment.NewLine, "");
                importedObject.Schema = importedObject.Schema?.Trim().Replace(Environment.NewLine, "");
                importedObject.ParentName = importedObject.ParentName?.Trim().Replace(Environment.NewLine, "");
                importedObject.ParentType = importedObject.ParentType?.Trim().Replace(Environment.NewLine, "");
            }

            return importedFiles;
        }

        /// <summary>
        /// Fill in objects with information about count of childrens
        /// </summary>
        /// <param name="ImportedObjects">List of Imported file for filling up information about childrens</param>
        public void AssignNumberOfChilren(List<ImportedFile> ImportedObjects)
        {
            if (ImportedObjects.Count == 0)
            {
                Console.Error.WriteLine("Cannot assign number of children: list of elements is empty");
                return;
            }

            for (int i = 0; i < ImportedObjects.Count; i++)
            {
                ImportedFile parent = ImportedObjects[i];

                foreach (ImportedFile child in ImportedObjects)
                {
                    if (parent?.Type.ToUpper() != child?.ParentType.ToUpper()) { continue; }

                    if (parent?.Name != child?.ParentName) { continue; }

                    parent.NumberOfChildren += 1;
                }
            }
        }

        /// <summary>
        /// Print info about objects
        /// </summary>
        /// <param name="ImportedFiles">List type of imported files</param>
        public void PrintInfo(List<ImportedFile> ImportedFiles)
        {
            if (ImportedFiles.Count == 0) Console.WriteLine("File is empty. No info");

            foreach (var database in ImportedFiles)
            {
                if (database.Type == null)
                {
                    // I added this line for inform developer about problem
                    // Here could be another business logic 
                    Console.WriteLine("Data type is null => exit");
                    continue;
                }

                if (database?.Type.ToLower() != "database") continue;

                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                // print all database's tables
                foreach (var table in ImportedFiles)
                {
                    if (table.Type.ToLower() != "table") continue;

                    if (table.ParentType?.ToUpper() != database.Type.ToUpper()) continue;
                    if (table.ParentName?.ToUpper() != database.Name?.ToUpper()) { continue; }


                    Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                    // print all table's columns
                    foreach (var column in ImportedFiles)
                    {
                        if (column.Type.ToLower() != "column") continue;

                        if (column.ParentName != table.Name) continue;

                        if (column.ParentType?.ToUpper() != table.Type.ToUpper()) continue;

                        Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");

                    }
                }
            }
            Console.ReadLine();
        }
    }
}
