namespace ChordFactory.OpenSilver.repositories
{
    using System;
    using System.Windows;
    using CSHTML5;

    public static class BrowserInfo
    {
        public static bool DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer()
        {
            //-----------------------------------------------------------
            // When running inside Internet Explorer or Edge, the HTML5
            // Storage API is available only if the URL starts with http
            // or https. This method will display a message to the user
            // to inform her about this.
            //-----------------------------------------------------------
            if (CSharpXamlForHtml5.Environment.IsRunningInJavaScript)
            {
                //Execute a piece of JavaScript code:
                if (IsRunningFromLocalFileSystemOnInternetExplorer())
                {
                    MessageBox.Show("The local storage - used to persist data - is not available on Internet Explorer or Edge when running the website from the local file system (ie. the URL starts with 'c:\' or 'file:///'). To solve the problem, please run the website from a web server instead (ie. the URL must start with 'http://' or 'https://') or test the local storage using a different browser.");
                    return true;
                }
            }
            return false;
        }

        public static bool IsRunningFromLocalFileSystemOnInternetExplorer()
        {
            return Convert.ToBoolean(Interop.ExecuteJavaScript(@"window.IE_VERSION && document.location.protocol === ""file:"""));
        }
    }
}