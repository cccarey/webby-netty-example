<%@ Page Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeBehind="editPost.aspx.cs" Inherits="NettyExample.editPost" Title="Untitled Page" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="primaryContent" runat="server">
    <form id="editPost" runat="server">
        <asp:HiddenField ID="post_ID" runat="server" />
        <h3>Key</h3>
        <asp:TextBox ID="post_Key" runat="server"></asp:TextBox>
        <h3>Title</h3>
        <asp:TextBox ID="post_Title" runat="server" Width="50em"></asp:TextBox>
        <h3>Content</h3>
        <asp:TextBox ID="post_Content"  runat="server" Columns="50" Rows="15" TextMode="MultiLine"></asp:TextBox>
        <h3>Created</h3>
        <p><asp:Label ID="post_Created" runat="server" Text="Label"></asp:Label></p>
        <asp:Button ID="btnPost" runat="server" Text="Post" OnClick="btnPost_Click" />
    </form>
</asp:Content>
