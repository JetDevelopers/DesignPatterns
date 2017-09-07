using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterDemo
{
    public class DataRender
    {
        private readonly IDbDataAdapter _dataAdapter;

        public DataRender(IDbDataAdapter adapter)
        {
            _dataAdapter = adapter;
        }

        public void Render(TextWriter textWriter)
        {
            textWriter.Write("Rendering data");
            DataSet ds = new DataSet();

            _dataAdapter.Fill(ds);

            foreach (DataTable tbl in ds.Tables)
            {
                foreach (DataColumn col in tbl.Columns)
                {
                    textWriter.Write(col.ColumnName.PadRight(20) + " ");
                }
                foreach (DataRow row in tbl.Rows)
                {
                    for(int i=0; i< tbl.Columns.Count; i++)
                    {
                        textWriter.Write(row[i].ToString().PadRight(20) + " ");
                    }
                    textWriter.WriteLine();
                }
            }

          
        }
    }
}
