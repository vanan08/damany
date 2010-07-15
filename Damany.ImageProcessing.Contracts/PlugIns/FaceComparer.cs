using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Damany.Imaging.Common;
using MiscUtil;
using Damany.Imaging.Extensions;
using OpenCvSharp;

namespace Damany.Imaging.PlugIns
{
    public class FaceComparer
    {
        public IEnumerable<IFaceSatisfyCompareCretia> FacePreFilter { get; set; }
        public float Threshold { get; set; }
        public IRepositoryFaceComparer Comparer { get; set; }


        private readonly IEnumerable<PersonOfInterest> _personsOfInterests;

        public FaceComparer(IEnumerable<PersonOfInterest> personsOfInterests, IRepositoryFaceComparer comparer)
        {
            _personsOfInterests = personsOfInterests;
            if (comparer == null) throw new ArgumentNullException("comparer");

            this.Comparer = comparer;
            FacePreFilter = new List<IFaceSatisfyCompareCretia>(0);

        }


        public void Initialize()
        {
            this.Comparer.Load(this._personsOfInterests.ToList());

            this.worker = new Thread(this.DoComare);
            this.worker.IsBackground = true;
            this.goSignal = new AutoResetEvent(false);
            this.Run = true;
        }


        public void Start()
        {
            this.worker.Start(null);
        }

        public void HandlePortraits(IList<Portrait> portraits)
        {
            this.Enqueue(portraits);
        }

        public void Stop()
        {
            Run = false;
        }


        public float Sensitivity
        {
            set
            {
                this.Comparer.SetSensitivity(value);
            }
        }

        public event EventHandler<EventArgs<PersonOfInterestDetectionResult>> PersonOfInterestDected;

        private void InvokePersonOfInterestDected(PersonOfInterestDetectionResult e)
        {
            var handler = PersonOfInterestDected;
            if (handler != null) handler(this, new EventArgs<PersonOfInterestDetectionResult>(e));
        }

        private void DoComare(object state)
        {
            while (Run)
            {
                this.goSignal.WaitOne();

                var portraits = this.Dequeue();
                if (portraits == null)
                {
                    continue;
                }

                if (portraits.Count == 0)
                {
                    return;
                }

                IEnumerable<Portrait> filtered = FiltPortraits(portraits);

                foreach (var portrait in filtered)
                {

                    var compareResults = this.Comparer.CompareTo(portrait.GetIpl());

                    var matches = from r in compareResults
                                  where r.Similarity > Threshold
                                  orderby r.Similarity descending
                                  select r;

                    foreach (var match in matches)
                    {
                        var args = new PersonOfInterestDetectionResult
                        {
                            Details = match.PersonInfo,
                            Portrait = portrait,
                            Similarity = match.Similarity
                        };
                        this.InvokePersonOfInterestDected(args);

                    }
                }
            }

        }

        private IEnumerable<Portrait> FiltPortraits(IList<Portrait> portraits)
        {
            for (var i = 0; i < portraits.Count; i++)
            {
                var current = portraits[i];
                if (!this.CanProceedToCompare(current))
                {
                    current.Dispose();
                    current = null;
                }
            }

            return portraits.Where(p => p != null);
        }

        private bool CanProceedToCompare(Portrait portrait)
        {
            foreach (var facePreFilter in FacePreFilter)
            {
                if (!facePreFilter.CanSatisfy(portrait))
                {
                    return false;
                }
            }

            return true;
        }

        private void Enqueue(IList<Portrait> portraits)
        {
            if (portraits.Count == 0)
            {
                return;
            }


            lock (queueLocker)
            {
                if (portraitsQueue.Count == 100)
                {
                    return;
                }

                portraitsQueue.Enqueue(portraits);
                goSignal.Set();
            }
        }

        private IList<Portrait> Dequeue()
        {
            lock (queueLocker)
            {
                if (portraitsQueue.Count > 0)
                {
                    return portraitsQueue.Dequeue();
                }

                return null;
            }
        }

        private bool Run
        {
            get
            {
                lock (runLocker)
                {
                    return run;
                }
            }
            set
            {
                lock (runLocker)
                {
                    this.run = value;
                }
            }
        }



        private Queue<IList<Portrait>> portraitsQueue = new Queue<IList<Portrait>>();
        private object queueLocker = new object();

        private System.Threading.Thread worker;
        private AutoResetEvent goSignal;

        private bool run;
        private object runLocker = new object();


        public void ProcessPortraits(List<Portrait> portraits)
        {
            var clone = from p in portraits
                        select p.Clone();

            HandlePortraits(clone.ToList());
        }
    }
}