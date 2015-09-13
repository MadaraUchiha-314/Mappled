using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

class Web
{
    public static CookieContainer CC;
    public static Uri UriObj;
    public static Uri NewUriObj;
    public static int Count = 0; 
    public static void OpenFile(Uri uriObj, string path)
    {
        HttpWebRequest request = HttpWebRequest.Create(uriObj) as HttpWebRequest;
        request.Proxy = null;
        request.ContentType = "application/msword";
        request.CookieContainer = CC;
        request.AllowAutoRedirect = true;
        try
        {
            var response = request.GetResponse();
            Stream SR = response.GetResponseStream();
            SaveStreamToFile(SR, path);
        }
        catch(Exception ex )
        {
            Console.WriteLine("Exception occurred in the method");
            Console.WriteLine(ex.Message); 
        }

    }

    private static void SaveStreamToFile(Stream stream, string filename)
    {
        using (Stream destination = File.Create(filename))
            Write(stream, destination);
    }
    private static void Write(Stream from, Stream to)
    {
        for (int a = from.ReadByte(); a != -1; a = from.ReadByte())
            to.WriteByte((byte)a);
    }

    public static bool Login(string username, string password)
    {
        CC = new CookieContainer();
        UriObj = new Uri("http://10.1.1.242/moodle/login/index.php");
        NewUriObj = new Uri("http://10.1.1.242/moodle/my/"); 
        HttpWebRequest request = WebRequest.Create(UriObj) as HttpWebRequest;
        request.Proxy = null;
        request.Method = "POST";
        request.CookieContainer = CC;
       // request.ContentType = "application/x-www-form-urlencoded";
        string reqString = "username=" + username + "&password=" + password;
        byte[] encArray = System.Text.Encoding.UTF8.GetBytes(reqString);
        using (Stream reqStream = request.GetRequestStream())
        {
            reqStream.Write(encArray, 0, encArray.Length);
        }
        using (WebResponse res = request.GetResponse())
        {
            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader SR = new StreamReader(stream))
                {
                    string  html = SR.ReadToEnd();
                   // Console.WriteLine(html);
                    StreamWriter SW = new StreamWriter(@"C:\Users\Pratik\Desktop\temp.html");
                    SW.WriteLine(html);
                    SW.Close();
                }
            }
        }
        return true;
    }

    public static string OpenLink(Uri uriObj)
    {
        string html; 
        CookieAwareWebClient client = new CookieAwareWebClient();
        client.Proxy = null;
        client.CookieContainer = CC;
        html = client.DownloadString(uriObj);
        return html; 
    }
    public static void DownloadLink(Uri uriObj, string absPath)
    {
        string[] nameArr = uriObj.Segments;
        string name = nameArr[nameArr.Length - 1];
     //   Console.WriteLine(name); 
        //CookieAwareWebClient client = new CookieAwareWebClient();
        //client.Timeout = 2000; 
        //client.Proxy = null;
        //client.CookieContainer = CC; 
        try
        {
            //client.DownloadFileAsync(uriObj, absPath + @"\" + name);
            HttpWebRequest request = HttpWebRequest.Create(uriObj) as HttpWebRequest;
            request.Proxy = null;
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = CC;
            request.Timeout = 2000; 
            Console.WriteLine("File downloading Saved:{0}, Location: {1} ", uriObj.AbsoluteUri, absPath + @"\" + name);
            var response = request.GetResponse();
            Stream SR = response.GetResponseStream();
            SaveStreamToFile(SR, absPath + @"\" + name);
        }
        catch(WebException ex)
        {
            Console.WriteLine(ex.Status);
            if (ex.Status.ToString() == "Timeout")
            {
                Console.WriteLine("Time out processed"); 
                OpenFile(uriObj, absPath + @"\" + name);
            }
           // Console.WriteLine(ex.InnerException);
            //Console.WriteLine(ex.Response); 
        }
    }
}

class CookieAwareWebClient : WebClient
{
    public int Timeout { get; set; }

    public CookieAwareWebClient() : this(60000) { }

    public CookieAwareWebClient(int timeout)
    {
        this.Timeout = timeout;
    }
    public CookieContainer CookieContainer { get; set; }

    protected override WebRequest GetWebRequest(Uri address)
    {
        WebRequest request = base.GetWebRequest(address);
        if (request != null)
        {
            request.Timeout = this.Timeout;
        }
        if (request is HttpWebRequest)
        {
            (request as HttpWebRequest).CookieContainer = this.CookieContainer;
        }
        return request;
    }
}