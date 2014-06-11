using System;

public partial class Pages_Orders : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Authenticate();
    }

    #endregion
    #region Methods
    //Check if user is an administrator
    private void Authenticate()
    {
        if ((string) Session["type"] != "administrator")
        {
            Response.Redirect("~/Pages/Account/Login.aspx");
        }
    }

    #endregion
}