using System.Windows;
using System.Windows.Input;
using SuspectsRepository;

namespace FaceLibraryManager.ViewModel
{
    public class SuspectsListViewModel
    {
        private readonly SuspectsRepositoryManager _repositoryManager;
        private RelayCommand _deleteSuspectCommand;

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
            AllSuspects.Remove(SelectedSuspect);
            ZoomFactor *= 1.2;
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