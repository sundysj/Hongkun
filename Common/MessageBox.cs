using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MobileSoft
{
      public class MessageBox
      {
            public static void Show(System.Web.UI.Page Page,string Info, string Title, string Url)
            {
                  if (Url.ToString() == "")
                  {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=\"javascript\">ymPrompt.alert(\'" + Info + "\', null, null, \'" + Title + "\', handler); function handler(tp) {}</script>");
                  }
                  else
                  {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=\"javascript\">ymPrompt.alert(\'" + Info + "\', null, null, \'" + Title + "\', handler); function handler(tp) {if(tp=='ok'){window.location.href='" + Url + "'} }</script>");
                  }
            }

            public static void Show(System.Web.UI.Page Page, string Info)
            {
                   Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=\"javascript\">alert('"+Info+"');</script>");
            }

            public static void AjaxShow(System.Web.UI.Page Page, string Info, string Title, string Url)
            {
                  //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "ymPrompt.alert(\'" + Info + "\', null, null, \'" + Title + "\', handler); function handler(tp) {}", true);
            }
      }
}
