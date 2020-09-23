<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Vaga.aspx.cs" Inherits="Client.Vaga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function openModalConfirmation() {
            $('#modalConfirmation').modal('show');
        };
    </script>
    <style>
        .resize-none {
            resize: none;
        }
    </style>
    <div class="container">
        <form id="form" runat="server">
            <asp:Label ID="lblSelectedId" runat="server" Text="" Visible="false"></asp:Label>
            <div class="row">
                <div class="col-12">
                    <div class="form-group">
                        <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-1">
                    <div class="form-group">
                        <asp:Label ID="lblId" runat="server" Text="ID"></asp:Label>
                        <asp:TextBox class="form-control" ReadOnly="true" ID="txtId" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <asp:Label ID="lblEmpresa" runat="server" Text="Empresa"></asp:Label>
                        <asp:DropDownList CssClass="form-control" ID="ddlEmpresa" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <asp:Label ID="lblTipoEmprego" runat="server" Text="Tipo de Emprego"></asp:Label>
                        <asp:DropDownList CssClass="form-control" ID="ddlTipoEmprego" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-group">
                        <asp:Label ID="lblSalario" runat="server" Text="Salário"></asp:Label>
                        <asp:TextBox class="form-control" MaxLength="9" ID="txtSalario" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-6">
                    <div class="form-group">
                        <asp:Label ID="lblTitulo" runat="server" Text="Título"></asp:Label>
                        <asp:TextBox class="form-control" MaxLength="100" ID="txtTitulo" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-6">
                    <div class="form-group">
                        <asp:Label ID="lblDescricao" runat="server" Text="Descrição"></asp:Label>
                        <asp:TextBox class="form-control resize-none" MaxLength="5000" TextMode="MultiLine" Columns="50" Rows="5" ID="txtDescricao" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="form-group">
                        <asp:Button class="btn btn-success" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                        <asp:Button class="btn btn-warning" ID="btnLimpar" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />
                    </div>
                </div>
            </div>
            <asp:GridView ID="gvResult" runat="server" CellPadding="4" CssClass="table" ForeColor="#333333"
                GridLines="None" AutoGenerateColumns="False" OnRowCommand="GVResult_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#427bff" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="titulo" HeaderText="Título" />
                    <asp:BoundField DataField="descricao" HeaderText="Descrição" />
                    <asp:BoundField DataField="salario" HeaderText="Salário" />
                    <asp:BoundField DataField="TB_EMPRESA.nome" HeaderText="Empresa" />
                    <asp:BoundField DataField="TB_TIPO_EMPREGO.descricao" HeaderText="Tipo de Emprego" />
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="btnAlterarMidia" CommandName="ALTERAR"
                                CommandArgument='<%# Eval("id") %>' CssClass="btn btn btn-warning" Text="Alterar" />
                            <asp:LinkButton runat="server" ID="btnExcluirMidia" CommandName="EXCLUIR"
                                CommandArgument='<%# Eval("id") %>' CssClass="btn btn btn-danger" Text="Excluir" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="modal fade" id="modalConfirmation" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLongTitle">Confirmação de Exclusão</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            Deseja excluir o registro selecionado?
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                            <asp:Button ID="btnConfirm" CssClass="btn btn-danger" runat="server" Text="Confirmar" OnClick="btnConfirm_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
