using System;
using System.Threading.Tasks;
using System.Windows;
using Abp.Dependency;
using AutoSignin.People;
using AutoSignin.People.Dto;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace AutoSignin.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISingletonDependency
    {
        private readonly IPersonAppService _personAppService;
        System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();
        System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        //签卡列表
        string url_list = string.Empty;

        //申请签卡
        string url_req = string.Empty;

        string url_req_query = string.Empty;

        string webid = string.Empty;

        public string Url_req
        {
            get
            {
                //DateTime curTime = dpicker.SelectedDate ?? DateTime.Now;
                //return url_req+curTime.ToString("dd/MM/yyyy");
                return url_req;
            }
            private set
            {
                url_req = value;
            }
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        public MainWindow(IPersonAppService personAppService)
        {
            //_personAppService = personAppService;
            InitializeComponent();

            timer.Tick += Timer_Tick;
            timer.Interval = 8000;

            dpicker.SelectedDate = DateTime.Now;

            //read parameter
            StringBuilder param = new StringBuilder(99);
            IniHelper.GetPrivateProfileString("main", "url", "", param, 199, IniHelper.strFilePath);
            txtUrl.Text = param.ToString();
            param.Clear();
            IniHelper.GetPrivateProfileString("main", "cookie", "", param, 199, IniHelper.strFilePath);
            txtCookie.Text = param.ToString();

            //设置IE版本
            IEVersion.BrowserEmulationSet();

            web.ScriptErrorsSuppressed = true;
            host.Child = web;
            grid.Children.Add(host);

            host.SetValue(Grid.RowProperty, 1);
            host.SetValue(Grid.ColumnProperty, 2);

            InitialTray();
            Title += "----:" + Thread.CurrentThread.ManagedThreadId;
        }

        private void Web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private async void LoadAllPeopleButton_Click(object sender, RoutedEventArgs e)
        {
            //await LoadAllPeopleAsync();
        }

        private async Task LoadAllPeopleAsync()
        {
            //PeopleList.Items.Clear();
            //var result = await _personAppService.GetAllPeopleAsync();
            //foreach (var person in result.People)
            //{
            //    PeopleList.Items.Add(person.Name);
            //}
        }

        private async void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    await _personAppService.AddNewPerson(new AddNewPersonInput
            //    {
            //        Name = NameTextBox.Text
            //    });

            //    await LoadAllPeopleAsync();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            web.Navigate(url_list);
        }

        //签卡
        private void btnStartSignIn_Click(object sender, RoutedEventArgs e)
        {
            web.Navigate(Url_req);
            web.DocumentCompleted += Web_DocumentCompleted1;
        }

        private void Web_DocumentCompleted1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ApplyForm("9:00");
        }

        private void btnStartSignOut_Click(object sender, RoutedEventArgs e)
        {
            web.Navigate(Url_req);
            web.DocumentCompleted += Web_DocumentCompleted2;
        }

        private void Web_DocumentCompleted2(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ApplyForm("18:30");
        }

        private void ApplyForm(string time)
        {
            //HtmlElement Submit = web.Document.All["Submit"];
            //HtmlElement m_time = web.Document.All["m_time"];
            //HtmlElement reason = web.Document.All["reason"];
            //HtmlElement REMARK = web.Document.All["REMARK"];

            //if (Submit == null || m_time == null || reason == null || REMARK == null)
            //{
            //    return;
            //}

            //m_time.SetAttribute("value", time);
            //reason.SetAttribute("value", "16");
            //REMARK.SetAttribute("value", "IT要求作息时间同业务");

            //submit
            //Submit.InvokeMember("click");

            HtmlElement ddlVacatType = web.Document.All["ddlVacatType"];
            HtmlElement rabByTime = web.Document.All["rabByTime"];//rabByTime
            HtmlElement txtBegDay = web.Document.All["txtBegDay"];
            HtmlElement txtEndDay = web.Document.All["txtEndDay"];
            HtmlElement txtExplain = web.Document.All["txtExplain"];

            HtmlElement lbSelectingPerson = web.Document.All["lbSelectingPerson"];
            HtmlElement btnAdd = web.Document.All["btnAdd"];
            HtmlElement btnSave = web.Document.All["btnSave"];

            if (ddlVacatType==null || rabByTime==null || txtBegDay==null || txtEndDay==null || txtExplain==null || lbSelectingPerson==null || btnSave == null || btnAdd==null)
            {
                return;
            }

            ddlVacatType.SetAttribute("value","13");
            //rabByTime.SetAttribute("value", "rabByTime");
            rabByTime.InvokeMember("click");
            DateTime time2 = dpicker.SelectedDate ?? DateTime.Now;
            string time3 = time2.ToString("yyyy/M/d");
            txtBegDay.SetAttribute("value",time3+" 9:00");
            txtEndDay.SetAttribute("value", time3+" 18:30");
            txtExplain.SetAttribute("value", "IT要求作息时间同业务");

            //lbSelectingPerson.SetAttribute("value", "724");
            //btnAdd.InvokeMember("click");

            //btnSave.InvokeMember("click");

        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUrl.Text))
            {
                return;
            }

            //保存参数
            IniHelper.WritePrivateProfileString("main", "url", txtUrl.Text, IniHelper.strFilePath);
            IniHelper.WritePrivateProfileString("main", "cookie", txtCookie.Text, IniHelper.strFilePath);

            //cookie process
            //qqmail_alias=liugy@litsoft.com.cn; ASP.NET_SessionId=arivphmjtn1slizruugty0jz; 5411=3BC71FAEBE42E1639EB6FDED38D714CD
            Dictionary<string, string> cookies = ResolveCooke(txtCookie.Text);
            if (cookies.Count<1)
            {
                return;
            }
            //设置cookie
            foreach (var item in cookies)
            {
                InternetSetCookie(txtUrl.Text, item.Key, item.Value);
            }

            Match match = Regex.Match(txtUrl.Text.TrimEnd(), "\\d+$");
            webid = match.Value;



            if (!string.IsNullOrEmpty(webid))
            {
                //url_list = "http://ehr.litsoft.com.cn/scripts/mgrqispi.dll?appname=HRsoft2000&prgname=ATT_DETAIL_STF&arguments=-AS" + webid;
                //Url_req = "http://ehr.litsoft.com.cn/scripts/mgrqispi.dll?Appname=HRsoft2000&Prgname=ATT_MAKEUP_REC&ARGUMENTS=-AS"+webid+",-A";
                //url_req_query = "http://ehr.litsoft.com.cn/scripts/mgrqispi.dll?appname=hrsoft2000&prgname=ATT_MAKEUP_REC_QUERY&arguments=-AS"+webid;
                
            }
            url_list = "http://kaoqin.litsoft.com.cn/index.htm";
            Url_req = "http://kaoqin.litsoft.com.cn/WebForms/ApplyForManage.aspx?otype=C";
            url_req_query = "http://kaoqin.litsoft.com.cn/WebForms/ApplyforList.aspx";

            web.Navigate(txtUrl.Text);
            web.DocumentCompleted += Web_DocumentCompleted3;
        }

        private void Web_DocumentCompleted3(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private Dictionary<string, string> ResolveCooke(string cookie)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] items = cookie.Split(';');
            foreach (var item in items)
            {
                string[] s = item.Split('=');
                dict.Add(s[0].Trim(), s[1].Trim());
            }

            return dict;
        }

        private void btnReqQuery_Click(object sender, RoutedEventArgs e)
        {
            web.Navigate(url_req_query);
        }

        private void chk_Checked(object sender, RoutedEventArgs e)
        {
            chkShutdown.IsChecked = true;
            timer.Start();
        }

        Random random = new Random();//Timer_Tick调用
        private void Timer_Tick(object sender, EventArgs e)
        {
            
            ThreadPool.QueueUserWorkItem(a=> {
                timer.Stop();

                DateTime targetTime = DateTime.Parse("18:31");
                
                if (DateTime.Now.CompareTo(targetTime) > 0)
                {
                    int add = random.Next(3, 9);
                    Thread.Sleep(add * 60000);

                    Dispatcher.Invoke(() => {
                        btnGo_Click(null, null);
                    });

                    Thread.Sleep(random.Next(20, 70)*1000);

                    Dispatcher.Invoke(() => {
                        btnStartSignIn_Click(null, null);
                    });

                    Thread.Sleep(random.Next(20, 70)*1000);

                    Dispatcher.Invoke(() => {
                        btnStartSignOut_Click(null, null);
                        if (chkShutdown.IsChecked==true)
                        {
                            Process.Start("shutdown.exe", "-s -t 5");
                        }
                    });
                }
                timer.Start();
            });

            
        }

        private void chk_Unchecked(object sender, RoutedEventArgs e)
        {
            chkShutdown.IsChecked = false;
            timer.Stop();
        }

        #region 托盘设置
        private System.Windows.Forms.NotifyIcon notifyIcon = null;
        private void InitialTray()
        {
            //设置托盘的各个属性
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.BalloonTipText = "程序开始运行";
            notifyIcon.Text ="托盘图标";
            notifyIcon.Icon = new System.Drawing.Icon(System.Windows.Forms.Application.StartupPath + "\\bear_polar.ico");
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(2000);
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            //设置菜单项
            System.Windows.Forms.MenuItem menu1 = new System.Windows.Forms.MenuItem("菜单项1");
            System.Windows.Forms.MenuItem menu2 = new System.Windows.Forms.MenuItem("菜单项2");
            System.Windows.Forms.MenuItem menu = new System.Windows.Forms.MenuItem("菜单", new System.Windows.Forms.MenuItem[] { menu1 , menu2 });

            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("exit");
            exit.Click += new EventHandler(exit_Click);

            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { menu, exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            //窗体状态改变时候触发
            this.StateChanged += new EventHandler(SysTray_StateChanged);
        }
        /// <summary>
        /// 窗体状态改变时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SysTray_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 退出选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, EventArgs e)
        {
            if (System.Windows.MessageBox.Show("确定要关闭吗?","退出",MessageBoxButton.YesNo,MessageBoxImage.Question,MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                notifyIcon.Dispose();
                System.Windows.Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// 鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
        }
        #endregion
    }
}
