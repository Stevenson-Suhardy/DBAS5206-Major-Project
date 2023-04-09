using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBAS5206_Major_Project
{
    public partial class AddEditRoomType : Form
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public AddEditRoomType()
        {
            InitializeComponent();
        }
        #endregion

        #region Getters and Setters
        public string GetRoomType()
        {
            return textBoxRoomType.Text;
        }

        public void SetRoomType(string roomType)
        {
            textBoxRoomType.Text = roomType;
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// This function returns the database connection
        /// </summary>
        /// <returns></returns>
        private string ReturnDatabaseConnection()
        {
            return Properties.Resources.connectionString;
        }
        #endregion

        #region Event Handlers

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Check if the RoomLocation is Empty
            if (GetRoomType() == string.Empty)
            {
                MessageBox.Show("Room Type must not be empty", "Error");
            }
            else
            {
                // Create boolean variable
                bool valid = true;
                // Create SqlConnection
                using (SqlConnection conn = new SqlConnection(ReturnDatabaseConnection()))
                {
                    // Open the connection
                    conn.Open();
                    // Query
                    string sqlQuery = "SELECT ROOM_TYPE_DESCRIPTION FROM ROOM_TYPE;";
                    // Create SqlCommand
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        // Create SqlDataReader and execute command
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if an existing room location already exists in the database
                            while (reader.Read())
                            {
                                if (GetRoomType().Equals(reader.GetString(0)))
                                {
                                    // Set boolean to false
                                    valid = false;
                                }
                            }
                        }
                    }
                }
                // If all input is valid
                if (valid)
                {
                    // Return DialogResult.OK as the DialogResult
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    // Show error message
                    MessageBox.Show("Room Type is already registered", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
        }

        #endregion

    }
}
