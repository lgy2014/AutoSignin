using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoSignin.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args==null || args.Length<1)
            {
                System.Console.WriteLine("请指定日期参数，日期格式为：yyyy/m/d");
                return;
            }

            string date = args[0];
            bool b = Regex.IsMatch(date,"^\\d{4}/\\d{1,2}/\\d{1,2}$");
            if (!b)
            {
                System.Console.WriteLine("日期格式不正确。");
                return;
            }
            //ddlVacatType=13
            //            HolidayType = rabByTime
            //txtBegDay = 2017 - 06 - 25 09:00
            //txtEndDay = 2017 - 06 - 25 18:37
            //txtExplain = IT要求作息时间同业务
            //txtVacateExplain = 按整日：可以提交按整天的请假申请;
            //            按时段：可以提交一天之内按时段的请假申请;
            //            如果存在按整日和按时段连续请假的情况，请分别提交;
            //            txtVacateDetailExplain = 正常考勤
            //lbSelectingPerson = 724
            //lbSelectedPerson = 724
            string formData = "ddlVacatType=13&HolidayType=rabByTime&txtBegDay=" + date + " 9:00&txtEndDay=" + date + " 18:30&txtExplain=IT要求作息时间同业务&txtVacateDetailExplain=正常考勤&lbSelectedPerson=724&txtApplyForNum=5411&__VIEWSTATEGENERATOR=122D8F06&__EVENTTARGET=&__EVENTARGUMENT=&__LASTFOCUS=";//&txtAapplyForName=刘光印


            string viewstate = "&__VIEWSTATE=/wEPDwULLTE2NjU1NjgyNzQPZBYCAgMPFgIeB2VuY3R5cGUFE211bHRpcGFydC9mb3JtLWRhdGEWDgIFDw8WBB4EVGV4dAUENTQxMR4HRW5hYmxlZGhkZAIJDw8WBB8BBQnliJjlhYnljbAfAmhkZAINDxAPFgYeDURhdGFUZXh0RmllbGQFD0hvbGlkYXlUeXBlTmFtZR4ORGF0YVZhbHVlRmllbGQFC0hvbGlkYXlUeXBlHgtfIURhdGFCb3VuZGdkEBUOCeivt+mAieaLqQbkuovlgYcG55eF5YGHDOW4puiWquW5tOWBhwznlJ/ogrLkuqflgYcJ6Zmq5Lqn5YGHDOWboOWFrOWkluWHugblqZrlgYcG5Lin5YGHCea1geS6p+WBhwzlk7rkubPml7bpl7QT5Lqn5YmN5Z+56K6tL+ajgOafpQzoioLogrLmiYvmnK8M5q2j5bi46ICD5YukFQ4BMAExATIBMwE0ATUBNgE3ATgBOQIxMAIxMQIxMgIxMxQrAw5nZ2dnZ2dnZ2dnZ2dnZxYBAg1kAiMPDxYCHwEFvQHmjInmlbTml6XvvJrlj6/ku6Xmj5DkuqTmjInmlbTlpKnnmoTor7flgYfnlLPor7c7DQrmjInml7bmrrXvvJrlj6/ku6Xmj5DkuqTkuIDlpKnkuYvlhoXmjInml7bmrrXnmoTor7flgYfnlLPor7c7DQrlpoLmnpzlrZjlnKjmjInmlbTml6XlkozmjInml7bmrrXov57nu63or7flgYfnmoTmg4XlhrXvvIzor7fliIbliKvmj5DkuqQ7DQpkZAIlDw8WAh8BBQzmraPluLjogIPli6RkZAIrDxAPFgYfBAUGdXNlcmlkHwMFCFVzZXJpbmZvHwVnZBAVKhvpmL/kuI3pg73igKLogonoi48vNDgyOC9hYmQT5p+P5YawLzAxOTAvYmFpYmluZxTmm7nmnajlhokvMTAyNy9jYW95chPpq5jpuY8vMDIxNi9nYW9wZW5nFOmrmOWWnOmYsy8wNzM4L2dhb3h5FOmfqea9h+mijS8yODE1L2hhbnh5FOmfqeaMr+axny8wMDAxL2hhbnpqFOiDoeaZuuWLhy8xMzQxL2h1enlhFum7hOayp+mSvy8wMjk5L2h1YW5nY2QW6buE5riF6I2jLzAzMDMvaHVhbmdxchfpu4TmmZPok4kvODAwMC9odWFuZ3hyYRTpnI3lv5fmsJEvMDk5MC9odW96bRPmnY7lm73ls7AvMDAwNi9saWdmFeadjumTrmEvMDc5OC9saXpoZW5nYRPliJjoi7EvMTgxNS9saXV5aW5nE+i3r+W7uua1qS8wMTEyL2x1amgT6IuX5p6XLzM5MTQvbWlhb2xpbhXkuZTmsLjlt50vMzkwNS9xaWFveWMV6YK15o235LqsLzI4MzUvc2hhb2pqE+efs+WugS8zNDc0L3NoaW5pbmcU5a2Z5q+F6auYLzI5NTMvc3VueWcU5a2Z5rC45pe6LzAyNjAvc3VueXcT546L5Y2aLzMzODAvd2FuZ2JvYxXnjovkuLnov6ovMjYwMy93YW5nZGQW546L57uP5LyfLzA3MTkvd2FuZ2p3YRbnjovmmZPpvpkvMzkwNi93YW5neGxjEueOi+aXrS8yNjI5L3dhbmd4dRPlkLTngq/nuqIvMTcxMi93dWpoEeWQtOW3jS8wODY3L3d1d2VpE+WQtOWFhueQqi8wNjcwL3d1enEU6YKi57qiLzEzMDIveGluZ2hvbmcU6Zer55KQ55G2LzIwODYveWFubHkU5p2o5rSLLzI4NDAveWFuZ3lhbmcT5byg6aqcLzE3NjgvemhhbmdhbxTlvKDno4ovMDAwNC96aGFuZ2xlaRjlvKDlipvmlodhLzI0MTYvemhhbmdsd2EW5byg5LiW6ZGrLzA2MTIvemhhbmdzeBblvKDnjq5hLzA3OTAvemhhbmd3ZWlhF+W8oOS5ieWfuS8xMjkyL3poYW5neXBhFuW8oOWLhy8wMTE2L3poYW5neW9uZ2cW5byg5Y2T6IiqLzI0MjEvemhhbmd6aBXotbXmlofojIIvMDgwNS96aGFvd20VKgQ0NDgyAzY0MwM4MjADNjQ2AzcyNAQyNjEyAzYxOAQxMDgzAzY1NAM2NTYEMzQ5NgM4MDUDNjIxAzc0MwQxNTc3AzYzOAQzNjk2BDM2ODcEMjYzMQQzMjY0BDI3NTEDNjUwBDMxNzAEMjM5NQM3MjMEMzY5MgQyNDIxBDE0NzUDNzYzAzcxMgQxMDU1BDE4NDYEMjYzOAQxNTMxAzYyMAQyMTk1AzY5NAM3NDEEMTA0NwM2MzkEMjIwMAM3NDcUKwMqZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZGQCMQ8QZA8WAWYWARAFFOmrmOWWnOmYsy8wNzM4L2dhb3h5BQM3MjRnZGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgUFCHJhYkJ5RGF5BQhyYWJCeURheQUJcmFiQnlUaW1lBRFsYlNlbGVjdGluZ1BlcnNvbgUQbGJTZWxlY3RlZFBlcnNvbnzAvcFZ4CBveDoTDiw4Il+hLQCUnr/M3YDt2yu/jzsk";
            string url = "http://kaoqin.litsoft.com.cn/WebForms/ApplyForManage.aspx?otype=C";
            //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            client.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
            client.Headers.Add(HttpRequestHeader.LastModified, "zh-CN,zh;q=0.8");

            client.Headers.Add(HttpRequestHeader.Cookie, "qqmail_alias=liugy@litsoft.com.cn; ASP.NET_SessionId=3fmdplajg1lyxhq0bw3mufzp");//3fmdplajg1lyxhq0bw3mufzp
            
            byte[] data = Encoding.UTF8.GetBytes(formData+viewstate);
            byte[] data2 = client.UploadData(url,data);
            string ret = Encoding.UTF8.GetString(data2);

            client.Dispose();
            System.Console.WriteLine(ret);

        }
    }
}
