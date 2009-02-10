using System;
using System.Collections.Generic;
using Model;

namespace WebbyExample
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<Post> posts = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            posts = Post.GetAll();
        }
    }
}
