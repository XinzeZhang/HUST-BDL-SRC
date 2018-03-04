<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tingchejilu_list.aspx.cs" Inherits="tingchejilu_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title><script language="javascript" src="js/Calendar.js"></script><LINK href="images/StyleSheet.css" type=text/css rel=stylesheet>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="98%" border="1" align="center" cellpadding="3" cellspacing="1" bordercolor="#00FFFF" style="border-collapse:collapse">
            <tbody>
                <tr class="tr2">
                    <td bgcolor="#f1f8f5" style="padding-left: 5px; height: 25px">所有停车记录列表</td>
                </tr>
                <tr class="tr1">
                    <td style="padding-left: 5px; height: 25px">
                        &nbsp; 车牌：<asp:TextBox ID="chepai" runat="server" Style="border-right: #000000 1px solid;
                            border-top: #000000 1px solid; border-left: #000000 1px solid; color: #666666;
                            border-bottom: #000000 1px solid" Width="80px"></asp:TextBox>
                        车型：<asp:TextBox ID=chexing runat="server" style='border:solid 1px #000000; color:#666666' Width="80px"></asp:TextBox> 姓名：<asp:TextBox ID=xingming runat="server" style='border:solid 1px #000000; color:#666666' Width="80px"></asp:TextBox> 身份证号：<asp:TextBox ID=shenfenzheng runat="server" style='border:solid 1px #000000; color:#666666' Width="80px"></asp:TextBox> 电话：<asp:TextBox ID=dianhua runat="server" style='border:solid 1px #000000; color:#666666' Width="80px"></asp:TextBox> 车位：<asp:TextBox ID="chewei" runat="server" Style="border-right: #000000 1px solid;
                            border-top: #000000 1px solid; border-left: #000000 1px solid; color: #666666;
                            border-bottom: #000000 1px solid" Width="80px"></asp:TextBox>
                        计费类型：<asp:DropDownList ID='jifeileixing' runat='server'></asp:DropDownList>
						&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查找" style='border:solid 1px #000000; color:#666666' /> &nbsp;<asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BorderColor="Black" CellPadding="2" font-name="verdana" Font-Names="verdana"
                            Font-Size="8pt" HeaderStyle-BackColor="#F8FAFC" PageSize="8" Width="100%" OnPageIndexChanged="DataGrid1_PageIndexChanged" AllowPaging="True">
                            <HeaderStyle BackColor="#F8FAFC" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <PagerStyle Font-Bold="True" Font-Names="宋体" ForeColor="Blue" HorizontalAlign="Right"
                NextPageText="下一页" PrevPageText="上一页" />
                            <EditItemStyle BackColor="#E9F0F8" CssClass="input_text" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="序号">
                                    <HeaderStyle Width="50px" />
                                    <ItemTemplate>
                                    <%#Container.ItemIndex+1 %>
                                </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField=chepai HeaderText='车牌'></asp:BoundColumn><asp:BoundColumn DataField=chexing HeaderText='车型'></asp:BoundColumn><asp:BoundColumn DataField=xingming HeaderText='姓名'></asp:BoundColumn><asp:BoundColumn DataField=shenfenzheng HeaderText='身份证号'></asp:BoundColumn><asp:BoundColumn DataField=dianhua HeaderText='电话'></asp:BoundColumn><asp:BoundColumn DataField=chewei HeaderText='车位'></asp:BoundColumn><asp:BoundColumn DataField=tingrushijian HeaderText='停入时间'></asp:BoundColumn><asp:BoundColumn DataField=kaichushijian HeaderText='开出时间'></asp:BoundColumn><asp:BoundColumn DataField=tingcheshichang HeaderText='停车时长'></asp:BoundColumn><asp:BoundColumn DataField=jifeileixing HeaderText='计费类型'></asp:BoundColumn><asp:BoundColumn DataField=feiyong HeaderText='费用'></asp:BoundColumn><asp:BoundColumn DataField=beizhu HeaderText='备注'></asp:BoundColumn>
                                
                                <asp:TemplateColumn HeaderText="驶出结账">
                               		<ItemTemplate>
                                    	<a href='tingchejilu_updtlb.aspx?id=<%#DataBinder.Eval(Container.DataItem, "id") %>'>驶出结账</a>
                                	</ItemTemplate>
								<HeaderStyle Width="100px" />
					
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="删除">
                                	<ItemTemplate>
                                    	<a href='delid.aspx?delid=<%#DataBinder.Eval(Container.DataItem, "id") %>&tablename=tingchejilu&npage=tingchejilu_list.aspx' onclick="return confirm('确定要删除？')">删除</a>
                               		</ItemTemplate>
								<HeaderStyle Width="50px" />
					
                                </asp:TemplateColumn>
								
                            </Columns>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:DataGrid></td>
                </tr>
                <tr class="tr1">
                    <td bgcolor="#f1f8f5" style="padding-left: 5px; height: 25px">
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                       <a href="#" onclick="javascript:window.print();">打印本页</a></td>
                </tr>
            </tbody>
        </table>
    
    </div>
    </form>
</body>
</html>
