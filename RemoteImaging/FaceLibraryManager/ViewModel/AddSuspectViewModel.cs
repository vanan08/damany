using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Damany.Imaging.Common;
using OpenCvSharp;

namespace FaceLibraryManager.ViewModel
{
    public class AddSuspectViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<PersonOfInterest> _allSuspects;

        private PersonOfInterest _currentSuspect;

        private CvRect _currentFaceRect;
        public OpenCvSharp.CvRect CurrentFaceRect
        {
            get { return _currentFaceRect; }
            set
            {
                _currentFaceRect = value;
                InvokePropertyChanged("CurrentFaceRect");
            }
        }

        private string _imageFile;
        public string ImageFile
        {
            get { return _imageFile; }
            set
            {
                _imageFile = value;
                InvokePropertyChanged("ImageFile");
            }
        }

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
                    param => this.CanAdd());
            }
        }

        public AddSuspectViewModel(ObservableCollection<PersonOfInterest> allSuspects)
        {
            _allSuspects = allSuspects;

            CurrentSuspect = new PersonOfInterest();
            ImageFile = "value in VM";
        }

        void Add()
        {
            CurrentSuspect.FaceRect = CurrentFaceRect;
            _allSuspects.Add(CurrentSuspect);

            CurrentSuspect = new PersonOfInterest();
            CurrentFaceRect = new CvRect();

        }

        bool CanAdd()
        {
            return !string.IsNullOrEmpty(CurrentSuspect.ImageFilePath)
                   && CurrentFaceRect.Width != 0 && CurrentFaceRect.Height != 0;
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