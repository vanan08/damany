using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Util.Extensions;
using ImageProcess;

namespace Damany.Imaging.Processors
{
    using Damany.Imaging.Common;

    public class PortraitFinder : IConvertor<Frame, Portrait>
    {
        public event Action<IList<Portrait>> PortraitCaptured;

        public PortraitFinder()
        {
            this.listeners = new List<IPortraitHandler>();
            this.searcher = new FaceSearchWrapper.FaceSearch();
        }

        public IEnumerable<Portrait> Execute(IEnumerable<Frame> inputs)
        {
            return HandleMotionFrame(inputs);
        }


        public void AddListener(IPortraitHandler l)
        {
            if (l == null)
                throw new ArgumentNullException("l", "l is null.");

            lock (this.locker)
            {
                this.listeners.Add(l);
            }

        }

        IEnumerable<IPortraitHandler> GetListenersCopy()
        {
            lock (this.locker)
            {
                return this.listeners.ToList();
            }
        }

        void l_Stopped(object sender, MiscUtil.EventArgs<Exception> e)
        {
            IPortraitHandler handler = sender as IPortraitHandler;

            this.RemoveListener(handler);

            if (e.Value != null)
            {
                System.Diagnostics.Debug.WriteLine(handler.Name + " Exception:" + e.Value.Message);
            }
        }

        public void RemoveListener(IPortraitHandler l)
        {
            lock (this.locker)
            {
                this.listeners.Remove(l);
                System.Diagnostics.Debug.WriteLine("listener: " + l.Name + " removed");
            }

        }


        #region IMotionFrameHandler Members

        public IEnumerable<Portrait> HandleMotionFrame(IEnumerable<Frame> motionFrames)
        {
           return this.SearchIn(motionFrames);
        }

        #endregion


        private IEnumerable<Portrait> SearchIn(IEnumerable<Frame> motionFrames)
        {
            var mList = motionFrames;
            foreach (var item in mList)
            {
                this.searcher.AddInFrame(item);
            }

            var portraits = this.searcher.SearchFaces();

            var facelessFrames = GetFacelessFrames(mList, portraits);
            var faceFrames = GetFaceFrames(mList, portraits);
            var portraitList = ExpandPortraitsList(faceFrames, portraits);

            foreach (var facelessFrame in facelessFrames)
            {
                facelessFrame.Dispose();
            }

            return portraitList;
            
        }

        private static OpenCvSharp.CvRect FrameToPortrait(OpenCvSharp.CvRect bounds, OpenCvSharp.CvRect faceBounds)
        {
            faceBounds.X -= bounds.X;
            faceBounds.Y -= bounds.Y;

            return faceBounds;
        }

        private static IEnumerable<Portrait> ExpandPortraitsList(IEnumerable<Frame> motionFrames, IEnumerable<Target> portraits)
        {
            var portraitFoundFrameQuery = from m in motionFrames
                                          join p in portraits
                                            on m.Guid equals p.BaseFrame.guid
                                          select new { Frame = m, Portraits = p, };

            var expanedPortraits = from frame in portraitFoundFrameQuery
                                   from p in frame.Portraits.Portraits
                                   select new Portrait(p.Face)
                                   {
                                       FaceBounds = FrameToPortrait(p.FacesRect, p.FacesRectForCompare),
                                       FrameId = frame.Frame.Guid,
                                       CapturedAt = frame.Frame.CapturedAt,
                                       CapturedFrom = frame.Frame.CapturedFrom,
                                   };


            return expanedPortraits;
        }

        private static IEnumerable<Frame> GetFacelessFrames(IEnumerable<Frame> motionFrames, IEnumerable<Target> portraits)
        {
            var noPortraitFrameQuery = from m in motionFrames
                                       where !portraits.Any(t => t.BaseFrame.guid.Equals(m.Guid))
                                       select m;

            return noPortraitFrameQuery;
        }

        private static IEnumerable<Frame> GetFaceFrames(IEnumerable<Frame> motionFrames, IEnumerable<Target> portraits)
        {
            var portraitFrameQuery = from m in motionFrames
                                       where portraits.Any(t => t.BaseFrame.guid.Equals(m.Guid))
                                       select m;

            return portraitFrameQuery;
        }


        private static void NotifyAListenerWithCopy(
            IEnumerable<Frame> motionFrames,
            IEnumerable<Portrait> portraitList,
            IPortraitHandler listener)
        {
            var frameCpy = listener.WantFrame ? motionFrames.ToList().ConvertAll(m => m.Clone()) : null;
            var portraitCpy = portraitList.ToList().ConvertAll(p => p.Clone());

            try
            {
                listener.HandlePortraits(frameCpy, portraitCpy);
            }
            catch (System.Exception ex)
            {
                frameCpy.ToList().ForEach(f => f.Dispose());
                portraitCpy.ToList().ForEach(p => p.Dispose());
                throw;
            }
        }


        private void FirePortraitCapturedEvent(IList<Portrait> portraitList)
        {
            //event listener
            if (this.PortraitCaptured != null)
            {
                this.PortraitCaptured(portraitList);
            }
        }

        private void NotifyListeners(IList<Frame> motionFrames, IList<Portrait> portraitList)
        {
            foreach (var listener in this.GetListenersCopy())
            {
                try
                {
                    if (listener.WantCopy)
                        NotifyAListenerWithCopy(motionFrames, portraitList, listener);
                    else
                        listener.HandlePortraits(motionFrames, portraitList);
                }
                catch (System.Exception ex)//exception occurred, remove listener
                {
                    this.RemoveListener(listener);
                    System.Diagnostics.Debug.WriteLine(ex);
                    throw;
                }
            }
        }

        private void Dispatch(IList<Frame> motionFrames, IList<Portrait> portraitList)
        {
            try
            {
                FirePortraitCapturedEvent(portraitList);
                NotifyListeners(motionFrames, portraitList);
            }
            finally
            {
                portraitList.ToList().ForEach(p => p.Dispose());
                motionFrames.ToList().ForEach(f => f.Dispose());
            }
        }

        List<IPortraitHandler> listeners;
        FaceSearchWrapper.FaceSearch searcher;
        object locker = new object();
    }
}
