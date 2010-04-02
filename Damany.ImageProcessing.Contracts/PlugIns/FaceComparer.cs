using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using MiscUtil;

namespace Damany.Imaging.PlugIns
{
    public class FaceComparer : Common.IPortraitHandler
    {
        public FaceComparer(IEnumerable<PersonOfInterest> personsOfInterests, IFaceComparer comparer)
        {
            if (personsOfInterests == null) throw new ArgumentNullException("personsOfInterests");
            if (comparer == null) throw new ArgumentNullException("comparer");

            this.personsOfInterests = personsOfInterests;
            this.Comparer = comparer;
        }

        public void Initialize(){}

        public IFaceComparer Comparer { get; set; }

        public void Start(){}

        public void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits)
        {
            if (portraits.Count == 0)
            {
                return;
            }

            var matches = from p in portraits
                          from s in this.personsOfInterests
                            let r = this.Comparer.Compare(p.GetIpl(), s.Ipl)
                          where r.IsSimilar
                          select new {Portrait = p, Suspect = s, Result = r};

            foreach (var match in matches)
            {
                var args = new PersonOfInterestDetectionResult
                               {
                                   Details = match.Suspect,
                                   Portrait = match.Portrait,
                                   Similarity = match.Result.SimilarScore
                               };
                this.InvokePersonOfInterestDected(args);
                
            }

        }

        public void Stop(){}

        public string Name
        {
            get { return "FaceComparer"; }
        }

        public string Description
        {
            get { return "Damany FaceCompare Module"; }
        }

        public string Author
        {
            get { return "Damany Technology"; }
        }

        public Version Version
        {
            get { return new Version(1, 0); }
        }

        public bool WantCopy
        {
            get { return true; }
        }

        public bool WantFrame
        {
            get { return false; }
        }

        public event EventHandler< EventArgs<Exception> > Stopped;
        public event EventHandler< EventArgs<PersonOfInterestDetectionResult> > PersonOfInterestDected;

        private void InvokePersonOfInterestDected(PersonOfInterestDetectionResult e)
        {
            var handler = PersonOfInterestDected;
            if (handler != null) handler(this, new EventArgs<PersonOfInterestDetectionResult>(e));
        }


        private IEnumerable<PersonOfInterest> personsOfInterests;

    }
}