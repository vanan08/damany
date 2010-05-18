using SuspectsRepository;

namespace FaceLibraryManager.ViewModel
{
    public class SuspectsListViewModel
    {
        private readonly SuspectsRepositoryManager _repositoryManager;

        public SuspectsCollection AllSuspects { get; set; }

        public SuspectsListViewModel(SuspectsRepository.SuspectsRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;

            AllSuspects = new SuspectsCollection();
            foreach (var personOfInterest in repositoryManager.Peoples)
            {
                AllSuspects.Add(personOfInterest);
            }
        }
    }
}