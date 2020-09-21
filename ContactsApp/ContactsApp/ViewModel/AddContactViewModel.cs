using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class AddContactViewModel : INotifyPropertyChanged
    {

        public AddContactViewModel()
        {
            LaunchWebsiteCommand = new Command(LaunchWebsite,
                                                () => !IsBusy);
            SaveContactCommand = new Command(async () => await SaveContact(),
                                            () => !IsBusy);
        }

        string name;
        string website;
        bool addFavorite;
        bool addBookmark;
        bool isBusy = false;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public bool AddFavorite
        {
            get { return addFavorite; }
            set
            {
                addFavorite = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public bool AddBookmark
        {
            get { return addBookmark; }
            set
            {
                addBookmark = value;
                OnPropertyChanged();

                OnPropertyChanged(nameof(BookmarkMessage));
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;

                if (name == "Bob")
                    IsBusy = true;
                else
                    IsBusy = false;

                OnPropertyChanged();

                OnPropertyChanged(nameof(BookmarkMessage));
            }
        }

        public string DisplayMessage
        {
            get
            {
                return $"{Name} " +
                       $"{(addFavorite ? "is" : "is not")} on your favorites list.";
            }
        }

        public string BookmarkMessage
        {
            get
            {
                return $"{website} " +
                       $"{(addBookmark ? "is" : "is not")} on your bookmarks list";
                    
            }
        }


        public string Website
        {
            get
            {
                return website;
            }
            set
            {
                website = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;

                OnPropertyChanged();
                LaunchWebsiteCommand.ChangeCanExecute();
                SaveContactCommand.ChangeCanExecute();
            }
        }


        public Command LaunchWebsiteCommand { get; }
        public Command SaveContactCommand { get; }

        void LaunchWebsite()
        {
            try
            {
                Device.OpenUri(new Uri(website));
            }
            catch
            {

            }
        }

        async Task SaveContact()
        {
            IsBusy = true;
            await Task.Delay(4000);

            IsBusy = false;

            await Application.Current.MainPage.DisplayAlert("Save", "Contact has been saved", "OK");
        }

    }
}