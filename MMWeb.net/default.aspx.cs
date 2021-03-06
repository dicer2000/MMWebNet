﻿using System;
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
                int nGameID = mmd.P2_MM_Initialize(strPlayer1, 6);

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
            DataSet ds = mmd.P2_MM_GetBoard(GameID);

            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
                throw new Exception("Game not found");

            // The first table has the Game Info
            DataTable dtGame = ds.Tables[0];
            // Fill the game info
            lblName.Text = ds.Tables[0].Rows[0]["Player1"].ToString();

            // The second table has the Game Board layout
            DataTable dt = ds.Tables[1];

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

                if (dt == null || dt.Rows == null || dt.Rows.Count < 1)
                {
                    lblError.Text = "This game is complete.  Start a new game.";
                    lblError.Visible = true;
                    return;
                }
                else
                    lblError.Visible = false;

                // Get the number of black and white pegs
                int nNumberCorrectPosition = 0;
                int nNumberCorrectColor = 0;
                if (dt.Rows[0]["NumberCorrectPosition"] != null && 
                    dt.Rows[0]["NumberCorrectPosition"] != System.DBNull.Value)
                    nNumberCorrectPosition = (int)dt.Rows[0]["NumberCorrectPosition"];
                if (dt.Rows[0]["NumberCorrectColor"] != null &&
                    dt.Rows[0]["NumberCorrectColor"] != System.DBNull.Value)
                    nNumberCorrectColor = (int)dt.Rows[0]["NumberCorrectColor"];

                if(nNumberCorrectPosition==4)
                {
                    lblNoGuesses.Text = (index+1).ToString();
                    litWinDialog.Visible = true;
                    return;
                }
                else
                    litWinDialog.Visible = false;


                CreateNewGameBoard(nGameID);


            }

        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            litWinDialog.Visible = false;

            // Start a new game
            cmdGo_Click(null, null);
        }

        protected void cmdDone_Click(object sender, EventArgs e)
        {
            litWinDialog.Visible = false;

            // Reset entire game
            txtHandle.Text = "";
            hdnGameID.Value = "";
            pnlName.Visible = true;
            pnlGame.Visible = false;

        }

    }
}