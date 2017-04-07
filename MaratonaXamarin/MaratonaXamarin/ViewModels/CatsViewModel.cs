using MaratonaXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaratonaXamarin.ViewModels
{
    class CatsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool busy;

        public Command GetCatsCommand { get; set; }

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ObservableCollection<Cat> Cats { get; set; }

        private bool Busy;


        public CatsViewModel()
        {
            Cats = new ObservableCollection<Models.Cat>();
            GetCatsCommand = new Command(async () => await GetCats(),() => !IsBusy);
            GetCatsCommand.ChangeCanExecute();
        }

        public bool IsBusy
        {
            get
            {
                return Busy;
            }
            set
            {
                Busy = value;
                OnPropertyChanged();
            }
        }

        async Task GetCats()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetCatsAzure();
                    Cats.Clear();
                    foreach (var Cat in Items)
                    {
                        Cats.Add(Cat);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                    if (Error != null)
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                        "Error!", Error.Message, "OK");
                    }
                }
            }
            return;
        }
        
    }
}
