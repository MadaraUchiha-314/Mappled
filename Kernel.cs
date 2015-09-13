using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Parser;
class Kernel
{
    public static List<string> FolderList = new List<string>(); 
    public static  string usefulUrl(string contents)
    {
        //Console.WriteLine("The contents passed are {0}", contents)  ; 
        int position = contents.IndexOf("_");
        if (position < 0)
        {

            //Console.WriteLine("Required string is not found");
            return null;
        }
        else
        {
            string substring = contents.Substring(position + 1);
            FolderList.Add(substring);
            return substring;
        }
    }

   public static string[] Courses = {"CS F111", "EEE F111", "MATH F112", "MATH F113", "CHEM F110","BITS F111", "BITS F110"};
    //public static string[] Courses = { "BITS F110" }; 
    public static string HomePageSearch(Uri uriObj1, string pattern)
    {
        HtmlTag tag;

        CookieAwareWebClient client = new CookieAwareWebClient();

        client.CookieContainer = Web.CC;

        string html = client.DownloadString(uriObj1);

        HtmlParser parse = new HtmlParser(html);

        string returnedValue = "" ; 

        while (parse.ParseNext("a", out tag))
        {
            // See if this anchor links to us
            string value1, storeurl1, value2, storeurl2;
            storeurl1 = ""; storeurl2 = "";
            value1 = ""; 
            value2 = ""; 
            if (tag.Attributes.TryGetValue("href", out value1))
            {
                // value contains URL referenced by this link
                //Console.WriteLine(value);
                storeurl1 = value1;
                value1 = ""; 
            }
            if (tag.Attributes.TryGetValue("title", out value2))
            {
                // value contains URL referenced by this link
                //Console.WriteLine(value);
                storeurl2 = value2;
                returnedValue = usefulUrl(storeurl2);
                Console.WriteLine(returnedValue);
            }

                if ((returnedValue == pattern)&&(storeurl1!="http://10.1.1.242/moodle/course/index.php"))
                {
                    return storeurl1; 
                }
            

        }
        return null;  // debug problem . 
    }
    // End Of Function Home PAge Search

     public static List<string> FolderSearch(Uri uriObj1)
    {
        List<string> returnString = new List<string>();
        HtmlTag tag;
        CookieAwareWebClient client = new CookieAwareWebClient();
        client.CookieContainer = Web.CC;
        string html = client.DownloadString(uriObj1);
        HtmlParser parse = new HtmlParser(html);
        string returnedValue = "";
        string value1;
        while (parse.ParseNext("a", out tag))
        {
            if (tag.Attributes.TryGetValue("href", out value1))
            {
                returnedValue = value1;
                if((searchKeyword(returnedValue, "http://10.1.1.242/moodle/mod/folder/")&&(returnedValue!="http://10.1.1.242/moodle/course/index.php")))
                {
                    returnString.Add(returnedValue);
                }
            }

        }
        return returnString;
    }
     public static List<string> FileSearch(Uri uriObj1)
     {
         List<string> returnString = new List<string>();
         HtmlTag tag;
         CookieAwareWebClient client = new CookieAwareWebClient();
         client.CookieContainer = Web.CC;
         string html = client.DownloadString(uriObj1);
         HtmlParser parse = new HtmlParser(html);
         string returnedValue = "";
         string value1;
         while (parse.ParseNext("a", out tag))
         {
             if (tag.Attributes.TryGetValue("href", out value1))
             {
                 returnedValue = value1;
                 if (searchKeyword(returnedValue, "http://10.1.1.242/moodle/") && searchKeyword(returnedValue, ".pdf"))
                 {
                     returnString.Add(returnedValue);
                 }
                 if (searchKeyword(returnedValue, "http://10.1.1.242/moodle/") && searchKeyword(returnedValue, ".ppt"))
                 {
                     returnString.Add(returnedValue);
                 }
                 if (searchKeyword(returnedValue, "http://10.1.1.242/moodle/") && searchKeyword(returnedValue, ".doc"))
                 {
                     returnString.Add(returnedValue);
                 }
                 if (searchKeyword(returnedValue, "http://10.1.1.242/moodle/") && searchKeyword(returnedValue, ".docx"))
                 {
                     returnString.Add(returnedValue);
                 }
                 if (searchKeyword(returnedValue, "http://10.1.1.242/moodle/") && searchKeyword(returnedValue, ".dwg"))
                 {
                     returnString.Add(returnedValue);
                 }
             }

             //Console.WriteLine("Exiting filesearch...........");
         }
         return returnString;
     }
     public static List<string> ResourceSearch(Uri uriObj1)
     {
         List<string> returnString = new List<string>();
         HtmlTag tag;
         CookieAwareWebClient client = new CookieAwareWebClient();
         client.CookieContainer = Web.CC;
         string html = client.DownloadString(uriObj1);
         HtmlParser parse = new HtmlParser(html);
         string returnedValue = "";
         string value1;
         while (parse.ParseNext("a", out tag))
         {
             if (tag.Attributes.TryGetValue("href", out value1))
             {
                 returnedValue = value1;
                 if (searchKeyword(returnedValue, "http://10.1.1.242/moodle/mod/resource/"))
                 {
                     returnString.Add(returnedValue);
                    // Console.WriteLine("Resource Output: ", returnedValue); 
                 }
             }
             //Console.WriteLine("Exiting resource search...........");
         }
         return returnString;

     }


     public static bool searchKeyword(string contents, string filter)
     {
         if (contents.Contains(filter))
         {
             return true;
         }
         return false;
     }

}
