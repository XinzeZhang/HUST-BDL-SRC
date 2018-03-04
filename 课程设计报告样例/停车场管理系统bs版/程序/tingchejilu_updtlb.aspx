<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tingchejilu_updtlb.aspx.cs" Inherits="tingchejilu_updtlb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title><script language="javascript" src="js/Calendar.js"></script><LINK href="images/StyleSheet.css" type=text/css rel=stylesheet>
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
<script language="javascript">	
function OpenScript(url,width,height)
{
  var win = window.open(url,"SelectToSort",'width=' + width + ',height=' + height + ',resizable=1,scrollbars=yes,menubar=no,status=yes' );
}
	function OpenDialog(sURL, iWidth, iHeight)
{
   var oDialog = window.open(sURL, "_EditorDialog", "width=" + iWidth.toString() + ",height=" + iHeight.toString() + ",resizable=no,left=0,top=0,scrollbars=no,status=no,titlebar=no,toolbar=no,menubar=no,location=no");
   oDialog.focus();
}
</script>
	<body>
		<form id="Form1" method="post" runat="server">
			<br />
			   <table width="98%" border="1" align="center" cellpadding="3" cellspacing="1" bordercolor="#00FFFF" style="border-collapse:collapse">
				<tr bgColor="#f1f8f5">
					<td colSpan="2">
						<div align="center">设置停车记录</div>
					</td>
				</tr>
				<tr style='display:none'><td><FONT face='宋体'>车牌:</FONT></td><td width='79%'>
                    <asp:TextBox ID="chepai" runat="server" Style="border-right: #000000 1px solid; border-top: #000000 1px solid;
                        border-left: #000000 1px solid; color: #666666; border-bottom: #000000 1px solid"></asp:TextBox>*<asp:RequiredFieldValidator ID="RequiredFieldValidatorchepai" runat="server" ControlToValidate="chepai" ErrorMessage="必填"></asp:RequiredFieldValidator><tr style='display:none'><td><FONT face='宋体'>车型:</FONT></td><td width='79%'><asp:textbox id='chexing' runat='server' style='border:solid 1px #000000; color:#666666'></asp:textbox>*<asp:RequiredFieldValidator ID="RequiredFieldValidatorchexing" runat="server" ControlToValidate="chexing" ErrorMessage="必填"></asp:RequiredFieldValidator><tr style='display:none'><td><FONT face='宋体'>姓名:</FONT></td><td width='79%'><asp:textbox id='xingming' runat='server' style='border:solid 1px #000000; color:#666666'></asp:textbox></td></tr><tr style='display:none'><td><FONT face='宋体'>身份证:</FONT></td><td width='79%'><asp:textbox id='shenfenzheng' runat='server' style='border:solid 1px #000000; color:#666666'></asp:textbox></td></tr><tr style='display:none'><td><FONT face='宋体'>电话:</FONT></td><td width='79%'><asp:textbox id='dianhua' runat='server' style='border:solid 1px #000000; color:#666666'></asp:textbox></td></tr><tr style='display:none'><td><FONT face='宋体'>车位:</FONT></td><td width='79%'>
                    <asp:TextBox ID="chewei" runat="server" Style="border-right: #000000 1px solid;
                        border-top: #000000 1px solid; border-left: #000000 1px solid; color: #666666;
                        border-bottom: #000000 1px solid"></asp:TextBox></td></tr><tr><td><FONT face='宋体'>停入时间:</FONT></td><td width='79%'><asp:textbox id='tingrushijian' runat='server' style='border:solid 1px #000000; color:#666666'></asp:textbox>*<asp:RequiredFieldValidator ID="RequiredFieldValidatortingrushijian" runat="server" ControlToValidate="tingrushijian" ErrorMessage="必填"></asp:RequiredFieldValidator><tr><td><FONT face='宋体'>开出时间:</FONT></td><td width='79%'><asp:textbox id='kaichushijian' runat='server' style='border:solid 1px #000000; color:#666666'></asp:textbox></td></tr><tr><td><FONT face='宋体'>停车时长:</FONT></td><td width='79%'><asp:textbox id='tingcheshichang' runat='server' style='border:solid 1px #000000; color:#666666'></asp:textbox></td></tr><tr><td><FONT face='宋体'>计费类型:</FONT></td><td width='79%'><asp:DropDownList ID='jifeileixing' runat='server' AutoPostBack="True" OnSelectedIndexChanged="jifeileixing_SelectedIndexChanged"></asp:DropDownList></td></tr><tr><td><FONT face='宋体'>费用:</FONT></td><td width='79%'><asp:textbox id='feiyong' runat='server' style='border:solid 1px #000000; color:#666666'></asp:textbox></td></tr><tr style='display:none'><td><FONT face='宋体'>备注:</FONT></td><td width='79%'><asp:textbox id='beizhu' Width='400px' Height='100px' TextMode='MultiLine' runat='server' style='border:solid 1px #000000; color:#666666'></asp:textbox></td></tr>
				<tr>
					<td noWrap width="164" height="25" style="WIDTH: 164px">
						<div align="right"><FONT face="宋体"></FONT>&nbsp;</div>
					</td>
					<td width="59%" height="25">&nbsp;
						<asp:button id="Button1" runat="server" Text="设置" OnClick="Button1_Click" style='border:solid 1px #000000; color:#666666'></asp:button><FONT face="宋体">&nbsp;</FONT>
                        <input id="Reset1" type="reset" value="重置" style='border:solid 1px #000000; color:#666666' /></td>
				</tr>
				
				<tr bgColor="#f1f8f5">
					<td colSpan="2">&nbsp;</td>
				</tr>
			</table>
			<br>
		</form>
	</body>
</HTML>

