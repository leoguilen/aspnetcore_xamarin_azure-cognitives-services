using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using SmartAuth.Models;
using SmartAuth.Services;
using SmartAuth.View.Popup;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartAuth.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormularioCadastro : ContentPage
    {
        private readonly MediaFile _file;
        private readonly SmartAuthService _service;
        private Usuario _usuario;

        public FormularioCadastro(MediaFile file)
        {
            InitializeComponent();

            _file = file;
            _service = new SmartAuthService();
        }

        protected override async void OnAppearing()
        {
            try
            {
                var usuarioInfo = await _service.ExtractUsuarioInfo(
                    _file.GetStream(),
                    $"doc_{Guid.NewGuid()}.jpg");

                if (usuarioInfo is null)
                {
                    loader.IsVisible = false;
                    errorMsg.Text = "Algo deu errado ao extrair as informações da imagem, tente tirar outra foto ou processar novamente";
                    error.IsVisible = true;
                    return;
                }

                _usuario = usuarioInfo.Content;

                entry_nome.Text = usuarioInfo?.Content?.Nome;
                entry_sobrenome.Text = usuarioInfo?.Content?.Sobrenome;
                dp_dtNasc.Date = usuarioInfo.Content.DataNascimento;
                entry_rg.Text = usuarioInfo?.Content?.Rg;
                entry_cpf.Text = usuarioInfo?.Content?.Cpf;

                loader.IsVisible = false;
                form.IsVisible = true;
            }
            catch (Exception ex)
            {
                loader.IsVisible = false;
                errorMsg.Text = ex.Message;
                error.IsVisible = true;
            }

            base.OnAppearing();
        }

        private void TentarNovamente_Clicked(object sender, EventArgs e)
        {
            error.IsVisible = false;
            loader.IsVisible = true;

            OnAppearing();
        }

        private async void NovaFoto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        private async void Cancelar_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Confirmar cancelamento", "Ao cancelar as informações seram perdidas. Deseja realmente cancelar a operação?", "Sim", "Não");

            if (result is true)
                await Navigation.PopAsync(true);

            return;
        }

        private async void Confirmar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new ConfirmarEmailPopup(_usuario), true);
        }
    }
}