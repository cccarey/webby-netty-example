<%@ Page Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeBehind="view.aspx.cs" Inherits="NettyExample.view" Title="Untitled Page" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="primaryContent" runat="server">
    <form id="postComment" runat="server">
        <h3><asp:Label ID="postTitle" runat="server" Text="Label"></asp:Label></h3>
        <h4>Posted <asp:Label ID="postCreated" runat="server" Text="Label"></asp:Label></h4>
        <div style="display:block;border:2px solid grey;width:80%;margin-bottom:5px;">
            <p><asp:Label ID="postContent" runat="server" Text="Label"></asp:Label></p>
        </div>
        <h6><a href="default.aspx">Index</a></h6>
        <h5>Comments</h5>
        <asp:Repeater ID="commentRepeater" EnableViewState="false" runat="server">
            <ItemTemplate>
                <div style="display:block;border: 1px solid grey;width:80%;margin-bottom:2px;">
                    <p><%# Eval("Content") %></p>
                    <p><i>Posted at <%# Eval("Created") %></i></p>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div style="display:block;border:1px solid grey;width:80%;margin-bottom:2px;">
            <asp:HiddenField ID="commentPostId" runat="server" />
            <asp:HiddenField ID="commentNumber" runat="server" />
            <p><asp:TextBox ID="commentContent" runat="server" Columns="50" Rows="3" TextMode="MultiLine"></asp:TextBox></p>
            <asp:Button ID="btnSayIt" runat="server" Text="Say It" OnClick="btnSayIt_Click" />
        </div>
    </form>
    
</asp:Content>
