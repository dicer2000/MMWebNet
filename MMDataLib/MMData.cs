using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
namespace MMDataLib
{
    public class MMData
    {
        private string _connection = String.Empty;

        public MMData()
        {
            this._connection = ConfigurationManager.AppSettings["ConnectionString"];
            if (string.IsNullOrEmpty(this._connection))
                throw new Exception("No Connection String is defined in configuration");
        }

        public MMData(string ConfigurationString)
        {
            this._connection = ConfigurationString;
        }


        public int P2_MM_Initialize(String strPlayer1)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this._connection))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("P2_MM_Initialize", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("Player1Name", SqlDbType.VarChar).Value = (object)strPlayer1;
                        connection.Open();
                        object objVal = sqlCommand.ExecuteScalar();

                        // Get Game ID
                        if (objVal == null || DBNull.Value == objVal)
                            return 0;
                        else
                            return Int32.Parse(objVal.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public DataTable P2_MM_GetBoard(int GameID)
        {
            using (SqlConnection connection = new SqlConnection(this._connection))
            {
                using (SqlCommand sqlCommand = new SqlCommand("P2_MM_GetBoard", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("GameID", SqlDbType.Int).Value = (object)GameID;
                    connection.Open();

                    using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    {
                        var tb = new DataTable();
                        tb.Load(dr);
                        return tb;
                    }
                }
            }
        }

        public DataTable P2_MM_NewMove(int GameID, int GuessPosition1, int GuessPosition2, int GuessPosition3, int GuessPosition4)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this._connection))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("P2_MM_NewMove", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("GameID", SqlDbType.Int).Value = (object)GameID;
                        sqlCommand.Parameters.Add("GuessPosition1", SqlDbType.Int).Value = (object)GuessPosition1;
                        sqlCommand.Parameters.Add("GuessPosition2", SqlDbType.Int).Value = (object)GuessPosition2;
                        sqlCommand.Parameters.Add("GuessPosition3", SqlDbType.Int).Value = (object)GuessPosition3;
                        sqlCommand.Parameters.Add("GuessPosition4", SqlDbType.Int).Value = (object)GuessPosition4;
                        connection.Open();
                        using (SqlDataReader dr = sqlCommand.ExecuteReader())
                        {
                            var tb = new DataTable();
                            tb.Load(dr);
                            return tb;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
