namespace RemoteImaging.LicensePlate
{
    public interface ILicenseplateSearchScreen
    {
        void AttachPresenter(ILicensePlateSearchPresenter presenter);
        void Show();

        void AddLicensePlateInfo(LicensePlateInfo licensePlateInfo);
        void Clear();

        bool MatchLicenseNumber { get; set; }
        string LicenseNumber { get; set; }

        bool MatchTimeRange { get; set; }
        Damany.Util.DateTimeRange Range { get; set; }
    }
}