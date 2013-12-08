using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;
using DotNetNuke.Entities.Modules;

namespace PMCC.Modules.CITMessages.Components
{
   

    public class Message
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public string MessageCategory { get; set; }
        public int FromUserID { get; set; }
        public int ToUserID { get; set; }
        public int ToRoleID { get; set; }
        public int ModuleMessageType { get; set; }
        public int NumberOfViews { get; set; }
        public DateTime EffDate { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime CreateDate { get; set; }

        public void Add()
        {
            MessageController.AddMessage(this);
        }  // end Add()

        public void Delete()
        {
            MessageController.DeleteMessage(this);
        }  // end Delete()

        public void Update()
        {
            MessageController.UpdateMessage(this);
        }  // end Update()

    }  // end class
}  // end namespace