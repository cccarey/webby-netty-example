<%@ Page Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebbyExample.Default" Title="Untitled Page" %>
<%@ Import Namespace="Model" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="primaryContent" runat="server">
    <table id="blogList">
        <colgroup>
            <col width="60%" />
            <col width="25%" />
            <col width="15%" />
        </colgroup>
        <tr>
            <th>Blog Title</th>
            <th>Number of Comments</th>
            <th>Edit</th>
        </tr>
        <% if (posts != null)
           {
               foreach (Post post in posts)
               {
                   %>
                   <tr>
                    <td><a href="view.aspx?id=<%=post.ID %>"><%=post.Title %></a></td>
                    <td><%=post.CommentCount %></td>
                    <td><a href="editPost.aspx?id=<%=post.ID %>">Edit</a></td>
                   </tr>
                   <%
               }
           }
           else
           {
               %>
               <tr><td colspan="3">No Posts</td></tr>
               <%
           }
        %>
        <tr><td colspan="3"><a href="editPost.aspx">Add Post</a></td></tr>
    </table>
</asp:Content>
