using System;

using Model;

namespace WebbyExample
{
    public partial class addComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["comment.post_id"] == null) Response.Redirect("Default.aspx");
            
            int postId = Convert.ToInt32(Request["comment.post_id"]);

            if (string.IsNullOrEmpty(Request["comment.content"])) Response.Redirect(string.Format("view.aspx?id={0}", postId));

            Comment comment = new Comment();
            comment.PostID = postId;
            comment.Number = Convert.ToInt32(Request["comment.number"]);
            comment.Content = Request["comment.content"];
            comment.Save();

            Response.Redirect(string.Format("view.aspx?id={0}", postId));
        }
    }
}
