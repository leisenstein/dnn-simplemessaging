<%@ Control language="C#" Inherits="PMCC.Modules.CITMessages.Edit" AutoEventWireup="false"  Codebehind="Edit.ascx.cs" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("EditCitMessages")%></a></h2>
    
<table style="width: 100%;">
    <tr>
        <td>
            <strong>User:&nbsp;&nbsp; </strong>  
            <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="True" onselectedindexchanged="ddlUser_SelectedIndexChanged"></asp:DropDownList>
            &nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" Visible="False" />
        </td>
    </tr>

    <tr>
        <td>
        
            <asp:GridView ID="gvCITMessage" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="4" GridLines="None" 
                ShowFooter="True" onpageindexchanging="gvCITMessage_PageIndexChanging" 
                onrowcancelingedit="gvCITMessage_RowCancelingEdit" 
                onrowcommand="gvCITMessage_RowCommand" 
                onrowdatabound="gvCITMessage_RowDataBound" 
                onrowdeleting="gvCITMessage_RowDeleting" onrowediting="gvCITMessage_RowEditing" 
                onrowupdating="gvCITMessage_RowUpdating">
            <AlternatingRowStyle BackColor="#CEDFFF" />
            <Columns>
                <asp:TemplateField HeaderText="Message ID" SortExpression="MessageID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblMessageID" runat="server" Text='<%#Eval("MessageID") %>'></asp:Label>
                    </ItemTemplate>
   
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Text" SortExpression="MessageText">
                    <ItemTemplate>
                    <asp:Label ID="lblMessageText" runat="server" Text='<%#Eval("MessageText") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtMessageText" runat="server" Text='<%#Eval("MessageText") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtAddMessageText" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Category" SortExpression="MessageCategory">
                    <ItemTemplate>
                    <asp:Label ID="lblMessageCategory" runat="server" Text='<%#Eval("MessageCategory") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:DropDownList ID="ddlMessageCategory" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlAddMessageCategory" runat="server"></asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="FromUserID" SortExpression="FromUserID" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblFromUserID" runat="server" Text='<%#Eval("FromUserID") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtFromUserID" runat="server" Text='<%#Eval("FromUserID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtAddFromUserID" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="From UserName" SortExpression="FromUserName" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblFromUserName" runat="server" Text='<%#Eval("FromUserName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="lblFromUserName" runat="server" Text='<%#Eval("FromUserName") %>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtAddFromUserName" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ToUserID" SortExpression="ToUserID" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblToUserID" runat="server" Text='<%#Eval("ToUserID") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtToUserID" runat="server" Text='<%#Eval("ToUserID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtAddToUserID" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="To UserName" SortExpression="ToUserName">
                    <ItemTemplate>
                    <asp:Label ID="lblToUserName" runat="server" Text='<%#Eval("ToUserName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:Label ID="lblToUserName" runat="server" Text='<%#Eval("ToUserName") %>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtAddToUserName" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ToRoleID" SortExpression="ToRoleID" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblToRoleID" runat="server" Text='<%#Eval("ToRoleID") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtToRoleID" runat="server" Text='<%#Eval("ToRoleID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtAddToRoleID" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status Type" SortExpression="ModuleMessageType">
                    <ItemTemplate>
                    <asp:Label ID="lblModuleMessageType" runat="server" Text='<%#Eval("ModuleMessageType") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:DropDownList ID="ddlModuleMessageType" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlAddModuleMessageType" runat="server"></asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="NumberOfViews" SortExpression="NumberOfViews" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblNumberOfViews" runat="server" Text='<%#Eval("NumberOfViews") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtNumberOfViews" runat="server" Text='<%#Eval("NumberOfViews") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtAddNumberOfViews" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Effective Date" SortExpression="EffDate">
                    <ItemTemplate>
                    <asp:Label ID="lblEffDate" runat="server" Text='<%#Eval("EffDate", "{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtEffDate" runat="server" Text='<%#Eval("EffDate", "{0:d}") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtAddEffDate" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Expiration Date" SortExpression="ExpDate">
                    <ItemTemplate>
                    <asp:Label ID="lblExpDate" runat="server" Text='<%#Eval("ExpDate", "{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtExpDate" runat="server" Text='<%#Eval("ExpDate", "{0:d}") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtAddExpDate" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CreateDate" SortExpression="CreateDate" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblCreateDate" runat="server" Text='<%#Eval("CreateDate", "{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:TextBox ID="txtCreateDate" runat="server" Text='<%#Eval("CreateDate", "{0:d}") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtAddCreateDate" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>



                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" />
                    <br />
                    <span onclick="return confirm('Are you sure you want to delete this record?')">
                        <asp:LinkButton ID="btnDelete" Text="Delete" runat="server" CommandName="Delete" />
                    </span>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <asp:LinkButton ID="btnUpdate" Text="Update" runat="server" CommandName="Update" />
                    <br />
                    <asp:LinkButton ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" />
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:Button ID="btnAddRecord" runat="server" Text="Add" CommandName="Add"></asp:Button>
                    </FooterTemplate>
                </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#105D94" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#CCCCCC"  />
            </asp:GridView>
        </td>
    </tr>
</table>

<asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>