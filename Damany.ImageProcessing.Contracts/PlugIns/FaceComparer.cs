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
        private readonly IEnumerable<PersonOfInterest> _personsOfInterests;
        private readonly IRepositoryFaceComparer _comparer;
        private bool _initialized = false;
        private bool _started = false;
        private float _sensitivity = 0;


        public IEnumerable<IFaceSatisfyCompareCretia> FacePreFilter { get; set; }
        public float Threshold { get; set; }
        public IEventAggregator EventAggregator { get; set; }


        public FaceComparer(IEnumerable<PersonOfInterest> personsOfInterests, IRepositoryFaceComparer comparer)
        {
            _personsOfInterests = personsOfInterests;
            _comparer = comparer;
            if (comparer == null) throw new ArgumentNullException("comparer");

            FacePreFilter = new List<IFaceSatisfyCompareCretia>(0);

        }


        public void Initialize()
        {
            if (!_initialized)
            {
                _comparer.Load(this._personsOfInterests.ToList());

                this._worker = new Thread(this.DoComare);
                this._worker.IsBackground = true;
                this.goSignal = new AutoResetEvent(false);
                this.Run = true;
                _initialized = true;
            }
        }


        public void Start()
        {
            if (!_started)
            {
                this._worker.Start(null);
                _started = true;
            }

        }

        public void ProcessPortraits(IList<Portrait> portraits)
        {
            var clone = portraits.Select(p => p.Clone()).ToList();
            this.Enqueue(clone);
        }

        public void SignalToStop()
        {
            Run = false;
            goSignal.Set();
        }

        public void WaitForStop()
        {
            if (_worker != null)
            {
                _worker.Join();

                var list = portraitsQueue.ToList();
                portraitsQueue.Clear();
                list.ForEach(l => l.ToList().ForEach(p => p.Dispose()));
            }
        }


        public float Sensitivity
        {
            set
            {
                if (_sensitivity != value)
                {
                    _comparer.SetSensitivity(value);
                    _sensitivity = value;
                }
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

                    var compareResults = _comparer.CompareTo(portrait.GetIpl());

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

                        if (EventAggregator != null)
                        {
                            EventAggregator.PublishFaceMatchEvent(args);
                        }
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

        private System.Threading.Thread _worker;
        private AutoResetEvent goSignal;

        private bool run;
        private object runLocker = new object();
    }
}