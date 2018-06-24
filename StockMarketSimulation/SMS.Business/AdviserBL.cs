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
    public class AdviserBL
    {
        public List<Model.AdviserDTO> GetAdviserData()
        {
            List<AdviserDTO> results = new List<AdviserDTO>();
            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("SELECT [AdviserId], \n");
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
                    varname1.Append("FROM   [dbo].[Adviser] \n");
                    varname1.Append("WHERE  [Status] = ?Status");
                    oUniversalConnection.CommandText = varname1.ToString();
                    oUniversalConnection.Parameters.Clear();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Status", Value = Status.Active });
                    //oUniversalConnection.Parameters.Add(new Parameter { Name = "ProductionLotNo", Value = ProductionLotNo });

                    using (IDataReader dr = oUniversalConnection.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            AdviserDTO result = new AdviserDTO();
                            result.AdviserId = Helper.GetDataValue<int>(dr, "AdviserId");
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

        public AdviserDTO AdviserSearchByFilters(string adviserName, int adviserType)
        {
            throw new NotImplementedException();
        }

        public bool AdviserInsert(AdviserMaintanance oAdviserMaintanance)
        {
            AdviserDTO oAdviserDTOs = oAdviserMaintanance.oAdviserDTO;
            LoginDTO oLoginDTOs = oAdviserMaintanance.oLoginDTO;

            int resultCount = 0;
            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("INSERT INTO Adviser \n");
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
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Name", Value = (object)oAdviserDTOs.Name ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Address", Value = (object)oAdviserDTOs.Address ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Telephone", Value = (object)oAdviserDTOs.Telephone ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Email", Value = (object)oAdviserDTOs.Email ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Description", Value = (object)oAdviserDTOs.Description ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Type", Value = (object)oAdviserDTOs.Type ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "JoinDate", Value = (object)oAdviserDTOs.JoinDate ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Status", Value = (object)oAdviserDTOs.Status ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = (object)oAdviserDTOs.UserName ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Password", Value = (object)oAdviserDTOs.Password ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "CreatedUser", Value = (object)oAdviserDTOs.CreatedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "CreatedMachine", Value = (object)oAdviserDTOs.CreatedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "CreatedDateTime", Value = (object)oAdviserDTOs.CreatedDateTime ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedUser", Value = (object)oAdviserDTOs.ModifiedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedMachine", Value = (object)oAdviserDTOs.ModifiedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedDateTime", Value = (object)oAdviserDTOs.ModifiedDateTime ?? DBNull.Value });
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

        public bool AdviserUpdate(AdviserMaintanance oAdviserMaintanance)
        {
            AdviserDTO oAdviserDTOs = oAdviserMaintanance.oAdviserDTO;
            LoginDTO oLoginDTOs = oAdviserMaintanance.oLoginDTO;

            int resultCount = 0;
            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    oUniversalConnection.Parameters.Clear();

                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("UPDATE Adviser \n");
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
                    varname1.Append(" WHERE  AdviserId = ?AdviserId");

                    oUniversalConnection.CommandText = varname1.ToString();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "AdviserId", Value = (object)oAdviserDTOs.AdviserId ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Name", Value = (object)oAdviserDTOs.Name ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Address", Value = (object)oAdviserDTOs.Address ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Telephone", Value = (object)oAdviserDTOs.Telephone ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Email", Value = (object)oAdviserDTOs.Email ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Description", Value = (object)oAdviserDTOs.Description ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Type", Value = (object)oAdviserDTOs.Type ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "JoinDate", Value = (object)oAdviserDTOs.JoinDate ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Status", Value = (object)oAdviserDTOs.Status ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "UserName", Value = (object)oAdviserDTOs.UserName ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Password", Value = (object)oAdviserDTOs.Password ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedUser", Value = (object)oAdviserDTOs.ModifiedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedMachine", Value = (object)oAdviserDTOs.ModifiedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedDateTime", Value = (object)oAdviserDTOs.ModifiedDateTime ?? DBNull.Value });
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

        public bool AdviserDelete(Model.AdviserDTO oAdviserDTO)
        {
            int resultCount = 0;
            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    oUniversalConnection.Parameters.Clear();

                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("UPDATE Adviser \n");
                    varname1.Append("SET \n");
                    varname1.Append("     Status = ?Status, \n");
                    varname1.Append("     ModifiedUser = ?ModifiedUser, \n");
                    varname1.Append("     ModifiedMachine = ?ModifiedMachine, \n");
                    varname1.Append("     ModifiedDateTime = ?ModifiedDateTime \n");
                    varname1.Append(" WHERE  AdviserId = ?AdviserId");

                    oUniversalConnection.CommandText = varname1.ToString();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "AdviserId", Value = (object)oAdviserDTO.AdviserId ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Status", Value = (object)oAdviserDTO.Status ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedUser", Value = (object)oAdviserDTO.ModifiedUser ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedMachine", Value = (object)oAdviserDTO.ModifiedMachine ?? DBNull.Value });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "ModifiedDateTime", Value = (object)oAdviserDTO.ModifiedDateTime ?? DBNull.Value });
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

        public AdviserDTO AdviserSearchById(int AdviserId)
        {
            AdviserDTO result = new AdviserDTO();

            try
            {
                using (UniversalConnection oUniversalConnection = new UniversalConnection(Connection.Common.ConnectionString))
                {
                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("SELECT [AdviserId], \n");
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
                    varname1.Append("FROM   Adviser \n");
                    varname1.Append("WHERE  [Status] = ?Status");
                    varname1.Append(" AND  [AdviserId] = ?AdviserId");
                    oUniversalConnection.CommandText = varname1.ToString();
                    oUniversalConnection.Parameters.Clear();
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "Status", Value = Status.Active });
                    oUniversalConnection.Parameters.Add(new Parameter { Name = "AdviserId", Value = AdviserId });

                    using (IDataReader dr = oUniversalConnection.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.AdviserId = Helper.GetDataValue<int>(dr, "AdviserId");
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
