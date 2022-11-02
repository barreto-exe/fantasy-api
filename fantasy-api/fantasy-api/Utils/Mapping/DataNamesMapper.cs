using FantasyApi.DataAnotations;
using FantasyApi.Utils.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Core.Utils.Mapping
{
    public class DataNamesMapper<TEntity> where TEntity : class, new()
    {
        public TEntity Map(DataRow row)
        {
            TEntity entity = new TEntity();
            return Map(row, entity);
        }

        public TEntity Map(DataRow row, TEntity entity)
        {
            var columnNames = row.Table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
            var properties = GetEntityProperties();

            foreach (var prop in properties)
            {
                PassValues(row, entity, prop);
            }

            return entity;
        }

        public IEnumerable<TEntity> Map(DataTable table)
        {
            List<TEntity> entities = new List<TEntity>();
            var columnNames = table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
            var properties = GetEntityProperties();

            foreach (DataRow row in table.Rows)
            {
                TEntity entity = new();
                foreach (var prop in properties)
                {
                    PassValues(row, entity, prop);
                }
                entities.Add(entity);
            }

            return entities;
        }

        public string GetActualColumnName(string alternativeName)
        {
            var properties = GetEntityProperties();
            var property = properties.Where(p => p.Name.Equals(alternativeName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (property == null) return alternativeName;

            var dataNames = AttributeHelper.GetDataNames(typeof(TEntity), property.Name);
            return dataNames.FirstOrDefault() ?? alternativeName;
        }

        private static List<PropertyInfo> GetEntityProperties()
        {
            return (typeof(TEntity)).GetProperties()
                                    .Where(x =>
                                        x.GetCustomAttributes(typeof(DataNamesAttribute), true).Any() ||
                                        x.GetCustomAttributes(typeof(NestedDto), true).Any())
                                    .ToList();
        }

        private static void PassValues(DataRow row, TEntity entity, PropertyInfo prop)
        {
            if (prop.GetCustomAttributes(typeof(NestedDto), true).Any())
            {
                dynamic mapper = Activator
                    .CreateInstance(typeof(DataNamesMapper<>)
                    .MakeGenericType(prop.PropertyType));
                var value = mapper.Map(row);
                prop.SetValue(entity, value);
            }
            else
            {
                PropertyMapHelper.Map(typeof(TEntity), row, prop, entity);
            }
        }
    }
}
