using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.MetaData
{
    public class ZipCodes
    {
        public static String getZipCode(String city, string state)
        {
            String zipcode = "";

            String sqlStr = "SELECT zipcode FROM lkpZipCodes WHERE city=@city AND state=@state";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("city",city);
            pc.Add("state",state);

            DbDataReader reader = BCDB2.Instance.ExecuteReaderWithParams(sqlStr, pc);

            if (reader.Read())
            {
                zipcode = reader.GetValue(0).ToString();
            }

            reader.Close();
            return zipcode;
        }


        public static bool isValidZip(String zipcode)
        {
            bool validZip = false;

            String sqlStr = "SELECT city FROM lkpZipCodes WHERE zipcode=@zipcode";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("zipcode", zipcode);

            DbDataReader reader = BCDB2.Instance.ExecuteReaderWithParams(sqlStr, pc);

            if (reader.Read())
            {
                validZip = true;
            }

            reader.Close();
            return validZip;
        }

        public static double getDistance(String zipcode1, String zipcode2)
        {
            String lat1 = "";
            String long1 = "";
            String lat2 = "";
            String long2 = "";

            // get lat and long for first zipcode
            String sqlStr = "SELECT latitude, longitude FROM lkpZipCodes WHERE zipcode=@zipcode";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("zipcode", zipcode1);
            DbDataReader reader =BCDB2.Instance.ExecuteReaderWithParams(sqlStr, pc); 
            while (reader.Read())
            {
                lat1 = reader.GetValue(0).ToString();
                long1 = reader.GetValue(1).ToString();
            } 
            reader.Close();

            // get lat and long for second zipcode
            sqlStr = "SELECT latitude, longitude FROM lkpZipCodes WHERE zipcode=@zipcode";
            pc = new ParameterCollection();
            pc.Add("zipcode", zipcode2);
            reader = BCDB2.Instance.ExecuteReaderWithParams(sqlStr, pc); 
            while (reader.Read())
            {
                lat2 = reader.GetValue(0).ToString();
                long2 = reader.GetValue(1).ToString();
            }
            reader.Close();

            if (lat1 == "" || long1 == "" || lat2 == "" || long2 == "")
            {
                return 0.0;
            }

            double latitude1 = Double.Parse(lat1);
            double longitude1 = Double.Parse(long1);
            double latitude2 = Double.Parse(lat2);
            double longitude2 = Double.Parse(long2);

            return Haversine(latitude1, longitude1, latitude2, longitude2);
        }

        private static double Haversine(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            
            double dDistance = Double.MinValue;
            latitude1 = latitude1 * (Math.PI / 180.0);
            longitude1 = longitude1 * (Math.PI / 180.0);
            latitude2 = latitude2 * (Math.PI / 180.0);
            longitude2 = longitude2 * (Math.PI / 180.0);

            double dLongitude = longitude2 - longitude1;
            double dLatitude = latitude2 - latitude1;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                       Math.Cos(latitude1) * Math.Cos(latitude2) * Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            Double kEarthRadiusMiles = 3956.0;

            // Distance.
            dDistance = kEarthRadiusMiles * c;

            return dDistance;
        }

    }
}
