using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfDemoWebApp.ServiceReference1;

namespace WcfDemoWebApp
{
    public partial class _Default : Page
    {
        ServiceReference1.Service1Client proxy;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    proxy = new ServiceReference1.Service1Client();
                    GridViewStudentDetails.DataSource = proxy.GetStudents();
                    GridViewStudentDetails.DataBind();
                }
                catch (FaultException<ExceptionMessage> exceptionFromService)
                {
                    lblMsg.Text = "Error while loading student details :" + exceptionFromService.Detail.errorMessageOfAction;
                }
                catch (Exception exception)
                {
                    lblMsg.Text = "Error while loading student details :" + exception.Message;
                }
            }
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                long StudentId = 0;
                if (txtStudentId.Text != null && txtStudentId.Text != string.Empty)
                {
                    StudentId = Convert.ToInt64(txtStudentId.Text);
                }
                string FirstName = txtFirstName.Text.Trim();
                string LastName = txtLastName.Text.Trim();
                string RegisterNo = txtRegisterNo.Text.Trim();
                string Department = txtDepartment.Text.Trim();

                proxy = new ServiceReference1.Service1Client();
                ServiceReference1.Student newStudent =
                new ServiceReference1.Student()
                {
                    StudentId = StudentId,
                    FirstName = FirstName,
                    LastName = LastName,
                    RegisterNo = RegisterNo,
                    Department = Department
                };

                proxy.AddStudents(newStudent);

                GridViewStudentDetails.DataSource = proxy.GetStudents();
                GridViewStudentDetails.DataBind();
                lblMsg.Text = "Record Saved Successfully";
            }
            catch (FaultException<ExceptionMessage> exceptionFromService)
            {
                if (ButtonInsert.Visible == true)
                {
                    lblMsg.Text = "Error while adding new customer details :" + exceptionFromService.Detail.errorMessageOfAction;
                }
                else
                {
                    lblMsg.Text = "Error while updating customer details :" + exceptionFromService.Detail.errorMessageOfAction;
                }
            }
            catch (Exception exception)
            {
                if (ButtonInsert.Visible == true)
                {
                    lblMsg.Text = "Error while adding new customer details :" + exception.Message;
                }
                else
                {
                    lblMsg.Text = "Error while updating customer details :" + exception.Message;
                }
            }

            ResetAll();
        }

        protected void GridViewStudentDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtStudentId.Text = GridViewStudentDetails.DataKeys[GridViewStudentDetails.SelectedRow.RowIndex].Value.ToString();
            txtFirstName.Text = (GridViewStudentDetails.SelectedRow.FindControl("lblFirstName") as Label).Text;
            txtLastName.Text = (GridViewStudentDetails.SelectedRow.FindControl("lblLastName") as Label).Text;
            txtRegisterNo.Text = (GridViewStudentDetails.SelectedRow.FindControl("lblRegisterNo") as Label).Text;
            txtDepartment.Text = (GridViewStudentDetails.SelectedRow.FindControl("lblDepartment") as Label).Text;
            //make invisible Insert button during update/delete
            ButtonInsert.Visible = false;
            ButtonUpdate.Visible = true;
            ButtonDelete.Visible = true;
            ButtonCancel.Visible = true;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                long StudentId = Convert.ToInt64(txtStudentId.Text);
                proxy = new ServiceReference1.Service1Client();
                proxy.DeleteStudent(StudentId);
            }
            catch (FaultException<ExceptionMessage> exceptionFromService)
            {
                lblMsg.Text = "Error while deleteing student details :" + exceptionFromService.Detail.errorMessageOfAction;
            }
            catch (Exception exception)
            {
                lblMsg.Text = "Error while deleteing student details :" + exception.Message;
            }
        }

        private void ResetAll()
        {
            ButtonInsert.Visible = true;
            ButtonUpdate.Visible = false;
            ButtonDelete.Visible = false;
            ButtonCancel.Visible = false;
            txtStudentId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtRegisterNo.Text = "";
            txtDepartment.Text = "";
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ResetAll();
        }
    }
}