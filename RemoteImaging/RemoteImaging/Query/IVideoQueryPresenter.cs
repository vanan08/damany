namespace RemoteImaging.Query
{
    public interface IVideoQueryPresenter
    {
        void Start();
        void Search();

        void PlayVideo();
        void ShowRelatedFaces();
    }
}