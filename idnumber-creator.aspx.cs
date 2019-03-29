using System;

public partial class idnumber_creator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void B_Submit_Click(object sender, EventArgs e)
    {
        if (TB_Area.Text.Trim().Length != 6)
        {
            EZGoal.Common.Message(this, "地区码不正确！");
            return;
        }
        if (TB_Date.Text.Trim().Length != 8)
        {
            EZGoal.Common.Message(this, "日期码不正确！");
            return;
        }
        if (TB_Rank.Text.Trim().Length != 3)
        {
            EZGoal.Common.Message(this, "顺序码不正确！");
            return;
        }
        int[] numbers = new int[17];
        int[] factors = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        int sum = 0;
        string end = "10X98765432";

        for (int i = 0; i < 6; i++)
        {
            numbers[i] = int.Parse(TB_Area.Text.Trim().Substring(i, 1));
        }
        for (int i = 0; i < 8; i++)
        {
            numbers[i + 6] = int.Parse(TB_Date.Text.Trim().Substring(i, 1));
        }
        for (int i = 0; i < 3; i++)
        {
            numbers[i + 14] = int.Parse(TB_Rank.Text.Trim().Substring(i, 1));
        }
        for (int i = 0; i < 17; i++)
        {
            sum += numbers[i] * factors[i];
        }
        L_Result.Text = TB_Area.Text.Trim() + TB_Date.Text.Trim() + TB_Rank.Text.Trim() + end.Substring(sum % 11, 1);
    }

}