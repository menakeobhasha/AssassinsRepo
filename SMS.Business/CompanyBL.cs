using BlackLion;
using BlackLion.Connection;
using SMS.Common;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMS.Business
{
    public class CompanyBL
    {
        public List<CompanyDTO> GetCompanyData()
        {
            List<CompanyDTO> results = new List<CompanyDTO>();
            try
            {
                using (CloudConnection oCloudConnection = new CloudConnection(BlackLion.Common.ConnectionString))
                {
                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("SELECT [CompanyId], \n");
                    varname1.Append("       [Name], \n");
                    varname1.Append("       [Address], \n");
                    varname1.Append("       [Telephone], \n");
                    varname1.Append("       [Email], \n");
                    varname1.Append("       [Description], \n");
                    varname1.Append("       [Type], \n");
                    varname1.Append("       [NumberOfShares], \n");
                    varname1.Append("       [SharePrice], \n");
                    varname1.Append("       [Status], \n");
                    varname1.Append("       [CreatedUser], \n");
                    varname1.Append("       [CreatedMachine], \n");
                    varname1.Append("       [CreatedDatetime], \n");
                    varname1.Append("       [ModifiedUser], \n");
                    varname1.Append("       [ModifiedMachine], \n");
                    varname1.Append("       [ModifiedDatetime] \n");
                    varname1.Append("FROM   [dbo].[Company] \n");
                    varname1.Append("WHERE  [Status] = ?Status");
                    oCloudConnection.CommandText = varname1.ToString();
                    oCloudConnection.Parameters.Clear();
                    oCloudConnection.Parameters.Add(new Parameter { Name = "Status", Value = Status.Active });
                    //oCloudConnection.Parameters.Add(new Parameter { Name = "ProductionLotNo", Value = ProductionLotNo });

                    using (IDataReader dr = oCloudConnection.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CompanyDTO result = new CompanyDTO();
                            result.CompanyId = Helper.GetDataValue<int>(dr, "CompanyId");
                            result.Name = Helper.GetDataValue<string>(dr, "Name");
                            result.Address = Helper.GetDataValue<string>(dr, "Address");
                            result.Telephone = Helper.GetDataValue<string>(dr, "Telephone");
                            result.Email = Helper.GetDataValue<string>(dr, "Email");
                            result.Description = Helper.GetDataValue<string>(dr, "Description");
                            result.Type = Helper.GetDataValue<int>(dr, "Type");
                            result.NumberOfShares = Helper.GetDataValue<int>(dr, "NumberOfShares");
                            result.SharePrice = Helper.GetDataValue<decimal>(dr, "SharePrice");
                            result.Status = Helper.GetDataValue<int>(dr, "Status");
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

        public bool CompanyInsert(CompanyDTO oCompanyDTOs)
        {
            int resultCount = 0;
            try
            {
                using (CloudConnection oCloudConnection = new CloudConnection(BlackLion.Common.ConnectionString))
                {
                    StringBuilder varname1 = new StringBuilder();
                    varname1.Append("INSERT INTO Company \n");
                    varname1.Append("            (Name, \n");
                    varname1.Append("             Address, \n");
                    varname1.Append("             Telephone, \n");
                    varname1.Append("             Email, \n");
                    varname1.Append("             Description, \n");
                    varname1.Append("             Type, \n");
                    varname1.Append("             NumberOfShares, \n");
                    varname1.Append("             SharePrice, \n");
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
                    varname1.Append("             ?NumberOfShares, \n");
                    varname1.Append("             ?SharePrice, \n");
                    varname1.Append("             ?Status, \n");
                    varname1.Append("             ?UserName, \n");
                    varname1.Append("             ?Password, \n");
                    varname1.Append("             ?CreatedUser, \n");
                    varname1.Append("             ?CreatedMachine, \n");
                    varname1.Append("             ?CreatedDateTime, \n");
                    varname1.Append("             ?ModifiedUser, \n");
                    varname1.Append("             ?ModifiedMachine, \n");
                    varname1.Append("             ?ModifiedDateTime)");


                    oCloudConnection.CommandText = varname1.ToString();

                        oCloudConnection.Parameters.Clear();
                        oCloudConnection.Parameters.Add(new Parameter { Name = "Name", Value = (object)oCompanyDTOs.Name ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "Address", Value = (object)oCompanyDTOs.Address ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "Telephone", Value = (object)oCompanyDTOs.Telephone ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "Email", Value = (object)oCompanyDTOs.Email ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "Description", Value = (object)oCompanyDTOs.Description ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "Type", Value = (object)oCompanyDTOs.Type ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "NumberOfShares", Value = (object)oCompanyDTOs.NumberOfShares ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "SharePrice", Value = (object)oCompanyDTOs.SharePrice ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "Status", Value = (object)oCompanyDTOs.Status ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "UserName", Value = (object)oCompanyDTOs.UserName ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "Password", Value = (object)oCompanyDTOs.Password ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "CreatedUser", Value = (object)oCompanyDTOs.CreatedUser ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "CreatedMachine", Value = (object)oCompanyDTOs.CreatedMachine ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "CreatedDateTime", Value = (object)oCompanyDTOs.CreatedDateTime ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "ModifiedUser", Value = (object)oCompanyDTOs.ModifiedUser ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "ModifiedMachine", Value = (object)oCompanyDTOs.ModifiedMachine ?? DBNull.Value });
                        oCloudConnection.Parameters.Add(new Parameter { Name = "ModifiedDateTime", Value = (object)oCompanyDTOs.ModifiedDateTime ?? DBNull.Value });
                    resultCount = oCloudConnection.ExecuteQuery();

                    //if (resultCount > 0)
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}

                    return (resultCount > 0);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                throw ex;
            }
        }

        public bool CompanyUpdate(CompanyDTO oCompanyDTO)
        {
            try
            {
                using (CloudConnection oCloudConnection = new CloudConnection(BlackLion.Common.ConnectionString))
                {
                    oCloudConnection.Parameters.Clear();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
