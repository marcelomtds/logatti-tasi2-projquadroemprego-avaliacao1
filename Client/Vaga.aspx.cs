using Model;
using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Vaga : System.Web.UI.Page
    {
        Entities context = new Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridView();
                LoadCombos();
                txtTitulo.Focus();
            }
        }
        private void LoadCombos()
        {
            ddlTipoEmprego.DataSource = context.TB_TIPO_EMPREGO.ToList();
            ddlTipoEmprego.DataTextField = "descricao";
            ddlTipoEmprego.DataValueField = "id";
            ddlTipoEmprego.DataBind();

            ddlEmpresa.DataSource = context.TB_EMPRESA.ToList();
            ddlEmpresa.DataTextField = "nome";
            ddlEmpresa.DataValueField = "id";
            ddlEmpresa.DataBind();
        }
        private void LoadGridView()
        {
            gvResult.DataSource = context.TB_VAGA.ToList();
            gvResult.DataBind();
        }
        private void DisplayModal(Page page)
        {
            ClientScript.RegisterStartupScript(typeof(Page), Guid.NewGuid().ToString(), "openModalConfirmation();", true);
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            try
            {
                int id = Convert.ToInt32(lblSelectedId.Text);
                context.TB_VAGA.Remove(context.TB_VAGA.First(x => x.id == id));
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
                        TB_VAGA vagaResult = context.TB_VAGA.First(x => x.id == id);
                        vagaResult.titulo = txtTitulo.Text;
                        vagaResult.descricao = txtDescricao.Text;
                        vagaResult.salario = decimal.Parse(txtSalario.Text);
                        vagaResult.id_empresa = int.Parse(ddlEmpresa.SelectedValue.ToString());
                        vagaResult.id_tipo_emprego = int.Parse(ddlTipoEmprego.SelectedValue.ToString());
                        SendMessage("Registro alterado com sucesso.", Color.Green);
                    }
                    else
                    {
                        TB_VAGA vaga = new TB_VAGA()
                        {
                            titulo = txtTitulo.Text,
                            descricao = txtDescricao.Text,
                            salario = decimal.Parse(txtSalario.Text),
                            id_empresa = int.Parse(ddlEmpresa.SelectedValue.ToString()),
                            id_tipo_emprego = int.Parse(ddlTipoEmprego.SelectedValue.ToString())
                        };
                        context.TB_VAGA.Add(vaga);
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
            txtTitulo.Text = String.Empty;
            txtDescricao.Text = String.Empty;
            txtSalario.Text = String.Empty;
            ddlEmpresa.SelectedValue = null;
            ddlTipoEmprego.SelectedValue = null;
            txtTitulo.Focus();
        }
        private void LoadDataPage()
        {
            int id = Convert.ToInt32(lblSelectedId.Text);
            var result = context.TB_VAGA.First(x => x.id == id);
            txtId.Text = result.id.ToString();
            txtTitulo.Text = result.titulo;
            txtDescricao.Text = result.descricao;
            txtSalario.Text = result.salario.ToString();
            ddlEmpresa.SelectedValue = result.id_empresa.ToString();
            ddlTipoEmprego.SelectedValue = result.id_tipo_emprego.ToString();
            txtDescricao.Focus();
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
            return String.IsNullOrWhiteSpace(txtTitulo.Text)
                || String.IsNullOrWhiteSpace(txtDescricao.Text)
                || String.IsNullOrWhiteSpace(txtSalario.Text)
                || String.IsNullOrWhiteSpace(ddlEmpresa.SelectedValue)
                || String.IsNullOrWhiteSpace(ddlTipoEmprego.SelectedValue);
        }
    }
}