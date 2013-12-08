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

using System.Collections.Generic;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace PMCC.Modules.CITMessages.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for CITMessages
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController //: IPortable, ISearchable, IUpgradeable
    {


        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        public string ExportModule(int ModuleID)
        {
            //string strXML = "";

            //List<CITMessagesInfo> colCITMessagess = GetCITMessagess(ModuleID);
            //if (colCITMessagess.Count != 0)
            //{
            //    strXML += "<CITMessagess>";

            //    foreach (CITMessagesInfo objCITMessages in colCITMessagess)
            //    {
            //        strXML += "<CITMessages>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objCITMessages.Content) + "</content>";
            //        strXML += "</CITMessages>";
            //    }
            //    strXML += "</CITMessagess>";
            //}

            //return strXML;

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            //XmlNode xmlCITMessagess = DotNetNuke.Common.Globals.GetContent(Content, "CITMessagess");
            //foreach (XmlNode xmlCITMessages in xmlCITMessagess.SelectNodes("CITMessages"))
            //{
            //    CITMessagesInfo objCITMessages = new CITMessagesInfo();
            //    objCITMessages.ModuleId = ModuleID;
            //    objCITMessages.Content = xmlCITMessages.SelectSingleNode("content").InnerText;
            //    objCITMessages.CreatedByUser = UserID;
            //    AddCITMessages(objCITMessages);
            //}

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        {
            //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

            //List<CITMessagesInfo> colCITMessagess = GetCITMessagess(ModInfo.ModuleID);

            //foreach (CITMessagesInfo objCITMessages in colCITMessagess)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objCITMessages.Content, objCITMessages.CreatedByUser, objCITMessages.CreatedDate, ModInfo.ModuleID, objCITMessages.ItemId.ToString(), objCITMessages.Content, "ItemId=" + objCITMessages.ItemId.ToString());
            //    SearchItemCollection.Add(SearchItem);
            //}

            //return SearchItemCollection;

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        public string UpgradeModule(string Version)
        {
            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        #endregion

    }

}
