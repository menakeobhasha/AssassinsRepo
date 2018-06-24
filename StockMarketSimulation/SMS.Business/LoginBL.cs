using Connection;
using SMS.Model;
using System;
using System.Data;
using System.Text;

namespace SMS.Business
{
    public class LoginBL
    {
        public LoginDTO GetUserNameByUserId(string UserName)
        {
            LoginDTO result = new LoginDTO();
            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("SELECT UserName, \n");
                    varname1.Append("       Password, \n");
                    varname1.Append("       UserType, \n");
                    varname1.Append("       LoginAttempts, \n");
                    varname1.Append("       CreatedUser, \n");
                    varname1.Append("       CreatedMachine, \n");
                    varname1.Append("       CreatedDateTime, \n");
                    varname1.Append("       ModifiedUser, \n");
                    varname1.Append("       ModifiedMachine, \n");
                    varname1.Append("       ModifiedDateTime \n");
                    varname1.Append("FROM   Users \n");
                    varname1.Append("WHERE  UserName = ?UserName");

                    oUniversalConnection.CommandText = varname1.ToString();
                    oUniversalConnection.Parameters.Clear();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = UserName});
                    //oUniversalConnection.Parameters.Add(new Parameter { Name = "ProductionLotNo", Value = ProductionLotNo });

                    using (IDataReader dr = oUniversalConnection.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.UserName = Helper.GetDataValue<string>(dr, "UserName");
                            result.Password = Helper.GetDataValue<string>(dr, "Password");
                            result.UserType = Helper.GetDataValue<int>(dr, "UserType");
                            result.LoginAttempts = Helper.GetDataValue<int>(dr, "LoginAttempts");
                            result.CreatedUser = Helper.GetDataValue<string>(dr, "CreatedUser");
                            result.CreatedMachine = Helper.GetDataValue<string>(dr, "CreatedMachine");
                            result.CreatedDateTime = Helper.GetDataValue<DateTime>(dr, "CreatedDateTime");
                            result.ModifiedUser = Helper.GetDataValue<string>(dr, "ModifiedUser");
                            result.ModifiedMachine = Helper.GetDataValue<string>(dr, "ModifiedMachine");
                            result.ModifiedDateTime = Helper.GetDataValue<DateTime>(dr, "ModifiedDateTime");
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                throw ex;
            }

            return result;
        }

        public string GetUserNameByUserIdFromAllUsers(string userId)
        {
            string UserName = string.Empty;
            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("SELECT P.Name AS UserName FROM Player AS P WHERE P.UserName=?UserName \n");   
                    oUniversalConnection.CommandText = varname1.ToString();
                    oUniversalConnection.Parameters.Clear();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = userId });

                    using (IDataReader dr = oUniversalConnection.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UserName = Helper.GetDataValue<string>(dr, "UserName");
                        }
                        dr.Close();
                    }

                    if (UserName.ToString() != null || UserName.ToString() != string.Empty)
                    {
                        varname1 = new StringBuilder();
                        varname1.Append("SELECT A.Name AS UserName FROM Adviser AS A WHERE A.UserName=?UserName \n");
                        oUniversalConnection.CommandText = varname1.ToString();
                        oUniversalConnection.Parameters.Clear();
                        oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = userId });

                        using (IDataReader dr = oUniversalConnection.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                UserName = Helper.GetDataValue<string>(dr, "UserName");
                            }
                            dr.Close();
                        }
                    }
                    else if (UserName.ToString() != null || UserName.ToString() != string.Empty)
                    {
                        varname1 = new StringBuilder();
                        varname1.Append("SELECT B.Name AS UserName FROM Broker AS B WHERE B.UserName=?UserName \n");
                        oUniversalConnection.CommandText = varname1.ToString();
                        oUniversalConnection.Parameters.Clear();
                        oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = userId });

                        using (IDataReader dr = oUniversalConnection.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                UserName = Helper.GetDataValue<string>(dr, "UserName");
                            }
                            dr.Close();
                        }
                    }
                    else if (UserName.ToString() != null || UserName.ToString() != string.Empty)
                    {
                        varname1 = new StringBuilder();
                        varname1.Append("SELECT C.Name AS UserName FROM Company AS C WHERE C.UserName=?UserName \n");
                        oUniversalConnection.CommandText = varname1.ToString();
                        oUniversalConnection.Parameters.Clear();
                        oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = userId });

                        using (IDataReader dr = oUniversalConnection.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                UserName = Helper.GetDataValue<string>(dr, "UserName");
                            }
                            dr.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                throw ex;
            }

            return UserName;
        }
    }
}
