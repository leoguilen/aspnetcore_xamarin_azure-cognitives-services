using Rg.Plugins.Popup.Extensions;
using SmartAuth.Models;
using SmartAuth.Services;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Xamarin.Forms.Xaml;

namespace SmartAuth.View.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmarEmailPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly SmartAuthService _service;
        private readonly Usuario _usuario;

        public ConfirmarEmailPopup(Usuario usuario)
        {
            InitializeComponent();

            _usuario = usuario;
            _service = new SmartAuthService();
        }

        private async void Confirmar_Clicked(object sender, System.EventArgs e)
        {
            btn_confirmar.IsVisible = false;
            loader.IsVisible = true;

            try
            {
                if (!string.IsNullOrEmpty(entry_email.Text))
                {
                    _usuario.Email = entry_email.Text;

                    await _service.AddUsuariosAsync(_usuario);

                    loader.IsVisible = false;
                    await Navigation.PushAsync(new VisualizarUsuarios());
                    await Navigation.PopPopupAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR", ex.Message, "Fechar");
                return;
            }
        }

        private void entry_email_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (string.IsNullOrEmpty(e.NewTextValue)
                || !regex.IsMatch(e.NewTextValue))
            {
                btn_confirmar.IsEnabled = false;
            }
            else
            {
                btn_confirmar.BackgroundColor = Color.FromArgb(8, 120, 41);
                btn_confirmar.TextColor = Color.White;
                btn_confirmar.IsEnabled = true;
            }
        }
    }
}