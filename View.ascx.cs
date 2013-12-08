/*
' Copyright (c) 2012  PMCC
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using DotNetNuke.Security.Roles;
using DotNetNuke.Entities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Profile;
using DotNetNuke.UI.Skins.Controls;
using DotNetNuke.UI.Skins;

using PMCC.Modules.CITMessages.Components;

namespace PMCC.Modules.CITMessages
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewCITMessages class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CITMessagesModuleBase, IActionable
    {



        #region Event Handlers
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);

            //GreenSuccess = 0
            //YellowWarning = 1
            //RedError = 2
            //BlueInfo = 3

            DataTable t = GetUserMessages();
            ShowMessages(t);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            
        }

        #endregion


        private DataTable GetUserMessages()
        {
            UserInfo CURRENT_USER = UserController.GetCurrentUserInfo();
            string sNumOfViews;
            int iNumOfViews;

            sNumOfViews = Settings.Contains("MaxNumOfViews") ? Settings["MaxNumOfViews"].ToString() : "0";
            iNumOfViews = Int32.Parse(sNumOfViews);

            DataTable messagesByUserDT = MessageController.GetMessages(CURRENT_USER.UserID, iNumOfViews);
            return messagesByUserDT;
        
        } // end GetUserMessages


        public void ShowMessages(DataTable d)
        {
            int iMsgType;
            ModuleMessage.ModuleMessageType mmt;
            string sMaxMsgs, sMaxChars, sNumOfViews;
            int iMaxMessages, iMaxChars, iNumOfViews;
            int iCountMsgs = 0;
            String txtMessage;

            sMaxMsgs = Settings.Contains("MaxMessages") ? Settings["MaxMessages"].ToString() : "4";
            iMaxMessages = Int32.Parse(sMaxMsgs);
            if (d != null && d.Rows.Count < iMaxMessages)
                iMaxMessages = d.Rows.Count;

            sMaxChars = Settings.Contains("MaxChars") ? Settings["MaxChars"].ToString() : "100";
            iMaxChars = Int32.Parse(sMaxChars);

            


            if (d == null || d.Rows.Count <= 0)
            {
                return;
            }
            else
                for (int i = 0; i < d.Rows.Count && i <= iMaxMessages; i++)
                {
                    iMsgType = Int32.Parse(d.Rows[i]["ModuleMessageType"].ToString());
                    mmt = MessageController.ConvertIntToMessageType(iMsgType);
                    if (d.Rows[i]["MessageText"].ToString().Length < iMaxChars)
                        iMaxChars = d.Rows[i]["MessageText"].ToString().Length;
                    txtMessage = d.Rows[i]["MessageText"].ToString().Substring(0, iMaxChars);
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, txtMessage, mmt);
                }
        } // end ShowMessages()

        #region Optional Interfaces

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), Localization.GetString("EditModule", this.LocalResourceFile), "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

        #endregion
    }
}
