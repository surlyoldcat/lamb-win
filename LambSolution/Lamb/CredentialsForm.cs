using System;
using System.Linq;
using System.Windows.Forms;
using Amazon;
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

        private CredentialsType AuthType
            => radioEnterCreds.Checked ? CredentialsType.UserEntered : CredentialsType.FromProfile;

        public AWSCredentials UserAwsCredentials { get; set; }
        public RegionEndpoint AwsRegion { get; set; }

        private void CredentialsForm_Load(object sender, EventArgs e)
        {
            cboRegion.DataSource = RegionEndpoint.EnumerableAllRegions.ToArray();
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
            RegionEndpoint reg = (RegionEndpoint)cboRegion.SelectedItem;
            var verify = LambService.VerifyCredentials(creds, reg);
            if (!verify.Success)
            {
                MessageBox.Show(verify.Format(), "Fail!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                UserAwsCredentials = creds;
                AwsRegion = reg;
                DialogResult = DialogResult.OK;
                Close();
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

            cboRegion.Enabled = enable;
            
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