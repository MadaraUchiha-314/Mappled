using System;
using System.Collections.Generic; 
using Parser;
using System.IO;
using System.Net;
using System.Windows.Forms; 

class Test 
{
    public static string FolderPath = @"C:\Users\Pratik\Desktop\Moodle\";

    public static void Main()
    {
        //Directory.CreateDirectory(FolderPath); 
        //Console.WriteLine("Welcomt to Mappled");
        //Console.Write("Please enter your username: ");
        //string username = Console.ReadLine();
        //Console.Write(Environment.NewLine + "Please enter your password: ");
        //string password = Console.ReadLine();
        //Console.WriteLine(Environment.NewLine + "Checking your credentials...."
        
        Application.EnableVisualStyles();
        Application.Run(new MainForm());  
    }
}