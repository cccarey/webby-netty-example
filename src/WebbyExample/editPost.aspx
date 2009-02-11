<%@ Page Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeBehind="editPost.aspx.cs" Inherits="WebbyExample.editPost" Title="Untitled Page" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="primaryContent" runat="server">
    <form id="editPost" method="post" action="editPost.aspx">
        <input type="hidden" id="post.id" name="post.id" value="<%=post.ID %>" />
        <h3>Key</h3>
        <input type="text" id="post.key" name="post.key" value="<%=post.Key %>" style="width:30em"/>
        <h3>Title</h3>
        <input type="title" id="post.title" name="post.title" value="<%=post.Title %>" style="width:50em"/>
        <h3>Content</h3>
        <textarea id="post.content" name="post.content" cols="50" rows="15"><%=post.Content %></textarea>
        <h3>Created</h3>
        <p><%=post.Created %></p>
        <input type="submit" value="Post" />
    </form>
</asp:Content>
