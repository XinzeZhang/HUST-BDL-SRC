<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="main" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>停车场管理系统</title>
    <link rel="stylesheet" type="text/css" href="images/style.css" />
<script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="js/manager.js"></script>
<style type="text/css">
<!--
.STYLE2 {color: #00FFFF}
.STYLE5 {color: #72AC27;
	font-size: 10pt;
}
.STYLE10 {color: #006666}
-->
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" height="100%" width="1348" style="background:#149484;">
	<tbody>
		<tr>
			<td height="130" colspan="3" >
			
		
			<IFRAME 
      style="Z-INDEX:2; VISIBILITY:inherit; WIDTH:100%; HEIGHT:100%" 
      name="topFrame" id="topFrame"  marginWidth="0" marginHeight="0"
      src="top.aspx" frameBorder="0" noResize scrolling="no">
	</IFRAME>
			</td>
		</tr>
		
		<tr>
			<td width="201" align="middle" valign="top" id="mainLeft" ><IFRAME 
      style="Z-INDEX:2; VISIBILITY:inherit; WIDTH:201; HEIGHT:100%" 
      name="leftFrame" id="leftFrame"  marginWidth="0" marginHeight="0"
      src="mygo.aspx" frameBorder="0" noResize scrolling="yes">
	</IFRAME></td>
			<td width="137" valign="middle" style="width:8px;"><%--<div id="sysBar" style="cursor:pointer;"><img id="barImg" src="images/butClose.gif" alt="关闭/打开左栏" /></div>--%></td>
			<td width="947" valign="top" style="width:100%"><iframe frameborder="0" id="mainFrame" name="mainFrame" scrolling="yes" src="sy.aspx" style="height:100%;visibility:inherit; width:100%;z-index:1;"></iframe></td>
		</tr>
		<tr>
			<td height="28" colspan="3" bgcolor="#EBF5FC" style="padding:0px 10px; font-size:12px;color:#2C89AD;background:url(images/down.gif) repeat-x;">
			  <span class="STYLE2">停车场管理系统 当前日期：<%=DateTime.Now.Date.ToShortDateString().Trim()%></span> </td>
		</tr>
	</tbody>
</table>
    </div>
    </form>
</body>
</html>
