<%@ Page Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeBehind="view.aspx.cs" Inherits="WebbyExample.view" Title="Untitled Page" %>
<%@ Import Namespace="Model" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="primaryContent" runat="server">
    <h3><%=post.Title %></h3>
    <h4>Posted <%=post.Created %></h4>
    <div style="display:block;border:2px solid grey;width:80%;margin-bottom:5px;">
        <p><%=post.Content %></p>
    </div>
    <h6><a href="default.aspx">Index</a></h6>
    <h5>Comments</h5>
    <%
        foreach (Comment comment in post.Comments)
        {
            %>
            <div style="display:block;border: 1px solid grey;width:80%;margin-bottom:2px;">
                <p><%=comment.Content %></p>
                <p><i>Posted at <%=comment.Created %></i></p>
            </div>
            <%
        }
    %>
    <form id="postComment" method="post" action="view.aspx">
        <div style="display:block;border:1px solid grey;width:80%;margin-bottom:2px;">
            <input type="hidden" id="comment.post_id" name="comment.post_id" value="<%=post.ID %>" />
            <input type="hidden" id="comment.number" name="comment.number" value="<%=post.Comments.Count+1 %>" />
            <p><textarea id="comment.content" name="comment.content" cols="50" rows="5"></textarea></p>
            <input type="submit" value="Say It" />
        </div>
    </form>
</asp:Content>
