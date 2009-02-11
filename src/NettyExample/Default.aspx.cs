using System;

using Model;

namespace NettyExample
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            blogList.DataSource = Post.GetAll();
            blogList.DataBind();
        }
    }
}
