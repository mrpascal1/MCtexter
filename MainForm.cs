using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdvancedNotepad_CSharp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static List<String> OpenedFilesList = new List<String> { };


        //***********************************************************************************
        //  ChangeTextOfReadyLabel() function to change text of ReadyLabel
        //***********************************************************************************
        public void ChangeTextOfReadyLabel(ToolStripMenuItem menuitem)
        {
            menuitem.MouseEnter += new EventHandler(this.menuitem_MouseEnter);
            menuitem.MouseLeave += new EventHandler(this.menuitem_MouseLeave);
        }
        private void menuitem_MouseEnter(object sender,EventArgs e)
        {
            Object b = (ToolStripMenuItem)sender;
            String s = b.ToString().Trim();
            switch(s)
            {
                case "File": AboutLabel.Text = "Create New,Open,Save,Close and Print Documents";
                    break;
                case "New": AboutLabel.Text = "Create New document";
                    break;
                case "Open": AboutLabel.Text = "Open New Document";
                    break;
                case "Save": AboutLabel.Text = "Save Current Document";
                    break;
                case "Save As": AboutLabel.Text = "Save As Current Document";
                    break;
                case "Save All": AboutLabel.Text = "Save All opened documents";
                    break;
                case "Close": AboutLabel.Text = "Close Current Document";
                    break;
                case "Close All": AboutLabel.Text = "Close All Opened Documents";
                    break;
                case "Open In System Editor": AboutLabel.Text = "Open current document in its system editor";
                    break;
                case "Print": AboutLabel.Text = "Print Current Document";
                    break;
                case "Print Preview": AboutLabel.Text = "Print Preview Current Document";
                    break;
                case "Exit": AboutLabel.Text = "Exit from Application";
                    break;

                case "Edit": AboutLabel.Text = "Cut,Copy,Paste,Undo,Redo,Find,Replace etc. in current document";
                    break;
                case "Cut": AboutLabel.Text = "Cut the selected text from current document";
                    break;
                case "Copy": AboutLabel.Text = "Copy the selected text from current document";
                    break;
                case "Paste": AboutLabel.Text = "Paste the text into current document";
                    break;
                case "Undo": AboutLabel.Text = "Perform Undo operation in current document";
                    break;
                case "Redo": AboutLabel.Text = "Perform Redo operation in current document";
                    break;
                case "Find": AboutLabel.Text = "Find a text in current document";
                    break;
                case "Replace": AboutLabel.Text = "Replace text in current document";
                    break;
                case "GoTo": AboutLabel.Text = "GoTo the specific line number in current document";
                    break;
                case "Select All": AboutLabel.Text = "Select all text in current document";
                    break;
                case "Change Case": AboutLabel.Text = "Change Upper,Lower and Sentence case of selected text";
                    break;
                case "Upper": AboutLabel.Text = "Change selected text case to Upper case";
                    break;
                case "Lower": AboutLabel.Text = "Change selected text case to Lower case";
                    break;
                case "Sentence": AboutLabel.Text = "Change selected text case to Sentence case";
                    break;
                case "Next Document": AboutLabel.Text = "Go to next document";
                    break;
                case "Previous Document": AboutLabel.Text = "Go to previous document";
                    break;

                case "View": AboutLabel.Text = "Set Font,Fore and Back color";
                    break;
                case "Font": AboutLabel.Text = "Set Font in current document";
                    break;
                case "Fore Color": AboutLabel.Text = "Set Fore Color in current document";
                    break;
                case "Back Color": AboutLabel.Text = "Set Back Color in current document";
                    break;
            }
        }
        private void menuitem_MouseLeave(object sender, EventArgs e)
        {
            AboutLabel.Text = "Ready";
        }

        public void UpdateReadyLabel()
        {
            ChangeTextOfReadyLabel(File_MenuItem);
            ChangeTextOfReadyLabel(File_New_MenuItem);
            ChangeTextOfReadyLabel(File_Open_MenuItem);
            ChangeTextOfReadyLabel(File_Save_MenuItem);
            ChangeTextOfReadyLabel(File_SaveAs_MenuItem);
            ChangeTextOfReadyLabel(File_SaveAll_MenuItem);
            ChangeTextOfReadyLabel(File_Close_MenuItem);
            ChangeTextOfReadyLabel(File_CloseAll_MenuItem);
            ChangeTextOfReadyLabel(File_OpenInSystemEditor_MenuItem);
            ChangeTextOfReadyLabel(File_Print_MenuItem);
            ChangeTextOfReadyLabel(File_PrintPreview_MenuItem);
            ChangeTextOfReadyLabel(File_Exit_MenuItem);

            ChangeTextOfReadyLabel(Edit_MenuItem);
            ChangeTextOfReadyLabel(Edit_Cut_MenuItem);
            ChangeTextOfReadyLabel(Edit_Copy_MenuItem);
            ChangeTextOfReadyLabel(Edit_Paste_MenuItem);
            ChangeTextOfReadyLabel(Edit_Undo_MenuItem);
            ChangeTextOfReadyLabel(Edit_Redo_MenuItem);
            ChangeTextOfReadyLabel(Edit_Find_MenuItem);
            ChangeTextOfReadyLabel(Edit_Replace_MenuItem);
            ChangeTextOfReadyLabel(Edit_GoTo_MenuItem);
            ChangeTextOfReadyLabel(Edit_SelectAll_MenuItem);
                     ChangeTextOfReadyLabel(Edit_NextDocument_MenuItem);
            ChangeTextOfReadyLabel(Edit_PreviousDocument_MenuItem);

            ChangeTextOfReadyLabel(View_MenuItem);
            ChangeTextOfReadyLabel(View_Font_MenuItem);
            ChangeTextOfReadyLabel(View_ForeColor_MenuItem);
            ChangeTextOfReadyLabel(View_BackColor_MenuItem);
        }




        //***************************************************************************
        //       IsArgumentNull Property  
        //***************************************************************************
        public static Boolean _isArgsNull = true;
        public Boolean IsArgumentNull
        {
            get { return _isArgsNull; }
            set { _isArgsNull = value; Invalidate(); }
        }


        //***************************************************************************
        //         MainForm Load
        //***************************************************************************
        private void MainForm_Load(object sender, EventArgs e)
        {
            if(_isArgsNull)
            {
                File_New_MenuItem_Click(sender, e);
                UpdateReadyLabel();
            }
        }

        //***************************************************************************
        //         MainForm Closing
        //***************************************************************************
        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;
                foreach(TabPage tabpage in tabcoll)
                {
                    if(tabpage.Text.Contains("*"))
                    {
                       DialogResult dg= MessageBox.Show("Do you want to save file " + tabpage.Text + " before close ?", "Save or Not", MessageBoxButtons.YesNoCancel);
                       
                        if(dg==DialogResult.Yes)
                        {
                            File_Save_MenuItem_Click(sender, e);
                        }
                        else if(dg==DialogResult.Cancel)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        //******************************************************************************************
        //         myTabControlZ_SelectedIndexChanged
        //******************************************************************************************
        private void myTabControlZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                TabPage tabpage = myTabControlZ.SelectedTab;
                if (tabpage.Text.Contains("Untitled"))
                {
                    FilenameToolStripLabel.Text = tabpage.Text;
                    this.Text = "MC texter [ "+tabpage.Text+" ]";
                    UpdateWindowsList_WindowMenu();
                }
                else
                {
                    foreach (String filename in OpenedFilesList)
                    {
                        if (tabpage != null)
                        {
                            String str = filename.Substring(filename.LastIndexOf("\\") + 1);
                            if (tabpage.Text.Contains("*"))
                            {
                                String str2 = tabpage.Text.Remove(tabpage.Text.Length - 1);
                                if (str == str2)
                                {
                                    FilenameToolStripLabel.Text = filename;
                                    this.Text = "MCtexter [ " + tabpage.Text + " ]";
                                }
                            }

                            else
                            {
                                if (str == tabpage.Text)
                                {
                                    FilenameToolStripLabel.Text = filename;
                                    this.Text = "MCTexter [ " + tabpage.Text + " ]";
                                }
                            }
                        }
                    }

                    UpdateWindowsList_WindowMenu();
                }
            }
            else
            {
                FilenameToolStripLabel.Text = "MCtexter#";
                this.Text = "MCtexter";
                UpdateWindowsList_WindowMenu();
            }
        }



        public void UpdateWindowsList_WindowMenu()
        {
            TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;

            int n = Window_MenuItem.DropDownItems.Count;
            for (int i = n - 1; i >= 4; i--)
            {
                Window_MenuItem.DropDownItems.RemoveAt(i);
            }


            foreach (TabPage tabpage in tabcoll)
            {
                ToolStripMenuItem menuitem = new ToolStripMenuItem();
                String s = tabpage.Text;
                menuitem.Text = s;
                if (myTabControlZ.SelectedTab == tabpage)
                {
                    menuitem.Checked = true;
                }
                else
                {
                    menuitem.Checked = false;
                }
                Window_MenuItem.DropDownItems.Add(menuitem);

                menuitem.Click += new System.EventHandler(WindowListEvent_Click);
            }
        }

        private void WindowListEvent_Click(object sender, EventArgs e)
        {
            ToolStripItem toolstripitem = (ToolStripItem)sender;
            TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;
            foreach (TabPage tb in tabcoll)
            {
                if (toolstripitem.Text == tb.Text)
                {
                    myTabControlZ.SelectedTab = tb;

                    var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                    _myRichTextBox.richTextBox1.Select();

                    UpdateWindowsList_WindowMenu();
                }
            }
        }


        //*************************************************************************************
        //  File_MenuItem_DropDownOpening
        //*************************************************************************************
        private void File_MenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                File_New_MenuItem.Enabled = true;
                File_Open_MenuItem.Enabled = true;
                File_Save_MenuItem.Enabled = true;
                File_SaveAs_MenuItem.Enabled = true;
                File_SaveAll_MenuItem.Enabled = true;
                File_Close_MenuItem.Enabled = true;
                File_CloseAll_MenuItem.Enabled = true;
                File_OpenInSystemEditor_MenuItem.Enabled = true;
                File_Print_MenuItem.Enabled = true;
                File_PrintPreview_MenuItem.Enabled = true;
                File_Exit_MenuItem.Enabled = true;
            }
            else
            {
                File_New_MenuItem.Enabled = true;
                File_Open_MenuItem.Enabled = true;
                File_Save_MenuItem.Enabled = false;
                File_SaveAs_MenuItem.Enabled = false;
                File_SaveAll_MenuItem.Enabled = false;
                File_Close_MenuItem.Enabled = false;
                File_CloseAll_MenuItem.Enabled = false;
                File_OpenInSystemEditor_MenuItem.Enabled = false;
                File_Print_MenuItem.Enabled = false;
                File_PrintPreview_MenuItem.Enabled = false;
                File_Exit_MenuItem.Enabled = true;
            }
        }


        //*************************************************************************************
        //  Edit_MenuItem_DropDownOpening
        //*************************************************************************************
        private void Edit_Menu_DropDownOpening(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                Edit_Cut_MenuItem.Enabled = true;
                Edit_Copy_MenuItem.Enabled = true;
                Edit_Paste_MenuItem.Enabled = true;
                Edit_Undo_MenuItem.Enabled = true;
                Edit_Redo_MenuItem.Enabled = true;
                Edit_Find_MenuItem.Enabled = true;
                Edit_Replace_MenuItem.Enabled = true;
                Edit_GoTo_MenuItem.Enabled = true;
                Edit_SelectAll_MenuItem.Enabled = true;
         

                if(myTabControlZ.TabCount>1)
                {
                    Edit_NextDocument_MenuItem.Enabled = true;
                    Edit_PreviousDocument_MenuItem.Enabled = true;
                }
            }
            else
            {
                Edit_Cut_MenuItem.Enabled = false;
                Edit_Copy_MenuItem.Enabled = false;
                Edit_Paste_MenuItem.Enabled = false;
                Edit_Undo_MenuItem.Enabled = false;
                Edit_Redo_MenuItem.Enabled = false;
                Edit_Find_MenuItem.Enabled = false;
                Edit_Replace_MenuItem.Enabled = false;
                Edit_GoTo_MenuItem.Enabled = false;
                Edit_SelectAll_MenuItem.Enabled = false;
         
                Edit_NextDocument_MenuItem.Enabled = false;
                Edit_PreviousDocument_MenuItem.Enabled = false;
            }
        }


        //*************************************************************************************
        //  View_MenuItem_DropDownOpening
        //*************************************************************************************
        private void View_MenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                View_Font_MenuItem.Enabled = true;
                View_ForeColor_MenuItem.Enabled = true;
                View_BackColor_MenuItem.Enabled = true;
            }
            else
            {
                View_Font_MenuItem.Enabled = false;
                View_ForeColor_MenuItem.Enabled = false;
                View_BackColor_MenuItem.Enabled = false;
            }
        }


        //*************************************************************************************
        //  Run_MenuItem_DropDownOpening
        //*************************************************************************************
       


        

        //*************************************************************************************
        //  OpenAssociatedFiles_WhenApplicationStarts()
        //*************************************************************************************
        public void OpenAssociatedFiles_WhenApplicationStarts(String[] files)
        {
            StreamReader strReader;
            String str;
            foreach (string filename in files)
            {
                MyTabPage tabpage = new MyTabPage(this);

                strReader = new StreamReader(filename);
                str = strReader.ReadToEnd();
                strReader.Close();

                String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                tabpage.Text = fname;

                //add contextmenustrip to richTextBox1
                tabpage._myRichTextBox.richTextBox1.ContextMenuStrip = myContextMenuStrip;

                tabpage._myRichTextBox.richTextBox1.Text = str;
                myTabControlZ.TabPages.Add(tabpage);
                myTabControlZ.SelectedTab = tabpage;


               


                /* check (*) is available on TabPage Text
                 adding filename to tab page by removing (*) */
                fname = tabpage.Text;
                if (fname.Contains("*"))
                {
                    fname = fname.Remove(fname.Length - 1);
                }
                tabpage.Text = fname;

                //adding filenames to OpenedFilesList list
                OpenedFilesList.Add(filename);

                FilenameToolStripLabel.Text = filename;
                this.Text = "MCtexter [ " + fname + " ]";
            }


            if (myTabControlZ.SelectedIndex >= 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                _myRichTextBox.richTextBox1.Select();
            }
            UpdateWindowsList_WindowMenu();
        }



//*****************************************************************************************************************************
//                          File
//*****************************************************************************************************************************

        //***************************************************************************
        //         File -> New
        //***************************************************************************
        public static int count = 1;
        private void File_New_MenuItem_Click(object sender, EventArgs e)
        {
            MyTabPage tabpage = new MyTabPage(this);
            tabpage.Text = "Untitled " + count;
            myTabControlZ.TabPages.Add(tabpage);

            myTabControlZ.SelectedTab = tabpage;

            var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
            _myRichTextBox.richTextBox1.Select();

            //add contextmenustrip to richTextBox1
            _myRichTextBox.richTextBox1.ContextMenuStrip = myContextMenuStrip;

            

            this.Text = "MCtexter";

            FilenameToolStripLabel.Text = tabpage.Text;

            UpdateWindowsList_WindowMenu();

            count++;
            
        }


        //***************************************************************************
        //          File -> Open
        //***************************************************************************
        private void File_Open_MenuItem_Click(object sender, EventArgs e)
        {
            StreamReader strReader;
            String str;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String[] files = openFileDialog1.FileNames;
                foreach (string filename in files)
                {
                    MyTabPage tabpage = new MyTabPage(this);

                    strReader = new StreamReader(filename);
                    str = strReader.ReadToEnd();
                    strReader.Close();

                    String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                    tabpage.Text = fname;

                    //add contextmenustrip to richTextBox1
                    tabpage._myRichTextBox.richTextBox1.ContextMenuStrip = myContextMenuStrip;

                    tabpage._myRichTextBox.richTextBox1.Text = str;
                    myTabControlZ.TabPages.Add(tabpage);
                    myTabControlZ.SelectedTab = tabpage;


            //        this.UpdateDocumentSelectorList();


                    /* check (*) is available on TabPage Text
                     adding filename to tab page by removing (*) */
                    fname = tabpage.Text;
                    if (fname.Contains("*"))
                    {
                        fname = fname.Remove(fname.Length - 1);
                    }
                    tabpage.Text = fname;

                    //adding filenames to OpenedFilesList list
                    OpenedFilesList.Add(filename);

                    FilenameToolStripLabel.Text = filename;
                    this.Text = "MCtexter [ "+fname+" ]";
                }


                if (myTabControlZ.SelectedIndex >= 0)
                {
                    var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                    _myRichTextBox.richTextBox1.Select();
                }
                UpdateWindowsList_WindowMenu();
            }
        }


        //***************************************************************************
        //         File -> Save
        //***************************************************************************
        private void File_Save_MenuItem_Click(object sender, EventArgs e)
        {
            TabPage seltab = myTabControlZ.SelectedTab;
            String selecttabname = seltab.Text;

            if (FilenameToolStripLabel.Text.Contains("\\"))
            {
                TabPage tabpage = myTabControlZ.SelectedTab;
                if (tabpage.Text.Contains("*"))
                {
                    String filename = FilenameToolStripLabel.Text;
                    if (File.Exists(filename))
                    {
                        var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                        File.WriteAllText(filename, "");
                        StreamWriter strwriter = System.IO.File.AppendText(filename);
                        strwriter.Write(_myRichTextBox.richTextBox1.Text);
                        strwriter.Close();
                        strwriter.Dispose();
                        tabpage.Text = tabpage.Text.Remove(tabpage.Text.Length - 1);

                        UpdateWindowsList_WindowMenu();

              //          this.UpdateDocumentSelectorList();
                    }
                }
            }
            else
            {
                File_SaveAs_MenuItem_Click(sender, e);
            }
        }


        //***************************************************************************
        //         File -> Save As
        //***************************************************************************
        private void File_SaveAs_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                TabPage tabpage = myTabControlZ.SelectedTab;
                if (tabpage != null)
                {
                    var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        String filename = saveFileDialog1.FileName;
                        if (filename != "")
                        {
                            File.WriteAllText(filename, "");
                            StreamWriter strw = new StreamWriter(filename);
                            strw.Write(_myRichTextBox.richTextBox1.Text);
                            strw.Close();
                            strw.Dispose();

                            String fname = filename.Substring(filename.LastIndexOf("\\") + 1);
                            tabpage.Text = fname;
                            this.Text = "MCtexter [ " + fname + " ]";
                            FilenameToolStripLabel.Text = filename;

                            OpenedFilesList.Add(filename);
                            UpdateWindowsList_WindowMenu();

                            
                        }
                    }
                }
            }
        }



        //***************************************************************************
        //         File -> Save All
        //***************************************************************************
        private void File_SaveAll_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                OpenedFilesList.Reverse();
                TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;

                foreach(TabPage tabpage in tabcoll)
                {
                    myTabControlZ.SelectedTab = tabpage;
                    myTabControlZ_SelectedIndexChanged(sender, e);
                    
                    if( ! tabpage.Text.Contains("Untitled"))
                    {
                        try
                        {
                            var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                            File.WriteAllText(FilenameToolStripLabel.Text, "");
                            StreamWriter strwriter = System.IO.File.AppendText(FilenameToolStripLabel.Text);
                            strwriter.Write(_myRichTextBox.richTextBox1.Text);
                            strwriter.Close();
                            strwriter.Dispose();
                        }
                        catch { }
                    }
                }

                System.Windows.Forms.TabControl.TabPageCollection tabcollection = myTabControlZ.TabPages;
                foreach (TabPage tabpage in tabcollection)
                {
                    String str = tabpage.Text;
                    if (str.Contains("*")&& !str.Contains("Untitled"))
                    {
                        str = str.Remove(str.Length - 1);
                    }
                    tabpage.Text = str;
                }
                UpdateWindowsList_WindowMenu();
            }
        }


        //***************************************************************************
        //         RemoveFileNamesFromTreeView()
        //***************************************************************************
      


        //***************************************************************************
        //         File -> Close
        //***************************************************************************
        private void File_Close_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                TabPage tabpage = myTabControlZ.SelectedTab;
                if (tabpage.Text.Contains("*"))
                {
                    DialogResult dg = MessageBox.Show("Do you want to save " + tabpage.Text + " file before close ?", "Save before Close ?", MessageBoxButtons.YesNo);
                    if (dg == DialogResult.Yes)
                    {
                        //save file before close
                        File_Save_MenuItem_Click(sender, e);
                        //remove tab
                        myTabControlZ.TabPages.Remove(tabpage);

                        //RemoveFileNamesFromTreeView(tabpage.Text);
                 //       this.UpdateDocumentSelectorList();

                        UpdateWindowsList_WindowMenu();
                        myTabControlZ_SelectedIndexChanged(sender, e);

                        LineToolStripLabel.Text = "Line";
                        ColumnToolStripLabel.Text = "Col";

                        if (myTabControlZ.TabCount == 0)
                        {
                            FilenameToolStripLabel.Text = "MCtexter";
                        }
                    }
                    else
                    {
                        //remove tab
                        myTabControlZ.TabPages.Remove(tabpage);

                        

                        UpdateWindowsList_WindowMenu();
                        myTabControlZ_SelectedIndexChanged(sender, e);

                        LineToolStripLabel.Text = "Line";
                        ColumnToolStripLabel.Text = "Col";

                        if (myTabControlZ.TabCount == 0)
                        {
                            FilenameToolStripLabel.Text = "MCtexter";
                        }
                    }
                }
                else
                {
                    //remove tab
                    myTabControlZ.TabPages.Remove(tabpage);

                   
                    UpdateWindowsList_WindowMenu();
                    myTabControlZ_SelectedIndexChanged(sender, e);

                    LineToolStripLabel.Text = "Line";
                    ColumnToolStripLabel.Text = "Col";

                    if (myTabControlZ.TabCount == 0)
                    {
                        FilenameToolStripLabel.Text = "";
                    }
                }

                if (myTabControlZ.SelectedIndex >= 0)
                {
                    var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                    _myRichTextBox.richTextBox1.Select();
                }

            }
            else
            {
                FilenameToolStripLabel.Text = "Advanced Notepad in C#";

                LineToolStripLabel.Text = "Line";
                ColumnToolStripLabel.Text = "Col";
            }
        }



        //***************************************************************************
        //         File -> Close All
        //***************************************************************************
        private void File_CloseAll_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                System.Windows.Forms.TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;
                foreach (TabPage tabpage in tabcoll)
                {
                    myTabControlZ.SelectedTab = tabpage;

                    if (tabpage.Text.Contains("*"))
                    {
                        DialogResult dg = MessageBox.Show("Do you want to save file  " + tabpage.Text + "  before close ?", "Save before Close ?", MessageBoxButtons.YesNo);
                        if (dg == DialogResult.Yes)
                        {
                            //save file
                            File_Save_MenuItem_Click(sender, e);
                            //remove tab
                            myTabControlZ.TabPages.Remove(tabpage);
                           
                            UpdateWindowsList_WindowMenu();
                            myTabControlZ_SelectedIndexChanged(sender, e);
                            LineToolStripLabel.Text = "Line";
                            ColumnToolStripLabel.Text = "Col";
                        }
                        else
                        {
                            //remove tab
                            myTabControlZ.TabPages.Remove(tabpage);
                            
                            UpdateWindowsList_WindowMenu();
                            myTabControlZ_SelectedIndexChanged(sender, e);
                            LineToolStripLabel.Text = "Line";
                            ColumnToolStripLabel.Text = "Col";
                        }
                    }
                    else
                    {
                        //remove tab
                        myTabControlZ.TabPages.Remove(tabpage);
                        
                        UpdateWindowsList_WindowMenu();
                        myTabControlZ_SelectedIndexChanged(sender, e);
                        LineToolStripLabel.Text = "Line";
                        ColumnToolStripLabel.Text = "Col";
                    }
                }
            }
            else
            {
                FilenameToolStripLabel.Text = "Advanced Notepad in C#";
                LineToolStripLabel.Text = "Line";
                ColumnToolStripLabel.Text = "Col";
            }
        }


        //***************************************************************************
        //         File -> Open In System Editor
        //***************************************************************************
        private void File_OpenInSystemEditor_MenuItem_Click(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount >0)
            {
                if(FilenameToolStripLabel.Text.Contains("\\"))
                {
                    Process.Start(FilenameToolStripLabel.Text);
                }
            }
        }


        //***************************************************************************
        //         File -> Print
        //***************************************************************************
        private void File_Print_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                _myRichTextBox.richTextBox1.Print();
            }
        }


        //***************************************************************************
        //         File -> Print Preview
        //***************************************************************************
        private void File_PrintPreview_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                if (myTabControlZ.SelectedTab.Text != "Start Page")
                {
                    int select_index = myTabControlZ.SelectedIndex;
                    var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                    e.Graphics.DrawString(_myRichTextBox.richTextBox1.Text, _myRichTextBox.richTextBox1.Font, Brushes.Black, e.MarginBounds.Left, 0, new StringFormat());
                    e.Graphics.PageUnit = GraphicsUnit.Inch;
                }
            }
        }


        //***************************************************************************
        //         File -> Exit
        //***************************************************************************
        private void File_Exit_MenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


//*****************************************************************************************************************************
//                          Edit
//*****************************************************************************************************************************

        //***************************************************************************
        //         Edit -> Cut
        //***************************************************************************
        private void Edit_Cut_MenuItem_Click(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                if(_myRichTextBox.richTextBox1.SelectedText!="")
                {
                    if(Clipboard.ContainsText())
                    {
                        Clipboard.Clear();
                        Clipboard.SetText(_myRichTextBox.richTextBox1.SelectedText);
                        _myRichTextBox.richTextBox1.SelectedText = "";
                    }
                    else
                    {
                        Clipboard.SetText(_myRichTextBox.richTextBox1.SelectedText);
                        _myRichTextBox.richTextBox1.SelectedText = "";
                    }
                }
            }
        }

        //***************************************************************************
        //         Edit -> Copy
        //***************************************************************************
        private void Edit_Copy_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                if (_myRichTextBox.richTextBox1.SelectedText != "")
                {
                    if (Clipboard.ContainsText())
                    {
                        Clipboard.Clear();
                        Clipboard.SetText(_myRichTextBox.richTextBox1.SelectedText);
                    }
                    else
                    {
                        Clipboard.SetText(_myRichTextBox.richTextBox1.SelectedText);
                    }
                }
            }
        }

        //***************************************************************************
        //         Edit -> Paste
        //***************************************************************************
        private void Edit_Paste_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];

                if (Clipboard.ContainsText())
                {
                    String str = Clipboard.GetText();
                    _myRichTextBox.richTextBox1.Paste();
                }
            }
        }

        //***************************************************************************
        //         Edit -> Undo
        //***************************************************************************
        private void Edit_Undo_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                if(_myRichTextBox.richTextBox1.CanUndo)
                {
                    _myRichTextBox.richTextBox1.Undo();
                }
            }
        }

        //***************************************************************************
        //         Edit -> Redo
        //***************************************************************************
        private void Edit_Redo_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                if (_myRichTextBox.richTextBox1.CanRedo)
                {
                    _myRichTextBox.richTextBox1.Redo();
                }
            }
        }

        //***************************************************************************
        //         Edit -> Find
        //***************************************************************************
        private void Edit_Find_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                Find_Form f = new Find_Form(_myRichTextBox.richTextBox1);
                f.Show();
            }
        }

        //***************************************************************************
        //         Edit -> Replace
        //***************************************************************************
        private void Edit_Replace_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                Replace_Form f = new Replace_Form(_myRichTextBox.richTextBox1);
                f.ShowDialog();
            }
        }

        //***************************************************************************
        //         Edit -> GoTo
        //***************************************************************************
        private void Edit_GoTo_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                GoTo_Form rtf = new GoTo_Form(_myRichTextBox.richTextBox1);
                rtf.ShowDialog();
            }
        }

        //***************************************************************************
        //         Edit -> Select All
        //***************************************************************************
        private void Edit_SelectAll_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                _myRichTextBox.richTextBox1.SelectAll();
            }
        }

        
        //***************************************************************************
        //         Edit -> Next Document
        //***************************************************************************
        private void Edit_NextDocument_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int count = myTabControlZ.TabCount;
                if (myTabControlZ.SelectedIndex <= count)
                {
                    myTabControlZ.SelectedIndex = myTabControlZ.SelectedIndex + 1;
                }
                UpdateWindowsList_WindowMenu();
            }
        }

        //***************************************************************************
        //         Edit -> Previous Document
        //***************************************************************************
        private void Edit_PreviousDocument_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                if (myTabControlZ.SelectedIndex == 0)
                {
                }
                else
                {
                    myTabControlZ.SelectedIndex = myTabControlZ.SelectedIndex - 1;
                }
                UpdateWindowsList_WindowMenu();
            }
        }


        
//*****************************************************************************************************************************
//                           View
//*****************************************************************************************************************************

        //***************************************************************************
        //         View -> Font
        //***************************************************************************
        private void View_Font_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                FontDialog fd = new FontDialog();
                if(fd.ShowDialog()==DialogResult.OK)
                {
                    _myRichTextBox.richTextBox1.Font = fd.Font;
                }
            }
        }

        //***************************************************************************
        //         View -> Fore Color
        //***************************************************************************
        private void View_ForeColor_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    _myRichTextBox.richTextBox1.ForeColor = cd.Color;
                }
            }
        }

        //***************************************************************************
        //         View -> Back Color
        //***************************************************************************
        private void View_BackColor_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    _myRichTextBox.richTextBox1.BackColor = cd.Color;
                }
            }
        }

        //***************************************************************************
        //         View -> Document Selector
        //***************************************************************************
       

        //***************************************************************************
        //         View -> ToolStrip
        //***************************************************************************
        private void View_ToolStrip_MenuItem_Click(object sender, EventArgs e)
        {
            if(View_ToolStrip_MenuItem.Checked==false)
            {
                myToolStripZ.Visible = true;
                View_ToolStrip_MenuItem.Checked = true;
            }
            else
            {
                myToolStripZ.Visible = false;
                View_ToolStrip_MenuItem.Checked = false;
            }
        }

        //***************************************************************************
        //         View -> Status Strip
        //***************************************************************************
        private void View_StatusStrip_MenuItem_Click(object sender, EventArgs e)
        {
            if (View_StatusStrip_MenuItem.Checked == false)
            {
                statusStrip1.Visible = true;
                View_StatusStrip_MenuItem.Checked = true;
            }
            else
            {
                 statusStrip1.Visible = false;
                View_StatusStrip_MenuItem.Checked = false;
            }
        }

        private void View_FullScreen_MenuItem_Click(object sender, EventArgs e)
        {
            if(View_FullScreen_MenuItem.Checked==false)
            {
                this.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Visible = true;

                View_FullScreen_MenuItem.Checked = true;
            }
            else
            {
                this.Visible = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Visible = true;

                View_FullScreen_MenuItem.Checked =false ;
            }
        }

        

//*****************************************************************************************************************************
//                           Run
//*****************************************************************************************************************************

        //***************************************************************************
        //         Run -> Run
        //***************************************************************************
        private void Run_Run_MenuItem_Click(object sender, EventArgs e)
        {
            
        }

       
        
        //***************************************************************************
        //         Run -> runa as Html file
        //***************************************************************************
        private void Run_PreviewHTMLPage_MenuItem_Click(object sender, EventArgs e)
        {
            if (myTabControlZ.TabCount > 0)
            {
                int select_index = myTabControlZ.SelectedIndex;
                var _myRichTextBox = (MyRichTextBox)myTabControlZ.TabPages[myTabControlZ.SelectedIndex].Controls[0];
                PreviewHTMLPage_Form phtmlf = new PreviewHTMLPage_Form(_myRichTextBox.richTextBox1.Text,FilenameToolStripLabel.Text);
                phtmlf.Show();
            }
        }

        
//*****************************************************************************************************************************
//                           Window
//*****************************************************************************************************************************
        //***************************************************************************
        //         Window -> Restart
        //***************************************************************************
        private void Window_Restart_MenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //***************************************************************************
        //         Window -> Close All Windows
        //***************************************************************************
       

//*****************************************************************************************************************************
//                           Help
//*****************************************************************************************************************************
        //***************************************************************************
        //         Help -> Help Contents
        //***************************************************************************
        private void Help_HelpContents_MenuItem_Click(object sender, EventArgs e)
        {
            String filename = Application.StartupPath + "\\help content.pdf";
            if(File.Exists(filename))
            {
                Process.Start(filename);
            }
        }

        //***************************************************************************
        //         Help -> Online Help
        //***************************************************************************
       

        //***************************************************************************
        //         Help -> About
        //***************************************************************************
        private void Help_About_MenuItem_Click(object sender, EventArgs e)
        {
            About_Form af = new About_Form();
            af.ShowDialog();
        }




//*****************************************************************************************************************************
//                           Tool Strip Buttons Actions
//*****************************************************************************************************************************
        private void New_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_New_MenuItem_Click(sender, e);
        }

        private void Open_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_Open_MenuItem_Click(sender, e);
        }

        private void Save_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_Save_MenuItem_Click(sender, e);
        }

        private void SaveAs_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_SaveAs_MenuItem_Click(sender, e);
        }

        private void Print_ToolStripButton_Click(object sender, EventArgs e)
        {
            File_Print_MenuItem_Click(sender, e);
        }

        private void Cut_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Cut_MenuItem_Click(sender, e);
        }

        private void Copy_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Copy_MenuItem_Click(sender, e);
        }

        private void Paste_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Paste_MenuItem_Click(sender, e);
        }

        private void Undo_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Undo_MenuItem_Click(sender, e);
        }

        private void Redo_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Redo_MenuItem_Click(sender, e);
        }

        private void Find_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_Find_MenuItem_Click(sender, e);
        }

        private void GoTo_ToolStripButton_Click(object sender, EventArgs e)
        {
            Edit_GoTo_MenuItem_Click(sender, e);
        }

        private void Font_ToolStripButton_Click(object sender, EventArgs e)
        {
            View_Font_MenuItem_Click(sender, e);
        }

        private void PreviewHTMLPage_ToolStripButton_Click(object sender, EventArgs e)
        {
            Run_PreviewHTMLPage_MenuItem_Click(sender, e);
        }




//*****************************************************************************************************************************
//                        richTextBox1 Context Menu Strip menus Actions
//*****************************************************************************************************************************
        private void Cut_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Cut_MenuItem_Click(sender, e);
        }

        private void Copy_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Copy_MenuItem_Click(sender, e);
        }

        private void Paste_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Paste_MenuItem_Click(sender, e);
        }

        private void SelectAll_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Edit_SelectAll_MenuItem_Click(sender, e);
        }

     
        private void SetFont_ContextMenuItem_Click(object sender, EventArgs e)
        {
            View_Font_MenuItem_Click(sender, e);
        }

        private void PreviewHTMLPage_ContextMenuItem_Click(object sender, EventArgs e)
        {
            Run_PreviewHTMLPage_MenuItem_Click(sender, e);
        }





//*****************************************************************************************************************************
//                        myTabControlZ Context Menu Strip menus Actions
//*****************************************************************************************************************************
        private void myTabControl_ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                TabPage tabpage = myTabControlZ.SelectedTab;
                myTabControl_Save_MenuItem.Text = "Save  " + tabpage.Text;
            }
        }

        private void myTabControl_Save_MenuItem_Click(object sender, EventArgs e)
        {
            File_Save_MenuItem_Click(sender, e);
        }

        private void myTabControl_SaveAll_MenuItem_Click(object sender, EventArgs e)
        {
            File_SaveAll_MenuItem_Click(sender, e);
        }

        private void myTabControl_Close_MenuItem_Click(object sender, EventArgs e)
        {
            File_Close_MenuItem_Click(sender, e);
        }

        private void myTabControl_CloseAll_MenuItem_Click(object sender, EventArgs e)
        {
            File_CloseAll_MenuItem_Click(sender, e);
        }


        private void myTabControl_CloseAllButThis_MenuItem_Click(object sender, EventArgs e)
        {
            String tabtext = myTabControlZ.SelectedTab.Text;
            if (myTabControlZ.TabCount > 1)
            {
                TabControl.TabPageCollection tabcoll = myTabControlZ.TabPages;
                foreach (TabPage tabpage in tabcoll)
                {
                    myTabControlZ.SelectedTab = tabpage;
                    if (myTabControlZ.SelectedTab.Text != tabtext)
                    {
                        File_Close_MenuItem_Click(sender, e);
                    }
                }
            }
            else if (myTabControlZ.TabCount == 1)
            {
                File_Close_MenuItem_Click(sender, e);
            }
        }


        private void myTabControl_OpenFileFolder_MenuItem_Click(object sender, EventArgs e)
        {
            if(myTabControlZ.TabCount>0)
            {
                if( ! myTabControlZ.SelectedTab.Text.Contains("Untitled"))
                {
                    if(FilenameToolStripLabel.Text.Contains("\\"))
                    {
                        TabPage tabpage = myTabControlZ.SelectedTab;
                        String tabtext = tabpage.Text;
                        if(tabtext.Contains("*"))
                        {
                            tabtext = tabtext.Remove(tabtext.Length - 1);
                        }
                        String fname = FilenameToolStripLabel.Text;
                        String filename=fname.Remove(fname.Length-(tabtext.Length+1));
                        Process.Start(filename);
                    }
                }
            }
        }

        private void myTabControlZ_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void myTabControlZ_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

       
       

        


       

       

        

        

       

        




    }
}
