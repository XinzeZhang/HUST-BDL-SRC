<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hsgupfile.aspx.cs" Inherits="hsgupfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>上传文件</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css">

</head>
        <script language="JavaScript"> 
<!-- 
function CopyPath(FilePath)
{
    var str=location.toString()
    var Result=((((str.split('?'))[1]).split('='))[1]);
	window.opener.Form1(Result).focus();								
	window.opener.document.Form1(Result).value=FilePath;
    
	window.opener=null;
    window.close();
}
//--> 
</script> 
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <div>
    <table border="0" align="center" cellpadding="12" cellspacing="1" bgcolor="#6298E1" style="width: 474px">
  
  <tr>
    <td bgcolor="#EBF2F9">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="30" nowrap style="width: 83px">选择文件：</td>
        <td style="width: 332px">
            <input id="UploadFile" runat="server" name="UploadFile" type="file" style="width: 296px" /></td>
          <td>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="上传" Width="65px" /></td>
      </tr>
     <tr>
        <td nowrap style="width: 83px; height: 30px;"></td>
        <td style="width: 332px; height: 30px">
            &nbsp;<a onclick="CopyPath('uppic/<%=fname %>');"><img runat=server src="images/copyUpload.gif" border=0 id="hsgimage" /></a></td>
      </tr>
    </table>
	</td>
  </tr>

</table>
    </div>
    </form>
</body>
</html>
