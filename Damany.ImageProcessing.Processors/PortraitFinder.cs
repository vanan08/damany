﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Util.Extensions;

namespace Damany.Imaging.Processors
{
    using Damany.Imaging.Contracts;

    public class PortraitFinder : IMotionFrameHandler
    {
        public event Action<IList<Portrait>> PortraitCaptured;

        public PortraitFinder()
        {
            this.listeners = new List<IPortraitHandler>();
            this.searcher = new FaceSearchWrapper.FaceSearch();
        }

        public void AddListener(IPortraitHandler l)
        {
            if (l == null)
                throw new ArgumentNullException("l", "l is null.");

            lock (this.locker)
            {
                this.listeners.Add(l);
                System.Diagnostics.Debug.WriteLine("listener: " + l.Name + " added");
            }

        }

        IList<IPortraitHandler> GetListenersCopy()
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

        public void HandleMotionFrame(IList<Frame> motionFrames)
        {
            if (motionFrames.Count == 0) return;

            this.SearchIn(motionFrames);
        }

        #endregion


        private void SearchIn(IList<Frame> motionFrames)
        {
            foreach (var item in motionFrames)
            {
                this.searcher.AddInFrame(item);
            }

            var portraits = this.searcher.SearchFaces();

            DisposeFacelessFrames(motionFrames, portraits);
            var portraitList = ExpandPortraitsList(motionFrames, portraits);
            Dispatch(motionFrames, portraitList);
        }

        private static OpenCvSharp.CvRect FrameToPortrait(OpenCvSharp.CvRect bounds, OpenCvSharp.CvRect faceBounds)
        {
            faceBounds.X -= bounds.X;
            faceBounds.Y -= bounds.Y;

            return faceBounds;
        }

        private static List<Portrait> ExpandPortraitsList(IList<Frame> motionFrames, ImageProcess.Target[] portraits)
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


            return expanedPortraits.ToList();
        }

        private static void DisposeFacelessFrames(IList<Frame> motionFrames, ImageProcess.Target[] portraits)
        {
            var noPortraitFrameQuery = from m in motionFrames
                                       where !portraits.Any(t => t.BaseFrame.guid.Equals(m.Guid))
                                       select m;

            Array.ForEach(noPortraitFrameQuery.ToArray(), mf => { motionFrames.Remove(mf); mf.Dispose(); });
        }


        private static void NotifyAListenerWithCopy(
            IList<Frame> motionFrames,
            IList<Portrait> portraitList,
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
