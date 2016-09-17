using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon.Runtime;

namespace Lamb
{
    static class Program
    {
        private static AWSCredentials s_Credentials = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new CredentialsForm());
            CredentialsForm credFrm = new CredentialsForm();
            var credentialResult = credFrm.ShowDialog();

            
            if (credentialResult == DialogResult.OK
                && null != credFrm.UserAwsCredentials)
            {
                s_Credentials = credFrm.UserAwsCredentials;
                credFrm.Close();
                LambMainForm mainFrm = new LambMainForm();
                mainFrm.UserAwsCredentials = credFrm.UserAwsCredentials;
                mainFrm.ShowDialog();
            }
        }
    }
}
