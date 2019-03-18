using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

public partial class SendHttp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public static string HttpPost(string url, string querystring, CookieContainer cookies)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = Encoding.UTF8.GetByteCount(querystring);
        request.CookieContainer = cookies;
        Stream requestStream = request.GetRequestStream();
        StreamWriter streamWriter = new StreamWriter(requestStream);
        streamWriter.Write(querystring);
        streamWriter.Close();

        request.CookieContainer = cookies;

        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        response.Cookies = cookies.GetCookies(response.ResponseUri);
        cookies.Add(response.Cookies);
        return GetResponseString(response);
    }

    public HttpWebResponse HttpResponseByPost(string url, IDictionary<string, string> parameters)
    {
        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";

        if (!(parameters == null || parameters.Count == 0))
        {
            StringBuilder buffer = new StringBuilder();
            bool first = true;
            foreach (string key in parameters.Keys)
            {
                if (!first)
                {
                    buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                }
                else
                {
                    buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    first = false;
                }
            }
            byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }
        return request.GetResponse() as HttpWebResponse;
    }

    public static HttpWebResponse HttpResponseByGet(string url)
    {
        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";
        return request.GetResponse() as HttpWebResponse;
    }

    public static string HttpGet(string url)
    {
        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        request.Method = "GET";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        return GetResponseString(response);
    }

    public static string GetResponseString(HttpWebResponse response)
    {
        string result = null;
        using (Stream stream = response.GetResponseStream())
        {
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
        }
        return result;
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("id", "4");
        TextBox1.Text = GetResponseString(HttpResponseByPost("http://127.0.0.1/proxy/Log.aspx", dic));
        //get请求并调用

        TextBox2.Text = GetResponseString(HttpResponseByGet("http://127.0.0.1/proxy/Log.aspx?id=000"));
    }
}