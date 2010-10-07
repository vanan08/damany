namespace Damany.Imaging.Common
{
    public interface ISimpleFaceComparer
    {

        void SetSensitivity(float value);
        FaceCompareResult Compare(OpenCvSharp.IplImage a, OpenCvSharp.IplImage b);

        string Name { get; }
        string Description { get; }
        System.Guid UUID { get; }
    }
}