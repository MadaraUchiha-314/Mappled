using System;
using System.Net; 
using System.Collections.Generic;
using System.IO;
using System.Threading; 
using System.Windows.Forms;

class MainForm:Form 
{
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem openDirectoryToolStripMenuItem;
    private Label label1;
    private TextBox textBox1;
    private Label label2;
    private TextBox textBox2;
    private Button button1;
    private ToolStripMenuItem closeToolStripMenuItem;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private ProgressBar progressBar1;
    public static string FolderPath = @"C:\Users\Pratik\Desktop\Moodle\";
    public static string ApplicationPath = Application.UserAppDataPath.ToString(); 
    public static string DataBasePath = ApplicationPath + @"\Mapled\DataBase" ; 
    public static string UsernamePath = DataBasePath + @"\" + "username.txt" ; 
    public static string PasswordPath = DataBasePath + @"\" + "password.txt" ;

    public MainForm()
    {
        InitializeComponent();
       // System.Diagnostics.Process.Start(DataBasePath); 
        if (!Directory.Exists(DataBasePath))
        {
            Directory.CreateDirectory(DataBasePath);
        }
        if (File.Exists(UsernamePath))
        {
            textBox1.Text = File.ReadAllText(UsernamePath);
        }
        if(File.Exists(PasswordPath))
        {
            textBox2.Text = File.ReadAllLines(PasswordPath)[0] ;
        }
        this.MaximizeBox = false; 
    }

    private void InitializeComponent()
    {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDirectoryToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openDirectoryToolStripMenuItem
            // 
            this.openDirectoryToolStripMenuItem.Name = "openDirectoryToolStripMenuItem";
            this.openDirectoryToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.openDirectoryToolStripMenuItem.Text = "Open Downloads Directory";
            this.openDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openDirectoryToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 71);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(137, 112);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.UseSystemPasswordChar = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 27);
            this.button1.TabIndex = 5;
            this.button1.Text = "Download";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 245);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(284, 17);
            this.progressBar1.TabIndex = 6;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Mapled ";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start(Test.FolderPath); 
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit(); 
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            backgroundWorker1.RunWorkerAsync();
        }
        catch (InvalidOperationException)
        {
            MessageBox.Show("A current download is in progress, please wait for it to finish!", "Download in progress", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
        string username = textBox1.Text;
        string password = textBox2.Text;
        try
        {
            using (StreamWriter SW = new StreamWriter(UsernamePath, false))
            {
                SW.WriteLine(username);
            }
            using (StreamWriter SW = new StreamWriter(PasswordPath, false))
            {
                SW.WriteLine(password);
            }
            Web.Login(username, password); 
            using (StreamWriter SW = new StreamWriter(UsernamePath, false))
            {
                SW.WriteLine(username);
            }
            using (StreamWriter SW = new StreamWriter(PasswordPath, false))
            {
                SW.WriteLine(password);
            }
            for (int i = 0; i < Kernel.Courses.Length; i++)
            {
                backgroundWorker1.ReportProgress((int)(i + 1 + 0.0 / Kernel.Courses.Length) * 10);
                string url = Kernel.HomePageSearch(new Uri("http://10.1.1.242/moodle/my/"), Kernel.Courses[i]);
                if (url != null)
                {
                    //Console.WriteLine("The url is {0}", url); 
                    List<string> filesReturned = Kernel.FileSearch(new Uri(url));
                    List<string> folderReturned = Kernel.FolderSearch(new Uri(url));
                    List<string> resourcesReturned = Kernel.ResourceSearch(new Uri(url));
                    if (!Directory.Exists(FolderPath + Kernel.Courses[i]))
                    {
                        Directory.CreateDirectory(FolderPath + Kernel.Courses[i]);
                    }
                    foreach (string path in filesReturned)
                    {
                        Web.DownloadLink(new Uri(path), FolderPath + Kernel.Courses[i]);
                    }
                    foreach (string folders in folderReturned)
                    {
                        List<string> newFiles = Kernel.FileSearch(new Uri(folders));
                        List<string> newResource = Kernel.ResourceSearch(new Uri(folders));
                        string folderName = Web.GetFolderName(new Uri(folders)).Replace(":", " "); 
                        foreach (string files in newFiles)
                        {
                            Web.DownloadLink(new Uri(files), FolderPath + Kernel.Courses[i] + @"\" + folderName); 
                        }
                        foreach (string resource in newResource)
                        {
                            List<string> tempFile = Kernel.FileSearch(new Uri(resource));
                            foreach (string files in tempFile)
                            {

                                Web.DownloadLink(new Uri(files), FolderPath + Kernel.Courses[i] + folderName);
                            }
                        }
                    }
                    foreach (string path in resourcesReturned)
                    {
                        // Console.WriteLine("Inside resource for loop"); 
                        //Console.WriteLine("PAth is {0}", path);
                        HttpWebRequest request = HttpWebRequest.Create(path) as HttpWebRequest;
                        request.Proxy = null;
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.AllowAutoRedirect = true;
                        request.CookieContainer = Web.CC;
                        try
                        {
                            var response = request.GetResponse();
                            if (response.ContentType == "text/html; charset=utf-8")
                            {
                                List<string> tempFile = Kernel.FileSearch(new Uri(path));
                                response.Close(); 
                                foreach (string val in tempFile)
                                {
                                    Web.DownloadLink(new Uri(val), FolderPath + Kernel.Courses[i]);
                                }
                            }
                            else
                            {
                                response.Close(); 
                                Web.DownloadLink(new Uri(path), FolderPath + Kernel.Courses[i]);
                            }
                        }
                        catch (WebException ex)
                        {
                            Console.WriteLine("Exception has occurred {0}", ex.Status.ToString());
                            Console.WriteLine(ex.Status.ToString());
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine(ex.Status.ToString());
            if (ex.Status == WebExceptionStatus.ConnectFailure)
            {
                MessageBox.Show("Connecting failure!. Please check your internet connection and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ex.Status == WebExceptionStatus.Success)
            {
                Console.WriteLine("Login succesfull..");
            }
            else if (ex.Status == WebExceptionStatus.Timeout)
            {
                MessageBox.Show("Request timed out! Please check your internet connection and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ex.Status == WebExceptionStatus.UnknownError)
            {
                Console.WriteLine("Unknown error has occurred..");
                MessageBox.Show("Unknown error has occured! Please check your internet connection and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
            }
        }
    }

    private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
    {
        progressBar1.Value = e.ProgressPercentage; 
    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
        progressBar1.Value = 100;
        progressBar1.Update();
        Console.WriteLine("Download succesfull..");
        Application.Exit();
    }

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            button1.PerformClick();
        }
    }
}

