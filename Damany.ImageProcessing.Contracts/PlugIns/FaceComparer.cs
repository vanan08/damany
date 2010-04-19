using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Damany.Imaging.Common;
using MiscUtil;
using Damany.Imaging.Extensions;
using OpenCvSharp;
using Lokad;

namespace Damany.Imaging.PlugIns
{
    public class FaceComparer : IPortraitHandler
    {
        private IEnumerable<PersonOfInterest> _personsOfInterests;
        private readonly ILog _logger;

        public FaceComparer(IEnumerable<PersonOfInterest> personsOfInterests,
                            IRepositoryFaceComparer comparer,
                            ILog logger)
        {
            if (personsOfInterests == null) throw new ArgumentNullException("personsOfInterests");
            if (comparer == null) throw new ArgumentNullException("comparer");
            if (logger == null) throw new ArgumentNullException("logger");

            _personsOfInterests = personsOfInterests;
            _logger = logger;

            this.Comparer = comparer;

            if (_personsOfInterests.Count() == 0)
            {
                System.Windows.Forms.MessageBox.Show("人脸特征库为空，将不进行实时人脸比对");
            }
        }

        public float Threshold { get; set; }

        public void Initialize()
        {
            _logger.Debug("Before Load in FaceComparer");
            this.Comparer.Load(_personsOfInterests.ToList());
            _logger.Debug("After Load in FaceComparer");

            this.worker = new Thread(this.DoComare);
            this.worker.IsBackground = true;
            this.goSignal = new AutoResetEvent(false);
            this.Run = true;
        }

        public IRepositoryFaceComparer Comparer { get; set; }

        public void Start()
        {
            _started = true;
            this.worker.Start(null);
        }

        protected bool Started
        {
            get { return _started; }
            set { _started = value; }
        }

        public void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits)
        {
            this.Enqueue(portraits);
        }

        public void Stop()
        {
            Run = false;
        }

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

        public float Sensitivity
        {
            set
            {
                this.Comparer.SetSensitivity(value);
            }
        }


        public event EventHandler< EventArgs<Exception> > Stopped;
        public event EventHandler< EventArgs<PersonOfInterestDetectionResult> > PersonOfInterestDected;

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

                foreach (var portrait in portraits)
                {
                    _logger.Debug("before CompareTo");
                    var compareResults = this.Comparer.CompareTo(portrait.GetIpl());
                    _logger.Debug("after CompareTo");

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


        private IEnumerable<PersonOfInterest> personsOfInterests;

        private Queue<IList<Portrait>> portraitsQueue = new Queue<IList<Portrait>>();
        private object queueLocker = new object();

        private System.Threading.Thread worker;
        private AutoResetEvent goSignal;

        private bool run;
        private object runLocker = new object();
        private bool _started;
    }
}