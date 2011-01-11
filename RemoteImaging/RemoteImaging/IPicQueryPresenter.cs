using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public interface IPicQueryPresenter
    {
        void Start();
        void Search();
        void PlayVideo();

        void SelectedItemChanged();

        void NavigateToPrev();
        void NavigateToNext();
        void NavigateToLast();
        void NavigateToFirst();

        void PageSizeChanged();
    }
}
