namespace Damany.Imaging.Common
{
    public interface IFaceComparer
    {
        FaceCompareResult Compare(OpenCvSharp.IplImage a, OpenCvSharp.IplImage b);

        string Name { get; }
        string Description { get; }
        bool CanConfig { get; }
        System.Windows.Forms.Control ConfigControl { get; }
        System.Guid UUID { get; }
    }
}