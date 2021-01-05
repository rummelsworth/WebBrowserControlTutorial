using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async Task InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += ReceiveMessage;
            await webView21.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.addEventListener(\'message\', event => alert(event.data));");
        }

        private void ReceiveMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            var message = args.TryGetWebMessageAsString();
            Test(message);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            const string HTML_CONTENT =
@"
<html>

<body>
    <button onclick=""window.chrome.webview.postMessage('called from script code')"">call client code from script code</button>
</body>

</html>
";

            await InitializeAsync();
            webView21.NavigateToString(HTML_CONTENT);
        }

        public void Test(string message) => MessageBox.Show(message, "client code");

        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.PostWebMessageAsString("called from client code");
        }
    }
}
