<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="left" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" href="css.css">
    <script src="images/prototype.js"></script>
    <style type="text/css">
<!--
body,td,th {
	font-size: 12px;
}
body {
	background-color: #F7F7F7;
	background-image: url(images/left_02_01_02.gif);
}
.STYLE2 {color: #FFFFFF}
.STYLE3 {color: #17A25F}
-->
</style>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <div>
        <table id="__01" width="184" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td background="images/left_02_01_02.gif">
                    <table width="184" border="0" cellpadding="0" cellspacing="0" background="images/left_02_01_02.gif"
                        id="Table1">
                        <tr>
                            <td>
                                <table id="Table2" width="184" border="0" cellpadding="0" cellspacing="0">
                                    <tr onclick="new Element.toggle('submenu1')" style="cursor: hand;">
                                        <td width="184" background="images/left_02_01_01.gif">
                                            <table width="100%" height="26" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="90%" height="26" align="center" valign="bottom">
                                                        <span class="STYLE3"><strong>系统管理员</strong></span>
                                                    </td>
                                                    <td width="10%">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="184" style="display: none" id="submenu1">
                                            <table width="91%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="edituser.aspx" target="mainFrame">管理员添加</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="listuser.aspx" target="mainFrame">管理员查询</a>
                                                    </td>
                                                </tr>
                                  
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="mod.aspx" target="mainFrame">修改密码</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
      
                        <tr>
                            <td>
                                <table id="Table4" width="184" border="0" cellpadding="0" cellspacing="0">
                                    <tr onclick="new Element.toggle('submenu3')" style="cursor: hand;">
                                        <td width="184" background="images/left_02_01_01.gif">
                                            <table width="100%" height="26" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="90%" height="26" align="center" valign="bottom">
                                                        <span class="STYLE3"><strong>收费标准管理</strong></span>
                                                    </td>
                                                    <td width="10%">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="184" background="images/left_02_01_02.gif" style="display: none" id="submenu3">
                                            <table width="91%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="shoufeibiaozhun_add.aspx" target="mainFrame">收费标准添加</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="shoufeibiaozhun_list.aspx" target="mainFrame">收费标准查询</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="Table5" width="184" border="0" cellpadding="0" cellspacing="0">
                                    <tr onclick="new Element.toggle('submenu4')" style="cursor: hand;">
                                        <td width="184" background="images/left_02_01_01.gif">
                                            <table width="100%" height="26" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="90%" height="26" align="center" valign="bottom">
                                                        <span class="STYLE3"><strong>车位信息管理</strong></span>
                                                    </td>
                                                    <td width="10%">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="184" background="images/left_02_01_02.gif" style="display: none" id="submenu4">
                                            <table width="91%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="cheweixinxi_add.aspx" target="mainFrame">车位信息添加</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="cheweixinxi_list.aspx" target="mainFrame">车位信息查询</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="Table6" width="184" border="0" cellpadding="0" cellspacing="0">
                                    <tr onclick="new Element.toggle('submenu5')" style="cursor: hand;">
                                        <td width="184" background="images/left_02_01_01.gif">
                                            <table width="100%" height="26" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="90%" height="26" align="center" valign="bottom">
                                                        <span class="STYLE3"><strong>车主信息管理</strong></span>
                                                    </td>
                                                    <td width="10%">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="184" background="images/left_02_01_02.gif" style="display: none" id="submenu5">
                                            <table width="91%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="chezhuxinxi_add.aspx" target="mainFrame">车主信息添加</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="chezhuxinxi_list.aspx" target="mainFrame">车主信息查询</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="Table7" width="184" border="0" cellpadding="0" cellspacing="0">
                                    <tr onclick="new Element.toggle('submenu6')" style="cursor: hand;">
                                        <td width="184" background="images/left_02_01_01.gif">
                                            <table width="100%" height="26" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="90%" height="26" align="center" valign="bottom">
                                                        <span class="STYLE3"><strong>停车管理</strong></span>
                                                    </td>
                                                    <td width="10%">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="184" background="images/left_02_01_02.gif" style="display: none" id="submenu6">
                                            <table width="91%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="tingchejilu_add.aspx" target="mainFrame">停车登记</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="36" height="22" align="center">
                                                        <img src="images/left_02_01.gif">
                                                    </td>
                                                    <td>
                                                        <a href="tingchejilu_list.aspx" target="mainFrame">停车记录查询</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
