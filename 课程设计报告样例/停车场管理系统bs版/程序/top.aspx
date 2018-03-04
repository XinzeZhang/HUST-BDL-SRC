<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="top" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<style type="text/css">
<!--
body,td,th {
	font-size: 12px;
}
.STYLE5 {color: #72AC27;
	font-size: 26pt;
}
.STYLE6 {color: #00FFFF}
.STYLE7 {color: #FFFFFF}
-->
</style>
</head>
<BODY bgColor=#d6dff7 leftMargin=0 topMargin=0 marginwidth="0" marginheight="0">

    <form id="form1" runat="server">
    <div>
<table id="__01" width="1350" height="130" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="1350" height="77" background="images/2_01_01.gif"><table width="100%" height="37" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="47%"><table width="80%" height="64" border="0" align="center">
          <tr>
            <td><div style="font-family:宋体; color:#FFFFFF; filter:Glow(Color=#000000,Strength=2); WIDTH: 100%; FONT-WEIGHT: bold; FONT-SIZE: 19pt; margin-top:5pt">
                <div align="center" class="STYLE5">
                  <div align="left" class="STYLE6">停车场管理系统</div>
                </div>
            </div></td>
          </tr>
        </table></td>
        <td width="53%">&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td width="1350" height="26" background="images/2_01_02.gif"><table width="100%" height="19" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="77%">&nbsp;</td>
        <td width="23%" ><font class="STYLE7">当前用户：<%=Session["username"].ToString().Trim()%> [<%=Session["cx"].ToString().Trim()%>] </font><a href="logout.aspx" target="_parent"><font class="STYLE7">退出</font></a></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><img src="images/2_01_03.gif" width="1350" height="27" alt=""></td>
  </tr>
</table>


    </div>
    </form>
</body>
</html>
