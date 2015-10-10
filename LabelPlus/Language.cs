/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

/*
 *  Author:             Noodlefitghter
 *  Describe:           Simple Multilingual Program Lib
 *  Edit Data:          2014/07/05
 *  Original Author:    sharemeteor(http://www.cnblogs.com/sharemeteor/articles/215069.html)
 */

using System;
using System.Xml;
using System.IO;
using System.Data;
using System.Collections;
using System.Windows.Forms;
namespace LabelPlus
{
    static class Language
    {
        //static Hashtable stringResources;

        const string Folder=@"Lang/";
        const string DefaultLanguageFile = Folder + "DefaultLanguage.xml";
        const string AppConfigFile = Folder + "AppConfig.xml";

        /** ===========DefaultLanguage File R/W=========== **/
        /* Read Setting from DefaultLanguageFile */
        public static string ReadDefaultLanguage()
        {
            try
            {
                XmlReader reader = new XmlTextReader(DefaultLanguageFile);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                XmlNode root = doc.DocumentElement;
                //选取DefaultLangugae节点 
                XmlNode node = root.SelectSingleNode("DefaultLanguage");

                string result = "EN";
                if (node != null)
                    //取出节点中的内容 
                    result = node.InnerText;

                reader.Close();
                
                return result;
            }
            catch
            {
                return "EN";
            }
        }
        /* Write Setting to  DefaultLanguageFile */ 
        public static void WriteDefaultLanguage(string lang)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(DefaultLanguageFile);
            DataTable dt = ds.Tables["Language"];

            dt.Rows[0]["DefaultLanguage"] = lang;
            ds.AcceptChanges();
            ds.WriteXml(DefaultLanguageFile);
        }

        /** ===========Language List=========== **/
        /* Get Language List from AppConfigFile */
        public static IList GetLanguageList(string lang)
        {
            IList result = new ArrayList();

            XmlReader reader = new XmlTextReader(AppConfigFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            XmlNode root = doc.DocumentElement;
            XmlNodeList nodelist = root.SelectNodes("Area[Language='" + lang + "']/List/Item");
            foreach (XmlNode node in nodelist)
            {
                result.Add(node.InnerText);
            }
            reader.Close();

            return result;
        }

        /** ===========Form Lauguage Resource=========== **/
        /* Read "Resource/Form" to a Hashtable form lang.xml */
        public static Hashtable ReadFromResource(string frmName, string lang)
        {
            Hashtable result = new Hashtable();

            XmlDocument doc = getLangXmlDocument(lang);

            XmlNode root = doc.DocumentElement;
            XmlNodeList nodelist = root.SelectNodes("Form[Name='" + frmName + "']/Controls/Control");

            //读窗口文本
            try
            {
                XmlNode frmRoot = root.SelectSingleNode("Form[Name='" + frmName + "']/Text");
                result.Add(frmName.ToLower(), frmRoot.InnerText);
            }
            catch {
                Console.WriteLine("not found frm language resource: " + frmName);
            }

            foreach (XmlNode node in nodelist)
            {
                try
                {
                    XmlNode node1 = node.SelectSingleNode("@name");
                    XmlNode node2 = node.SelectSingleNode("@text");
                    if (node1 != null)
                    {
                        result.Add(node1.InnerText.ToLower(), replaceNewLineSymbol(node2.InnerText));
                    }
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.ToString());
                }
                catch { }
            }
            

            return result;
        }
        /* Init Form Language */
        public static void InitFormLanguage(Form form,string lang) 
        { 
            //根据用户选择的语言获得表的显示文字 
            Hashtable table = ReadFromResource(form.Name, lang);
            Control.ControlCollection controlNames = form.Controls;
            try
            {
                SetControlNames(form.Controls, table);
                if(table.Contains(form.Name.ToLower()))
                    form.Text = (string)table[form.Name.ToLower()];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /** ===========String Resource=========== **/
        /* Init String Resource From lang.xml 
           "\n" will be replace to "newline symbol"
         */
        public static void InitStringResouce(string lang)
        {
            //stringResources = new Hashtable();

            XmlDocument doc = getLangXmlDocument(lang);

            XmlNode root = doc.DocumentElement;
            XmlNodeList nodelist = root.SelectNodes("Strings/String");
            foreach (XmlNode node in nodelist)
            {
                try
                {
                    XmlNode node1 = node.SelectSingleNode("@name");
                    XmlNode node2 = node.SelectSingleNode("@text");
                    if (node1 != null)
                    {
                        //stringResources.Add(node1.InnerText.ToLower(), replaceNewLineSymbol(node2.InnerText));
                        StringResources.SetValue(node1.InnerText.ToLower(), replaceNewLineSymbol(node2.InnerText));
                    }
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.ToString());
                }
            }


        }
        private static string replaceNewLineSymbol(string str) { 
            return  str.Replace(@"\n","\r\n");
        }
        /* Get Stirng Resource in lang.xml (InitStringResouce() in advance) */
        //public static string GetStringResource(string key)
        //{
        //    string tmp = (string)stringResources[key.ToLower()];
        //    if(tmp!=null)
        //        return tmp;
        //    else
        //        return "";

        //}

        /** =========== private =========== **/
        /* Recursion Set Controls' Names */
        private static void SetControlNames(Control.ControlCollection controls, Hashtable table)
        {
            foreach (Control control in controls)
            {
                if (control.GetType() == typeof(System.Windows.Forms.Panel))
                    SetControlNames(control.Controls, table);

                if (control.GetType() == typeof(System.Windows.Forms.GroupBox))
                    SetControlNames(control.Controls, table);
                
                if (control.GetType() == typeof(System.Windows.Forms.TabPage))
                    SetControlNames(control.Controls, table);

                if (control.GetType() == typeof(System.Windows.Forms.TabControl))
                    SetControlNames(control.Controls, table);
                
                if (control.GetType() == typeof(System.Windows.Forms.SplitContainer))
                    SetControlNames(control.Controls, table);

                if (control.GetType() == typeof(System.Windows.Forms.SplitterPanel))
                    SetControlNames(control.Controls, table);

                if (control.GetType() == typeof(System.Windows.Forms.TableLayoutPanel))
                    SetControlNames(control.Controls, table);

                if (control.GetType() == typeof(System.Windows.Forms.MenuStrip))
                {
                    SetToolStripItemNames(((MenuStrip)control).Items, table);
                    SetControlNames(control.Controls, table);
                }

                if (control.GetType() == typeof(System.Windows.Forms.ToolStrip)) {
                    SetToolStripItemNames(((ToolStrip)control).Items, table);
                    SetControlNames(control.Controls, table);
                }

                if (table.Contains(control.Name.ToLower()))
                {
                    control.Text = (string)table[control.Name.ToLower()];
                    control.Refresh();
                }
            }
        }

        private static void SetToolStripItemNames(ToolStripItemCollection controls, Hashtable table){
            foreach (var control in controls)
            {
                try
                {
                    if (control.GetType() == typeof(ToolStripMenuItem))
                    {
                        ToolStripMenuItem menuItem = (ToolStripMenuItem)control;
                        SetToolStripItemNames(menuItem.DropDownItems, table);

                        if (table.Contains(menuItem.Name.ToLower()))
                        {
                            menuItem.Text = (string)table[menuItem.Name.ToLower()];
                            menuItem.Owner.Refresh();
                        }
                    }
                    else {
                        ToolStripItem item = (ToolStripItem)control;
                        if (table.Contains(item.Name.ToLower()))
                        {                            
                            item.Text = (string)table[item.Name.ToLower()];
                            item.Owner.Refresh();
                        }
                    }
                }
                catch { }
            }
        }
        /* get lang.xml's XmlDocument Object */
        private static XmlDocument getLangXmlDocument(string lang)
        {
            try {
                XmlReader reader = null;
                FileInfo fi = new FileInfo(Folder + lang + ".xml");
                if (!fi.Exists)
                    reader = new XmlTextReader(Folder + "EN.xml");
                else
                    reader = new XmlTextReader(Folder + lang + ".xml");

                XmlDocument doc = new XmlDocument();
                doc.Load(reader);

                reader.Close();
                return doc;
            }
            catch
            {
                MessageBox.Show("Language Definition Error.");
                Environment.Exit(1); // kill this process
                return null; // make compile pass (never reach)
            }
        }
    }
}
