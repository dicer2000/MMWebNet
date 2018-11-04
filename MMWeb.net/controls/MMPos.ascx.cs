using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MMWeb.net.lib;

namespace MMWeb.net.controls
{
    
    [System.ComponentModel.DefaultBindingProperty("CurrentState")]
    public partial class MMPos : System.Web.UI.UserControl
    {
//        private MMPosStates _currentState = MMPosStates.unselected;
        public string CurrentState
        {
            set
            {
                ViewState["CurrentState"] = value;
                ReRenderControl();
            }
        }

        private MMPosStates GetPosState()
        {
            if (ViewState["CurrentState"] == null)
                return MMPosStates.unselected;
            string strPosState = ViewState["CurrentState"].ToString();
            bool test = Enum.TryParse(strPosState, out MMPosStates val);
            return val;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ReRenderControl();
        }

        protected void ReRenderControl()
        {
            switch(GetPosState())
            {
                case MMPosStates.unselected:
                    lnkMMPos.Attributes.Add("style", "color: #cccccc");
                    lnkMMPos.Attributes.Add("class", "far fa-circle butt");
                    break;
                case MMPosStates.current:
                    lnkMMPos.Attributes.Add("style", "color: black");
                    lnkMMPos.Attributes.Add("class", "far fa-circle butt");
                    break;
                case MMPosStates.red:
                    lnkMMPos.Attributes.Add("style", "color: red");
                    lnkMMPos.Attributes.Add("class", "fas fa-circle butt");
                    break;
                case MMPosStates.blue:
                    lnkMMPos.Attributes.Add("style", "color: dodgerblue");
                    lnkMMPos.Attributes.Add("class", "fas fa-circle butt");
                    break;
                case MMPosStates.green:
                    lnkMMPos.Attributes.Add("style", "color: forestgreen");
                    lnkMMPos.Attributes.Add("class", "fas fa-circle butt");
                    break;
                case MMPosStates.yellow:
                    lnkMMPos.Attributes.Add("style", "color: gold");
                    lnkMMPos.Attributes.Add("class", "fas fa-circle butt");
                    break;
                case MMPosStates.magenta:
                    lnkMMPos.Attributes.Add("style", "color: magenta");
                    lnkMMPos.Attributes.Add("class", "fas fa-circle butt");
                    break;
                case MMPosStates.cyan:
                    lnkMMPos.Attributes.Add("style", "color: cyan");
                    lnkMMPos.Attributes.Add("class", "fas fa-circle butt");
                    break;
                default:
                    lnkMMPos.Attributes.Add("class", "far fa-circle butt");
                    lnkMMPos.Attributes.Add("style", "color: #ffec99");
                    break;
            }
        }
    }
}