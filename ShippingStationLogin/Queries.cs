using ShippingStationLogin.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingStationLogin
{   
    /// <summary>
    /// Static strings for queries to execute
    /// </summary>
    public static class Queries
    {
        public static string GET_CURRENT_CLOCKED_IN_EMPLOYEES()
        {
            return @"SELECT
  e.user_id,
  e.first_name,
  e.last_name,
  shst.session_key,
  shst.station,
  shst.clock_in
FROM
  sh_employee e
  LEFT JOIN sh_ship_station_session shst on e.user_id = shst.user_id
WHERE
  to_char(shst.clock_in, 'YYYY-MM-DD') = to_char(current_timestamp, 'YYYY-MM-DD')
  and shst.clock_out is null
  and shst.session_key is not null;";
        }

        public static string GET_NUMBER_OF_EMPLOYEES_AT_STATION(string station)
        {
            return string.Format(@"SELECT
  coalesce(shst.station, null) as station,
  coalesce(count(*), null) as number_at_station
FROM
  doarni.sh_ship_station_session shst
WHERE
  to_char(shst.clock_in, 'YYYY-MM-DD') = to_char(current_timestamp, 'YYYY-MM-DD')
  and shst.clock_out is null
  and shst.station = {0}
GROUP BY
  shst.station;", station);
        }

        public static string GET_CURRENT_ACTIVITY()
        {
            return @"SELECT
  e.*,
  shst.*
FROM
  sh_employee e
  LEFT JOIN sh_ship_station_session shst on e.user_id = shst.user_id
WHERE
  to_char(shst.clock_in, 'YYYY-MM-DD') = to_char(current_timestamp, 'YYYY-MM-DD')
  and shst.clock_out is null;
";
        }

        public static string GET_SHIPPING_EMPLOYEES()
        {
            return "select user_id, first_name, last_name from doarni.sh_employee where works_shipping is true and active = 1";
        }

        public static string CLOCK_IN(string userId, string stationNumber, string sessionKey)
        {
            return string.Format("select doarni.clock_into_station({0}, {1}, {2});",
                userId, stationNumber, sessionKey                 
                );
        }

        public static string CLOCK_OUT(string userId, string stationNumber, string sessionKey)
        {
            return string.Format("select doarni.clock_out_station({0}, {1}, {2});",
                userId, stationNumber, sessionKey
                );
        }

    }
}
