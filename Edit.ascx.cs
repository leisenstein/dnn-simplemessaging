/*
' Copyright (c) 2012 PMCC
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
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Globalization;

using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using DotNetNuke.Security.Roles;
using DotNetNuke.Entities;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Profile;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using PMCC.Modules.CITMessages.Components;

namespace PMCC.Modules.CITMessages
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditCITMessages class is used to manage content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : CITMessagesModuleBase
    {
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        UserInfo CURRENT_USER = null;
        

    
        private DataTable GetViewState()
        { return (DataTable)ViewState["dtCITMessages"]; }

        private void SetViewState(DataTable d)
        { ViewState["dtCITMessages"] = d; }




#region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                CURRENT_USER = null;
                CURRENT_USER = UserController.GetCurrentUserInfo();
                if (!IsPostBack)
                {
                    LoadUserDropDownList();
                    BindGrid();
                }
            } // end try
            catch (Exception exc) // Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }  // end Page_Load()

#endregion



        private void BindGrid()
        {
            bool userSelectedState = ddlUser.SelectedValue == "0" ? false : true;
            int userID = 0;

            userID = Int32.Parse(ddlUser.Text);
            DataTable dtCITMessages;
            if (userSelectedState)
                dtCITMessages = MessageController.GetMessagesForEditor(userID);
            else
                dtCITMessages = MessageController.GetMessages(-2, 0);

            SetViewState(dtCITMessages);
            
            // Handle empty DataTable; Create an empty row and add an empty cell
            // This keeps the FooterRow from being NULL
            if (dtCITMessages != null && dtCITMessages.Rows.Count > 0)
            {
                gvCITMessage.DataSource = dtCITMessages;
                gvCITMessage.DataBind();
            }
            else
            {
                // Since Table is NULL/empty, create a temporary table to bind to & clear it.
                // This way, you can still have the GridView with the Footer
                DataTable fakeTable = MessageController.CreateFakeCITMessageTable();

                gvCITMessage.DataSource = fakeTable;
                gvCITMessage.DataBind();
                // Clear the GridView Row & add an empty one
                gvCITMessage.Rows[0].Cells.Clear();
                gvCITMessage.Rows[0].Cells.Add(new TableCell());
                SetViewState(null);
            }
            SetFooter(userSelectedState);

        }
        private void LoadUserDropDownList()
        {
            // ** Tried UserController and Linq, but too slow
            //ArrayList allUsers = UserController.GetUsers(0);
            //var orderedUsers = from UserInfo s in allUsers orderby s.LastName,s.FirstName select s;
            //ddlUser.DataSource = orderedUsers;

            ddlUser.DataSource = MessageController.GetActiveUsers(); 
            ddlUser.DataValueField = "UserID"; // Alias Name from Sproc
            ddlUser.DataTextField = "ListName";   // Alias name from sproc
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("Select One", "0")); // Adds items to the DDL
        }

        private void populateCategoryDDL(DropDownList d1)
        {
            d1.DataSource = MessageController.GetActiveCategories();
            d1.DataValueField = "CategoryName"; // Alias Name from Sproc
            d1.DataTextField = "CategoryName";   // Alias name from sproc
            d1.DataBind();
            d1.Items.Insert(0, new ListItem("Select One", "0")); // Adds items to the DDL
        }


        private void OLDpopulateCategoryDDL(DropDownList d1)
        {
            if (d1.Items.Count < 4)
            {
                // SHOULD PULL FROM TABLE
                ListItem l1 = new ListItem("PQRS", "PQRS");
                ListItem l2 = new ListItem("CLAIMS", "CLAIMS");
                ListItem l3 = new ListItem("MESSAGE", "MESSAGE");
                ListItem l4 = new ListItem("OTHER", "OTHER");
                d1.Items.Add(l1);
                d1.Items.Add(l2);
                d1.Items.Add(l3);
                d1.Items.Add(l4);
            }
        }

        private void populateModuleMessageTypeDDL(DropDownList d1)
        {
            if (d1.Items.Count < 4)
            {
                // These are values from DNN
                ListItem l1 = new ListItem("Green Success", "0");
                ListItem l2 = new ListItem("Yellow Warning", "1");
                ListItem l3 = new ListItem("Red Error", "2");
                ListItem l4 = new ListItem("Blue Info", "3");
                d1.Items.Add(l1);
                d1.Items.Add(l2);
                d1.Items.Add(l3);
                d1.Items.Add(l4);
            }
        }



        public string ValidateFields(TextBox txtAddMessageText, TextBox txtAddEffDate, TextBox txtAddExpDate)
        {
            string errorMsg = "";
            
            //// Validate Dates
            if (txtAddMessageText.Text.Trim().Length < 1)
                errorMsg = errorMsg + "\n- Missing Message Text";
            if (!ValidateDate(txtAddEffDate.Text))
                errorMsg = errorMsg + "\n- Invalid Effective Date";
            if (!ValidateDate(txtAddExpDate.Text))
                errorMsg = errorMsg + "\n- Invalid Expiration Date";


            return errorMsg.Trim();
        }

        public bool ValidateDate(string dt)
        {
            if (!string.IsNullOrEmpty(dt))
            {
                string[] formats = { "M/d/yyyy", "M/d/yy" };
                DateTime value;

                if (!DateTime.TryParseExact(dt, formats, new CultureInfo("en-US"), DateTimeStyles.None, out value))
                {
                    return false;
                }
            }
            return true;
        }




        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError.Visible = false;
            BindGrid();

            bool userSelected = ddlUser.SelectedValue == "0" ? false : true;
            SetFooter(userSelected);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindGrid();
        }





        private void SortGridView(string sortExpression, string direction)
        {
            BindGrid();
            DataTable dt = GetViewState();
            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;

            gvCITMessage.DataSource = dv;
            gvCITMessage.DataBind();
        }


        private void SetFooter(bool showFooter)
        {
            TextBox txtMessageText = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddMessageText");
            txtMessageText.Enabled = showFooter;

            DropDownList ddlMessageCategory = (DropDownList)gvCITMessage.FooterRow.FindControl("ddlAddMessageCategory");
            populateCategoryDDL(ddlMessageCategory);
            ddlMessageCategory.Enabled = showFooter;


            TextBox txtAddFromUserID = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddFromUserID");
            txtAddFromUserID.Enabled = false;
            TextBox txtAddFromUserName = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddFromUserName");
            txtAddFromUserName.Enabled = false;

            
            TextBox txtAddToUserID = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddToUserID");
            txtAddToUserID.Enabled = false;
            TextBox txtAddToUserName = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddToUserName");
            txtAddToUserName.Enabled = false;


            TextBox txtAddToRoleID = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddToRoleID");
            txtAddToRoleID.Enabled = false;


            DropDownList ddlModuleMessageType = (DropDownList)gvCITMessage.FooterRow.FindControl("ddlAddModuleMessageType");
            populateModuleMessageTypeDDL(ddlModuleMessageType);
            ddlModuleMessageType.Enabled = showFooter;

            TextBox txtAddNumberOfViews = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddNumberOfViews");
            txtAddNumberOfViews.Enabled = false;


            TextBox txtAddEffDate = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddEffDate");
            txtAddEffDate.Enabled = showFooter;
            
            TextBox txtAddExpDate = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddExpDate");
            txtAddExpDate.Enabled = showFooter;
            
            TextBox txtAddCreateDate = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddCreateDate");
            txtAddCreateDate.Enabled = showFooter;

            txtAddEffDate.Text = showFooter ? DateTime.Now.ToShortDateString() : "n/a";
            txtAddExpDate.Text = showFooter ? DateTime.Now.AddMonths(1).ToShortDateString() : "n/a";
            txtAddToUserName.Text = showFooter ? ddlUser.SelectedItem.Text : "n/a";

            Button btnAdd = (Button)gvCITMessage.FooterRow.FindControl("btnAddRecord");
            btnAdd.Enabled = showFooter;

        }


#region GridView Event Handlers
        protected void gvCITMessage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCITMessage.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gvCITMessage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCITMessage.EditIndex = -1;
            BindGrid();

        }

        protected void gvCITMessage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblMessageId = (Label)gvCITMessage.Rows[e.RowIndex].FindControl("lblMessageId");
            Message m = new Message();
            m.MessageID = Int32.Parse(lblMessageId.Text);

            MessageController.DeleteMessage(m);
            gvCITMessage.EditIndex = -1;
            BindGrid();
        }

        protected void gvCITMessage_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Fired when Edit button clicked
            gvCITMessage.EditIndex = e.NewEditIndex;
            BindGrid();
        }


        protected void gvCITMessage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int addResult = 0;

            string errorMsg = "";
            Message m;
            if (e.CommandName.Equals("Add"))
            {
                ListItem selectedUser = (ListItem)ddlUser.SelectedItem;
                Label lblMessageID = (Label)gvCITMessage.FooterRow.FindControl("lblMessageID");
                TextBox txtAddMessageText = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddMessageText");
                DropDownList ddlAddMessageCategory = (DropDownList)gvCITMessage.FooterRow.FindControl("ddlAddMessageCategory");
                TextBox txtAddFromUserID = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddFromUserID");
                TextBox txtAddFromUserName = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddFromUserName");
                // TextBox txtAddToUserID = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddToUserID");
                TextBox txtAddToUserName = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddToUserName");
                TextBox txtAddToRoleID = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddToRoleID");
                DropDownList ddlAddModuleMessageType = (DropDownList)gvCITMessage.FooterRow.FindControl("ddlAddModuleMessageType");
                TextBox txtAddNumberOfViews = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddNumberOfViews");
                TextBox txtAddEffDate = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddEffDate");
                TextBox txtAddExpDate = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddExpDate");
                TextBox txtAddCreateDate = (TextBox)gvCITMessage.FooterRow.FindControl("txtAddCreateDate");

                lblError.Text = "";
                lblError.Visible = false;

                errorMsg = ValidateFields(txtAddMessageText, txtAddEffDate, txtAddExpDate).Trim();
                

                if (errorMsg.Length <= 0)
                {
                    m = new Message();
                    m.MessageText = txtAddMessageText.Text.Trim();
                    m.MessageCategory = ddlAddMessageCategory.SelectedValue;
                    m.FromUserID = CURRENT_USER.UserID;
                    m.ToUserID = Int32.Parse(selectedUser.Value);
                    m.ToRoleID = txtAddToRoleID.Text.Length > 0 ? Int32.Parse(txtAddToRoleID.Text) : 0;
                    m.ModuleMessageType = Int32.Parse(ddlAddModuleMessageType.SelectedValue);
                    m.NumberOfViews = 0;
                    m.EffDate = txtAddEffDate.Text.Trim().Length > 0 ? DateTime.Parse(txtAddEffDate.Text.Trim()) : DateTime.Now;
                    m.ExpDate = txtAddExpDate.Text.Trim().Length > 0 ? DateTime.Parse(txtAddExpDate.Text.Trim()) : DateTime.Now.AddYears(1);
                    m.CreateDate = DateTime.Now;
                    if (m.EffDate.CompareTo(m.ExpDate) > 0)
                    {   // if EffDate>ExpDate, swap them
                        DateTime t = m.EffDate;
                        m.EffDate = m.ExpDate;
                        m.ExpDate = t;
                    }

                    addResult = MessageController.AddMessage(m);

                    if (addResult == -1)
                    {
                        lblError.Text = "There was an error adding the Message.";
                        lblError.Visible = true;
                    }
                    else
                    {
                        lblError.Text = "";
                        lblError.Visible = false;
                    }
                }
                else
                {
                    lblError.Text = errorMsg;
                    lblError.Visible = true;
                }

                gvCITMessage.EditIndex = -1;
                BindGrid();
            }
        }

       

        protected void gvCITMessage_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int updateResult = 0;
            string errorMsg = "";
            Message m;

            ListItem selectedUser = (ListItem)ddlUser.SelectedItem;
            Label lblMessageID = (Label)gvCITMessage.Rows[e.RowIndex].FindControl("lblMessageID");
            TextBox txtMessageText = (TextBox)gvCITMessage.Rows[e.RowIndex].FindControl("txtMessageText");
            DropDownList ddlMessageCategory = (DropDownList)gvCITMessage.Rows[e.RowIndex].FindControl("ddlMessageCategory");
            TextBox txtFromUserID = (TextBox)gvCITMessage.Rows[e.RowIndex].FindControl("txtFromUserID");
            TextBox txtFromUserName = (TextBox)gvCITMessage.Rows[e.RowIndex].FindControl("txtFromUserName");
            
            //TextBox txtToUserID = (TextBox)gvCITMessage.Rows[e.RowIndex].FindControl("txtToUserID");
            string txtToUserID = selectedUser.Value;
            TextBox txtToUserName = (TextBox)gvCITMessage.Rows[e.RowIndex].FindControl("txtToUserName");
            TextBox txtToRoleID = (TextBox)gvCITMessage.Rows[e.RowIndex].FindControl("txtToRoleID");
            DropDownList ddlModuleMessageType = (DropDownList)gvCITMessage.Rows[e.RowIndex].FindControl("ddlModuleMessageType");
            TextBox txtNumberOfViews = (TextBox)gvCITMessage.Rows[e.RowIndex].FindControl("txtNumberOfViews");
            TextBox txtEffDate = (TextBox)gvCITMessage.Rows[e.RowIndex].FindControl("txtEffDate");
            TextBox txtExpDate = (TextBox)gvCITMessage.Rows[e.RowIndex].FindControl("txtExpDate");
            TextBox txtCreateDate = (TextBox)gvCITMessage.Rows[e.RowIndex].FindControl("txtCreateDate");

            lblError.Text = "";
            lblError.Visible = false;

            errorMsg = ValidateFields(txtMessageText, txtEffDate, txtExpDate).Trim();


            if (errorMsg.Length <= 0)
            {
                m = new Message();
                m.MessageID = Int32.Parse(lblMessageID.Text);
                m.MessageText = txtMessageText.Text.Trim();
                m.MessageCategory = ddlMessageCategory.SelectedValue;
                m.FromUserID = CURRENT_USER.UserID;
                m.ToUserID = Int32.Parse(txtToUserID);
                m.ToRoleID = txtToRoleID.Text.Length > 0 ? Int32.Parse(txtToRoleID.Text) : 0;
                m.ModuleMessageType = Int32.Parse(ddlModuleMessageType.SelectedValue);
                m.NumberOfViews = 0;
                m.EffDate = txtEffDate.Text.Trim().Length > 0 ? DateTime.Parse(txtEffDate.Text.Trim()) : DateTime.Now;
                m.ExpDate = txtExpDate.Text.Trim().Length > 0 ? DateTime.Parse(txtExpDate.Text.Trim()) : DateTime.Now.AddYears(1);
                m.CreateDate = DateTime.Now;
                if (m.EffDate.CompareTo(m.ExpDate) > 0)
                {   // if EffDate>ExpDate, swap them
                    DateTime t = m.EffDate;
                    m.EffDate = m.ExpDate;
                    m.ExpDate = t;
                }

                updateResult = MessageController.UpdateMessage(m);

                if (updateResult == -1)
                {
                    lblError.Text = "There was an error updating the Message.";
                    lblError.Visible = true;
                }
                else
                {
                    lblError.Text = "";
                    lblError.Visible = false;
                }
            }
            else
            {
                lblError.Text = errorMsg;
                lblError.Visible = true;
            }

            gvCITMessage.EditIndex = -1;
            BindGrid();
        }


        protected void gvCITMessage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Populates the control value for each of the records
            DataRowView drv = e.Row.DataItem as DataRowView;
            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataTable vs = GetViewState();
                    DropDownList dpCat = (DropDownList)e.Row.FindControl("ddlMessageCategory");
                    populateCategoryDDL(dpCat);
                    dpCat.SelectedIndex = MessageController.ConvertMessageCategoryToInt(vs.Rows[0]["MessageCategory"].ToString());

                    DropDownList dpType = (DropDownList)e.Row.FindControl("ddlModuleMessageType");
                    populateModuleMessageTypeDDL(dpType);
                    dpType.SelectedIndex = MessageController.ConvertMessageTypeToInt(vs.Rows[0]["ModuleMessageType"].ToString());

                }
                else if ((e.Row.RowState & DataControlRowState.Normal) > 0)
                {
                    Label lblStatus = (Label)e.Row.FindControl("lblModuleMessageType");
                    lblStatus.Text = "HERE";

                }
            }
        }

#endregion GridView Event Handlers


    }  // end Class
}  // end namespace