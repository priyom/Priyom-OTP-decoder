using System;

namespace PriyomOTPcoder
{
    public class FileObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MessageText { get; set; }
        public string MessageFigure { get; set; }
        public bool SpaceFlag { get; set; }

        public void WriteFile()
        {
            var linestowrite = new[]
                                   {
                                       new FileOperations.FileLine(Id.ToString()),
                                       new FileOperations.FileLine(SpaceFlag.ToString()),
                                       new FileOperations.FileLine(Name),
                                       new FileOperations.FileLine(MessageText),
                                       new FileOperations.FileLine(MessageFigure)
                                   };
            var newStuff = new FileOperations.FileLines(linestowrite);
            FileOperations.WriteFile(Name, newStuff);
        }
        public void ReadFile(string name)
        {
            Name = name;
            var file = FileOperations.Readfile(Name);
            Id = new Guid(file[0].Fileline);
            SpaceFlag = Convert.ToBoolean(file[1].Fileline);
            MessageText = file[3].Fileline;
            //MessageFigure = file[4].Fileline;
        }
    }
}
