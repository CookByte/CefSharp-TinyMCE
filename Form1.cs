using CefSharp.WinForms;
using System.Windows.Forms;
using System.IO;
using CefSharp;
using System;
using System.Net.Http;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitBrowser();
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // 设置窗体居中
        }
        public ChromiumWebBrowser browser;
        public void InitBrowser()
        {
            CefSettings settings = new CefSettings();
            CefSharp.Cef.Initialize(settings);

            // 指定html位置
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\@tinymce\index.html";  // 这里注意2个parent搭配获取目录的方法，index.html是放在项目sln文件同目录内@tiny_mce文件夹里面的。
            // string path = Path.GetFullPath("D:\\GitHub\\WindowsFormsApp4\\@tinymce\\index.html");
            string url = "file://" + path;
            // MessageBox.Show(url);
            browser = new ChromiumWebBrowser(url.ToString());

            browser.Dock = DockStyle.Fill;  // 铺满
            this.Controls.Add(browser);

            // 注册JsObj对象，实现JS调用C#
            CefSharpSettings.WcfEnabled = true;
            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;  // 允许调用JS函数调用后端代码
            browser.JavascriptObjectRepository.Register("JsObj", new getWinFormData(browser, this), isAsync: false, options: BindingOptions.DefaultBinder);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CefSharp.Cef.Shutdown();
        }

        // 自定义类（JS调用C#）
        public class getWinFormData
        {
            private static ChromiumWebBrowser chromiumWebBrowser;
            private static Form1 form1;

            private int myId = 2;
            private String text_str = "首发知乎";
            private String startTime = "2023-05-10";

            // 构造方法
            public getWinFormData(ChromiumWebBrowser OriginachromiumWebBrowser, Form1 Originaform1)
            {
                chromiumWebBrowser = OriginachromiumWebBrowser;
                form1 = Originaform1;
            }
            // 窗口加载完毕时需要出发的全局JS对象
            public void OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
            {
                if (e.Frame.IsMain)
                {
                    chromiumWebBrowser.ExecuteScriptAsync(@"document.body.onmouseup = function()
                    {
                        JsObj.onSelected(window.getSelection().toString());
                    }");
                }
            }
            public void showMsg(string msg)
            {
                //String Content = msg;  // 后台接收前端提供的值，可以进行其它处理，比如本地存储；
                MessageBox.Show("C#弹窗：" + msg);
            }
            public string readMessage()
            {
                return myId +"；"+ text_str + "；" + startTime;
            }
            public int getId()
            {
                return myId;
            }
        }

        // winform窗体大小变化时，执行js代码更新前端（实时更新）
        void Form1_SizeChanged(object sender, EventArgs e)
        {
            int formHeight = this.ClientSize.Height;
            string jsCode =
            $"document.querySelector('#idcardmsg').value = {formHeight};"
            + $"tinymce.activeEditor.editorContainer.style.height = {formHeight - 300} + 'px'"  // 减去200是为了让底部的“提交”按钮在合适位置；公式可以按需调整。
            ;
            browser.ExecuteScriptAsync(jsCode);
        }

    }
}
