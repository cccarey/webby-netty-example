using System;

using Model;

namespace WebbyExample
{
    public partial class view : System.Web.UI.Page
    {
        protected Post post;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["id"] == null && Request["comment.post_id"] == null) Response.Redirect("Default.aspx");

            if (Request["comment.post_id"] != null) SaveComment();

            post = Post.Get(Convert.ToInt32(Request["id"]));
        }

        private void SaveComment()
        {
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
