
using System; 
using System.Collections; 
using System.ComponentModel; 
using System.Data; 
using System.Web; 
using System.Web.SessionState; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
using System.Web.UI.HtmlControls; 
using System.Drawing; 
using System.Drawing.Imaging; 
using System.Drawing.Text; 
/**///// <summary> 
/// 页面验证码程序 
/// 使用：在页面中加入HTML代码 <img src="VerifyCode.aspx"> 
/// </summary> 
public partial class VerifyCode : System.Web.UI.Page 
{ 
static string[] FontItems = new string[] 
{ "Arial", 
"Helvetica", 
"Geneva", 
"sans-serif", 
"Verdana" 
}; 
static Brush[] BrushItems = new Brush[]
{ Brushes.OliveDrab, 
Brushes.ForestGreen, 
Brushes.DarkCyan, 
Brushes.LightSlateGray, 
Brushes.RoyalBlue, 
Brushes.SlateBlue, 
Brushes.DarkViolet, 
Brushes.MediumVioletRed, 
Brushes.IndianRed, 
Brushes.Firebrick, 
Brushes.Chocolate, 
Brushes.Peru, 
Brushes.Goldenrod 
}; 
static string[] BrushName = new string[] 
{ "OliveDrab", 
"ForestGreen", 
"DarkCyan", 
"LightSlateGray", 
"RoyalBlue", 
"SlateBlue", 
"DarkViolet", 
"MediumVioletRed", 
"IndianRed", 
"Firebrick", 
"Chocolate", 
"Peru", 
"Goldenrod" 
}; 
private static Color BackColor = Color.White; 
private static Pen BorderColor = Pens.DarkGray; 
private static int Width = 52; 
private static int Height = 21; 
private Random _random; 
private string _code; 
private int _brushNameIndex; 
override protected void OnInit(EventArgs e) 
{ 
// 
// CODEGEN: This call is required by the ASP.NET Web Form Designer. 
// 
//InitializeComponent(); 
//base.OnInit(e); 
} 
/**//**//**//// <summary> 
/// Required method for Designer support - do not modify 
/// the contents of this method with the code editor. 
/// </summary> 
private void InitializeComponent() 
{ 
//this.Load += new System.EventHandler(this.Page_Load); 
} 
/**//// <summary> 
/// 
/// </summary> 
/// <param name="sender"></param> 
/// <param name="e"></param> 
public void Page_Load(object sender, System.EventArgs e) 
{ 
if (!IsPostBack) 
{ 
// 
// TODO : initialize 
// 
this._random = new Random(); 
this._code = GetRandomCode(); 
// 
// TODO : use Session["code"] save the VerifyCode 
// 
Session["code"] = this._code; 
// 
// TODO : output Image 
// 
this.SetPageNoCache(); 
this.OnPaint(); 
} 
} 
/**//**//**//// <summary> 
/// 设置页面不被缓存 
/// </summary> 
private void SetPageNoCache() 
{ 
Response.Buffer = true; 
Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1); 
Response.Expires = 0; 
Response.CacheControl = "no-cache"; 
Response.AppendHeader("Pragma","No-Cache"); 
} 
/**//**//**//// <summary> 
/// 取得一个 4 位的随机码 
/// </summary> 
/// <returns></returns> 
private string GetRandomCode() 
{ 
return Guid.NewGuid().ToString().Substring(0, 4); 
} 
/**//**//**//// <summary> 
/// 随机取一个字体 
/// </summary> 
/// <returns></returns> 
private Font GetFont() 
{ 
int fontIndex = _random.Next(0, FontItems.Length); 
FontStyle fontStyle = GetFontStyle(_random.Next(0, 2)); 
return new Font(FontItems[fontIndex], 12, fontStyle); 
} 
/**//**//**//// <summary> 
/// 取一个字体的样式 
/// </summary> 
/// <param name="index"></param> 
/// <returns></returns> 
private FontStyle GetFontStyle(int index) 
{ 
switch (index) 
{ 
case 0: 
return FontStyle.Bold; 
case 1: 
return FontStyle.Italic; 
default: 
return FontStyle.Regular; 
} 
} 
/**//**//**//// <summary> 
/// 随机取一个笔刷 
/// </summary> 
/// <returns></returns> 
private Brush GetBrush() 
{ 
int brushIndex = _random.Next(0, BrushItems.Length); 
_brushNameIndex = brushIndex; 
return BrushItems[brushIndex]; 
} 
/**//**//**//// <summary> 
/// 绘画事件 
/// </summary> 
private void OnPaint() 
{ 
Bitmap objBitmap = null; 
Graphics g = null; 
try 
{ 
objBitmap = new Bitmap(Width, Height); 
g = Graphics.FromImage(objBitmap); 
Paint_Background(g); 
Paint_Text(g); 
Paint_TextStain(objBitmap); 
Paint_Border(g); 
objBitmap.Save(Response.OutputStream, ImageFormat.Gif); 
Response.ContentType = "image/gif"; 
} 
catch 
{} 
finally 
{ 
if (null != objBitmap) 
objBitmap.Dispose(); 
if (null != g) 
g.Dispose(); 
} 
} 
/**//**//**//// <summary> 
/// 绘画背景颜色 
/// </summary> 
/// <param name="g"></param> 
private void Paint_Background(Graphics g) 
{ 
g.Clear(BackColor); 
} 
/**//**//**//// <summary> 
/// 绘画边框 
/// </summary> 
/// <param name="g"></param> 
private void Paint_Border(Graphics g) 
{ 
g.DrawRectangle(BorderColor, 0, 0, Width - 1, Height - 1); 
} 
/**//**//**//// <summary> 
/// 绘画文字 
/// </summary> 
/// <param name="g"></param> 
private void Paint_Text(Graphics g) 
{ 
g.DrawString(_code, GetFont(), GetBrush(), 3, 1); 
} 
/**//**//**//// <summary> 
/// 绘画文字噪音点 
/// </summary> 
/// <param name="g"></param> 
private void Paint_TextStain(Bitmap b) 
{ 
for (int n=0; n<30; n++) 
{ 
int x = _random.Next(Width); 
int y = _random.Next(Height); 
b.SetPixel(x, y, Color.FromName(BrushName[_brushNameIndex])); 
} 
} 
} 