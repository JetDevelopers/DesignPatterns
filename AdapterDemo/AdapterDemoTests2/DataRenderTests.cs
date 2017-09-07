using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdapterDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace AdapterDemo.Tests
{
    [TestClass()]
    public class DataRenderTests
    {
        [TestMethod()]
        public void RenderTest()
        {
            FakeAdapter fakeAdapter = new FakeAdapter();
            DataRender dataRender = new DataRender(fakeAdapter);
            StringWriter sb = new StringWriter();

            dataRender.Render(sb);

            var result = sb.ToString();
            Console.Write(result);

            var count = result.Count(c => c == '\n');
            Assert.AreEqual(1, count);
        }
        [TestMethod()]
        public void StudentRenderTest()
        {
            IEnumerable < Student > students = (new Student[] { new Student { Rno=1, Name = "A"  }, new Student { Rno=2, Name="B" } }).AsEnumerable<Student>();

            //EmployeeAdapter adapter = new EmployeeAdapter();
            StudentClient cleint = new StudentClient();

            var result = cleint.ListsPatterns(students);
            Console.Write(result);

            var count = result.Count(c => c == '\n');
            Assert.AreEqual(students.Count(), count);
        }


    }

    public class FakeAdapter : IDbDataAdapter
    {
        public IDbCommand SelectCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDbCommand InsertCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDbCommand UpdateCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDbCommand DeleteCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MissingMappingAction MissingMappingAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MissingSchemaAction MissingSchemaAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ITableMappingCollection TableMappings => throw new NotImplementedException();

        public int Fill(DataSet dataSet)
        {
            DataTable dt = new DataTable("tbl");
            dt.Columns.Add(new DataColumn { ColumnName = "RNo", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn { ColumnName="Name", DataType= typeof(string) });
            DataRow dr = dt.NewRow();

            dr["Rno"] = "1";
            dr["Name"] = "Gopi";

            dt.Rows.Add( dr );

            dataSet.Tables.Add(dt);
            dataSet.AcceptChanges();

            return 1;
        }

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
        {
            throw new NotImplementedException();
        }

        public IDataParameter[] GetFillParameters()
        {
            throw new NotImplementedException();
        }

        public int Update(DataSet dataSet)
        {
            throw new NotImplementedException();
        }
    }
}