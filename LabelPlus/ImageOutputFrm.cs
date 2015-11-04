/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;

namespace LabelPlus
{
    public partial class ImageOutputFrm : Form
    {

        Workspace wsp;
        PicView pv;

        public ImageOutputFrm(Workspace wsp, PicView pv)
        {
            this.wsp = wsp;
            this.pv = pv;

            InitializeComponent();

            Language.InitFormLanguage(this, StringResources.GetValue("lang"));
        }

        /**
         * replace extension of a path
         * Exp: input="d:\\a.jpg",".png";output="d:\\a.png"
        */
        internal string replaceExtension(string filename, string extension)
        {
            FileInfo fileinfo = new FileInfo(filename);
            string nowExtension = fileinfo.Extension;
            if (extension == "")
            {
                return filename + extension;
            }
            else
            {
                return fileinfo.Directory + "\\" + fileinfo.Name.Replace(nowExtension, extension);
            }
        }

        Thread th = null; // 分离UI线程，防止进度条卡死

        private void button_Click(object sender, EventArgs e)
        {
            if (th != null) th.Abort();

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

            button.Enabled = false;
            buttonAbort.Enabled = true;

            th = new Thread(doOutput);
            th.Start();
        }

        void doOutput()
        {
            string log = "";
            Invoke(new Action(() => outPgBar.Maximum = wsp.Store.Filenames.Length));

            try
            {
                foreach (string key in wsp.Store.Filenames)
                {
                    if (checkBoxJumpNoNum.Checked && wsp.Store[key].Count == 0) { }
                    else
                    {

                        string outputFilename = folderBrowserDialog.SelectedPath + @"\" + key;
                        outputFilename = replaceExtension(outputFilename, radioButtonPNG.Checked ? ".png" : ".jpg");

                        string inputFilename = wsp.DirPath + @"\" + key;
                        try
                        {
                            Image in_img = Image.FromFile(inputFilename);
                            Image out_img = null;
                            var rslt = pv.MakeImage(ref out_img, ref in_img, Convert.ToSingle(textBox.Text), wsp.Store[key]);
                            in_img.Dispose();
                            if (!rslt)
                            {
                                throw new FormatException();
                            }
                            else
                            {
                                using (Stream stream = new FileStream(outputFilename, FileMode.Create))
                                {
                                    out_img.Save(stream, radioButtonPNG.Checked ? ImageFormat.Png : ImageFormat.Jpeg);
                                    out_img.Dispose();
                                }
                                //stream.Close();
                            }
                        }
                        catch
                        { log += "\n" + StringResources.GetValue("can_not_output_file") + outputFilename; }
                    }
                    Invoke(new Action(() => ++outPgBar.Value));
                }
                log += "\n\n" + StringResources.GetValue("output_complete");
            }
            catch (ThreadAbortException)
            { log += "\n\n" + StringResources.GetValue("output_aborted"); }
            catch (Exception)
            { log += "\n\n" + StringResources.GetValue("output_fail"); }
            finally
            {
                try
                {
                    MessageBox.Show(log);
                    Invoke(new Action(() => { if (button != null) button.Enabled = true; }));
                    Invoke(new Action(() => { if (buttonAbort != null) buttonAbort.Enabled = false; }));
                    Invoke(new Action(() => { if (outPgBar != null) outPgBar.Value = 0; }));
                }
                catch { /* 窗口销毁后发生的线程冲突之 InvalidOperationException 强行不要了，嗯 */ }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        { if (th != null) th.Abort(); }

        private void ImageOutputFrm_FormClosing(object sender, FormClosingEventArgs e)
        { if (th != null) th.Abort(); }
    }
}
