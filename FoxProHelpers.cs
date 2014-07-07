using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

using SysconCommon.Foxpro;
using SysconCommon.Common;
using SysconCommon.Common.Validity;
using SysconCommon.Algebras.DataTables;

namespace BiddersList
{
    public static class FoxProHelpers
    {
        public static string FoxproInsertString(this DataRow self, string table_name)
        {
            var keys = from c in self.Table.Columns
                       select c.ColumnName;

            var vals = self.ItemArray.Select(v => v.FQ());

            return string.Format("insert into {0} ({1}) values ({2})", table_name, string.Join(",", keys), string.Join(",", vals));
        }

        /// <summary>
        /// Changelog- added support for boolean type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FQ(this object obj)
        {
            var t = obj.GetType();

            var numeric_types = new Type[] {
                typeof(decimal), typeof(int), typeof(long), typeof(double), typeof(float)
            };

            var boolean_type = new Type[] { typeof(bool) };

            var date_types = new Type[] {
                typeof(DateTime)
            };

            if (DBNull.Value.Equals(obj) || obj == null)
            {
                return "null";
            }

            if (numeric_types.Contains(t))
            {
                return obj.ToString();
            }

            if (boolean_type.Contains(t))
            {
                return (bool)obj ? "1" : "0";
            }

            if (date_types.Contains(t))
            {
                return ((DateTime)obj).ToFoxproDate();
            }

            return obj.ToString().FoxproQuote();
        }
       
    }
}
