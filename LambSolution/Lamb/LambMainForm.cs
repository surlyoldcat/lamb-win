using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon;
using Amazon.Runtime;
using Lamb.service;

namespace Lamb
{
    public partial class LambMainForm : Form
    {
        private ILambService LambdaSvc { get; set; }

        public LambMainForm()
        {
            InitializeComponent();
        }

        public AWSCredentials UserAwsCredentials { get; set; }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void LambMainForm_Load(object sender, EventArgs e)
        {
            LambdaSvc = new LambService(UserAwsCredentials, RegionEndpoint.USEast1);
            PopulateLambdas();
        }

        private async void PopulateLambdas()
        {
            var result = await LambdaSvc.ListAllLambdas();
            cboLambda.DataSource = result.Lambdas;
            cboLambda.DisplayMember = "Name";
            cboLambda.ValueMember = "Name";
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {

        }
    }
}
