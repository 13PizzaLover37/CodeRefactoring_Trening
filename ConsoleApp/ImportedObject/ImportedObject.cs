using ConsoleApp.Interfaces;

namespace ConsoleApp.ImportedObject
{
    public class ImportedFile : IImportedObjectBase
    {
        public string Name { get; set; }
        public string Schema;
        public string Type { get; set; }
        public string ParentName;
        public string ParentType { get; set; }
        public string DataType { get; set; }
        public string IsNullable { get; set; }
        public int NumberOfChildren;
    }
}
