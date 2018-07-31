using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingStationLogin.Objects
{
    public enum DiagnosisTable
    {
        NO_EMPLOYEES_CLOCKED_IN,

        STATION_AT_MAX_CAPACITY,

        EMPLOYEE_NOT_CLOCKED_IN,

        EMPLOYEE_ALREADY_CLOCKED_IN_AT_STATION,

        EMPLOYEE_ALREADY_CLOCKED_IN_AT_ANOTHER_STATION,

        INVALID_SESSION_KEY,

        INCORRECT_SESSION_KEY,

        UNKNOWN
    }
}
