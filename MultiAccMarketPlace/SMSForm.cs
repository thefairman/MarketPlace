using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDmp
{
    public partial class SMSForm : Form
    {
        RequestLogic reqLogic;
        public SMSForm(RequestLogic reqLogic, string phone)
        {
            InitializeComponent();
            this.reqLogic = reqLogic;
            if (!string.IsNullOrEmpty(phone))
                lblPhone.Text = phone;
        }

        private void BtnSMSOk_Click(object sender, EventArgs e)
        {
            if (txtSMSText.Text.Length < 2)
            {
                lblSMSError.Text = "Enter a sms > minimum 2 characters";
                return;
            }
            reqLogic.smsText = txtSMSText.Text;
            this.Close();
        }
    }
}
