using System;

using Model;

namespace WebbyExample
{
    public partial class updatePost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["post.id"] == null) Response.Redirect("Default.aspx");
            
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
