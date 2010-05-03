﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class MockLicensePlateGenerator : ILicensePlateEventGenerator
    {
        private readonly ILicensePlateEventPublisher _publisher;

        public MockLicensePlateGenerator(ILicensePlateEventPublisher publisher)
        {
            _publisher = publisher;

        }


        void Run(object  state)
        {
            while (true)
            {
                System.Threading.Thread.Sleep(3000);
                var licensePlate = new LicensePlateInfo();
                licensePlate.CapturedFrom = 1;
                licensePlate.CaptureTime = DateTime.Now;
                licensePlate.LicensePlateNumber = "川D12345";

                _publisher.PublishLicensePlate(licensePlate);
            }
            
        }


        public void Start()
        {
            var worker = new System.Threading.Thread(this.Run);
            worker.IsBackground = true;
            worker.Start();
        }
    }
}
