﻿using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBAS5206_Major_Project
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(tabPageLogout);
            tabControl1.TabPages.Remove(tabPageRoom);
            tabControl1.TabPages.Remove(tabPagePhysicianPatient);
        }

        /// <summary>
        /// This function will return the connection string needed to connect to database
        /// </summary>
        /// <returns></returns>
        private string ReturnDatabaseConnection()
        {
            return Properties.Resources.connectionString;
        }

        /// <summary>
        /// This event handler will happen when the form loads. It will query from the database and count how many patients are discharging.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dashboard_Load(object sender, EventArgs e)
        {
            #region Room Utilization Dashboard

            #region First ListView Properties
            // Set the first list view properties
            listViewOccupancyByRooms.View = View.Details;
            listViewOccupancyByRooms.FullRowSelect = true;
            listViewOccupancyByRooms.GridLines= true;
            // Add columns to list view
            listViewOccupancyByRooms.Columns.Add("No.", 50, HorizontalAlignment.Left);
            listViewOccupancyByRooms.Columns.Add("Room Location", 90, HorizontalAlignment.Left);
            listViewOccupancyByRooms.Columns.Add("Room Type", 90, HorizontalAlignment.Left);
            listViewOccupancyByRooms.Columns.Add("Total Number of Beds", 125, HorizontalAlignment.Left);
            listViewOccupancyByRooms.Columns.Add("Number Of Empty Beds", 125, HorizontalAlignment.Left);
            listViewOccupancyByRooms.Columns.Add("Empty Bed Id(s)", 100, HorizontalAlignment.Left);
            listViewOccupancyByRooms.Columns.Add("Is Full?", 100, HorizontalAlignment.Left);
            #endregion

            #region Second ListView Properties
            // Set the second list view properties
            listViewOccupancyByRoomType.View = View.Details;
            listViewOccupancyByRoomType.FullRowSelect = true;
            listViewOccupancyByRoomType.GridLines = true;
            // Add columns to list view
            listViewOccupancyByRoomType.Columns.Add("No.", 50, HorizontalAlignment.Left);
            listViewOccupancyByRoomType.Columns.Add("Room Type", 90, HorizontalAlignment.Left);
            listViewOccupancyByRoomType.Columns.Add("Total Number of Beds", 125, HorizontalAlignment.Left);
            listViewOccupancyByRoomType.Columns.Add("Number Of Empty Beds", 125, HorizontalAlignment.Left);
            listViewOccupancyByRoomType.Columns.Add("Empty Bed Id(s)", 100, HorizontalAlignment.Left);
            listViewOccupancyByRoomType.Columns.Add("Is Full?", 100, HorizontalAlignment.Left);
            #endregion

            #region Third ListView Properties
            listViewDischargingPatient.View = View.Details;
            listViewDischargingPatient.FullRowSelect = true;
            listViewDischargingPatient.GridLines = true;
            // Add columns to list view
            listViewDischargingPatient.Columns.Add("Patient ID", 100, HorizontalAlignment.Left);
            listViewDischargingPatient.Columns.Add("Full Name", 125, HorizontalAlignment.Left);
            listViewDischargingPatient.Columns.Add("Address", 125, HorizontalAlignment.Left);
            listViewDischargingPatient.Columns.Add("City, Province, Postal Code", 125, HorizontalAlignment.Left);
            listViewDischargingPatient.Columns.Add("Phone Number", 100, HorizontalAlignment.Left);
            listViewDischargingPatient.Columns.Add("Gender", 100, HorizontalAlignment.Left);
            listViewDischargingPatient.Columns.Add("Extension", 100, HorizontalAlignment.Left);

            #endregion

            #region Database Queries
            // Create the connection
            using (SqlConnection conn = new SqlConnection(ReturnDatabaseConnection()))
            {
                conn.Open();
                PopulateRoomDashboardLists(conn);
            }
            #endregion
            #endregion
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            tabControl1.TabPages.Remove(tabPageLogin);
            tabControl1.TabPages.Add(tabPageLogout);
            tabControl1.TabPages.Add(tabPageRoom);
            tabControl1.TabPages.Add(tabPagePhysicianPatient);
        }

        private void buttonAddRoom_Click(object sender, EventArgs e)
        {
            using (AddEditRoom addRoom = new AddEditRoom())
            {
                addRoom.RemoveDeleteButton();
                if (addRoom.ShowDialog() == DialogResult.OK)
                {
                    using (SqlConnection conn = new SqlConnection(ReturnDatabaseConnection()))
                    {
                        // Open the connection
                        conn.Open();

                        string sqlQuery = "EXEC Add_New_Room '" + addRoom.GetRoomLocation() + "', " + addRoom.GetRoomTypeID().ToString();
                        using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                        {
                            int rows = command.ExecuteNonQuery();
                            if (rows == 1)
                            {
                                MessageBox.Show("Room " + addRoom.GetRoomLocation() + " has successfully been added.", "Sucess!");

                                ResetListViews();
                                PopulateRoomDashboardLists(conn);
                            }
                            else
                            {
                                MessageBox.Show("An error has happened", "ERROR");
                            }
                        }

                    }
                }
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {

            tabControl1.TabPages.Clear();
            tabControl1.TabPages.Insert(0, tabPageLogin);
        }

        private void ResetListViews()
        {
            listViewDischargingPatient.Items.Clear();
            listViewOccupancyByRooms.Items.Clear();
            listViewOccupancyByRoomType.Items.Clear();
        }

        private void PopulateRoomDashboardLists(SqlConnection conn)
        {

            #region Populating the First List View
            string sqlQuery = "EXEC Num_Of_NonOrAvailable_Bed_Each_Room 1;";
            List<string> numOfEmptyBeds = new List<string>();
            using (SqlCommand command = new SqlCommand(sqlQuery, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        numOfEmptyBeds.Add(reader.GetString(0));
                        numOfEmptyBeds.Add(reader.GetInt32(1).ToString());
                    }
                }
            }
            sqlQuery = "EXEC Bed_IDs_NonOrAvailable_Each_Room 1;";
            List<string> emptyBeds = new List<string>();
            using (SqlCommand command = new SqlCommand(sqlQuery, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (emptyBeds.Contains(reader.GetString(0)))
                        {
                            int index = emptyBeds.IndexOf(reader.GetString(0));
                            string stringConcat = emptyBeds[index + 1] + ", " + reader.GetInt32(1).ToString();
                            emptyBeds.RemoveAt(index + 1);
                            emptyBeds.Insert(index + 1, stringConcat);
                        }
                        else
                        {
                            emptyBeds.Add(reader.GetString(0));
                            emptyBeds.Add(reader.GetInt32(1).ToString());
                        }
                    }
                }
            }
            sqlQuery = "SELECT * FROM View_Room_Details ORDER BY ROOM_LOCATION;";
            List<string> arr = new List<string>();
            using (SqlCommand command = new SqlCommand(sqlQuery, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        arr.Add(i.ToString());
                        arr.Add(reader.GetString(0));
                        arr.Add(reader.GetString(1));
                        arr.Add(reader.GetInt32(2).ToString());
                        if (reader.GetString(0).Equals(numOfEmptyBeds[0]))
                        {
                            arr.Add(numOfEmptyBeds[1]);
                            numOfEmptyBeds.Remove(numOfEmptyBeds[0]);
                            numOfEmptyBeds.Remove(numOfEmptyBeds[0]);
                        }
                        else
                        {
                            arr.Add("0");
                        }
                        if (reader.GetString(0).Equals(emptyBeds[0]))
                        {
                            arr.Add(emptyBeds[1]);
                            emptyBeds.Remove(emptyBeds[0]);
                            emptyBeds.Remove(emptyBeds[0]);
                            arr.Add("No");
                        }
                        else
                        {
                            arr.Add("");
                            arr.Add("Yes");
                        }
                        listViewOccupancyByRooms.Items.Add(new ListViewItem(arr.ToArray()));
                        arr.Clear();
                        i++;
                    }
                }
            }
            #endregion

            #region Populating the Second List View

            sqlQuery = "EXEC Num_Of_NonOrAvailable_Beds_Each_Room_Type 1";
            numOfEmptyBeds.Clear();
            using (SqlCommand command = new SqlCommand(sqlQuery, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        numOfEmptyBeds.Add(reader.GetString(0));
                        numOfEmptyBeds.Add(reader.GetInt32(1).ToString());
                    }
                }
            }

            sqlQuery = "SELECT * FROM View_Room_Type_Details;";
            arr.Clear();
            using (SqlCommand command = new SqlCommand(sqlQuery, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        arr.Add(i.ToString());
                        arr.Add(reader.GetString(0));
                        arr.Add(reader.GetInt32(1).ToString());
                        if (reader.GetString(0).Equals(numOfEmptyBeds[0]))
                        {
                            arr.Add(numOfEmptyBeds[1]);
                            numOfEmptyBeds.RemoveAt(0);
                            numOfEmptyBeds.RemoveAt(0);
                        }
                        else
                        {
                            arr.Add("0");
                        }
                        listViewOccupancyByRoomType.Items.Add(new ListViewItem(arr.ToArray()));
                        arr.Clear();
                        i++;
                    }
                }
            }

            #endregion

            sqlQuery = "SELECT * FROM View_Discharging_Patients ORDER BY 'Patient ID'";
            arr.Clear();
            using (SqlCommand command = new SqlCommand(sqlQuery, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        arr.Add(reader.GetInt32(0).ToString());
                        arr.Add(reader.GetString(1));
                        arr.Add(reader.GetString(2));
                        arr.Add(reader.GetString(3));
                        arr.Add(reader.GetString(4));
                        arr.Add(reader.GetString(5));
                        arr.Add(reader.GetString(6));
                        listViewDischargingPatient.Items.Add(new ListViewItem(arr.ToArray()));
                        arr.Clear();
                    }
                }
            }
        }

        private void listViewOccupancyByRooms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (AddEditRoom editRoom = new AddEditRoom())
            {
                editRoom.ShowDeleteButton();
                editRoom.SetRoomLocation(listViewOccupancyByRooms.SelectedItems[0].SubItems[1].Text);
                string oldLocation = editRoom.GetRoomLocation();
                string sqlQuery = "SELECT * FROM ROOM_TYPE;";
                using (SqlConnection conn = new SqlConnection(ReturnDatabaseConnection()))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                if (listViewOccupancyByRooms.SelectedItems[0].SubItems[2].Text.Equals(reader.GetString(1)))
                                {
                                    editRoom.SetRoomTypeID(reader.GetInt32(0));
                                    break;
                                }
                            }
                        }
                    }
                }
                if (editRoom.ShowDialog() == DialogResult.OK)
                {
                    sqlQuery = "EXEC Update_Room '" + oldLocation + "', '" + editRoom.GetRoomLocation() + "', " + editRoom.GetRoomTypeID().ToString();
                    using (SqlConnection conn = new SqlConnection(ReturnDatabaseConnection()))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                        {
                            int row = command.ExecuteNonQuery();
                            if (row == 1)
                            {
                                MessageBox.Show("Room " + oldLocation + " has been updated.", "Sucess!");
                                ResetListViews();
                                PopulateRoomDashboardLists(conn);
                            }
                            else
                            {
                                MessageBox.Show("An error has occured.", "ERROR");
                            }
                        }
                    }
                }
                else if (editRoom.DialogResult == DialogResult.Yes)
                {
                    sqlQuery = "EXEC Delete_Room '" + oldLocation + "';";
                    using (SqlConnection conn = new SqlConnection(ReturnDatabaseConnection()))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                        {
                            int row = command.ExecuteNonQuery();
                            if (row == 1)
                            {
                                MessageBox.Show("Room " + oldLocation + " has been deleted.", "Sucess!");
                                ResetListViews();
                                PopulateRoomDashboardLists(conn);
                            }
                            else
                            {
                                MessageBox.Show("An error has occured.", "ERROR");
                            }
                        }
                    }
                }
            }
        }
    }
}
