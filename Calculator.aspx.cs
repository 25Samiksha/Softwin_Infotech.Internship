using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace calculatorDemo
{
    public partial class Calculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            double num1 = Convert.ToDouble(txtnum1.Text);
            double num2 = Convert.ToDouble(txtnum2.Text);

            double result = num1 + num2;

            txtResult.Text = result.ToString();
        }

        protected void btnSub_Click(object sender, EventArgs e)
        {
            double num1 = Convert.ToDouble(txtnum1.Text);
            double num2 = Convert.ToDouble(txtnum2.Text);

            double result = num1 - num2;

            txtResult.Text = result.ToString();
        }

        protected void btnMul_Click(object sender, EventArgs e)
        {
            double num1 = Convert.ToDouble(txtnum1.Text);
            double num2 = Convert.ToDouble(txtnum2.Text);

            double result = num1 * num2;

            txtResult.Text = result.ToString();
        }

        protected void btnDiv_Click(object sender, EventArgs e)
        {
            double num1 = Convert.ToDouble(txtnum1.Text);
            double num2 = Convert.ToDouble(txtnum2.Text);

            double result = num1 / num2;

            txtResult.Text = result.ToString();
        }
    }
}