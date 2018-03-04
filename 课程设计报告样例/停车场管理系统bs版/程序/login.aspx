<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>停车场管理系统</title>
<style type="text/css">
<!--
*{overflow:hidden; font-size:9pt;}
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	background-repeat: repeat-x;
	background-color: #E6FEDA;
}
.STYLE6 {color: #FFFFFF}
.STYLE5 {color: #CCFFCC;
	font-size: 26pt;
}
.STYLE7 {color: #FFFFFF}
-->
</style>
</head>
<script language="javascript">
function loadimage(){ 
document.getElementById("getcode").src = "VerifyCode.aspx?"+Math.random(); 
} 
</script>
<body>
    <form id="form1" runat="server">
    <div>
  <table width="999" height="548" border="0" align="center" cellpadding="0" cellspacing="0" id="__01">
  <tr>
    <td height="230" colspan="3"><table width="64%" height="56" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td height="56"><div style="font-family:宋体; color:#FFFFFF; filter:Glow(Color=#000000,Strength=2); WIDTH: 100%; FONT-WEIGHT: bold; FONT-SIZE: 19pt; margin-top:5pt">
            <div align="center" class="STYLE5">停车场管理系统</div>
        </div></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td width="457" rowspan="2">&nbsp;</td>
    <td width="239" height="152">
        <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 87%;
            height: 139px">
            <tr>
                <td align="right" style="width: 58px">
                    <span class="STYLE7">用户:</span></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox1" runat="server" Style="border-right: #cadcb2 1px solid;
                        border-top: #cadcb2 1px solid; font-size: 12px; border-left: #cadcb2 1px solid;
                        width: 130px; color: #81b432; border-bottom: #cadcb2 1px solid; height: 18px"
                        Width="138px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="width: 58px">
                    <span class="STYLE7">密码:</span></td>
                <td colspan="3">
                    <asp:TextBox ID="TextBox2" runat="server" Style="border-right: #cadcb2 1px solid;
                        border-top: #cadcb2 1px solid; font-size: 12px; border-left: #cadcb2 1px solid;
                        width: 130px; color: #81b432; border-bottom: #cadcb2 1px solid; height: 18px"
                        TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="width: 58px">
                    <span class="STYLE7">权限:</span></td>
                <td colspan="3">
                    <asp:DropDownList ID="cx" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 58px">
                    <span class="STYLE7">验证码:</span></td>
                <td>
                    <asp:TextBox ID="yzm" runat="server" Width="51px"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    <a href="javascript:loadimage()" title="看不清楚，换个图片。"><asp:Image ID="getcode" runat="server" src="VerifyCode.aspx" /></a></td>
            </tr>
            <tr>
                <td colspan="4">
                    <div align="center">
                        <asp:Button ID="Button1" runat="server" Height="26px" OnClick="Button1_Click" onmouseout="this.style.backgroundColor='#FFCCFF'"
                            onmouseover="this.style.backgroundColor='#ffffff'" Style="border-right: #e1f4ee 1px solid;
                            border-top: #e1f4ee 1px solid; font-size: 9pt; border-left: #e1f4ee 1px solid;
                            color: #000000; border-bottom: #e1f4ee 1px solid; background-color: #ffccff"
                            Text="登录" Width="60px" />
                        &nbsp; &nbsp;
                        <input id="Reset1" onmouseout="this.style.backgroundColor='#FFCCFF'" onmouseover="this.style.backgroundColor='#ffffff'"
                            style="border-right: #e1f4ee 1px solid; border-top: #e1f4ee 1px solid; font-size: 9pt;
                            border-left: #e1f4ee 1px solid; width: 60px; color: #000000; border-bottom: #e1f4ee 1px solid;
                            height: 26px; background-color: #ffccff" type="reset" value="重置" />
                        &nbsp;
                    </div>
                </td>
            </tr>
        </table>
    </td>
    <td width="303" rowspan="2">&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>
    </div> </form>
</body>
</html>
