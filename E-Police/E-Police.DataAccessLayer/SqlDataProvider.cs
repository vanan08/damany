using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.SqlClient;

namespace E_Police.DataAccessLayer
{
    using L2S;
    using System.Drawing;

    public class SqlDataProvider : IDataProvider
    {
        public SqlDataProvider()
        {

        }
        public DTO.VehicleOwner CreateVehicleOwner(DTO.VehicleOwner owner)
        {
            using (EPoliceDataContext db = new EPoliceDataContext
                (Properties.Settings.Default.TrafficPoliceConnectionString))
            {
                Owner nwOwner = new Owner
                {
                    OwnerID = owner.OwnerID,
                    Addr = owner.Address,
                    Name = owner.Name,
                    Phone = owner.PhoneNumber
                };

                db.Owners.InsertOnSubmit(nwOwner);

                try
                {
                    db.SubmitChanges();
                }
                catch (SqlException sqlEx)
                {
                    throw;
                }

                return GetVehicleOwner(nwOwner.OwnerID);

            }
        }
        public E_Police.DTO.VehicleOwner[] GetVehicleOwners()
        {
            throw new NotImplementedException();
        }

        public E_Police.DTO.VehicleOwner GetVehicleOwner(int ownerID)
        {
            using (EPoliceDataContext db = new EPoliceDataContext
                (Properties.Settings.Default.TrafficPoliceConnectionString))
            {
                var vechicleOwnerQuery = from owner in db.Owners
                                         where owner.OwnerID == ownerID
                                         select new DTO.VehicleOwner
                                         {
                                             Address = owner.Addr,
                                             Name = owner.Name,
                                             OwnerID = owner.OwnerID,
                                             PhoneNumber = owner.Phone
                                         };

                DTO.VehicleOwner vo = vechicleOwnerQuery.Single();
                return vo;
            }
        }

        public DTO.Vehicle CreateVehicle(DTO.Vehicle vehicle)
        {
            using (EPoliceDataContext db = new EPoliceDataContext())
            {
                Vehicle v = new Vehicle()
                {
                    Category = vehicle.Category,
                    LicenseAreaCode = vehicle.LicenseAreaCode,
                    LicenseNumber = vehicle.LicenseNumber,
                    OwnerID = vehicle.OwnerID,
                    VehicleID = vehicle.VehicleID,
                };

                db.Vehicles.InsertOnSubmit(v);

                try
                {
                    db.SubmitChanges();
                }
                catch (SqlException sqlEx)
                {
                    throw;
                }

                return GetVehicle(v.VehicleID);
            }
        }
        public DTO.TrafficMonitorSpot CreateMonitorSpot(DTO.TrafficMonitorSpot spot)
        {
            using (EPoliceDataContext db = new EPoliceDataContext())
            {
                MonitorSpot nwSpot = new MonitorSpot()
                {
                    ConnectionString = spot.ConnectionString,
                    MonitoredBy = spot.MonitoredBy,
                    MonitorSpotID = spot.MonitorSpotID,
                    Name = spot.Name,
                };
            }

            return null;
        }
        public DTO.TrafficLawViolationEvent CreateTrafficLawViolationEvent(DTO.TrafficLawViolationEvent evt)
        {
            using (EPoliceDataContext db = new EPoliceDataContext())
            {
                TrafficViolentionEvent e = new TrafficViolentionEvent()
                {
                    CapturedAt = evt.CapturedAt,
                    Time = evt.Time,
                    VehicleID = evt.VehicleID,
                    Description = evt.Description,
                    EventID = evt.EventID,
                    EvidencePicture = ImageToBinary(evt.EvidencePicture),
                };

                db.TrafficViolentionEvents.InsertOnSubmit(e);

                try
                {
                    db.SubmitChanges();
                }
                catch (SqlException ex)
                {
                    throw;
                }

                return GetTrafficLawViolationEvent(e.EventID);

            }
        }

        #region IDataProvider Members


        public E_Police.DTO.TrafficMonitorSpot GetMonitorSpot(int spotID)
        {
            using (EPoliceDataContext db = new EPoliceDataContext())
            {
                DataLoadOptions opts = new DataLoadOptions();
                opts.LoadWith<MonitorSpot>(spot => spot.MonitoredBy);
                db.LoadOptions = opts;

                var spotQuery = from spot in db.MonitorSpots
                                where spot.MonitorSpotID == spotID
                                select new DTO.TrafficMonitorSpot
                                {
                                    Name = spot.Name,
                                    ConnectionString = spot.ConnectionString,
                                    MonitorSpotID = spot.MonitorSpotID,
                                    MonitoredBy = spot.MonitoredBy
                                };

                return spotQuery.Single();
            }
        }

        #endregion

        #region IDataProvider Members


        public E_Police.DTO.TrafficPoliceDepartment CreatePoliceDepartment(E_Police.DTO.TrafficPoliceDepartment department)
        {
            using (EPoliceDataContext db = new EPoliceDataContext())
            {
                TrafficPoliceDepartment d = new TrafficPoliceDepartment()
                {
                    DepartmentID = department.DepartmentID,
                    Address = department.Address,
                    Name = department.Name,
                    OfficeAddress = department.OfficeAddress,
                    OfficeReportTo = department.Office
                };

                d.MonitorSpots.AddRange(
                    from spot in department.LocationsUnderMonitor
                    select new MonitorSpot
                    {
                        ConnectionString = spot.ConnectionString,
                        MonitorSpotID = spot.MonitorSpotID,
                        Name = spot.Name,
                        MonitoredBy = spot.MonitorSpotID
                    });

                db.TrafficPoliceDepartments.InsertOnSubmit(d);
                db.MonitorSpots.InsertAllOnSubmit(d.MonitorSpots);

                try
                {
                    db.SubmitChanges();
                }
                catch (SqlException ex)
                {
                    throw;
                }

                return GetPoliceDepartment(d.DepartmentID);
            }
        }

        public E_Police.DTO.TrafficPoliceDepartment GetPoliceDepartment(int departmentID)
        {
            using (EPoliceDataContext db = new EPoliceDataContext())
            {
                DataLoadOptions opts = new DataLoadOptions();
                opts.LoadWith<TrafficPoliceDepartment>(d => d.MonitorSpots);

                var departmentQuery = from dept in db.TrafficPoliceDepartments
                                      where dept.DepartmentID == departmentID
                                      select new DTO.TrafficPoliceDepartment()
                                      {
                                          Address = dept.Address,
                                          DepartmentID = dept.DepartmentID,
                                          Name = dept.Name,
                                          Office = dept.OfficeReportTo,
                                          OfficeAddress = dept.OfficeAddress,
                                          LocationsUnderMonitor =
                                            (from spot in dept.MonitorSpots
                                             select new DTO.TrafficMonitorSpot()
                                             {
                                                 ConnectionString = spot.ConnectionString,
                                                 MonitoredBy = spot.MonitoredBy,
                                                 MonitorSpotID = spot.MonitorSpotID,
                                                 Name = spot.Name
                                             }).ToList()
                                      };

                return departmentQuery.Single();
            }
        }

        #endregion

        #region IDataProvider Members


        public E_Police.DTO.Vehicle GetVehicle(int vehicleID)
        {
            using (EPoliceDataContext db = new EPoliceDataContext())
            {
                var vehicleQuery = from v in db.Vehicles
                                   where v.VehicleID == vehicleID
                                   select new DTO.Vehicle()
                                   {
                                       Category = v.Category,
                                       LicenseAreaCode = v.LicenseAreaCode,
                                       LicenseNumber = v.LicenseNumber,
                                       VehicleID = v.VehicleID,
                                       OwnerID = v.OwnerID,
                                   };

                return vehicleQuery.Single();

            }
        }

        #endregion

        private Binary ImageToBinary(Image img)
        {
            //serialize picture
            System.IO.MemoryStream ms = new System.IO.MemoryStream(1024);
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Binary bytes = new Binary(ms.ToArray());

            return bytes;
        }
        Image BinaryToImage(Binary bytes)
        {
            byte[] data = bytes.ToArray();
            System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
            Image img = Image.FromStream(ms);

            return img;
        }
        #region IDataProvider Members


        public E_Police.DTO.TrafficLawViolationEvent GetTrafficLawViolationEvent(int evtID)
        {
            using (EPoliceDataContext db = new EPoliceDataContext())
            {
                var evtQuery = from evt in db.TrafficViolentionEvents
                               where evt.EventID == evtID
                               select new DTO.TrafficLawViolationEvent()
                               {
                                   CapturedAt = evt.CapturedAt,
                                   Description = evt.Description,
                                   EventID = evt.EventID,
                                   Time = evt.Time,
                                   VehicleID = evt.VehicleID,
                                   EvidencePicture = BinaryToImage(evt.EvidencePicture),
                               };

                return evtQuery.Single();
            }
        }

        #endregion
    }
}
