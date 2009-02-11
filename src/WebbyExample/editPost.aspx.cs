using System;

using Model;

namespace WebbyExample
{
    public partial class editPost : System.Web.UI.Page
    {
        protected Post post;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["post.id"] != null) SavePost();
            post = (Request.QueryString["id"] == null) ?
                post = new Post() :
                post = Post.Get(Convert.ToInt32(Request.QueryString["id"]));
        }

        private void SavePost()
        {
            int id = Convert.ToInt32(Request["post.id"]);

            Post post = (id > 0) ? Post.Get(id) : new Post();
            post.Title = Request["post.title"];
            post.Key = Request["post.key"];
            post.Content = Request["post.content"];
            post.Save();
            Response.Redirect("Default.aspx");
        }
    }
}
