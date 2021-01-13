using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public interface IForm1
    {
        void Test(string message);
    }

    [ComDefaultInterface(typeof(IForm1))]
    public partial class Form1 : Form, IForm1
    {
        private readonly WebBrowser webBrowser1 = new WebBrowser();

        public Form1()
        {
            InitializeComponent();

            webBrowser1.Dock = DockStyle.Fill;

            Controls.Add(webBrowser1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;

            webBrowser1.ObjectForScripting = this;

            webBrowser1.DocumentText =
@"
<html>

<head>
    <script>function test(message) { alert(message); }</script>
</head>

<body>
    <button onclick=""window.external.Test('called from script code')"">call client code from script code</button>
</body>

</html>
";
        }

        public void Test(string message) => MessageBox.Show(message, "client code");

        private void button1_Click(object sender, EventArgs e) => webBrowser1.Document.InvokeScript("test", new[] { "called from client code" });
    }
}
