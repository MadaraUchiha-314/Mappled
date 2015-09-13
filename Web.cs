using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using System.IO; 

class Web
{
    public static CookieContainer CC;
    public static Uri UriObj;
    public static Uri NewUriObj;
    public static int Count = 0  ; 

    public static void OpenFile(Uri uriObj, string path)
    {
        HttpWebRequest request = HttpWebRequest.Create(uriObj) as HttpWebRequest;
        request.Proxy = null;
        request.ContentType = "application/x-www-form-urlencoded";
        request.CookieContainer = CC;
        request.AllowAutoRedirect = true;
        try
        {
            var response = request.GetResponse();
            Stream SR = response.GetResponseStream();
            SaveStreamToFile(SR, path);
            response.Close(); 
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception occurred in the method");
            Console.WriteLine(ex.Message);
        }

    }

    public static void SaveStreamToFile(Stream stream, string filename)
    {
        using (var fileStream = File.Create(filename))
        {
            stream.CopyTo(fileStream);
        }
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
        request.Timeout = 3000; 
        request.ContentType = "application/x-www-form-urlencoded";
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
                    string html = SR.ReadToEnd();
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
        string tempName = null; 
        CookieAwareWebClient client = new CookieAwareWebClient();
        client.Proxy = null;
        client.CookieContainer = CC;
        if (!Directory.Exists(absPath))
        {
            Directory.CreateDirectory(absPath);
        }
        //Console.WriteLine("File Saved:{0}, Location: {1} ", name, absPath);
        try
        {
            //client.DownloadFile(uriObj, absPath + @"\" + name);
            HttpWebRequest request = HttpWebRequest.Create(uriObj) as HttpWebRequest;
            request.Proxy = null;
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = CC;
            request.AllowAutoRedirect = true; 
            //request.Timeout = 10000;
            //Console.WriteLine("File downloading Saved:{0}, Location: {1} ", uriObj.AbsoluteUri, absPath + @"\" + name);
           // Console.WriteLine("Awaiting response in the download function..."); 
            var response = request.GetResponse();
            string[] tempArray = response.ResponseUri.Segments;
            tempName = tempArray[tempArray.Length - 1];
           // Console.WriteLine("The response type is {0}", response.ContentType);
           // Console.WriteLine("The response url is {0}", response.ResponseUri.AbsoluteUri);
            Stream SR = response.GetResponseStream();
            SaveStreamToFile(SR, absPath + @"\" + tempName);
            
        }
        catch(WebException ex)
        {
            Console.WriteLine(ex.InnerException);
            Console.WriteLine(ex.Response); 
        }
    }
    public static string GetFolderName(Uri uriObj)
    {
        CookieAwareWebClient client = new CookieAwareWebClient();
        client.CookieContainer = CC;
        client.Proxy = null;
        string html = client.DownloadString(uriObj);
        Regex regex = new Regex("<h2 class=\"main\">(.*)</h2>");
        var v = regex.Match(html);
        return v.Groups[1].ToString();
    }
}

class CookieAwareWebClient : WebClient
{
    public CookieAwareWebClient()
        : this(new CookieContainer())
    { }
    public CookieAwareWebClient(CookieContainer c)
    {
        this.CookieContainer = c;
    }
    public CookieContainer CookieContainer { get; set; }

    protected override WebRequest GetWebRequest(Uri address)
    {
        WebRequest request = base.GetWebRequest(address);
        if (request is HttpWebRequest)
        {
            (request as HttpWebRequest).CookieContainer = this.CookieContainer;
        }
        return request;
    }
}