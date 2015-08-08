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

        Thread th; // 分离UI线程，防止进度条卡死

        private void button_Click(object sender, EventArgs e)
        {

            th = new Thread(doOutput);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        void doOutput()
        {
            try
            {
                bool png_or_jpg = radioButtonPNG.Checked;
                ImageFormat imageFormat = png_or_jpg ? ImageFormat.Png : ImageFormat.Jpeg;
                string extension = png_or_jpg ? ".png" : ".jpg";

                float zoom = Convert.ToSingle(textBox.Text);
                var store = wsp.Store;
                var keys = store.Filenames;

                if (folderBrowserDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                Invoke(new Action(() => button.Enabled = false)); // 防止出现2个输出线程
                Invoke(new Action(() => buttonAbort.Enabled = true));

                string log = "";

                Invoke(new Action(() => outPgBar.Maximum = keys.Length));
                Invoke(new Action(() => outPgBar.Value = 0));

                foreach (string key in keys)
                {
                    string outputFilename = folderBrowserDialog.SelectedPath + @"\" + key;

                    outputFilename = replaceExtension(outputFilename, extension);

                    string inputFilename = wsp.DirPath + @"\" + key;
                    try
                    {
                        Image in_img = Image.FromFile(inputFilename);
                        Image out_img = null;
                        var rslt = pv.MakeImage(ref out_img, ref in_img, zoom, store[key]);
                        in_img.Dispose();
                        if (!rslt)
                        {
                            throw new FormatException();
                        }
                        else
                        {
                            using (Stream stream = new FileStream(outputFilename, FileMode.Create))
                            {
                                out_img.Save(stream, imageFormat);
                                out_img.Dispose();
                            }
                            //stream.Close();
                        }
                    }
                    catch
                    {
                        log += "\n" + StringResources.GetValue("can_not_output_file") + outputFilename;
                    }
                    Invoke(new Action(() => ++outPgBar.Value));
                }
                MessageBox.Show(log + "\n\n" + StringResources.GetValue("output_complete"));
                Invoke(new Action(() => this.Close()));
            }
            catch (ThreadAbortException) {
                MessageBox.Show(StringResources.GetValue("output_aborted"));
            }
            catch 
            {
                MessageBox.Show(
                    StringResources.GetValue("output_fail")
                    );
                Invoke(new Action(() => this.Close()));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (th != null) th.Abort();
            button.Enabled = true;
            buttonAbort.Enabled = false;
            outPgBar.Value = 0;
        }
    }
}
