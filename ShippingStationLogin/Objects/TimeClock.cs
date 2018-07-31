using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingStationLogin.Database;
using ShippingStationLogin.Objects;
using System.Data;

namespace ShippingStationLogin.Objects
{
    public class TimeClock
    {
        private Postgres postgres;

        /// <summary>
        /// TimeClock object for controlling in and out punches 
        /// to shipping stations
        /// </summary>
        /// <param name="postgres">Postgres database connection object</param>
        public TimeClock(Postgres postgres)
        {
            this.postgres = postgres;
        }

        /// <summary>
        /// Check that session key is a calid session key,
        /// A valid session key contains only numbers
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <returns>bool</returns>
        public bool ValidateSessionKey(string sessionKey)
        {
            int temp;
            return int.TryParse(sessionKey, out temp);
        }

        /// <summary>
        /// Get current status of station, returns true if station is empty,
        /// false if an employee is currently working there.
        /// </summary>
        /// <param name="stationNumber"></param>
        /// <returns></returns>
        private bool getStationStatus(string stationNumber)
        {
            try
            {
                DataTable dt = postgres.execute(Queries.GET_NUMBER_OF_EMPLOYEES_AT_STATION(stationNumber));

                if (Int32.Parse(dt.Rows[0][1].ToString()) > 0)
                {
                    return false;
                }
            }
            catch
            {
                // Station is empty, query returned nothing
            }

            return true;
        }

        /// <summary>
        /// Get status of employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>string</returns>
        public string GetStatus(Employee employee)
        {
            try
            {
                DataTable dt = postgres.execute(Queries.GET_CURRENT_CLOCKED_IN_EMPLOYEES());

                foreach (DataRow row in dt.Rows)
                {
                    if (row[0].ToString() == employee.UserId)
                    {
                        return string.Format("Employee: {0}\nStation: {1}\nClocked In: {2}",
                            employee.ToString(), row[4].ToString(), row[5].ToString()
                            );
                    }
                }

                return string.Format("{0} is not currently at a station.", employee.ToString());
            }
            catch (Exception e)
            {
                return "No employees currently shipping!";
            }
        }

        /// <summary>
        /// Clock into a shipping station
        /// </summary>
        /// <param name="e">Employee</param>
        /// <param name="sessionKey">unique session key</param>
        /// <returns>bool</returns>
        public bool ClockIn(Employee e, string station, string sessionKey)
        {
            if (getStationStatus(station))
            {
                DataTable dt = postgres.execute(Queries.CLOCK_IN(e.UserId, station, sessionKey));
                bool status = false;

                foreach (DataRow row in dt.Rows)
                {
                    status = (bool)row[0];
                }

                return status;
            }

            return false;
        }

        /// <summary>
        /// Clock out of a shipping station
        /// </summary>
        /// <param name="e">Employee</param>
        /// <param name="sessionKey">unique session key</param>
        /// <returns>bool</returns>
        public bool ClockOut(Employee e, string station, string sessionKey)
        {
            DataTable dt = postgres.execute(Queries.CLOCK_OUT(e.UserId, station, sessionKey));
            bool status = false;

            foreach (DataRow row in dt.Rows)
            {
                status = (bool)row[0];
            }

            return status;
        }

        /// <summary>
        /// Diagnose issue with clock in or clock out
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <param name="station">station number</param>
        /// <param name="sessionKey">session key</param>
        /// <param name="clockIn">issue was caused by a clock in</param>
        /// <param name="clockOut">issue was caused by a clock out</param>
        internal DiagnosisTable DiagnoseTimeIssue(Employee employee, string station, string sessionKey, bool clockIn = false, bool clockOut = false)
        {
            // Make sure they aren't giving a bad session key first
            if (!ValidateSessionKey(sessionKey))
                return DiagnosisTable.INVALID_SESSION_KEY;

            /*
             * There was an issue clocking in to the station, this can be caused
             * by multiple things.          
             */
            if (clockIn)
            {
                try
                {
                    foreach (DataRow row in postgres.execute(Queries.GET_CURRENT_ACTIVITY()).Rows)
                    {
                        if (row[0].ToString() == employee.UserId)
                        {
                            // See if employee is trying to clock into the same station again
                            if (row[11].ToString() == station)
                            {
                                return DiagnosisTable.EMPLOYEE_ALREADY_CLOCKED_IN_AT_STATION;
                            }

                            // See if employee is clocked into another station
                            if (row[11].ToString() != station)
                            {
                                return DiagnosisTable.EMPLOYEE_ALREADY_CLOCKED_IN_AT_ANOTHER_STATION;
                            }
                        }
                    }
                }
                catch
                {
                    // No employees clocked in

                    return DiagnosisTable.NO_EMPLOYEES_CLOCKED_IN;
                }

                // See if another employee is at this station
                if (!getStationStatus(station))
                {
                    return DiagnosisTable.STATION_AT_MAX_CAPACITY;
                }
            }

            /*
             * There was an issue clocking out of the station, this can be caused
             * by multiple things.          
             */
            if (clockOut)
            {
                // Check to see if employee is clocked in
                foreach (DataRow row in postgres.execute(Queries.GET_CURRENT_ACTIVITY()).Rows)
                {
                    if (row[0].ToString() == employee.UserId)
                    {
                        // See if employee is giving the wrong pass key
                        if (row[11].ToString() == station && row[14].ToString() != sessionKey)
                        {
                            return DiagnosisTable.INCORRECT_SESSION_KEY;
                        }

                        // See if employee is clocked into another station
                        if (row[11].ToString() != station)
                        {
                            return DiagnosisTable.EMPLOYEE_ALREADY_CLOCKED_IN_AT_ANOTHER_STATION;
                        }
                    }
                }

                return DiagnosisTable.EMPLOYEE_NOT_CLOCKED_IN;
            }

            return DiagnosisTable.UNKNOWN;
        }
    }
}
