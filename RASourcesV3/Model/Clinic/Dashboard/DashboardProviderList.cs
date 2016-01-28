using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using RiskApps3.Utilities;
using System.Reflection;
using RiskApps3.View;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class DashboardProviderList : HraObject
    {
        /**************************************************************************************************/
        

        /**************************************************************************************************/

        public DashboardProviderList()
        {
        }

        /**************************************************************************************************/

        public void RemoveViewHandlers(HraView view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
            //string sql = "select * from v_3_DashboardProviderList order by 1";
            //using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(sql, connection);

            //    try
            //    {
            //        SqlDataReader reader = command.ExecuteReader();
            //        if (reader != null)
            //        {
            //            while (reader.Read())
            //            {
            //                DashboardProviderListEntry dple = new DashboardProviderListEntry();
            //                for (int i = 0; i < reader.FieldCount; i++)
            //                {
            //                    if (reader.IsDBNull(i) == false)
            //                    {
            //                        foreach (
            //                            FieldInfo fi in
            //                                dple.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
            //                        {
            //                            string name = fi.Name;
            //                            if (name == reader.GetName(i))
            //                            {
            //                                fi.SetValue(dple, reader.GetValue(i));
            //                                break;
            //                            }
            //                        }
            //                    }
            //                }
            //                ProviderNames.Add(dple);
            //            }
            //            reader.Close();
            //        }
            //    }
            //    catch (Exception exception)
            //    {
            //        Logger.Instance.WriteToLog(exception.ToString());
            //    }
            //}
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {

        }
    }

}
