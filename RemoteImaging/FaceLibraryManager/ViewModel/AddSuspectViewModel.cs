using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Damany.Imaging.Common;

namespace FaceLibraryManager.ViewModel
{
    public class AddSuspectViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<PersonOfInterest> _allSuspects;

        private PersonOfInterest _currentSuspect;

        public PersonOfInterest CurrentSuspect
        {
            get { return _currentSuspect; }
            set
            {
                _currentSuspect = value;
                InvokePropertyChanged("CurrentSuspect");
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand(
                    param => this.Add(),
                    param => true);
            }
        }

        public AddSuspectViewModel(System.Collections.ObjectModel.ObservableCollection<Damany.Imaging.Common.PersonOfInterest> allSuspects)
        {
            _allSuspects = allSuspects;

            CurrentSuspect = new PersonOfInterest();
        }

        void Add()
        {
            _allSuspects.Add(CurrentSuspect);
            CurrentSuspect = new PersonOfInterest();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(name);
                handler(this, e);
            }
        }
    }
}