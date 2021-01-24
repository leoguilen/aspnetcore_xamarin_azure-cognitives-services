using Rg.Plugins.Popup.Extensions;
using SmartAuth.Models;
using System;
using Xamarin.Forms.Xaml;

namespace SmartAuth.View.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesUsuarioPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public DetalhesUsuarioPopup(Usuario usuario)
        {
            InitializeComponent();

            lbl_nome.Text = usuario.Nome;
            lbl_sobrenome.Text = usuario.Sobrenome;
            lbl_email.Text = usuario.Email;
            lbl_rg.Text = usuario.Rg;
            lbl_cpf.Text = usuario.Cpf;
            lbl_dtNasc.Text = usuario.DataNascimento.ToShortDateString();
            lbl_dtCad.Text = usuario.DataCadastro.ToShortDateString();
        }

        private async void Fechar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
            
            lbl_nome.Text = string.Empty;
            lbl_sobrenome.Text = string.Empty;
            lbl_email.Text = string.Empty;
            lbl_rg.Text = string.Empty;
            lbl_cpf.Text = string.Empty;
            lbl_dtNasc.Text = string.Empty;
            lbl_dtCad.Text = string.Empty;
        }
    }
}