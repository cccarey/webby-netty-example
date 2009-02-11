<%@ Page Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NettyExample.Default" Title="Untitled Page" %>
<%@ Import Namespace="Model" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="primaryContent" runat="server">
    <form id="form1" runat="server">
        <asp:GridView ID="blogList" BorderWidth="0" GridLines="None" AutoGenerateColumns="false" EnableViewState="false" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="Blog Title" HeaderStyle-Width="60%">
                    <ItemTemplate>
                        <a href="view.aspx?id=<%# Eval("ID") %>"><%# Eval("Title") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Number of Comments" HeaderStyle-Width="25%">
                    <ItemTemplate><%# Eval("CommentCount") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="15%">
                    <ItemTemplate><a href="editPost.aspx?id=<%# Eval("ID") %>">Edit</a></ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:HyperLink ID="HyperLink1" NavigateUrl="editPost.aspx" runat="server">Add Post</asp:HyperLink>
    </form>
</asp:Content>
