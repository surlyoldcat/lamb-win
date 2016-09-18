using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon;
using Amazon.Runtime;
using Lamb.model;
using Lamb.service;

namespace Lamb
{
    public partial class LambMainForm : Form
    {
        private readonly SynchronizationContext m_SyncContext;

        public LambMainForm()
        {
            InitializeComponent();
            m_SyncContext = SynchronizationContext.Current;
        }

        private ILambService LambdaSvc { get; set; }

        public AWSCredentials UserAwsCredentials { get; set; }
        public RegionEndpoint AwsRegion { get; set; }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private async void LambMainForm_Load(object sender, EventArgs e)
        {
            LambdaSvc = new LambService(UserAwsCredentials, AwsRegion);
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

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            if (ValidatePayload())
            {
                LambdaInvokeInfo ii = new LambdaInvokeInfo

                {
                    Name = ((LambdaListItem) cboLambda.SelectedItem).Name,
                    FunctionPayload = txtPayload.Text
                };
                await RunIt(ii);
            }
        }

        private bool ValidatePayload()
        {
            string payload = txtPayload.Text;
            bool isValid;
            if (String.IsNullOrEmpty(txtPayload.Text))
            {
                isValid = true;
            }
            else
            {
                isValid = payload.IsValidJson();
            }

            if (!isValid)
            {
                MessageBox.Show("The entered payload is not valid Json!", "Fail", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            return isValid;
        }

        private async Task RunIt(LambdaInvokeInfo startInfo)
        {
            ClearOutputText();
            AppendOutputText("Invoking Lambda: " + startInfo.Name);
            AppendOutputText(Environment.NewLine);
            SetWaitState(true);
            try
            {
                var result = await LambdaSvc.Execute(startInfo);
                AppendOutputText(result.Format());
            }
            catch (Exception ex)
            {
                AppendOutputText(Environment.NewLine);
                AppendOutputText("An error occurred!");
                AppendOutputText(ex.Message);
            }
            finally
            {
                AppendOutputText(Environment.NewLine);
                AppendOutputText("Lambda invocation complete.");
                SetWaitState(false);
            }
        }


        public void ClearOutputText()
        {
            m_SyncContext.Post(o => { txtOutput.Text = String.Empty; }, null);
        }

        public void AppendOutputText(string message)
        {
            m_SyncContext.Post(o => { txtOutput.Text += (string) o; }, message);
        }

        public void SetWaitState(bool isWaiting)
        {
            m_SyncContext.Post(o =>
            {
                bool waiting = (bool) o;
                Cursor = waiting ? Cursors.WaitCursor : Cursors.Default;
                btnExecute.Enabled = !waiting;
            }, isWaiting);
        }

        private void SetOutputTextFromWorker(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (txtOutput.InvokeRequired)
            {
                SetTextCallback d = SetOutputTextFromWorker;
                Invoke(d, text);
            }
            else
            {
                txtOutput.AppendText(text);
                txtOutput.AppendText(Environment.NewLine);
            }
        }


        delegate void SetTextCallback(string text);
    }
}