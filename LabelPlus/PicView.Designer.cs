namespace LabelPlus
{
    partial class PicView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // PicView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PicView";
            this.Size = new System.Drawing.Size(200, 188);
            this.Load += new System.EventHandler(this.PicView_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PicView_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicView_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicView_MouseUp);
            this.Resize += new System.EventHandler(this.PicView_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
    }
}
