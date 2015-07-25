#region Using Directives
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
#endregion

namespace LabelPlus
{
    public partial class PicView : UserControl
    {
        public class LabelUserActionEventArgs : EventArgs{
            int index;
            float x_percent;
            float y_percent;
            public LabelUserActionEventArgs(int n, float X_percent, float Y_percent)
            {
                index = n;
                x_percent = X_percent;
                y_percent = Y_percent;
            }

            public int Index { get{return index;} }
            public float X_percent { get { return x_percent; } }
            public float Y_percent { get { return y_percent; } }
        }

        public delegate void UserActionEventHandler(object sender, LabelUserActionEventArgs e);
        /*Label相关*/
        public UserActionEventHandler LabelUserClickAction; 
        public UserActionEventHandler LabelUserAddAction;
        public UserActionEventHandler LabelUserDelAction;
        /*图像相关*/
        private Image imageOriginal;
        private Image image;
        Rectangle clientRect;
        private float zoom = 0;
        private PointF startP;

        private PointF StartP{
            get{
                if(startP==null) startP =new PointF(0,0);
                return startP;
            }
            set{
                    try
                    {
                        if (value.X < 0)
                            startP.X = 0;
                        else if ((clientRect.Width < image.Size.Width) && (value.X * zoom > image.Size.Width - clientRect.Width))
                            startP.X = (image.Size.Width - clientRect.Width) / zoom;
                        else if (clientRect.Width >= image.Size.Width)
                            startP.X = 0;
                        else
                            startP.X = value.X;

                        if (value.Y < 0)
                            startP.Y = 0;
                        else if ((clientRect.Height < image.Size.Height) && (value.Y * zoom > image.Size.Height - clientRect.Height))
                            startP.Y = (image.Size.Height - clientRect.Height) / zoom;
                        else if (clientRect.Height >= image.Size.Height)
                            startP.Y = 0;
                        else
                            startP.Y = value.Y;
                    }
                    catch { }
                }
        }
        public Image Image {
            set {
                if (imageOriginal != null) { 
                    imageOriginal.Dispose();
                    imageOriginal = value;
                } else {
                    imageOriginal = value;
                    if(value!=null)
                        Zoom = (float)(this.ClientSize.Width) / imageOriginal.Width;//首次运行 设定缩放值
                }               
                    
                StartP = new PointF(0, 0);
                MakeImage(ref image, ref imageOriginal);
                Refresh();
            } 
        }        

        /*共用*/
        public float Zoom
        {            
            set {
                var beforeValue = zoom;

                if (value < 0.05) zoom = 0.05f;
                else if (value > 1.0) zoom = 1.0f;
                else zoom = value;

            }
            get { return zoom; }
        }

        /**
         * LabelSideLength函数用途：根据图片的大小，确定标签的大小
         * getLabelRectangle函数用途：取得一个表示标签位置、大小的矩形
         */
        public float LabelSideLength() { return LabelSideLength(image); }
        public float LabelSideLength(Image image) {
            return (float)(Math.Max(image.Width, image.Height)*0.03);
        }
        RectangleF getLabelRectangle(float x,float y) {
            return getLabelRectangle(x, y, image);
        }
        RectangleF getLabelRectangle(float x, float y, Image image)
        {
            if (image == null) return new RectangleF();
            return new RectangleF(x * image.Width, y * image.Height, 1.4f * LabelSideLength(image), LabelSideLength(image));
        }

        public PicView()
        {
            InitializeComponent();

            //拖拽操作相关事件
            this.MouseDown += new MouseEventHandler(this.PicView_Draging_MouseDown);
            this.MouseMove += new MouseEventHandler(this.PicView_Draging_MouseMove);
            this.MouseUp += new MouseEventHandler(this.PicView_Draging_MouseUp);
            //缩放操作相关事件
            this.MouseWheel += new MouseEventHandler(PicView_Zooming_MouseWheel);
            
            //标签操作相关事件
            this.MouseClick += new MouseEventHandler(this.PicView_Label_MouseClick);

            //提示标签
            toolTip.UseFading = false;
            toolTip.UseAnimation = false;
            toolTip.BackColor = Color.Black;
            toolTip.ForeColor = Color.White;
        }

        #region 绘图操作
        /**
         * imageOriginal为原始图片
         * image为将整幅图片缩放、标号后的缓存，
         * MakeImage函数对其进行绘制
         * PicView_Paint函数为重画事件，将image中的一部分截取出来，绘制到用户界面上
         */
        public bool LoadImage(string path)
        {
            try
            {
                Image = Image.FromFile(path);
                return true;
            }
            catch { return false; }
        }        
        public bool MakeImage(ref Image image,ref Image imageOriginal, float zoom = 0, List<LabelItem> labels = null)
        {
            try
            {
                if (zoom == 0) zoom = this.Zoom;
                if (labels == null) labels = this.labels;

                if (imageOriginal == null)
                {
                    //若无东西 就清空image
                    image = null;
                    return false;
                }
                //缩图
                if (image != null) image.Dispose();
                image = new Bitmap(imageOriginal, (int)(imageOriginal.Size.Width * zoom), (int)(imageOriginal.Size.Height * zoom));
                //贴上标签
                Graphics tmp = Graphics.FromImage(image);
                for (int i = 0; i < labels.Count; i++)
                {
                    RectangleF rect = getLabelRectangle(labels[i].X_percent, labels[i].Y_percent,image);
                    Font myFont = new System.Drawing.Font(new FontFamily("Arial"), LabelSideLength(image) / 1.5f, FontStyle.Bold);

                    Brush myBrushRed = new SolidBrush(Color.Red);
                    Brush myBrushWhite = new SolidBrush(Color.White);
                    Pen mySidePen = new Pen(myBrushRed, LabelSideLength(image) / 10f);

                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    ////背发光
                    //tmp.DrawString(i.ToString(), myFont, myBrushWhite, rect.X - LabelSideLength * 0.03f, rect.Y - LabelSideLength * 0.03f);
                    //实体字

                    tmp.DrawString((i + 1).ToString(), myFont, myBrushRed, rect, sf);
                    //外框                
                    //tmp.DrawRectangle(mySidePen, rect.X, rect.Y, rect.Width, rect.Height);
                }

                Refresh();
                return true;
            }
            catch { return false; }
        }
        public new void Refresh() {
            this.OnPaint(new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle));
        }

        private void PicView_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;
                clientRect = e.ClipRectangle;
                RectangleF imageRect = new RectangleF(startP.X * zoom, startP.Y * zoom, clientRect.Width, clientRect.Height);

                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;

                //构建缓存
                Image myBuffer = new Bitmap(clientRect.Width, clientRect.Height);
                Graphics gBuffer = Graphics.FromImage(myBuffer);
                gBuffer.Clear(Color.White);
                if (image != null)
                    gBuffer.DrawImage(image, clientRect, imageRect, GraphicsUnit.Pixel);

                //缓存->屏幕
                g.DrawImage(myBuffer, 0, 0);
            }
            catch { }
        }
        private void PicView_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
        private void PicView_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.AllPaintingInWmPaint, true);
        }

#endregion

        #region 拖拽操作
        Boolean draging = false;
        Point draging_mosuestartpoint;
        PointF draging_beforeStartP;
        Thread zooming_thread;
        void PicView_Draging_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                draging = true;
                draging_mosuestartpoint = e.Location;
                draging_beforeStartP = startP;
            }
        }

        void PicView_Draging_MouseUp(object sender, MouseEventArgs e)
        {
            draging = false;
        }

        void PicView_Draging_MouseMove(object sender, MouseEventArgs e)
        {
            if (draging == false) return;
            
            float dx = e.Location.X - draging_mosuestartpoint.X;
            float dy = e.Location.Y - draging_mosuestartpoint.Y;
            StartP = new PointF(draging_beforeStartP.X - dx/zoom,draging_beforeStartP.Y - dy/zoom);
            Refresh();

            
        }
        #endregion

        #region 缩放操作
        void PicView_Zooming_MouseWheel(object sender, MouseEventArgs e) 
        {
            //if (Control.ModifierKeys != Keys.Control) return;

            var beforeValue = this.Zoom;
            if (e.Delta > 0) {
                this.Zoom += 0.1f;
            } else {
                this.Zoom -= 0.1f;
            }
            if (beforeValue == this.Zoom) return;   //未做改变 不刷新

            //只有进程结束后 才能再执行一次
            if(zooming_thread ==null){
                zooming_thread = new Thread(zooming_drawing);
                zooming_thread.Start();
            }
        }

        delegate void makeImageDelegate();
        void zooming_drawing() {
            Thread.Sleep(100);
            if (this.InvokeRequired)
            {
                makeImageDelegate tmp = new makeImageDelegate(zooming_drawing);
                this.Invoke(tmp);
                zooming_thread = null;
            }
            else {
                MakeImage(ref image, ref imageOriginal);
            }
            
        }

        #endregion

        #region Label操作
        List<LabelItem> labels = new List<LabelItem>(); 

        public void ClearLabels(){
            labels = new List<LabelItem>();
        }

        ////x,y单位为百分比
        //public void AddLabel(float x,float y){
        //    LabelItem tmp = new LabelItem(); 
        //    tmp.X=x;
        //    tmp.Y=y;
        //    labels.Add(tmp);
        //}

        public void SetLabels(List<LabelItem> items)
        {
            labels = items;
            MakeImage(ref image, ref imageOriginal);
        }

        int getLabelIndex(int x, int y) { 
            float realX = startP.X*zoom +x;
            float realY = startP.Y*zoom +y;
            PointF realP = new PointF(realX,realY);
            RectangleF[] rectList = new RectangleF[labels.Count];
            for (int i=0;i<labels.Count;i++) {
                RectangleF tmpRect = getLabelRectangle(labels[i].X_percent, labels[i].Y_percent);
                if (tmpRect.Contains(realP))
                    return i;               
            }
            return -1;
        }

        void PicView_Label_MouseClick(object sender, MouseEventArgs e)
        {
            if (image == null) return; 

            bool ctrlBepush = (Control.ModifierKeys == Keys.Control);
            int index = getLabelIndex(e.X, e.Y);
            float x_percent = (startP.X * zoom + e.X) / image.Width;
            float y_percent = (startP.Y * zoom + e.Y) / image.Height;
            if (x_percent > 1.0f || y_percent > 1.0f) return;   //忽略超出边界的点击

            if (ctrlBepush && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //添加
                var referRect = getLabelRectangle(0, 0); //参考矩形
                x_percent = (startP.X * zoom + e.X - referRect.Width / 2) / image.Width;
                y_percent = (startP.Y * zoom + e.Y - referRect.Height / 2) / image.Height;
                //重新计算，为了契合鼠标点击的目标
                if(LabelUserAddAction!=null)
                    LabelUserAddAction(this, new LabelUserActionEventArgs(index, x_percent, y_percent));
            }
            else if (ctrlBepush && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //删除
                if (index == -1) return;                
                if(LabelUserDelAction!=null)
                    LabelUserDelAction(this, new LabelUserActionEventArgs(index, x_percent, y_percent));
            }
            else if (!ctrlBepush && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //点击事件
                if (index == -1) return;
                if (LabelUserClickAction != null)
                    LabelUserClickAction(this, new LabelUserActionEventArgs(index, x_percent, y_percent));
            }
            else if (!ctrlBepush && e.Button == System.Windows.Forms.MouseButtons.Right) 
            { 
                //检阅模式

            }
        }

        bool tooltop_showing = false;
        private void PicView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //鼠标样式
                if (Control.ModifierKeys == Keys.Control)
                {
                    this.Cursor = Cursors.Cross;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }

                //提示文本

                int index = getLabelIndex(e.X, e.Y);
                if (index != -1)
                {
                    if (!tooltop_showing)
                    {
                        var location = e.Location;
                        location.X += this.Cursor.Size.Width / 3;
                        location.Y += this.Cursor.Size.Height / 3;
                        toolTip.Show(labels[index].Text, this, location);
                        tooltop_showing = true;
                    }
                }
                else
                {
                    toolTip.Hide(this);
                    tooltop_showing = false;
                }
            }
            catch { }
        }

        #endregion

    }
}
