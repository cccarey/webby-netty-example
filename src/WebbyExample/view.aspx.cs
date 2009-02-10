using System;

using Model;

namespace WebbyExample
{
    public partial class view : System.Web.UI.Page
    {
        protected Post post;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["id"] == null) Response.Redirect("Default.aspx");
            
            post = Post.Get(Convert.ToInt32(Request["id"]));
        }
    }
}
