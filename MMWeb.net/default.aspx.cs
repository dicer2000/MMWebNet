using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MMDataLib;
using MMWeb.net.lib;

namespace MMWeb.net
{
    public partial class _default : System.Web.UI.Page
    {
        // Used to keep track of what game we're in
        private int _nGameID = 0;

        // Connection String
        private string GetConnectionString()
        {
            System.Configuration.Configuration rootWebConfig =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                return rootWebConfig.ConnectionStrings.ConnectionStrings["ConnectionString"].ToString();
            else
                return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["DebugGameID"] != null)
                {
                    // Load a game and show
                    string strGameID = AntiSqlInjection.ValidateSqlValue(Request.QueryString["DebugGameID"]);
                    Int32.TryParse(strGameID, out _nGameID);
                    hdnGameID.Value = _nGameID.ToString();

                    // Successfully got existing game
                    if (_nGameID > 0)
                    {
                        CreateNewGameBoard(_nGameID);

                        pnlName.Visible = false;
                        pnlGame.Visible = true;
                        return;
                    }
                }
            }
        }

        protected void cmdGo_Click(object sender, EventArgs e)
        {
            try
            {
                string strPlayer1 = AntiSqlInjection.ValidateSqlValue(txtHandle.Text);
                lblName.Text = strPlayer1;

                MMData mmd = new MMData(GetConnectionString());
                int nGameID = mmd.P2_MM_Initialize(strPlayer1);

                CreateNewGameBoard(nGameID);
                hdnGameID.Value = nGameID.ToString();

                pnlName.Visible = false;
                pnlGame.Visible = true;

            }
            catch (Exception ex)
            {
                return;
            }
        }

        void CreateNewGameBoard(int GameID)
        {
            MMData mmd = new MMData(GetConnectionString());
            // Get this game's board
            DataTable dt = mmd.P2_MM_GetBoard(GameID);
            int guessRow = dt.Rows.Count;
            int origRow = guessRow;

            // Fill up a half-empty game with rows
            for (; guessRow < 10; guessRow++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = (guessRow + 1);
                dr[1] = (int)MMPosStates.unselected;
                dr[2] = (int)MMPosStates.unselected;
                dr[3] = (int)MMPosStates.unselected;
                dr[4] = (int)MMPosStates.unselected;
                dr[5] = (int)MMPosStates.unselected;
                dt.Rows.Add(dr);
            }

            gvGame.DataSource = dt;
            gvGame.EditIndex = origRow;
            gvGame.DataBind();

        }


        protected void gvGame_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddMove")
            {
                // Retrieve the row index stored in the CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button from the Rows collection.
                GridViewRow row = gvGame.Rows[index];

                // Get the individual values from the hidden controls
                HiddenField hf0 = (HiddenField)row.FindControl("hiddenPos0");
                HiddenField hf1 = (HiddenField)row.FindControl("hiddenPos1");
                HiddenField hf2 = (HiddenField)row.FindControl("hiddenPos2");
                HiddenField hf3 = (HiddenField)row.FindControl("hiddenPos3");

                // Check they are available
                if ((hf0 == null || hf0.Value.Length < 1) ||
                    (hf1 == null || hf1.Value.Length < 1) ||
                    (hf2 == null || hf2.Value.Length < 1) ||
                    (hf3 == null || hf3.Value.Length < 1))
                    throw new Exception("Please enter a value for each field.");

                // Check for SQL Injection
                string strGameID = AntiSqlInjection.ValidateSqlValue(hdnGameID.Value);
                string strG0 = AntiSqlInjection.ValidateInteger(hf0.Value);
                string strG1 = AntiSqlInjection.ValidateInteger(hf1.Value);
                string strG2 = AntiSqlInjection.ValidateInteger(hf2.Value);
                string strG3 = AntiSqlInjection.ValidateInteger(hf3.Value);

                // Convert to Integer in prep for inserting into DB
                int nGameID = Int32.Parse(strGameID);
                int nG0 = Int32.Parse(strG0);
                int nG1 = Int32.Parse(strG1);
                int nG2 = Int32.Parse(strG2);
                int nG3 = Int32.Parse(strG3);

                // Create the connection to the DB and insert it
                MMData mmd = new MMData(GetConnectionString());
                DataTable dt = mmd.P2_MM_NewMove(nGameID, nG0, nG1, nG2, nG3);
                CreateNewGameBoard(nGameID);
            }

        }
    }
}