using System;

using Model;

namespace WebbyExample
{
    public partial class editPost : System.Web.UI.Page
    {
        protected Post post;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                post = new Post();
            }
            else
            {
                post = Post.Get(Convert.ToInt32(Request.QueryString["id"]));
            }
        }
    }
}
