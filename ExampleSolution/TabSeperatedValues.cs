using System;
using System.Collections.Generic;
using System.IO;

namespace ExampleSolution
{
    public class TabSeperatedValues
    {
        public Stream Stream { get; }
        public StreamReader StreamReader { get; }

        public string[] ColumnNames { get; }

        public TabSeperatedValues( Stream stream )
        {
            Stream          = stream;
            StreamReader    = new StreamReader( stream );

            ColumnNames     = StreamReader.ReadLine().Split('\t');
        }

        public TabSeperatedValues( string filepath ) : this( new FileStream( filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) )
        {

        }

        public TabSeperatedValues( byte[] buffer ) : this( new MemoryStream( buffer ) )
        {

        }

        public IEnumerable<string[]> GetAll()
        {
            string line;
            while( (line = StreamReader.ReadLine()) == null )
            {
                yield return line.Split('\t');
            }
        }
    }
}
