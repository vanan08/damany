using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Police.DataAccessLayer
{
    public interface IDataProvider
    {
        DTO.VehicleOwner CreateVehicleOwner(DTO.VehicleOwner owner);
        DTO.VehicleOwner[] GetVehicleOwners();
        DTO.VehicleOwner GetVehicleOwner(int ownerID);


        DTO.Vehicle CreateVehicle(DTO.Vehicle vehicle);
        DTO.Vehicle GetVehicle(int vehicleID);


        DTO.TrafficMonitorSpot CreateMonitorSpot(DTO.TrafficMonitorSpot spot);
        DTO.TrafficMonitorSpot GetMonitorSpot(int spotID);

        DTO.TrafficPoliceDepartment CreatePoliceDepartment(DTO.TrafficPoliceDepartment department);
        DTO.TrafficPoliceDepartment GetPoliceDepartment(int departmentID);


        DTO.TrafficLawViolationEvent CreateTrafficLawViolationEvent(DTO.TrafficLawViolationEvent evt);
        DTO.TrafficLawViolationEvent GetTrafficLawViolationEvent(int evtID);

    }
}
