using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using Db4objects.Db4o;
using Db4objects.Db4o.Activation;
using Db4objects.Db4o.TA;
using OpenCvSharp;

namespace Damany.PortraitCapturer.DAL.DTO
{
    public class Frame : IActivatable
    {
        [Transient]
        private IActivator _activator;

        private DateTime _capturedAt;
        public DateTime CapturedAt
        {
            get
            {
                Activate(ActivationPurpose.Read);
                return _capturedAt;
            }
            set
            {
                Activate(ActivationPurpose.Write);
                _capturedAt = value;
            }
        }

        private int _sourceId;
        public int SourceId
        {
            get
            {
                Activate(ActivationPurpose.Read);
                return _sourceId;
            }
            set
            {
                Activate(ActivationPurpose.Write);
                _sourceId = value;
            }
        }

        private Guid _guid;
        public System.Guid Guid
        {
            get
            {
                Activate(ActivationPurpose.Read);
                return _guid;
            }
            set
            {
                Activate(ActivationPurpose.Write);
                _guid = value;
            }
        }

        private List<CvRect> _facesBounds;
        public List<OpenCvSharp.CvRect> FacesBounds
        {
            get
            {
                Activate(ActivationPurpose.Read);
                return _facesBounds;
            }
            set
            {
                Activate(ActivationPurpose.Write);
                _facesBounds = value;
            }
        }

        private string _path;
        public string Path
        {
            get
            {
                Activate(ActivationPurpose.Read);
                return _path;
            }
            set
            {
                Activate(ActivationPurpose.Write);
                _path = value;
            }
        }

        public void Activate(ActivationPurpose purpose)
        {
            if (_activator != null)
            {
                _activator.Activate(purpose);
            }
        }

        public void Bind(IActivator activator)
        {
            if (_activator == activator)
            {
                return;
            }
            if (activator != null && null != _activator)
            {
                throw new System.InvalidOperationException();
            }
            _activator = activator;
        }

    }
}
