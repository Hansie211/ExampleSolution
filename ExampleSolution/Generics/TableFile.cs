using ExampleSolution.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExampleSolution.Generics
{
    public class TableFile<TEntity> : TableFile, IEnumerable<TEntity> where TEntity : new()
    {
        private PropertyInfo[] ColumnBindings { get; }

        private static string GetColumnName( PropertyInfo info )
        {
            if ( !info.IsDefined( typeof( ColumnNameAttribute ) ) )
            {
                return null;
            }

            return info.GetCustomAttribute<ColumnNameAttribute>().Value;
        }

        private static PropertyInfo[] GetColumnBindings( Type entityType, string[] columnNames )
        {
            var result      = new PropertyInfo[ columnNames.Length ];
            var properties  = entityType.GetProperties( BindingFlags.Public | BindingFlags.Instance );

            var pairs = properties.Where( o => o.IsDefined( typeof(ColumnNameAttribute) ) ).Select( o => new KeyValuePair<string, PropertyInfo>( GetColumnName( o ), o ) );
            for ( int i = 0; i < result.Length; i++ )
            {
                result[ i ] = pairs.First( x => x.Key.Equals( columnNames[ i ] ) ).Value;
            }

            return result;
        }

        public TableFile( Stream stream ) : base( stream )
        {
            ColumnBindings = GetColumnBindings( typeof(TEntity), ColumnNames );
        }

        public TableFile( string filepath ) : base( filepath )
        {
            // ColumnBindings ??
        }

        public TableFile( byte[] buffer ) : base( buffer )
        {
            // ColumnBindings ??
        }

        private TEntity CreateEntity( string[] row )
        {
            var entity  = new TEntity();

            // Ommitted

            return entity;
        }

        public new IEnumerable<TEntity> GetAll()
        {
            foreach ( var row in base.GetAll() )
            {
                yield return CreateEntity( row );
            }
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
