using Connection;
using SMS.Common;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Business
{
    public class BrokerBL
    {
        public List<Model.BrokerDTO> GetBrokerData()
        {
            List<BrokerDTO> results = new List<BrokerDTO>();
            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("SELECT [BrokerId], \n");
                    varname1.Append("       [Name], \n");
                    varname1.Append("       [Address], \n");
                    varname1.Append("       [Telephone], \n");
                    varname1.Append("       [Email], \n");
                    varname1.Append("       [Description], \n");
                    varname1.Append("       [Type], \n");
                    varname1.Append("       [JoinDate], \n");
                    varname1.Append("       [Status], \n");
                    varname1.Append("       [UserName], \n");
                    varname1.Append("       [Password], \n");
                    varname1.Append("       [CreatedUser], \n");
                    varname1.Append("       [CreatedMachine], \n");
                    varname1.Append("       [CreatedDatetime], \n");
                    varname1.Append("       [ModifiedUser], \n");
                    varname1.Append("       [ModifiedMachine], \n");
                    varname1.Append("       [ModifiedDatetime] \n");
                    varname1.Append("FROM   [dbo].[Broker] \n");
                    varname1.Append("WHERE  [Status] = ?Status");
                    oUniversalConnection.CommandText = varname1.ToString();
                    oUniversalConnection.Parameters.Clear();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Status", Value = Status.Active });
                    //oUniversalConnection.Parameters.Add(new Parameter { Name = "ProductionLotNo", Value = ProductionLotNo });

                    using (IDataReader dr = oUniversalConnection.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BrokerDTO result = new BrokerDTO();
                            result.BrokerId = Helper.GetDataValue<int>(dr, "BrokerId");
                            result.Name = Helper.GetDataValue<string>(dr, "Name");
                            result.Address = Helper.GetDataValue<string>(dr, "Address");
                            result.Telephone = Helper.GetDataValue<string>(dr, "Telephone");
                            result.Email = Helper.GetDataValue<string>(dr, "Email");
                            result.Description = Helper.GetDataValue<string>(dr, "Description");
                            result.Type = Helper.GetDataValue<int>(dr, "Type");
                            result.JoinDate = Helper.GetDataValue<DateTime>(dr, "JoinDate");
                            result.Status = Helper.GetDataValue<int>(dr, "Status");
                            result.UserName = Helper.GetDataValue<string>(dr, "UserName");
                            result.Password = Helper.GetDataValue<string>(dr, "Password");
                            results.Add(result);
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

            return results;
        }

        public bool BrokerInsert(BrokerMaintanance oBrokerMaintanance)
        {
            BrokerDTO oBrokerDTOs = oBrokerMaintanance.oBrokerDTO;
            LoginDTO oLoginDTOs = oBrokerMaintanance.oLoginDTO;

            int resultCount = 0;
            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("INSERT INTO Broker \n");
                    varname1.Append("            (Name, \n");
                    varname1.Append("             Address, \n");
                    varname1.Append("             Telephone, \n");
                    varname1.Append("             Email, \n");
                    varname1.Append("             Description, \n");
                    varname1.Append("             Type, \n");
                    varname1.Append("             JoinDate, \n");
                    varname1.Append("             Status, \n");
                    varname1.Append("             UserName, \n");
                    varname1.Append("             Password, \n");
                    varname1.Append("             CreatedUser, \n");
                    varname1.Append("             CreatedMachine, \n");
                    varname1.Append("             CreatedDateTime, \n");
                    varname1.Append("             ModifiedUser, \n");
                    varname1.Append("             ModifiedMachine, \n");
                    varname1.Append("             ModifiedDateTime) \n");
                    varname1.Append("VALUES      (?Name, \n");
                    varname1.Append("             ?Address, \n");
                    varname1.Append("             ?Telephone, \n");
                    varname1.Append("             ?Email, \n");
                    varname1.Append("             ?Description, \n");
                    varname1.Append("             ?Type, \n");
                    varname1.Append("             ?JoinDate, \n");
                    varname1.Append("             ?Status, \n");
                    varname1.Append("             ?UserName, \n");
                    varname1.Append("             ?Password, \n");
                    varname1.Append("             ?CreatedUser, \n");
                    varname1.Append("             ?CreatedMachine, \n");
                    varname1.Append("             ?CreatedDateTime, \n");
                    varname1.Append("             ?ModifiedUser, \n");
                    varname1.Append("             ?ModifiedMachine, \n");
                    varname1.Append("             ?ModifiedDateTime)");

                    oUniversalConnection.CommandText = varname1.ToString();

                    oUniversalConnection.Parameters.Clear();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Name", Value = (object)oBrokerDTOs.Name ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Address", Value = (object)oBrokerDTOs.Address ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Telephone", Value = (object)oBrokerDTOs.Telephone ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Email", Value = (object)oBrokerDTOs.Email ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Description", Value = (object)oBrokerDTOs.Description ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Type", Value = (object)oBrokerDTOs.Type ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "JoinDate", Value = (object)oBrokerDTOs.JoinDate ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Status", Value = (object)oBrokerDTOs.Status ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = (object)oBrokerDTOs.UserName ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Password", Value = (object)oBrokerDTOs.Password ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "CreatedUser", Value = (object)oBrokerDTOs.CreatedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "CreatedMachine", Value = (object)oBrokerDTOs.CreatedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "CreatedDateTime", Value = (object)oBrokerDTOs.CreatedDateTime ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedUser", Value = (object)oBrokerDTOs.ModifiedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedMachine", Value = (object)oBrokerDTOs.ModifiedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedDateTime", Value = (object)oBrokerDTOs.ModifiedDateTime ?? DBNull.Value });
                    resultCount = oUniversalConnection.ExecuteQuery();

                    StringBuilder varname2 = new StringBuilder();
                    varname2.Append("INSERT INTO Users \n");
                    varname2.Append("            (UserName, \n");
                    varname2.Append("             Password, \n");
                    varname2.Append("             UserType, \n");
                    varname2.Append("             LoginAttempts, \n");
                    varname2.Append("             CreatedUser, \n");
                    varname2.Append("             CreatedMachine, \n");
                    varname2.Append("             CreatedDateTime, \n");
                    varname2.Append("             ModifiedUser, \n");
                    varname2.Append("             ModifiedMachine, \n");
                    varname2.Append("             ModifiedDateTime) \n");
                    varname2.Append("VALUES      (?UserName, \n");
                    varname2.Append("             ?Password, \n");
                    varname2.Append("             ?UserType, \n");
                    varname2.Append("             ?LoginAttempts, \n");
                    varname2.Append("             ?CreatedUser, \n");
                    varname2.Append("             ?CreatedMachine, \n");
                    varname2.Append("             ?CreatedDateTime, \n");
                    varname2.Append("             ?ModifiedUser, \n");
                    varname2.Append("             ?ModifiedMachine, \n");
                    varname2.Append("             ?ModifiedDateTime)");

                    oUniversalConnection.CommandText = varname2.ToString();

                    oUniversalConnection.Parameters.Clear();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = (object)oLoginDTOs.UserName ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Password", Value = (object)oLoginDTOs.Password ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "UserType", Value = (object)oLoginDTOs.UserType ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "LoginAttempts", Value = (object)oLoginDTOs.LoginAttempts ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "CreatedUser", Value = (object)oLoginDTOs.CreatedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "CreatedMachine", Value = (object)oLoginDTOs.CreatedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "CreatedDateTime", Value = (object)oLoginDTOs.CreatedDateTime ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedUser", Value = (object)oLoginDTOs.ModifiedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedMachine", Value = (object)oLoginDTOs.ModifiedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedDateTime", Value = (object)oLoginDTOs.ModifiedDateTime ?? DBNull.Value });
                    resultCount = oUniversalConnection.ExecuteQuery();

                    return (resultCount > 0);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                throw ex;
            }
        }

        public bool BrokerUpdate(BrokerMaintanance oBrokerMaintanance)
        {
            BrokerDTO oBrokerDTOs = oBrokerMaintanance.oBrokerDTO;
            LoginDTO oLoginDTOs = oBrokerMaintanance.oLoginDTO;

            int resultCount = 0;
            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    oUniversalConnection.Parameters.Clear();

                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("UPDATE Broker \n");
                    varname1.Append("SET \n");
                    varname1.Append("     Name = ?Name, \n");
                    varname1.Append("     Address = ?Address, \n");
                    varname1.Append("     Telephone = ?Telephone, \n");
                    varname1.Append("     Email = ?Email, \n");
                    varname1.Append("     Description = ?Description, \n");
                    varname1.Append("     Type = ?Type, \n");
                    varname1.Append("     JoinDate = ?JoinDate, \n");
                    varname1.Append("     Status = ?Status, \n");
                    varname1.Append("     UserName = ?UserName, \n");
                    varname1.Append("     Password = ?Password, \n");
                    varname1.Append("     ModifiedUser = ?ModifiedUser, \n");
                    varname1.Append("     ModifiedMachine = ?ModifiedMachine, \n");
                    varname1.Append("     ModifiedDateTime = ?ModifiedDateTime \n");
                    varname1.Append(" WHERE  BrokerId = ?BrokerId");

                    oUniversalConnection.CommandText = varname1.ToString();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "BrokerId", Value = (object)oBrokerDTOs.BrokerId ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Name", Value = (object)oBrokerDTOs.Name ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Address", Value = (object)oBrokerDTOs.Address ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Telephone", Value = (object)oBrokerDTOs.Telephone ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Email", Value = (object)oBrokerDTOs.Email ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Description", Value = (object)oBrokerDTOs.Description ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Type", Value = (object)oBrokerDTOs.Type ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "JoinDate", Value = (object)oBrokerDTOs.JoinDate ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Status", Value = (object)oBrokerDTOs.Status ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = (object)oBrokerDTOs.UserName ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Password", Value = (object)oBrokerDTOs.Password ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedUser", Value = (object)oBrokerDTOs.ModifiedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedMachine", Value = (object)oBrokerDTOs.ModifiedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedDateTime", Value = (object)oBrokerDTOs.ModifiedDateTime ?? DBNull.Value });
                    resultCount = oUniversalConnection.ExecuteQuery();

                    StringBuilder varname2 = new StringBuilder();
                    varname2.Append("UPDATE Users \n");
                    varname2.Append("             SET [UserName] = ?UserName \n");
                    varname2.Append("             ,[Password] = ?Password \n");
                    varname2.Append("             ,[UserType] = ?UserType \n");
                    varname2.Append("             ,[LoginAttempts] = ?LoginAttempts \n");
                    varname2.Append("             ,[ModifiedUser] = ?ModifiedUser \n");
                    varname2.Append("             ,[ModifiedMachine] = ?ModifiedMachine \n");
                    varname2.Append("             ,[ModifiedDateTime] = ?ModifiedDateTime \n");
                    varname2.Append("WHERE  UserName = ?UserName \n");

                    oUniversalConnection.CommandText = varname2.ToString();

                    oUniversalConnection.Parameters.Clear();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = (object)oLoginDTOs.UserName ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Password", Value = (object)oLoginDTOs.Password ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "UserType", Value = (object)oLoginDTOs.UserType ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "LoginAttempts", Value = (object)oLoginDTOs.LoginAttempts ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedUser", Value = (object)oLoginDTOs.ModifiedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedMachine", Value = (object)oLoginDTOs.ModifiedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedDateTime", Value = (object)oLoginDTOs.ModifiedDateTime ?? DBNull.Value });
                    oUniversalConnection.ExecuteQuery();
                }
                return (resultCount > 0);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                throw ex;
            }
        }

        public bool BrokerDelete(Model.BrokerDTO oBrokerDTO)
        {
            int resultCount = 0;
            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    oUniversalConnection.Parameters.Clear();

                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("UPDATE Broker \n");
                    varname1.Append("SET \n");
                    varname1.Append("     Status = ?Status, \n");
                    varname1.Append("     ModifiedUser = ?ModifiedUser, \n");
                    varname1.Append("     ModifiedMachine = ?ModifiedMachine, \n");
                    varname1.Append("     ModifiedDateTime = ?ModifiedDateTime \n");
                    varname1.Append(" WHERE  BrokerId = ?BrokerId");

                    oUniversalConnection.CommandText = varname1.ToString();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "BrokerId", Value = (object)oBrokerDTO.BrokerId ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Status", Value = (object)oBrokerDTO.Status ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedUser", Value = (object)oBrokerDTO.ModifiedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedMachine", Value = (object)oBrokerDTO.ModifiedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedDateTime", Value = (object)oBrokerDTO.ModifiedDateTime ?? DBNull.Value });
                    resultCount = oUniversalConnection.ExecuteQuery();
                }
                return (resultCount > 0);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                throw ex;
            }
        }

        public BrokerDTO BrokerSearchById(int BrokerId)
        {
            BrokerDTO result = new BrokerDTO();

            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("SELECT [BrokerId], \n");
                    varname1.Append("       [Name], \n");
                    varname1.Append("       [Address], \n");
                    varname1.Append("       [Telephone], \n");
                    varname1.Append("       [Email], \n");
                    varname1.Append("       [Description], \n");
                    varname1.Append("       [Type], \n");
                    varname1.Append("       [JoinDate], \n");
                    varname1.Append("       [Status], \n");
                    varname1.Append("       [UserName], \n");
                    varname1.Append("       [Password], \n");
                    varname1.Append("       [CreatedUser], \n");
                    varname1.Append("       [CreatedMachine], \n");
                    varname1.Append("       [CreatedDatetime], \n");
                    varname1.Append("       [ModifiedUser], \n");
                    varname1.Append("       [ModifiedMachine], \n");
                    varname1.Append("       [ModifiedDatetime] \n");
                    varname1.Append("FROM   [dbo].[Broker] \n");
                    varname1.Append("WHERE  [Status] = ?Status");
                    varname1.Append(" AND  [BrokerId] = ?BrokerId");
                    oUniversalConnection.CommandText = varname1.ToString();
                    oUniversalConnection.Parameters.Clear();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Status", Value = Status.Active });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "BrokerId", Value = BrokerId });

                    using (IDataReader dr = oUniversalConnection.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.BrokerId = Helper.GetDataValue<int>(dr, "BrokerId");
                            result.Name = Helper.GetDataValue<string>(dr, "Name");
                            result.Address = Helper.GetDataValue<string>(dr, "Address");
                            result.Telephone = Helper.GetDataValue<string>(dr, "Telephone");
                            result.Email = Helper.GetDataValue<string>(dr, "Email");
                            result.Description = Helper.GetDataValue<string>(dr, "Description");
                            result.Type = Helper.GetDataValue<int>(dr, "Type");
                            result.JoinDate = Helper.GetDataValue<DateTime>(dr, "JoinDate");
                            result.Status = Helper.GetDataValue<int>(dr, "Status");
                            result.UserName = Helper.GetDataValue<string>(dr, "UserName");
                            result.Password = Helper.GetDataValue<string>(dr, "Password");
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
    }
}
