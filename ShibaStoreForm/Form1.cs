using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShibaStoreForm
{
    public partial class Form1 : Form
    {
        StoreDBEntities db = new StoreDBEntities();
        public Form1()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Dogs.ToList();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            db.SaveChanges();
            MessageBox.Show("Data updated! :)");
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {

            if(dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("No row selected. :(");
            }
            else
            {
                string selectedRow = dataGridView1.CurrentRow.Cells[2].FormattedValue.ToString();
                int selectedID = Int32.Parse(selectedRow);



                foreach (var user in db.Users)
                {
                    User users = db.Users.First(i => i.UserID == user.UserID);
                    Dog dog = db.Dogs.First(i => i.DogID == selectedID);

                    users.Dogs.Remove(dog);
                    db.Dogs.Remove(dog);
                }
                db.SaveChanges();
                MessageBox.Show("Data deleted! :)");
                dataGridView1.DataSource = db.Dogs.ToList();

            }
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            Dog dog = new Dog();
            dog.DogName = dognametxt.Text;
            dog.DogDesc = dogdescriptiontxt.Text;
            dog.ImageName = dogimagetxt.Text;

            db.Dogs.Add(dog);
            db.SaveChanges();
            MessageBox.Show("Entry saved! :)");
            dognametxt.Text = "";
            dogdescriptiontxt.Text = "";
            dogimagetxt.Text = "";
        }
    }
}
