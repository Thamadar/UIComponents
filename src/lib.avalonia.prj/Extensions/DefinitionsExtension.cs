using Avalonia.Controls; 

namespace Lib.Avalonia.Extensions
{
    public static class DefinitionsExtension
    {
        /// <summary>
        /// Клонирование ColumnDefinitions.
        /// </summary> 
        public static ColumnDefinitions Clone(this ColumnDefinitions column)
        {
            var columnDefinitions = new ColumnDefinitions();

            if(column == null ||
               column.Count == 0)
                return columnDefinitions;

            for(int i = 0; i < column.Count; i++)
            {
                var columnDefinition = new ColumnDefinition();

                columnDefinition.Width = new GridLength(column[i].Width.Value, column[i].Width.GridUnitType);

                columnDefinitions.Add(columnDefinition);
            }

            return columnDefinitions;
        }
    }
}
