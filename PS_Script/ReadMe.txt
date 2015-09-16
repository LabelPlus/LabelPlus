## 简述
该文件夹中的文件内容用于生成ps脚本
可根据需要自行修改

附官方文档：
http://www.adobe.com/devnet/photoshop/scripting.html
---

## 引用顺序说明：
若存在两张图片，每张图片有两个标签，在默认设置下，引用顺序如下。
```
ps_header.txt	

//第一张图片
ps_file_header.txt	//打开文件
ps_labeltext.txt	//添加标签
ps_labeltext.txt	//添加标签
ps_file_footer.txt	//保存文件
ps_close_file.txt	//关闭文件

//第二张图片
ps_file_header.txt
ps_labeltext.txt
ps_labeltext.txt
ps_file_footer.txt
ps_close_file.txt
```

---

## 详细说明

### ps_header.txt
执行时机：一个脚本，只在最初执行一次
功能：选择图片所在文件夹，定义变量
参数列表：无

### ps_file_header.txt
执行时机：每张一张图片时执行一次
默认功能：打开图片文件
参数列表：
{0}：图片文件名

### ps_blank_layer.txt
执行时机：需要新建空白图层时
默认功能：新建空白图层
参数列表：
{0}：图层名

### ps_labeltext.txt
执行时机：需要输出标号文本时
默认功能：新建文本图层，设置样式
参数列表：
{0}：标签文本
{1}：宽度百分比
{2}：高度百分比
{3}：文字尺寸
{4}：字体名

### ps_labelnum.txt
执行时机：需要输出数字标号时
默认功能：新建文本图层，设置样式
参数列表：
{0}：标签序号
{1}：宽度百分比
{2}：高度百分比

### ps_file_footer.txt
执行时机：处理完一张图片后执行
默认功能：另存文件
参数列表：
{0}：另存为文件名

### ps_close_file.txt
执行时机：执行完ps_file_footer后，需要关闭文件时执行
默认功能：关闭文件
参数列表：无
