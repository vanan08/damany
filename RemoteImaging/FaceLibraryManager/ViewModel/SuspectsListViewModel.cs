using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Damany.Imaging.Common;
using SuspectsRepository;

namespace FaceLibraryManager.ViewModel
{
    public class SuspectsListViewModel
    {
        private readonly SuspectsRepositoryManager _repositoryManager;
        private RelayCommand _deleteSuspectCommand;
        private RelayCommand _saveCommand;
        private RelayCommand _reloadCommand;
        private RelayCommand _addNewFaceCommand;
        private System.Collections.Generic.List<Damany.Imaging.Common.PersonOfInterest>
            deletedPersons = new List<PersonOfInterest>();

        public SuspectsCollection AllSuspects { get; private set; }

        public Damany.Imaging.Common.PersonOfInterest SelectedSuspect { get; set; }

        public double ZoomFactor { get; set; }

        public ICommand AddNewSuspect { get; private set; }

        public ICommand DeleteSuspect
        {
            get
            {
                if (_deleteSuspectCommand == null)
                {
                    _deleteSuspectCommand = new RelayCommand(
                        param => this.Delete(),
                        param => this.CanDelete
                        );
                }

                return _deleteSuspectCommand;
            }
        }

        public ICommand AddNewFaceCommand
        {
            get
            {
                if (_addNewFaceCommand == null)
                {
                    _addNewFaceCommand = new RelayCommand(
                        param => this.AddNewFace(),
                        param => true
                        );
                }

                return _addNewFaceCommand;
                
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.Save(),
                        param => true);
                }

                return _saveCommand;
            }
        }

        public ICommand ReloadCommand
        {
            get
            {
                if (_reloadCommand == null)
                {
                    _reloadCommand = new RelayCommand(
                        param => this.Reload(),
                        param => true);
                }

                return _reloadCommand;
            }
        }

        


        public SuspectsListViewModel(SuspectsRepository.SuspectsRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;

            AllSuspects = new SuspectsCollection();
            foreach (var personOfInterest in repositoryManager.Peoples)
            {
                AllSuspects.Add(personOfInterest);
            }

            ZoomFactor = 1;


        }


        void Delete()
        {
            var delete = SelectedSuspect;
            AllSuspects.Remove(delete);
            deletedPersons.Add(delete);
        }

        void Save()
        {
            _repositoryManager.Clear();

            foreach (var personOfInterest in deletedPersons)
            {
                _repositoryManager.RemovePerson(personOfInterest);
            }

            foreach (var suspect in AllSuspects)
            {
                _repositoryManager.AddNewPerson(suspect);
            }

            _repositoryManager.Save();
        }

        void AddNewFace()
        {
            var view = new View.AddNewFace();
            var vm = new ViewModel.AddSuspectViewModel(AllSuspects);

            view.DataContext = vm;
            view.Owner = App.Current.MainWindow;

            view.ShowDialog();

        }

        void Reload()
        {
            AllSuspects.Clear();
            deletedPersons.Clear();

            _repositoryManager.Clear();
            _repositoryManager.Load();

            foreach (var personOfInterest in _repositoryManager.Peoples)
            {
                AllSuspects.Add(personOfInterest);
            }
        }

        bool CanDelete
        {
            get
            {
                return SelectedSuspect != null;
            }
        }
    }
}