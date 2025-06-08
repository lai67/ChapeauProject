using System;
using ChapeauUI;
using DAL;
using Model;
using Service;
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
    public partial class PaymentFormCompleteBill : Form
    {
        public PaymentFormCompleteBill()
        {
            InitializeComponent();
        }

        private void btnFinalizePayment_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Payment successful!");
        }
    }
}
