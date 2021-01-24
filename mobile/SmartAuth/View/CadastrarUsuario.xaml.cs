using Plugin.Media;
using Plugin.Media.Abstractions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartAuth.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarUsuario : ContentPage
    {
        public CadastrarUsuario()
        {
            InitializeComponent();
        }

        private async void OpenCamera_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Câmera Indisponível", "Não foi possível acessar a câmera.", "OK");
                return;
            }

            var armazenamento = new StoreCameraMediaOptions()
            {
                SaveToAlbum = false,
                Directory = "Documents",
                Name = $"doc_{Guid.NewGuid()}.jpg"
            };

            var foto = await CrossMedia.Current.TakePhotoAsync(armazenamento);

            if (foto == null)
                return;

            await Navigation.PushAsync(new FormularioCadastro(foto));
        }
    }
}