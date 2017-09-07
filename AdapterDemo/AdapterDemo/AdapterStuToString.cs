using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterDemo
{
    public class StudentClient
    {
        private readonly IStudentDetailAdapter studentDetailAdapter;

        public StudentClient(IStudentDetailAdapter studentDetailAdapter)
        {
            this.studentDetailAdapter = studentDetailAdapter;
        }

        public StudentClient() : this(new StudentAdapter()) 
        {

        }
        public string ListsPatterns(IEnumerable<Student> students) 
        {
            return studentDetailAdapter.ListsPatterns(students);
        }
    }

    public class EmployeeAdapter : IStudentDetailAdapter
    {
        public string ListsPatterns(IEnumerable<Student> students)
        {
            throw new NotImplementedException();
        }
    }

    public class StudentAdapter : IStudentDetailAdapter
    {
        private  DataRender dataRender;

        public StudentAdapter()
        {
          
        }
        public string ListsPatterns(IEnumerable<Student> students)
        {
            MyOwnDataAdapter adap = new MyOwnDataAdapter(students);
            dataRender = new DataRender(adap);
            StringWriter sw = new StringWriter();

            dataRender.Render(sw);
            return sw.ToString();
        }
    }

    public class MyOwnDataAdapter : IDbDataAdapter
    {
        private readonly IEnumerable<Student> students;

        public MyOwnDataAdapter(IEnumerable<Student> students)
        {
            this.students = students;
        }

        IDbCommand IDbDataAdapter.SelectCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IDbCommand IDbDataAdapter.InsertCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IDbCommand IDbDataAdapter.UpdateCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IDbCommand IDbDataAdapter.DeleteCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        MissingMappingAction IDataAdapter.MissingMappingAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        MissingSchemaAction IDataAdapter.MissingSchemaAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        ITableMappingCollection IDataAdapter.TableMappings => throw new NotImplementedException();

        int IDataAdapter.Fill(DataSet dataSet)
        {
            DataTable dt = new DataTable("tbl");
            dt.Columns.Add(new DataColumn { ColumnName = "RNo", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn { ColumnName = "Name", DataType = typeof(string) });

            foreach (var student in students)
            {
                DataRow dr = dt.NewRow();

                dr["Rno"] = student.Rno;
                dr["Name"] = student.Name;

                dt.Rows.Add(dr);
            }

            dataSet.Tables.Add(dt);
            dataSet.AcceptChanges();

            return students.Count();
        }

        DataTable[] IDataAdapter.FillSchema(DataSet dataSet, SchemaType schemaType)
        {
            throw new NotImplementedException();
        }

        IDataParameter[] IDataAdapter.GetFillParameters()
        {
            throw new NotImplementedException();
        }

        int IDataAdapter.Update(DataSet dataSet)
        {
            throw new NotImplementedException();
        }
    }
}
