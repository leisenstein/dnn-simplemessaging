using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMCC.Modules.CITMessages.Data;
using DotNetNuke.Common.Utilities;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;
using DotNetNuke.UI.Skins.Controls;
using DotNetNuke.UI.Skins;


namespace PMCC.Modules.CITMessages.Components
{
    public class MessageController
    {

        public static IDataReader GetActiveUsers()
        {
            return DataProvider.Instance().GetActiveUsers();
        }

        public static IDataReader GetActiveCategories()
        {
            return DataProvider.Instance().GetActiveCategories();
        }


        public static int AddMessage(Message c)
        {
            int result = 0;
            if (c.MessageID == null || c.MessageID < 1)
            {   // new Message Record
                result = DataProvider.Instance().AddMessage(c);
                return result;
            }
            else
            {   // update and existing record
                result = -1;
                return result;
            }
        }

        public static int DeleteMessage(Message c)
        {
            int result = 0;
            if (c.MessageID == null || c.MessageID < 1)
            {   result = -1;
                return result;
            }
            else
            {   // update and existing record
                result = DataProvider.Instance().DeleteMessage(c);
                return result; 
            }
        }



        public static int UpdateMessage(Message c)
        {
            int result = 0;
            if (c.MessageID == null || c.MessageID < 1)
            {
                result = -1;
                return result;
            }
            else
            {
                result = DataProvider.Instance().UpdateMessage(c);
                return result;
            }
        }


        public static DataTable GetMessages(int userID, int iNumOfViews)
        {
            DataTable dt = DataProvider.Instance().GetMessages(userID, iNumOfViews);
            return dt;
        }



        public static DataTable GetMessagesForEditor(int userID)
        {
            DataTable dt = DataProvider.Instance().GetMessagesForEditor(userID);
            return dt;
        }



        public static ModuleMessage.ModuleMessageType ConvertIntToMessageType(int i)
        {
            switch (i)
            {
                case 0:
                    return ModuleMessage.ModuleMessageType.GreenSuccess;
                    break;
                case 1:
                    return ModuleMessage.ModuleMessageType.YellowWarning;
                    break;
                case 2:
                    return ModuleMessage.ModuleMessageType.RedError;
                    break;
                case 3:
                    return ModuleMessage.ModuleMessageType.BlueInfo;
                    break;
                default:
                    return ModuleMessage.ModuleMessageType.GreenSuccess;
            }
        }

        public static int ConvertMessageCategoryToInt(string s)
        {
            switch (s)
            {
                case "PQRS":
                    return 0;
                    break;
                case "CLAIMS":
                    return 1;
                    break;
                case "MESSAGE":
                    return 2;
                    break;
                case "OTHER":
                    return 3;
                    break;
                default:
                    return 3;
            }
        }


        public static int ConvertMessageTypeToInt(string s)
        {
            switch (s)
            {
                case "Green Success":
                    return 0;
                    break;
                case "Yellow Warning":
                    return 1;
                    break;
                case "Red Error":
                    return 2;
                    break;
                case "Blue Info":
                    return 3;
                    break;
                default:
                    return 3;
            }
        }

        public static DataTable CreateFakeCITMessageTable()
        {
            DataTable fakeTable = new DataTable();
            fakeTable.Columns.Add("MessageID", typeof(int));
            fakeTable.Columns.Add("MessageText", typeof(string));
            fakeTable.Columns.Add("MessageCategory", typeof(string));
            fakeTable.Columns.Add("FromUserID", typeof(int));
            fakeTable.Columns.Add("FromUserName", typeof(string));
            fakeTable.Columns.Add("ToUserID", typeof(int));
            fakeTable.Columns.Add("ToUserName", typeof(string));
            fakeTable.Columns.Add("ToRoleID", typeof(int));
            fakeTable.Columns.Add("ModuleMessageType", typeof(int));
            fakeTable.Columns.Add("NumberOfViews", typeof(int));
            fakeTable.Columns.Add("EffDate", typeof(DateTime));
            fakeTable.Columns.Add("ExpDate", typeof(DateTime));
            fakeTable.Columns.Add("CreateDate", typeof(DateTime));
            fakeTable.Rows.Add(-1, "n/a", "n/a", -1, "n/a", -1, "n/a", -1, -1, -1, DateTime.Now, DateTime.Now, DateTime.Now);
            return fakeTable;
        }

    }  // end Class MessageController
}  // end namespace