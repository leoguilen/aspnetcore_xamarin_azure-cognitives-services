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
    public partial class VisualizarUsuarios : ContentPage
    {
        private readonly SmartAuthService _service;

        public VisualizarUsuarios()
        {
            InitializeComponent();

            _service = new SmartAuthService();
        }

        protected override async void OnAppearing()
        {
            loader.IsVisible = true;
            listView.IsVisible = false;

            var usuarios = await _service.GetUsuariosAsync();
            list.ItemsSource = usuarios.Content;

            loader.IsVisible = false;
            listView.IsVisible = true;

            base.OnAppearing();
        }

        private async void VoltarInicio_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastrarUsuario());
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null || e.Item.GetType() != typeof(Usuario))
                return;

            list.SelectedItem = null;

            await Navigation.PushPopupAsync(new DetalhesUsuarioPopup((Usuario)e.Item));
        }
    }
}