#pragma once


namespace FaceSearchWrapper {

	/// <summary>
	/// Summary for FC
	/// </summary>
	public ref class FaceSearchConfiguration
	{
	public:
		int EnvironmentMode;
		float LeftRation;
		float RightRation;
		float TopRation;
		float BottomRation;
		int MinFaceWidth;
		float FaceWidthRatio;
		System::Drawing::Rectangle^ SearchRectangle;
	};
}
