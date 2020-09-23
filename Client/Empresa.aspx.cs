using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Empresa : System.Web.UI.Page
    {
        Entities context = new Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridView();
                LoadComboSetor();
                txtNome.Focus();
            }
        }
        private void LoadComboSetor()
        {
            ddlSetor.DataSource = context.TB_SETOR.ToList();
            ddlSetor.DataTextField = "descricao";
            ddlSetor.DataValueField = "id";
            ddlSetor.DataBind();
        }
        private void LoadGridView()
        {
            gvResult.DataSource = context.TB_EMPRESA.ToList();
            gvResult.DataBind();
        }
        private void DisplayModal(Page page)
        {
            ClientScript.RegisterStartupScript(typeof(Page), Guid.NewGuid().ToString(), "openModalConfirmation();", true);
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lblSelectedId.Text);
            if (context.TB_VAGA.FirstOrDefault(x => x.TB_EMPRESA.id == id) == null)
            {
                try
                {
                    context.TB_EMPRESA.Remove(context.TB_EMPRESA.First(x => x.id == id));
                    context.SaveChanges();
                    SendMessage("Registro excluído com sucesso.", Color.Green);
                    LoadGridView();
                    ResetForm(true);
                }
                catch (Exception ex)
                {
                    SendMessage($"Ocorreu um erro inesperado: {ex.Message}", Color.Red);
                }
            }
            else
            {
                SendMessage("Não é possível excluir, pois existe um vínculo com o registro.", Color.Red);

            }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (IsInvalidForm())
            {
                SendMessage("Campos obrigatórios não preenchidos.", Color.Red);
            }
            else
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(txtId.Text))
                    {
                        int id = int.Parse(txtId.Text);
                        TB_EMPRESA empresaResult = context.TB_EMPRESA.First(x => x.id == id);
                        empresaResult.nome = txtNome.Text;
                        empresaResult.telefone = txtTelefone.Text;
                        empresaResult.id_setor = int.Parse(ddlSetor.SelectedValue.ToString());
                        SendMessage("Registro alterado com sucesso.", Color.Green);
                    }
                    else
                    {
                        TB_EMPRESA empresa = new TB_EMPRESA()
                        {
                            nome = txtNome.Text,
                            telefone = txtTelefone.Text,
                            id_setor = int.Parse(ddlSetor.SelectedValue.ToString())
                        };
                        context.TB_EMPRESA.Add(empresa);
                        SendMessage("Registro criado com sucesso.", Color.Green);
                    }
                    context.SaveChanges();
                    ResetForm(true);
                    LoadGridView();
                }
                catch (Exception ex)
                {
                    SendMessage($"Ocorreu um erro inesperado: {ex.Message}", Color.Red);
                }
            }

        }
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = String.Empty;
            ResetForm(false);
        }
        private void ResetForm(bool clearId)
        {
            if (clearId)
            {
                txtId.Text = String.Empty;
            }
            txtNome.Text = String.Empty;
            txtTelefone.Text = String.Empty;
            ddlSetor.SelectedValue = null;
            txtNome.Focus();
        }
        private void LoadDataPage()
        {
            int id = Convert.ToInt32(lblSelectedId.Text);
            var result = context.TB_EMPRESA.First(x => x.id == id);
            txtId.Text = result.id.ToString();
            txtNome.Text = result.nome;
            txtTelefone.Text = result.telefone;
            ddlSetor.SelectedValue = result.id_setor.ToString();
            txtNome.Focus();
        }
        private void SendMessage(string message, Color color)
        {
            lblMensagem.Text = message;
            lblMensagem.ForeColor = color;
            lblMensagem.Font.Bold = true;
        }
        protected void GVResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idItem = int.Parse(e.CommandArgument.ToString());
            lblSelectedId.Text = idItem.ToString();
            if (e.CommandName == "ALTERAR")
            {
                LoadDataPage();
            }
            else if (e.CommandName == "EXCLUIR")
            {
                DisplayModal(this);
            }
        }
        private Boolean IsInvalidForm()
        {
            return String.IsNullOrWhiteSpace(txtNome.Text)
                || String.IsNullOrWhiteSpace(txtTelefone.Text)
                || String.IsNullOrWhiteSpace(ddlSetor.SelectedValue);
        }
    }
}