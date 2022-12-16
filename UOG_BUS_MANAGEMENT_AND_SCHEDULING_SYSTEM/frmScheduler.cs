using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;


namespace Scheduler
{
    public partial class frmScheduler : Form
    {
        public int myID,sYearID,sYearLevel;
        public int sDay, sDay1,sDay2;
        public string sSubjectCode,mFormState;

        public frmScheduler()
        {
            InitializeComponent();
        }

        private void TimeTable()
        {
            String[] modules = new String[7] { "M", "T", "W", "H", "F", "S", "A" };
            for (int i = 0; i < 7; i++)
            {
                dataGridViewSchedule.Rows.Add();
                dataGridViewSchedule.Rows[i].Cells[0].Value = modules[i];
                dataGridViewSchedule.Rows[i].Height = 20;
            }

            dataGridViewSchedule.Rows[7].Cells[0].Value = "";
            dataGridViewSchedule.Rows[7].Height = 0;
            dataGridViewSchedule.Rows[7].ReadOnly = true;
        }

        public void GenerateOfferingID()
        {
            OleDbCommand cmddr = new OleDbCommand("select NextNo from tblGenerator where TableName='" + "tblSubjectOffering" + "'", clsCon.con);
            OleDbDataReader dr = cmddr.ExecuteReader();
            while (dr.Read())
            {
                string strid = dr["NextNo"].ToString();
                if (strid == "")
                {
                    txtSubjectOfferID.Text = "SubjOff-" + "1";
                    myID = 1;
                }
                else
                {
                    myID = Convert.ToInt32(dr["NextNo"]) + 1;
                    txtSubjectOfferID.Text = "SubjOff-" + myID.ToString();
                }
            }
            dr.Close();
            cmddr.Dispose();
        }

        public bool DataInUse()
        {
            bool Temp;
            Temp = false;

            if (FacultyInUse() == true)
            {
                MessageBox.Show("Faculty in use.");
                cboFaculty.Focus();
                DisplayFacultyInUse();
                DisplayLabelConflictForFaculty();
                Temp = true;
            }

            else if (RoomInUse() == true)
            {
                MessageBox.Show("Room in use.");
                cboRoom.Focus();
                DisplayRoomInUse();
                DisplayLabelConflictForRoom();
                Temp = true;
            }
            return Temp;
        }

        public bool FacultyInUse()
        {
            OleDbCommand com = new OleDbCommand(" Select * from qrySubjectOfferring Where cTimeIn >=#" + cboFrom.Text + "# and cTimeOut <=#" + cboTo.Text + "# and Faculty like'" + cboFaculty.Text + "%' and cDay Like '%" + cboDay.Text + "%'", clsCon.con);
            OleDbDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            { return true; }
            else
            { return false; }
        }

        public bool RoomInUse()
        {
            OleDbCommand com = new OleDbCommand("Select * from qrySubjectOfferring Where cTimeIn >=#" + cboFrom.Text + "# and cTimeOut <=#" + cboTo.Text + "# and cRoom like'%" + cboRoom.Text + "%' and cDay like '%" + cboDay.Text + "%'", clsCon.con);
            OleDbDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            { return true; }
            else
            { return false; }
        }

        public bool SubjectAlreadyOffered(string sSubject)
        {
            OleDbCommand com = new OleDbCommand("Select * from qrySubjectOfferring Where SubjectCode LIKE '%" + sSubject + "%'", clsCon.con);
            OleDbDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            { return true; }
            else
            { return false; }
        }


        void DisplayFacultyInUse()
        {
            string msql = " SELECT * From qrySubjectOfferring Where cTimeIn >=#" + cboFrom.Text + "# and cTimeOut <=#" + cboTo.Text + "# and cDay like '%" + cboDay.Text + "%' and Faculty Like'%" + cboFaculty.Text + "%'";

            OleDbCommand com = new OleDbCommand(msql, clsCon.con);
            OleDbDataReader dr = com.ExecuteReader();
            lvConflicts.Items.Clear();
            while (dr.Read())
            {
                ListViewItem lv = new ListViewItem(dr["Subject"].ToString());
                lv.SubItems.Add(dr["Section"].ToString());
                lv.SubItems.Add(dr["cRoom"].ToString());
                lv.SubItems.Add(dr["Schedule"].ToString());
                lv.SubItems.Add(dr["Faculty"].ToString());
                lv.ForeColor = Color.Red;
                lvConflicts.Items.AddRange(new ListViewItem[] { lv });
            }
            dr.Close();
        }

        void DisplayRoomInUse()
        {
            string msql = " SELECT * From qrySubjectOfferring Where cTimeIn >=#" + cboFrom.Text + "# and cTimeOut <=#" + cboTo.Text + "# and cDay like '%" + cboDay.Text + "%' and cRoom='" + cboRoom.Text + "'";

            OleDbCommand com = new OleDbCommand(msql, clsCon.con);
            OleDbDataReader dr = com.ExecuteReader();
            lvConflicts.Items.Clear();
            while (dr.Read())
            {
                ListViewItem lv = new ListViewItem(dr["Subject"].ToString());
                lv.SubItems.Add(dr["Section"].ToString());
                lv.SubItems.Add(dr["cRoom"].ToString());
                lv.SubItems.Add(dr["Schedule"].ToString());
                lv.SubItems.Add(dr["Faculty"].ToString());
                lv.ForeColor = Color.Red;
                lvConflicts.Items.AddRange(new ListViewItem[] { lv });
            }
            dr.Close();
        }

        void DisplayLabelConflictForFaculty()
        {
            if (lvSchedule.Items.Count >= 1)
            {
                txtStatus.Text = " Conflict Schedules For: " + cboFaculty.Text;
            }
            else
            {
                txtStatus.Text = "NO Concflict";
            }
        }

        void DisplayLabelConflictForRoom()
        {
            if (lvSchedule.Items.Count >= 1)
            {
                txtStatus.Text = " Conflict Schedules For Room: " + cboRoom.Text;
            }
            else
            {
                txtStatus.Text = "NO Concflict";
            }
        }

        void ClearItems()
        {
            cboFaculty.Text = "";
            cboRoom.Text = "";
            txtSubject.Text = "";
            cboDay.Text = "";
            cboFrom.Text = "";
            cboTo.Text = "";
        }

        private void LoadFaculty()
        {
            OleDbCommand cmd = new OleDbCommand("Select * From Faculty", clsCon.con);
            OleDbDataReader dr = cmd.ExecuteReader();
            cboFaculty.Items.Clear();
            while (dr.Read())
            {
                cboFaculty.Items.Add(dr["LName"].ToString() + ", " + dr["FName"].ToString());
            }
            dr.Close();
        }

        private void LoadRooms()
        {
            OleDbCommand cmd = new OleDbCommand("Select * From Room", clsCon.con);
            OleDbDataReader dr = cmd.ExecuteReader();
            cboRoom.Items.Clear();
            while (dr.Read())
            {
                cboRoom.Items.Add(dr["Building"].ToString() + "- " + dr["RoomNo"].ToString());
            }
            dr.Close();
        }


        private void AddSchedule(string sOfferingID,string sSection,string cTimeIn, string cTimeOut, string cDay, string cFaculty, string cSubject, string cRoom)
        {
            string sSQL = "Select * from qrySubjectOfferring Where [cTimeIn] >= #" + cTimeIn + "# and [cTimeOut] <= #" + cTimeOut + "# and [Faculty] like'%" + cFaculty + "%' and [Subject] like '%" + cSubject + "%' and [cRoom] like '%" + cRoom + "%' and [cDay] like '%" + cDay + "%'";
            OleDbCommand cmd = new OleDbCommand(sSQL, clsCon.con);
            OleDbDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                MessageBox.Show("Duplicate record.", "Duplicate record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dr.Close();
            }
            else
            {
                dr.Close();
                OleDbCommand cmd1 = new OleDbCommand();
                cmd1.CommandText = "INSERT INTO tblSubjectOffering(SubjectOfferingID,SectionID,cTimeIn,cTimeOut,FacultyID,SubjectCode,cRoom,cDay) VALUES('" + sOfferingID + "','"+ sSection +"','" + cTimeIn + "','" + cTimeOut + "','" + cFaculty + "','" + cSubject + "','" + cRoom + "','" + cDay + "')";
                cmd1.Connection = clsCon.con;
                cmd1.ExecuteNonQuery();

                OleDbCommand pipz = new OleDbCommand("Update tblGenerator Set NextNo='" + myID + "' where TableName ='" + "tblSubjectOffering" + "'", clsCon.con);
                pipz.ExecuteNonQuery();


                MessageBox.Show("New entry successfully saved to the record.");
                this.Close();
            }
        }

        private void DisplayAddSchedule(string sFaculty)
        {
            DateTime dTimeIn, dTimeOut;

            int num1, num2;
            Random randonGen = new Random();
            OleDbCommand com = new OleDbCommand("SELECT * FROM qrySubjectOfferring Where Faculty Like'" + sFaculty + "%'", clsCon.con);
            OleDbDataReader dr = com.ExecuteReader();
            lvSchedule.Items.Clear();
            dataGridViewSchedule.Rows.Clear();

            TimeTable();
            while (dr.Read())
            {
                ListViewItem lv = new ListViewItem(dr["Subject"].ToString());
                lv.SubItems.Add(dr["Section"].ToString());
                lv.SubItems.Add(dr["cRoom"].ToString());
                lv.SubItems.Add(dr["Schedule"].ToString());
                lv.SubItems.Add(dr["Faculty"].ToString());
                lv.ForeColor = Color.FromArgb(randonGen.Next(255), randonGen.Next(255), randonGen.Next(255));
                lvSchedule.Items.AddRange(new ListViewItem[] { lv });


                dTimeIn = DateTime.Parse(dr["cTimeIn"].ToString());
                dTimeOut = DateTime.Parse(dr["cTimeOut"].ToString());

                ConvertsDaysToInt(dr["cDay"].ToString());
                num1 = clsSchedule.ConvertsTimeINToInt(dTimeIn.ToLongTimeString());
                num2 = clsSchedule.ConvertsTimeOUTToInt(dTimeOut.ToLongTimeString());

                switch (dr["cDay"].ToString())
                {
                    case "MH":
                        #region Two Session
                        for (int time = num1; time <= num2; ++time)
                        {

                            dataGridViewSchedule[time, sDay].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay].ToolTipText = dr["Schedule"].ToString();

                            dataGridViewSchedule[time, sDay1].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay1].ToolTipText = dr["Schedule"].ToString();
                        }
                        #endregion
                        break;

                    case "TF":
                        #region Two Session
                        for (int time = num1; time <= num2; ++time)
                        {

                            dataGridViewSchedule[time, sDay].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay].ToolTipText = dr["Schedule"].ToString();

                            dataGridViewSchedule[time, sDay1].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay1].ToolTipText = dr["Schedule"].ToString();
                        }
                        #endregion
                        break;

                    case "WS":
                         #region Two Session
                        for (int time = num1; time <= num2; ++time)
                        {

                            dataGridViewSchedule[time, sDay].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay].ToolTipText = dr["Schedule"].ToString();

                            dataGridViewSchedule[time, sDay1].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay1].ToolTipText = dr["Schedule"].ToString();
                        }
                        #endregion
                        break;

                    case "MWF":
                         #region Three Session
                        for (int time = num1; time <= num2; ++time)
                        {

                            dataGridViewSchedule[time, sDay].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay].ToolTipText = dr["Schedule"].ToString();

                            dataGridViewSchedule[time, sDay1].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay1].ToolTipText = dr["Schedule"].ToString();

                            dataGridViewSchedule[time, sDay2].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay2].ToolTipText = dr["Schedule"].ToString();
                        }
                        #endregion
                        break;
                    case "THS":
                         #region Three Session
                        for (int time = num1; time <= num2; ++time)
                        {

                            dataGridViewSchedule[time, sDay].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay].ToolTipText = dr["Schedule"].ToString();

                            dataGridViewSchedule[time, sDay1].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay1].ToolTipText = dr["Schedule"].ToString();

                            dataGridViewSchedule[time, sDay2].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay2].ToolTipText = dr["Schedule"].ToString();
                        }
                        #endregion
                        break;

                    default:
                        #region Regular Session
                        for (int time = num1; time <= num2; time++)
                        {
                            dataGridViewSchedule[time, sDay].Style.BackColor = lv.ForeColor;
                            dataGridViewSchedule[time, sDay].ToolTipText = dr["Schedule"].ToString();
                        }
                        #endregion
                        break;
                }
            }
            dr.Close();
        }


        #region Time and Day Scheduling
        void ConvertsDaysToInt(string cDays)
        {
            switch (cDays)
            {
                case "M":
                    sDay = 0;
                    break;
                case "T":
                    sDay = 1;
                    break;
                case "W":
                    sDay = 2;
                    break;
                case "H":
                    sDay = 3;
                    break;
                case "F":
                    sDay = 4;
                    break;
                case "S":
                    sDay = 5;
                    break;
                case "A":
                    sDay = 6;
                    break;
                case "MH":
                    sDay = 0;
                    sDay1 = 3;
                    break;
                case "TF":
                    sDay = 1;
                    sDay1 = 4;
                    break;
                case "WS":
                    sDay = 2;
                    sDay1 = 5;
                    break;
                case "MWF":
                    sDay = 0;
                    sDay1 = 2;
                    sDay2 = 4;
                    break;
                case "THS":
                    sDay = 1;
                    sDay1 = 3;
                    sDay2 = 5;
                    break;
            }
        }
        #endregion


        private void frmScheduler_Load(object sender, EventArgs e)
        {
            if (clsCon.con.State == ConnectionState.Open)
            { clsCon.con.Close(); }
            clsCon.con.Open();
            switch(mFormState)
            {
                case "ADD":
                    if (SubjectAlreadyOffered(txtSubject.Text) == true)
                    {
                        MessageBox.Show("WARNING: Subject already been offered...", "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Close();
                    }
                    else
                    {
                        TimeTable();
                        GenerateOfferingID();
                        LoadFaculty();
                        LoadRooms();
                        clsCon.fillCombo(cboSection, "Select newSection From qrySection");
                    }
                    break;

                case "EDIT":
                    TimeTable();
                    LoadFaculty();
                    LoadRooms();
                    clsCon.fillCombo(cboSection, "Select newSection From qrySection");
                    break;
            }
        }

        public void GetTeacherByFullName(string sTeacherFullName)
        {
            OleDbCommand com = new OleDbCommand("SELECT *" +
            " From qryFaculty" +
            " WHERE FacultyName='" + sTeacherFullName + "'", clsCon.con);
            OleDbDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtFacultyID.Text = dr["FacultyID"].ToString();
            }
            dr.Close();
        }

         

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (cboFrom.Text == "" || cboTo.Text == "" || cboDay.Text == "" || cboFaculty.Text == "" || txtSubject.Text == "" || cboRoom.Text == "")
            {
                MessageBox.Show("Please fill up all the required fields");
            }
            else
            {
                if (DataInUse() == false)
                {
                    AddSchedule(txtSubjectOfferID.Text,txtSectionID.Text,cboFrom.Text, cboTo.Text, cboDay.Text, txtFacultyID.Text, txtSubject.Text, cboRoom.Text);
                    ClearItems();
                }
            }
        }

        private void cboFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayAddSchedule(cboFaculty.Text);
            GetTeacherByFullName(cboFaculty.Text);
        }

        public void GetSectionByName(string sSectionName)
        {
            OleDbCommand com = new OleDbCommand("SELECT *" +
            " From qrySection" +
            " WHERE newSection='" + sSectionName + "'", clsCon.con);
            OleDbDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtSectionID.Text = dr["SectionID"].ToString();
            }
            dr.Close();
        }

        private void cboSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            AssignYearLevel(cboSection.Text);
            if (sYearLevel != sYearID)
            {
                MessageBox.Show("Sorry unable to assign this section.\n Please select other section..","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                cboSection.Items.Clear();
                clsCon.fillCombo(cboSection, "Select newSection From qrySection");
            }
            GetSectionByName(cboSection.Text);
        }

        public void AssignYearLevel(string sSection)
        {
            OleDbCommand com = new OleDbCommand(" Select * from qrySection Where newSection Like'%" + sSection + "%'", clsCon.con);
            OleDbDataReader dr = com.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
               sYearID= int.Parse(dr["YearLvl"].ToString());
            }
            dr.Close();
        }


    }
}
