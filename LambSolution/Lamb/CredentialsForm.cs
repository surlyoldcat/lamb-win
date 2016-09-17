using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon.Runtime;
using Lamb.service;

namespace Lamb
{
    public partial class CredentialsForm : Form
    {
        public CredentialsForm()
        {
            InitializeComponent();
        }

        private CredentialsType AuthType => radioEnterCreds.Checked ? CredentialsType.UserEntered : CredentialsType.FromProfile;

        public AWSCredentials UserAwsCredentials { get; set; }

        private void CredentialsForm_Load(object sender, EventArgs e)
        {

        }

        private void radioEnterCreds_CheckedChanged(object sender, EventArgs e)
        {
            SetTextboxesState();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AWSCredentials creds = BuildCreds();
            var verify = LambService.VerifyCredentials(creds);
            if (!verify.Success)
            {
                MessageBox.Show(verify.Format(), "Fail!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                UserAwsCredentials = creds;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void radioProfileCreds_CheckedChanged(object sender, EventArgs e)
        {
            SetTextboxesState();
        }

        private void SetTextboxesState()
        {
            bool enable = AuthType == CredentialsType.UserEntered;
            txtSecretAccessKey.Enabled = enable;
            txtSecretAccessKey.ReadOnly = !enable;

            txtAccessKeyId.Enabled = enable;
            txtAccessKeyId.ReadOnly = !enable;

            txtAccount.Enabled = enable;
            txtAccount.ReadOnly = !enable;
            

        }

        private AWSCredentials BuildCreds()
        {
            AWSCredentials creds;
            if (AuthType == CredentialsType.UserEntered)
            {
                string accessKey = txtAccessKeyId.Text;
                string secretKey = txtSecretAccessKey.Text;

                creds = new BasicAWSCredentials(accessKey, secretKey);
            }
            else
            {
                creds = new StoredProfileAWSCredentials();
            }
            return creds;
        }
    }
}
