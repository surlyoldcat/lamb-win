using System;
using System.Windows.Forms;
using Amazon;
using Amazon.Runtime;

namespace Lamb
{
    static class Program
    {
        private static AWSCredentials s_Credentials;
        private static RegionEndpoint s_Region;

        /// <summary>
        ///     The main entry point for the application.
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
                s_Region = credFrm.AwsRegion;
                credFrm.Close();
                LambMainForm mainFrm = new LambMainForm();
                mainFrm.UserAwsCredentials = s_Credentials;
                mainFrm.AwsRegion = s_Region;
                mainFrm.ShowDialog();
            }
        }
    }
}