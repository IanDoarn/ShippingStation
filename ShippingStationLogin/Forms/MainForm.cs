using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ShippingStationLogin.Database;
using ShippingStationLogin.Objects;

namespace ShippingStationLogin.Forms
{
    public partial class MainForm : Form
    {
        private sealed class Diagnosis
        {
            public static void Diagnose(DiagnosisTable flag)
            {
                switch (flag)
                {
                    case DiagnosisTable.NO_EMPLOYEES_CLOCKED_IN:
                        MessageBox.Show(
                            string.Format("[{0}] No employees are clocked in!", (flag).ToString()),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Information
                            ); break;

                    case DiagnosisTable.STATION_AT_MAX_CAPACITY:
                        MessageBox.Show(
                            string.Format("[{0}] Cannot clock into this station, station is at max capacity", (flag).ToString()),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                            ); break;

                    case DiagnosisTable.INVALID_SESSION_KEY:
                        MessageBox.Show(
                            string.Format("[{0}] Cannot clock out, invalid Pass Key for employee", (flag).ToString()),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                            ); break;

                    case DiagnosisTable.INCORRECT_SESSION_KEY:
                        MessageBox.Show(
                            string.Format("[{0}] Cannot clock out, incorrect Pass Key for employee", (flag).ToString()),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                            ); break;

                    case DiagnosisTable.EMPLOYEE_ALREADY_CLOCKED_IN_AT_STATION:
                        MessageBox.Show(
                            string.Format("[{0}] You are already clocked into this station! Click the status button for more information", (flag).ToString()),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                            ); break;

                    case DiagnosisTable.EMPLOYEE_ALREADY_CLOCKED_IN_AT_ANOTHER_STATION:
                        MessageBox.Show(
                            string.Format("[{0}] You are already clocked into another station! Click the status button for more information", (flag).ToString()),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                            ); break;

                    case DiagnosisTable.EMPLOYEE_NOT_CLOCKED_IN:
                        MessageBox.Show(
                            string.Format("[{0}] You aren't clocked in!", (flag).ToString()),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                            ); break;

                    default:
                        MessageBox.Show(
                            string.Format("[{0}] An unknown error occured! Please contact a manager for help.", (DiagnosisTable.UNKNOWN).ToString()),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning
                            ); break;
                }
            }
        }

        private Postgres postgres;
        private TimeClock timeClock;

        private List<Employee> employees = new List<Employee>();

        public MainForm(Postgres postgres)
        {
            this.postgres = postgres;

            timeClock = new TimeClock(postgres);           

            InitializeComponent();

            sessionKeyTextBox.MaxLength = 4;

            getShippers();
        }

        private bool checkAllInputs(bool checkEmployee = true, bool checkStation = true, bool checkSessionKey = true)
        {
            if(checkEmployee)
                if (employeeComboBox.SelectedItem == null)
                {
                    MessageBox.Show("You didn't select an employee!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            if (checkStation)
                if (stationComboBox.SelectedItem == null)
                {
                    MessageBox.Show("You didn't select a station!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            if (checkSessionKey)
            {
                if (string.IsNullOrEmpty(sessionKeyTextBox.Text) || string.IsNullOrWhiteSpace(sessionKeyTextBox.Text))
                {
                    MessageBox.Show("You didn't enter a Pass Key!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (!timeClock.ValidateSessionKey(sessionKeyTextBox.Text))
                {
                    MessageBox.Show("Please enter a valid Pass Key. A valid pass key contains only numbers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;                    
                }
            }

            return true;
        }

        private void getShippers()
        {
            DataTable dt = postgres.execute(Queries.GET_SHIPPING_EMPLOYEES(), true);

            foreach(DataRow row in dt.Rows)
            {
                Employee e = new Employee(row[0].ToString(), row[1].ToString(), row[2].ToString());

                employees.Add(e);

                employeeComboBox.Items.Add(e.ToString());
            }
        }

        private Employee getEmployee()
        {
            foreach (Employee em in employees)
            {
                if (em.ToString() == employeeComboBox.SelectedItem.ToString())
                {
                    return em;
                }
            }
            
            return null;
        }

        private void statusButton_Click(object sender, EventArgs e)
        {
            if(checkAllInputs(checkSessionKey: false))
                MessageBox.Show(timeClock.GetStatus(getEmployee()), "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void clockInButton_Click(object sender, EventArgs e)
        {
            if(checkAllInputs())
            {
                Employee employee = getEmployee();
                string station = stationComboBox.SelectedItem.ToString();
                string sessionKey = sessionKeyTextBox.Text;

                bool success = timeClock.ClockIn(employee, station, sessionKey);

                if (!success)
                {
                    Diagnosis.Diagnose(timeClock.DiagnoseTimeIssue(employee, station, sessionKey, clockIn: true));
                }
                else
                {
                    MessageBox.Show(timeClock.GetStatus(employee), "Clock In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void clockOutButton_Click(object sender, EventArgs e)
        {
            if(checkAllInputs())
            {
                Employee employee = getEmployee();
                string station = stationComboBox.SelectedItem.ToString();
                string sessionKey = sessionKeyTextBox.Text;

                bool success = timeClock.ClockOut(employee, station, sessionKey);
                if (!success)
                {
                    Diagnosis.Diagnose(timeClock.DiagnoseTimeIssue(employee, station, sessionKey, clockOut: true));
                }
                else
                {
                    MessageBox.Show("Clock out " + employee.ToString(), "Clock Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
        }

        /// <summary>
        /// Event Method to ensure users pass key (session key) is only 4 digits long
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sessionKeyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
