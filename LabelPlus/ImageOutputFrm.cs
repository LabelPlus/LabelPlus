using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace LabelPlus
{
    public partial class ImageOutputFrm : Form
    {

        Workspace wsp;
        PicView pv;

        public ImageOutputFrm(Workspace wsp,PicView pv)
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

        private void button_Click(object sender, EventArgs e)
        {          
            try{
                bool png_or_jpg = radioButtonPNG.Checked;
                ImageFormat imageFormat = png_or_jpg?ImageFormat.Png:ImageFormat.Jpeg;
                string extension = png_or_jpg ?".png":".jpg";       
                
                float zoom = Convert.ToSingle(textBox.Text); 
                var store = wsp.Store;
                var keys = store.Filenames;

                if (folderBrowserDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                string log = "";
                foreach (string key in keys)
                {
                    string outputFilename = folderBrowserDialog.SelectedPath + @"\" + key;

                    outputFilename = replaceExtension(outputFilename, extension);

                    string inputFilename = wsp.DirPath + @"\" + key;
                    try
                    {
                        Image in_img = Image.FromFile(inputFilename);
                        Image out_img = null;
                        if (!pv.MakeImage(ref out_img, ref in_img, zoom, store[key]))
                        {
                            throw new FormatException();
                        }
                        else
                        {
                            Stream stream = new FileStream(outputFilename, FileMode.Create);
                            out_img.Save(stream, imageFormat);
                            stream.Close();
                        }
                    }
                    catch
                    {
                        log += "\n" + StringResources.GetValue("can_not_output_file") + outputFilename;
                    }
                }
                MessageBox.Show(log + "\n\n" + StringResources.GetValue("output_complete"));
                this.Close();
            }catch{
                MessageBox.Show(StringResources.GetValue("output_fail"));
                this.Close();
            }
        }
    }
}
