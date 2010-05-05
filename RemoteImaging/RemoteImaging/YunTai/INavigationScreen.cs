namespace RemoteImaging.YunTai
{
    public interface INavigationScreen
    {
        void AttachController(NavigationController controller);
        Damany.PC.Domain.CameraInfo SelectedCamera();
    }
}