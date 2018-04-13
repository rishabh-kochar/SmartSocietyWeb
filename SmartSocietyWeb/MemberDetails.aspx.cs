﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MemberDetails : System.Web.UI.Page
{
    SSAPIGen.GeneralClient ServiceObjectGeneral = new SSAPIGen.GeneralClient();
    SSAPIAdmin.AdminClient ServiceObjectAdmin = new SSAPIAdmin.AdminClient();
    public static JArray FlatDetailsObjArr { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            BindData();
        }

    }

    private void BindData()
    {
        try
        {
            PlaceHolder2.Visible = false;
            FlatDetailsObjArr = JArray.Parse(ServiceObjectGeneral.GetAllResidentsDetails(0, 0).ToString());
            rptResident.DataSource = FlatDetailsObjArr;
            rptResident.DataBind();
        }
        catch(Exception e)
        {
            Response.Write("<script>alert(\"Something went wrong! Try Again.\")</script>");
        }
    }

 

  

    protected void back_Click(object sender, EventArgs e)
    {
        PlaceHolder2.Visible = false;
        PlaceHolder1.Visible = true;
    }

    protected void lnkbtnInfo_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtnInfo = (LinkButton)sender;
        int ID = Convert.ToInt32(lnkbtnInfo.CommandArgument);
        var SingleObj = (from ob in FlatDetailsObjArr
                         where Convert.ToInt32(ob["ResidentID"]) == ID
                         select ob).Single();
        litName.Text = SingleObj["ResidentName"].ToString();
        litDOB.Text = Convert.ToDateTime( SingleObj["DOB"]).ToLongDateString();
        litOccupation.Text = SingleObj["Occupation"].ToString();
        litContact1.Text = SingleObj["ContactNo1"].ToString() + "," + SingleObj["ContactNo2"].ToString();
        litEmail.Text = SingleObj["Email"].ToString();
        litFlatno.Text = SingleObj["FlatNo"].ToString();
        litPosition.Text =  SingleObj["PositionID"].ToString();
        PlaceHolder1.Visible = false;
        PlaceHolder2.Visible = true;
    }

    protected void InactiveResident_Click(object sender, EventArgs e)
    {

    }
}