using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Queries
    {
        private SqlConnection connection = new SqlConnection("Server=tcp:sparking.database.windows.net,1433;Initial Catalog=ParkingDatabse;Persist Security Info=False;User ID=parkingAdmin;Password=p@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        string checkIfAuthorizedPlateQuery = "SELECT Plate FROM User_Plate P JOIN UserData D ON P.UserID = D.UserID WHERE D.AuthorizationStatus = 1 AND P.Plate = @plate";
        string checkIfAuthorizedUserQuery = "SELECT AuthorizationStatus FROM UserData WHERE UserID = @userID";
        string addPlateToUserQuery = "INSERT INTO User_Plate (UserID, Plate) VALUES (@userID, @plate)";

        public string CheckIfPlateIsAllowed(string plate)
        {
            string result = null;
            using (SqlCommand checkIfAuthorizedPlate = new SqlCommand(checkIfAuthorizedPlateQuery))
            {
                checkIfAuthorizedPlate.Connection = connection;
                checkIfAuthorizedPlate.Parameters.Add("@plate", SqlDbType.VarChar).Value = plate;
                connection.Open();
                using (SqlDataReader reader = checkIfAuthorizedPlate.ExecuteReader())
                {
                    if (reader.Read())
                        result = reader.GetString(0);
                }
                connection.Close();
            }

            return result;
        }

        internal bool CheckIfUserIsAllowed(string userID)
        {
            bool result;
            using (SqlCommand checkIfAuthorizedUser = new SqlCommand(checkIfAuthorizedUserQuery))
            {
                checkIfAuthorizedUser.Connection = connection;
                checkIfAuthorizedUser.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                connection.Open();
                using (SqlDataReader reader = checkIfAuthorizedUser.ExecuteReader())
                {
                    if (reader.Read())
                        result = reader.GetBoolean(0);
                    else
                        result = false;
                }
                connection.Close();
            }

            return result;
        }

        public void AddPlateToUser(string userID, string plateResponse)
        {
            using (SqlCommand addPlateToUser = new SqlCommand(addPlateToUserQuery))
            {
                addPlateToUser.Connection = connection;
                addPlateToUser.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                addPlateToUser.Parameters.Add("@plate", SqlDbType.VarChar).Value = plateResponse;
                connection.Open();
                addPlateToUser.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
