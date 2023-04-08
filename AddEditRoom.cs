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
    public partial class AddEditRoom : Form
    {
        public AddEditRoom()
        {
            InitializeComponent();
            comboBoxRoomType.DisplayMember= "Text";
            comboBoxRoomType.ValueMember= "ID";
            using (SqlConnection conn = new SqlConnection(ReturnDatabaseConnection()))
            {
                // Open the connection
                conn.Open();

                string sqlQuery = "SELECT * FROM ROOM_TYPE;";
                List<ComboItem> comboItem = new List<ComboItem>();
                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboItem.Add(new ComboItem { ID = reader.GetInt32(0), Text = reader.GetString(1) });
                        }
                    }
                }

                comboBoxRoomType.DataSource = comboItem.ToArray();
            }
        }

        private string ReturnDatabaseConnection()
        {
            return Properties.Resources.connectionString;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (GetRoomLocation() == string.Empty)
            {
                MessageBox.Show("Room Location must not be empty", "Error");
            }
            else
            {
                bool valid = true;
                using (SqlConnection conn = new SqlConnection(ReturnDatabaseConnection()))
                {
                    // Open the connection
                    conn.Open();

                    string sqlQuery = "SELECT ROOM_LOCATION FROM ROOM;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (GetRoomLocation().Equals(reader.GetString(0)))
                                {
                                    valid = false;
                                }
                            }
                        }
                    }
                }
                if (valid)
                {
                    DialogResult= DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Room Location is already registered", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
        }

        public void SetRoomLocation(string roomLocation)
        {
            textBoxRoomLocation.Text = roomLocation;
        }

        public string GetRoomLocation()
        {
            return textBoxRoomLocation.Text;
        }

        public void SetRoomTypeID(int roomTypeID)
        {
            comboBoxRoomType.SelectedValue = roomTypeID;
        }


        public int GetRoomTypeID()
        {
            return (int)comboBoxRoomType.SelectedValue;
        }

        public void RemoveDeleteButton()
        {
            buttonDelete.Visible = false;
        }

        public void ShowDeleteButton()
        {
            buttonDelete.Visible = true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmation == DialogResult.Yes)
            {
                DialogResult= DialogResult.Yes;
            }
        }
    }
}
