using System;

using Model;

namespace NettyExample
{
    public partial class editPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Post post = (Request["id"] == null) ? 
                    new Post() : 
                    Post.Get(Convert.ToInt32(Request["id"]));

                post_ID.Value = post.ID.ToString();
                post_Key.Text = post.Key;
                post_Title.Text = post.Title;
                post_Content.Text = post.Content;
                post_Created.Text = post.Created.ToString();
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            Post post = (Request["id"] == null) ?
                new Post() :
                Post.Get(Convert.ToInt32(Request["id"]));
            post.Key = post_Key.Text;
            post.Title = post_Title.Text;
            post.Content = post_Content.Text;
            post.Save();
            Response.Redirect("Default.aspx");
        }
    }
}
