using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class OrderForm : Form
    {
        // ctor. pass the table number ( id ) to the form.
        public OrderForm()
        {
            InitializeComponent();
        }


        private void OrderForm_Load(object sender, EventArgs e)
        {

        }
        // hide all panels.
        private void hideAllPanels()
        {
            pnlLunch.Visible = false;
            pnlDinner.Visible = false;
            pnlDrinks.Visible = false;
        }

    }
}
