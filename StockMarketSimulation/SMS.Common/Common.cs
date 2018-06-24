using SMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;

namespace SMS.Common
{
    public class Common
    {


        public List<DropListDTO> getEnumDescription<T>()
        {
            List<DropListDTO> oData = new List<DropListDTO>();
            try
            {
                oData.Add(new DropListDTO { Text = "- Select -", Value = 0 });
                foreach (IConvertible e in Enum.GetValues(typeof(T)))
                {
                    string text = e.ToString().Trim();
                    string value = e.ToType(Enum.GetUnderlyingType(typeof(T)), CultureInfo.CurrentCulture).ToString();
                    var attributes = typeof(T).GetField(text.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string Description = attributes.Length == 0 ? text : ((DescriptionAttribute)attributes[0]).Description.ToString();
                    oData.Add(new DropListDTO { Value = Convert.ToInt32(value), Text = Description });
                }
                return oData;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<DropListDTO> ComboDataBindByEnum<T>()
        {
            List<DropListDTO> oData = new List<DropListDTO>();
            try
            {
                oData.Add(new DropListDTO { Text = "- Select -", Value = 0 });
                foreach (IConvertible e in Enum.GetValues(typeof(T)))
                {
                    string text = e.ToString().Trim();
                    string value = e.ToType(Enum.GetUnderlyingType(typeof(T)), CultureInfo.CurrentCulture).ToString();
                    var attributes = typeof(T).GetField(text.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string Description = attributes.Length == 0 ? text : ((DescriptionAttribute)attributes[0]).Description.ToString();
                    oData.Add(new DropListDTO { Value = Convert.ToInt32(value), Text = Description });
                }
                return oData;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public List<DropListDTO> DropDownDataBind(Type enumType)
        {
            List<DropListDTO> oDroLists = new List<DropListDTO>();
            DropListDTO oDroList = new DropListDTO();
            oDroList.Value = 0;
            oDroList.Text = "- Select -";
            oDroLists.Add(oDroList);
            var listOfValues = Enum.GetValues(enumType);
            int i = 1;
            foreach (var value in listOfValues)
            {
                DropListDTO oDroListnew = new DropListDTO();
                oDroListnew.Value = i;
                oDroListnew.Text = value.ToString();
                oDroLists.Add(oDroListnew);
                i++;
            }
            return oDroLists;
        }

        public List<ComboboxDTO> ComboboxDataBind(Type enumType)
        {
            List<ComboboxDTO> oDroLists = new List<ComboboxDTO>();
            ComboboxDTO oDroList = new ComboboxDTO();
            oDroList.ValueField = "";
            oDroList.TextField = "- Select -";
            oDroLists.Add(oDroList);
            var listOfValues = Enum.GetValues(enumType);
            int i = 1;
            foreach (var value in listOfValues)
            {
                ComboboxDTO oDroListnew = new ComboboxDTO();
                oDroListnew.ValueField = i.ToString();
                oDroListnew.TextField = value.ToString();
                oDroLists.Add(oDroListnew);
                i++;
            }
            return oDroLists;
        }

        public static List<ComboboxDTO> ComboboxDataBindNoSelect<T>()
        {
            List<ComboboxDTO> oDroLists = new List<ComboboxDTO>();

            Array colors = Enum.GetValues(typeof(T));
            foreach (var color in colors)
            {
                ComboboxDTO oDroList = new ComboboxDTO();
                oDroList.ValueField = ((int)color).ToString();
                oDroList.TextField = color.ToString();
                oDroLists.Add(oDroList);
            }

            return oDroLists;
        }

        public string GenerateQueryFromListArray(List<ParamsDTO> oParamsDTO)
        {
            DataTable dtParams = new DataTable();
            dtParams.Merge(ListToDataTable<ParamsDTO>(oParamsDTO));

            StringBuilder selectquery = new StringBuilder();

            DataView oDataView = new DataView(dtParams);

            string[] strArr = { "ColumnName" };

            DataTable dtDistinct = new DataTable();
            dtDistinct.Merge(oDataView.ToTable(true, strArr));

            for (int i = 0; i < dtDistinct.Rows.Count; i++)
            {
                DataRow[] dr = dtParams.Select("ColumnName = '" + dtDistinct.Rows[i]["ColumnName"].ToString() + "'");
                if (dr.Length > 1)
                {
                    string subSelect = "";

                    for (int j = 0; j < dr.Length; j++)
                    {
                        if (j == 0)
                        {
                            subSelect += dr[j][0].ToString() + " " + dr[j][1].ToString() + " '" + dr[j][2].ToString() + "' ";
                        }
                        else
                        {
                            if (dr[j - 1]["Operator"].ToString().Contains(">=") || dr[j - 1]["Operator"].ToString().Contains("<="))
                            {
                                subSelect += " AND " + dr[j][0].ToString() + " " + dr[j][1].ToString() + " '" + dr[j][2].ToString() + "' ";
                            }
                            else
                            {
                                subSelect += " OR " + dr[j][0].ToString() + " " + dr[j][1].ToString() + " '" + dr[j][2].ToString() + "' ";
                            }
                        }
                    }
                    selectquery.AppendLine(" AND ((" + subSelect + "))");
                }
                else
                {
                    selectquery.AppendLine(" AND (" + dtDistinct.Rows[i]["ColumnName"].ToString() + " " + dr[0]["Operator"].ToString() + " '" + dr[0]["Value"].ToString() + "')");
                }
            }

            return selectquery.ToString();
        }

        public static DataTable ListToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];

            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        //public GrandTotalDTO CalculateGrandTotal(GrandTotalDTO oGrandTotalDTO)
        //{
        //    decimal total = oGrandTotalDTO.Total;
        //    decimal concession = oGrandTotalDTO.Concession;
        //    decimal totalAfterConcession = 0;
        //    decimal NBT = oGrandTotalDTO.NBT;
        //    decimal totalNBT = 0;
        //    decimal totalAfterNBT = 0;
        //    decimal VAT = oGrandTotalDTO.VAT;
        //    decimal totalVAT = 0;
        //    decimal grandTotal = 0;

        //    //totalAfterConcession = total * concession/100;
        //    totalAfterConcession = total * (100 - concession) / 100;
        //    oGrandTotalDTO.TotalAfterConcession = totalAfterConcession;

        //    totalNBT = totalAfterConcession * NBT / 100;
        //    oGrandTotalDTO.TotalNBT = totalNBT;

        //    totalAfterNBT = totalAfterConcession + totalNBT;
        //    oGrandTotalDTO.TotalAfterNBT = totalAfterNBT;

        //    totalVAT = totalAfterNBT * VAT / 100;
        //    oGrandTotalDTO.TotalVAT = totalVAT;

        //    grandTotal = totalAfterNBT + totalVAT;
        //    oGrandTotalDTO.GrandTotal = grandTotal;

        //    return oGrandTotalDTO;
        //}

        //public GrandTotalDTO CalculateGrandTotalConcessionAmount(GrandTotalDTO oGrandTotalDTO)
        //{
        //    decimal total = oGrandTotalDTO.Total;
        //    decimal concession = oGrandTotalDTO.Concession;
        //    decimal totalAfterConcession = 0;
        //    decimal NBT = oGrandTotalDTO.NBT;
        //    decimal totalNBT = 0;
        //    decimal totalAfterNBT = 0;
        //    decimal VAT = oGrandTotalDTO.VAT;
        //    decimal totalVAT = 0;
        //    decimal grandTotal = 0;

        //    totalAfterConcession = total - concession;
        //    oGrandTotalDTO.TotalAfterConcession = totalAfterConcession;

        //    totalNBT = totalAfterConcession * NBT / 100;
        //    oGrandTotalDTO.TotalNBT = totalNBT;

        //    totalAfterNBT = totalAfterConcession + totalNBT;
        //    oGrandTotalDTO.TotalAfterNBT = totalAfterNBT;

        //    totalVAT = totalAfterNBT * VAT / 100;
        //    oGrandTotalDTO.TotalVAT = totalVAT;

        //    grandTotal = totalAfterNBT + totalVAT;
        //    oGrandTotalDTO.GrandTotal = grandTotal;

        //    return oGrandTotalDTO;
        //}
    }

    public class ParamsDTO
    {
        public string ColumnName { get; set; }

        public string Operator { get; set; }

        public object Value { get; set; }
    }
}
