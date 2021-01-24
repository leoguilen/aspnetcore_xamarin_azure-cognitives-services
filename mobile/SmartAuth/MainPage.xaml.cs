using SmartAuth.View;
using System;
using Xamarin.Forms;

namespace SmartAuth
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Cadastrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastrarUsuario());
        }

        private void VisualizarCadastro_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Teste", "Visualizar cadastro", "Ok");
        }
    }
}
