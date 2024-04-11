using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using JapanStock_Application.Model;

namespace JapanStock_WebApplication.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class JapanStockController : ControllerBase
    {
        [HttpPost]
        [Route("insertStockData")]
        public void insertStockData(StockData xxx)
        {
            SqlServer sql = new SqlServer();
            SqlConnection cnn = new SqlConnection(sql.connectTo());
            cnn.Open();
            SqlCommand myCommand = new SqlCommand();
            myCommand = cnn.CreateCommand();
            myCommand.CommandType = CommandType.StoredProcedure;//宣告為SP
            myCommand.CommandText = "insert_StockData";
            // Add Number
            myCommand.Parameters.AddWithValue("@Number", xxx.Number);
            // Add Name
            myCommand.Parameters.AddWithValue("@StockName", xxx.Name);
            // Add Market
            myCommand.Parameters.AddWithValue("@Market", xxx.Market);
            // Add Industry
            myCommand.Parameters.AddWithValue("@Industry", xxx.Industry);
            // Add StockValue
            if (!(xxx.Value == null)) { myCommand.Parameters.AddWithValue("@StockValue", xxx.Value); }
            else { myCommand.Parameters.AddWithValue("@StockValue", DBNull.Value); }
            // Add DayBeforeDiff
            if (!(xxx.DayBeforeDiff == null)) { myCommand.Parameters.AddWithValue("@DayBeforeDiff", xxx.DayBeforeDiff); }
            else { myCommand.Parameters.AddWithValue("@DayBeforeDiff", DBNull.Value); }
            // Add DayBeforeRatio
            if (!(xxx.DayBeforeRatio == null)) { myCommand.Parameters.AddWithValue("@DayBeforeRatio", xxx.DayBeforeRatio); }
            else { myCommand.Parameters.AddWithValue("@DayBeforeRatio", DBNull.Value); }
            // Add DevidedYield
            if (!(xxx.DevidedYield == null)) { myCommand.Parameters.AddWithValue("@DevidedYield", xxx.DevidedYield); }
            else { myCommand.Parameters.AddWithValue("@DevidedYield", DBNull.Value); }
            // Add PER
            if (!(xxx.PER == null)) { myCommand.Parameters.AddWithValue("@PER", xxx.PER); }
            else { myCommand.Parameters.AddWithValue("@PER", DBNull.Value); }
            // Add PBR
            if (!(xxx.PBR == null)) { myCommand.Parameters.AddWithValue("@PBR", xxx.PBR); }
            else { myCommand.Parameters.AddWithValue("@PBR", DBNull.Value); }
            // Add ROE
            if (!(xxx.ROE == null)) { myCommand.Parameters.AddWithValue("@ROE", xxx.ROE); }
            else { myCommand.Parameters.AddWithValue("@ROE", DBNull.Value); }
            // Add AggregateValue
            if (!(xxx.AggregateValue == null)) { myCommand.Parameters.AddWithValue("@AggregateValue", xxx.AggregateValue); }
            else { myCommand.Parameters.AddWithValue("@AggregateValue", DBNull.Value); }
            // Add MovingAverage
            if (!(xxx.MovingAverage == null)) { myCommand.Parameters.AddWithValue("@MovingAverage", xxx.MovingAverage); }
            else { myCommand.Parameters.AddWithValue("@MovingAverage", DBNull.Value); }
            // Add EquityRatio
            if (!(xxx.EquityRatio == null)) { myCommand.Parameters.AddWithValue("@EquityRatio", xxx.EquityRatio); }
            else { myCommand.Parameters.AddWithValue("@EquityRatio", DBNull.Value); }
            // Add StockDatetime
            myCommand.Parameters.AddWithValue("@StockDatetime", xxx.Datetime);
            int recordsAffected = myCommand.ExecuteNonQuery();
            cnn.Close();
        }
        public class SqlServer
        {
            public string connectTo()
            {
                string connetionString = $@"data source=localhost;
                                initial catalog=Stock;user id=sa;
                                persist security info=True;
                                password=1qaz2wsx;
                                workstation id=LAPTOP-CSG847EV;
                                packet size=4096;";
                return connetionString;
            }
            public DataSet getDataByText(string commandText)
            {
                SqlServer sql = new SqlServer();
                SqlConnection cnn = new SqlConnection(sql.connectTo());
                cnn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand = cnn.CreateCommand();
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = @commandText;
                myCommand.ExecuteNonQuery();
                SqlDataAdapter myDataAdapter = new SqlDataAdapter(myCommand);
                cnn.Close();
                DataSet myDataSet = new DataSet();
                myDataAdapter.Fill(myDataSet);
                return myDataSet;
            }
        }
    }
}
