using SmartAuth.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SmartAuth.ViewModels
{
    public class UsuariosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Usuario> Usuarios { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public UsuariosViewModel(params Usuario[] usuarios)
        {
            Usuarios = new ObservableCollection<Usuario>(usuarios);
        }
    }
}
