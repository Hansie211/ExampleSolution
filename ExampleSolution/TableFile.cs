using System;
using System.Collections.Generic;
using System.IO;

namespace ExampleSolution
{
    public abstract class TableFile
    {
        public Stream Stream { get; }
        public StreamReader StreamReader { get; }

        public string[] ColumnNames { get; }

        public TableFile( Stream stream )
        {
            Stream          = stream;
            StreamReader    = new StreamReader( stream );

            ColumnNames     = StreamReader.ReadLine().Split('\t');
        }

        public TableFile( string filepath ) : this( new FileStream( filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) )
        {

        }

        public TableFile( byte[] buffer ) : this( new MemoryStream( buffer ) )
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
