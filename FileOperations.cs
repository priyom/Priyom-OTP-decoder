using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PriyomOTPcoder
{
    class FileOperations
    {
        private static readonly string Docsfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static FileLine[] Readfile(string filename)
        {
            var file = System.IO.File.ReadAllLines(Docsfolder + "\\" + filename + ".txt");
            var newlinesArray = new FileLine[file.Count()];
            var count = file.Count();
            for (var i = 0; i < count; i++ )
            {
                newlinesArray[i] = new FileLine(file[i]);
            }
            return newlinesArray;
        }
        public static void WriteFile(string filename, FileLines fileLines)
        {
            var file = new System.IO.StreamWriter(Docsfolder + "\\" + filename + ".txt");
            using (file)
            {
                 foreach (var fileLine in fileLines)
                {
                    file.WriteLine(fileLine.Fileline);
                }
            }
        }
        public class FileLine
        {
            public FileLine(string fline)
            {
                Fileline = fline;
            }
            public string Fileline;
            public int Length()
            {
                return Fileline.Length;
            }
        }
        public class FileLines : IEnumerable
        {
            private readonly FileLine[] _filelines;
            public FileLines(IList<FileLine> fArray)
            {
                _filelines = new FileLine[fArray.Count];
                for (var i = 0; i < fArray.Count; i++ )
                {
                    _filelines[i] = fArray[i];
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public FileLineEnum GetEnumerator()
            {
                return new FileLineEnum(_filelines);
            }
        }
        public class FileLineEnum : IEnumerator
        {
            public FileLine[] Filelines;

            private int _position = -1;
            public FileLineEnum(FileLine[] list)
            {
                Filelines = list;
            }
            public bool MoveNext()
            {
                _position++;
                return (_position < Filelines.Length);
            }
            public void Reset()
            {
                _position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }
            public FileLine Current
            {
                get
                {
                    try
                    {
                        return Filelines[_position];
                    }
                     catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
