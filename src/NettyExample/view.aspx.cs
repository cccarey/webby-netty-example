using System;

using Model;

namespace NettyExample
{
    public partial class view : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["id"] == null) Response.Redirect("Default.aspx");
                int id = Convert.ToInt32(Request["id"]);
                Post post = Post.Get(id);
                postTitle.Text = post.Title;
                postCreated.Text = post.Created.ToString();
                postContent.Text = post.Content;
                commentPostId.Value = post.ID.ToString();

                int commentCount = post.CommentCount + 1;
                commentNumber.Value = commentCount.ToString();

                commentRepeater.DataSource = post.Comments;
                commentRepeater.DataBind();
            }
        }

        protected void btnSayIt_Click(object sender, EventArgs e)
        {
            if (commentContent.Text.Length > 0)
            {
                Comment comment = new Comment();
                comment.PostID = Convert.ToInt32(commentPostId.Value);
                comment.Number = Convert.ToInt32(commentNumber.Value);
                comment.Content = commentContent.Text;
                comment.Save();
                Response.Redirect(string.Format("view.aspx?id={0}", comment.PostID));
            }
        }
    }
}
