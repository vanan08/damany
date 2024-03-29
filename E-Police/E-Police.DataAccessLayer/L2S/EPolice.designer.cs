﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_Police.DataAccessLayer.L2S
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="TrafficPolice")]
	public partial class EPoliceDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertMonitorSpot(MonitorSpot instance);
    partial void UpdateMonitorSpot(MonitorSpot instance);
    partial void DeleteMonitorSpot(MonitorSpot instance);
    partial void InsertTrafficPoliceDepartment(TrafficPoliceDepartment instance);
    partial void UpdateTrafficPoliceDepartment(TrafficPoliceDepartment instance);
    partial void DeleteTrafficPoliceDepartment(TrafficPoliceDepartment instance);
    partial void InsertOwner(Owner instance);
    partial void UpdateOwner(Owner instance);
    partial void DeleteOwner(Owner instance);
    partial void InsertTrafficViolentionEvent(TrafficViolentionEvent instance);
    partial void UpdateTrafficViolentionEvent(TrafficViolentionEvent instance);
    partial void DeleteTrafficViolentionEvent(TrafficViolentionEvent instance);
    partial void InsertVehicle(Vehicle instance);
    partial void UpdateVehicle(Vehicle instance);
    partial void DeleteVehicle(Vehicle instance);
    #endregion
		
		public EPoliceDataContext() : 
				base(global::E_Police.DataAccessLayer.Properties.Settings.Default.TrafficPoliceConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public EPoliceDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EPoliceDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EPoliceDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EPoliceDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<MonitorSpot> MonitorSpots
		{
			get
			{
				return this.GetTable<MonitorSpot>();
			}
		}
		
		public System.Data.Linq.Table<TrafficPoliceDepartment> TrafficPoliceDepartments
		{
			get
			{
				return this.GetTable<TrafficPoliceDepartment>();
			}
		}
		
		public System.Data.Linq.Table<Owner> Owners
		{
			get
			{
				return this.GetTable<Owner>();
			}
		}
		
		public System.Data.Linq.Table<TrafficViolentionEvent> TrafficViolentionEvents
		{
			get
			{
				return this.GetTable<TrafficViolentionEvent>();
			}
		}
		
		public System.Data.Linq.Table<Vehicle> Vehicles
		{
			get
			{
				return this.GetTable<Vehicle>();
			}
		}
	}
	
	[Table(Name="dbo.MonitorSpots")]
	public partial class MonitorSpot : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _ConnectionString;
		
		private string _Name;
		
		private int _MonitorSpotID;
		
		private int _MonitoredBy;
		
		private EntitySet<TrafficViolentionEvent> _TrafficViolentionEvents;
		
		private EntityRef<TrafficPoliceDepartment> _TrafficPoliceDepartment;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnConnectionStringChanging(string value);
    partial void OnConnectionStringChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnMonitorSpotIDChanging(int value);
    partial void OnMonitorSpotIDChanged();
    partial void OnMonitoredByChanging(int value);
    partial void OnMonitoredByChanged();
    #endregion
		
		public MonitorSpot()
		{
			this._TrafficViolentionEvents = new EntitySet<TrafficViolentionEvent>(new Action<TrafficViolentionEvent>(this.attach_TrafficViolentionEvents), new Action<TrafficViolentionEvent>(this.detach_TrafficViolentionEvents));
			this._TrafficPoliceDepartment = default(EntityRef<TrafficPoliceDepartment>);
			OnCreated();
		}
		
		[Column(Storage="_ConnectionString", DbType="NVarChar(50)")]
		public string ConnectionString
		{
			get
			{
				return this._ConnectionString;
			}
			set
			{
				if ((this._ConnectionString != value))
				{
					this.OnConnectionStringChanging(value);
					this.SendPropertyChanging();
					this._ConnectionString = value;
					this.SendPropertyChanged("ConnectionString");
					this.OnConnectionStringChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(30) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_MonitorSpotID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int MonitorSpotID
		{
			get
			{
				return this._MonitorSpotID;
			}
			set
			{
				if ((this._MonitorSpotID != value))
				{
					this.OnMonitorSpotIDChanging(value);
					this.SendPropertyChanging();
					this._MonitorSpotID = value;
					this.SendPropertyChanged("MonitorSpotID");
					this.OnMonitorSpotIDChanged();
				}
			}
		}
		
		[Column(Storage="_MonitoredBy", DbType="Int NOT NULL")]
		public int MonitoredBy
		{
			get
			{
				return this._MonitoredBy;
			}
			set
			{
				if ((this._MonitoredBy != value))
				{
					if (this._TrafficPoliceDepartment.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMonitoredByChanging(value);
					this.SendPropertyChanging();
					this._MonitoredBy = value;
					this.SendPropertyChanged("MonitoredBy");
					this.OnMonitoredByChanged();
				}
			}
		}
		
		[Association(Name="MonitorSpot_TrafficViolentionEvent", Storage="_TrafficViolentionEvents", ThisKey="MonitorSpotID", OtherKey="CapturedAt")]
		public EntitySet<TrafficViolentionEvent> TrafficViolentionEvents
		{
			get
			{
				return this._TrafficViolentionEvents;
			}
			set
			{
				this._TrafficViolentionEvents.Assign(value);
			}
		}
		
		[Association(Name="TrafficPoliceDepartment_MonitorSpot", Storage="_TrafficPoliceDepartment", ThisKey="MonitoredBy", OtherKey="DepartmentID", IsForeignKey=true)]
		public TrafficPoliceDepartment TrafficPoliceDepartment
		{
			get
			{
				return this._TrafficPoliceDepartment.Entity;
			}
			set
			{
				TrafficPoliceDepartment previousValue = this._TrafficPoliceDepartment.Entity;
				if (((previousValue != value) 
							|| (this._TrafficPoliceDepartment.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._TrafficPoliceDepartment.Entity = null;
						previousValue.MonitorSpots.Remove(this);
					}
					this._TrafficPoliceDepartment.Entity = value;
					if ((value != null))
					{
						value.MonitorSpots.Add(this);
						this._MonitoredBy = value.DepartmentID;
					}
					else
					{
						this._MonitoredBy = default(int);
					}
					this.SendPropertyChanged("TrafficPoliceDepartment");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_TrafficViolentionEvents(TrafficViolentionEvent entity)
		{
			this.SendPropertyChanging();
			entity.MonitorSpot = this;
		}
		
		private void detach_TrafficViolentionEvents(TrafficViolentionEvent entity)
		{
			this.SendPropertyChanging();
			entity.MonitorSpot = null;
		}
	}
	
	[Table(Name="dbo.TrafficPoliceDepartments")]
	public partial class TrafficPoliceDepartment : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Address;
		
		private int _DepartmentID;
		
		private string _Name;
		
		private string _OfficeReportTo;
		
		private string _OfficeAddress;
		
		private EntitySet<MonitorSpot> _MonitorSpots;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnAddressChanging(string value);
    partial void OnAddressChanged();
    partial void OnDepartmentIDChanging(int value);
    partial void OnDepartmentIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnOfficeReportToChanging(string value);
    partial void OnOfficeReportToChanged();
    partial void OnOfficeAddressChanging(string value);
    partial void OnOfficeAddressChanged();
    #endregion
		
		public TrafficPoliceDepartment()
		{
			this._MonitorSpots = new EntitySet<MonitorSpot>(new Action<MonitorSpot>(this.attach_MonitorSpots), new Action<MonitorSpot>(this.detach_MonitorSpots));
			OnCreated();
		}
		
		[Column(Storage="_Address", DbType="NVarChar(50)")]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this.OnAddressChanging(value);
					this.SendPropertyChanging();
					this._Address = value;
					this.SendPropertyChanged("Address");
					this.OnAddressChanged();
				}
			}
		}
		
		[Column(Storage="_DepartmentID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int DepartmentID
		{
			get
			{
				return this._DepartmentID;
			}
			set
			{
				if ((this._DepartmentID != value))
				{
					this.OnDepartmentIDChanging(value);
					this.SendPropertyChanging();
					this._DepartmentID = value;
					this.SendPropertyChanged("DepartmentID");
					this.OnDepartmentIDChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_OfficeReportTo", DbType="NVarChar(50)")]
		public string OfficeReportTo
		{
			get
			{
				return this._OfficeReportTo;
			}
			set
			{
				if ((this._OfficeReportTo != value))
				{
					this.OnOfficeReportToChanging(value);
					this.SendPropertyChanging();
					this._OfficeReportTo = value;
					this.SendPropertyChanged("OfficeReportTo");
					this.OnOfficeReportToChanged();
				}
			}
		}
		
		[Column(Storage="_OfficeAddress", DbType="NVarChar(50)")]
		public string OfficeAddress
		{
			get
			{
				return this._OfficeAddress;
			}
			set
			{
				if ((this._OfficeAddress != value))
				{
					this.OnOfficeAddressChanging(value);
					this.SendPropertyChanging();
					this._OfficeAddress = value;
					this.SendPropertyChanged("OfficeAddress");
					this.OnOfficeAddressChanged();
				}
			}
		}
		
		[Association(Name="TrafficPoliceDepartment_MonitorSpot", Storage="_MonitorSpots", ThisKey="DepartmentID", OtherKey="MonitoredBy")]
		public EntitySet<MonitorSpot> MonitorSpots
		{
			get
			{
				return this._MonitorSpots;
			}
			set
			{
				this._MonitorSpots.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_MonitorSpots(MonitorSpot entity)
		{
			this.SendPropertyChanging();
			entity.TrafficPoliceDepartment = this;
		}
		
		private void detach_MonitorSpots(MonitorSpot entity)
		{
			this.SendPropertyChanging();
			entity.TrafficPoliceDepartment = null;
		}
	}
	
	[Table(Name="dbo.Owners")]
	public partial class Owner : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Addr;
		
		private string _Name;
		
		private int _OwnerID;
		
		private string _Phone;
		
		private EntitySet<Vehicle> _Vehicles;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnAddrChanging(string value);
    partial void OnAddrChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnOwnerIDChanging(int value);
    partial void OnOwnerIDChanged();
    partial void OnPhoneChanging(string value);
    partial void OnPhoneChanged();
    #endregion
		
		public Owner()
		{
			this._Vehicles = new EntitySet<Vehicle>(new Action<Vehicle>(this.attach_Vehicles), new Action<Vehicle>(this.detach_Vehicles));
			OnCreated();
		}
		
		[Column(Storage="_Addr", DbType="NVarChar(50)")]
		public string Addr
		{
			get
			{
				return this._Addr;
			}
			set
			{
				if ((this._Addr != value))
				{
					this.OnAddrChanging(value);
					this.SendPropertyChanging();
					this._Addr = value;
					this.SendPropertyChanged("Addr");
					this.OnAddrChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(30) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_OwnerID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int OwnerID
		{
			get
			{
				return this._OwnerID;
			}
			set
			{
				if ((this._OwnerID != value))
				{
					this.OnOwnerIDChanging(value);
					this.SendPropertyChanging();
					this._OwnerID = value;
					this.SendPropertyChanged("OwnerID");
					this.OnOwnerIDChanged();
				}
			}
		}
		
		[Column(Storage="_Phone", DbType="NVarChar(30)")]
		public string Phone
		{
			get
			{
				return this._Phone;
			}
			set
			{
				if ((this._Phone != value))
				{
					this.OnPhoneChanging(value);
					this.SendPropertyChanging();
					this._Phone = value;
					this.SendPropertyChanged("Phone");
					this.OnPhoneChanged();
				}
			}
		}
		
		[Association(Name="Owner_Vehicle", Storage="_Vehicles", ThisKey="OwnerID", OtherKey="OwnerID")]
		public EntitySet<Vehicle> Vehicles
		{
			get
			{
				return this._Vehicles;
			}
			set
			{
				this._Vehicles.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Vehicles(Vehicle entity)
		{
			this.SendPropertyChanging();
			entity.Owner = this;
		}
		
		private void detach_Vehicles(Vehicle entity)
		{
			this.SendPropertyChanging();
			entity.Owner = null;
		}
	}
	
	[Table(Name="dbo.TrafficViolentionEvents")]
	public partial class TrafficViolentionEvent : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Description;
		
		private System.Data.Linq.Binary _EvidencePicture;
		
		private System.DateTime _Time;
		
		private System.Nullable<int> _VehicleID;
		
		private System.Nullable<int> _CapturedAt;
		
		private int _EventID;
		
		private EntityRef<MonitorSpot> _MonitorSpot;
		
		private EntityRef<Vehicle> _Vehicle;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnEvidencePictureChanging(System.Data.Linq.Binary value);
    partial void OnEvidencePictureChanged();
    partial void OnTimeChanging(System.DateTime value);
    partial void OnTimeChanged();
    partial void OnVehicleIDChanging(System.Nullable<int> value);
    partial void OnVehicleIDChanged();
    partial void OnCapturedAtChanging(System.Nullable<int> value);
    partial void OnCapturedAtChanged();
    partial void OnEventIDChanging(int value);
    partial void OnEventIDChanged();
    #endregion
		
		public TrafficViolentionEvent()
		{
			this._MonitorSpot = default(EntityRef<MonitorSpot>);
			this._Vehicle = default(EntityRef<Vehicle>);
			OnCreated();
		}
		
		[Column(Storage="_Description", DbType="NVarChar(100)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_EvidencePicture", DbType="Image", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary EvidencePicture
		{
			get
			{
				return this._EvidencePicture;
			}
			set
			{
				if ((this._EvidencePicture != value))
				{
					this.OnEvidencePictureChanging(value);
					this.SendPropertyChanging();
					this._EvidencePicture = value;
					this.SendPropertyChanged("EvidencePicture");
					this.OnEvidencePictureChanged();
				}
			}
		}
		
		[Column(Storage="_Time", DbType="DateTime NOT NULL")]
		public System.DateTime Time
		{
			get
			{
				return this._Time;
			}
			set
			{
				if ((this._Time != value))
				{
					this.OnTimeChanging(value);
					this.SendPropertyChanging();
					this._Time = value;
					this.SendPropertyChanged("Time");
					this.OnTimeChanged();
				}
			}
		}
		
		[Column(Storage="_VehicleID", DbType="Int")]
		public System.Nullable<int> VehicleID
		{
			get
			{
				return this._VehicleID;
			}
			set
			{
				if ((this._VehicleID != value))
				{
					if (this._Vehicle.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnVehicleIDChanging(value);
					this.SendPropertyChanging();
					this._VehicleID = value;
					this.SendPropertyChanged("VehicleID");
					this.OnVehicleIDChanged();
				}
			}
		}
		
		[Column(Storage="_CapturedAt", DbType="Int")]
		public System.Nullable<int> CapturedAt
		{
			get
			{
				return this._CapturedAt;
			}
			set
			{
				if ((this._CapturedAt != value))
				{
					if (this._MonitorSpot.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCapturedAtChanging(value);
					this.SendPropertyChanging();
					this._CapturedAt = value;
					this.SendPropertyChanged("CapturedAt");
					this.OnCapturedAtChanged();
				}
			}
		}
		
		[Column(Storage="_EventID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int EventID
		{
			get
			{
				return this._EventID;
			}
			set
			{
				if ((this._EventID != value))
				{
					this.OnEventIDChanging(value);
					this.SendPropertyChanging();
					this._EventID = value;
					this.SendPropertyChanged("EventID");
					this.OnEventIDChanged();
				}
			}
		}
		
		[Association(Name="MonitorSpot_TrafficViolentionEvent", Storage="_MonitorSpot", ThisKey="CapturedAt", OtherKey="MonitorSpotID", IsForeignKey=true)]
		public MonitorSpot MonitorSpot
		{
			get
			{
				return this._MonitorSpot.Entity;
			}
			set
			{
				MonitorSpot previousValue = this._MonitorSpot.Entity;
				if (((previousValue != value) 
							|| (this._MonitorSpot.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._MonitorSpot.Entity = null;
						previousValue.TrafficViolentionEvents.Remove(this);
					}
					this._MonitorSpot.Entity = value;
					if ((value != null))
					{
						value.TrafficViolentionEvents.Add(this);
						this._CapturedAt = value.MonitorSpotID;
					}
					else
					{
						this._CapturedAt = default(Nullable<int>);
					}
					this.SendPropertyChanged("MonitorSpot");
				}
			}
		}
		
		[Association(Name="Vehicle_TrafficViolentionEvent", Storage="_Vehicle", ThisKey="VehicleID", OtherKey="VehicleID", IsForeignKey=true)]
		public Vehicle Vehicle
		{
			get
			{
				return this._Vehicle.Entity;
			}
			set
			{
				Vehicle previousValue = this._Vehicle.Entity;
				if (((previousValue != value) 
							|| (this._Vehicle.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Vehicle.Entity = null;
						previousValue.TrafficViolentionEvents.Remove(this);
					}
					this._Vehicle.Entity = value;
					if ((value != null))
					{
						value.TrafficViolentionEvents.Add(this);
						this._VehicleID = value.VehicleID;
					}
					else
					{
						this._VehicleID = default(Nullable<int>);
					}
					this.SendPropertyChanged("Vehicle");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.Vehicles")]
	public partial class Vehicle : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Category;
		
		private int _VehicleID;
		
		private string _LicenseNumber;
		
		private string _LicenseAreaCode;
		
		private int _OwnerID;
		
		private EntitySet<TrafficViolentionEvent> _TrafficViolentionEvents;
		
		private EntityRef<Owner> _Owner;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCategoryChanging(string value);
    partial void OnCategoryChanged();
    partial void OnVehicleIDChanging(int value);
    partial void OnVehicleIDChanged();
    partial void OnLicenseNumberChanging(string value);
    partial void OnLicenseNumberChanged();
    partial void OnLicenseAreaCodeChanging(string value);
    partial void OnLicenseAreaCodeChanged();
    partial void OnOwnerIDChanging(int value);
    partial void OnOwnerIDChanged();
    #endregion
		
		public Vehicle()
		{
			this._TrafficViolentionEvents = new EntitySet<TrafficViolentionEvent>(new Action<TrafficViolentionEvent>(this.attach_TrafficViolentionEvents), new Action<TrafficViolentionEvent>(this.detach_TrafficViolentionEvents));
			this._Owner = default(EntityRef<Owner>);
			OnCreated();
		}
		
		[Column(Storage="_Category", DbType="NVarChar(20)")]
		public string Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if ((this._Category != value))
				{
					this.OnCategoryChanging(value);
					this.SendPropertyChanging();
					this._Category = value;
					this.SendPropertyChanged("Category");
					this.OnCategoryChanged();
				}
			}
		}
		
		[Column(Storage="_VehicleID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int VehicleID
		{
			get
			{
				return this._VehicleID;
			}
			set
			{
				if ((this._VehicleID != value))
				{
					this.OnVehicleIDChanging(value);
					this.SendPropertyChanging();
					this._VehicleID = value;
					this.SendPropertyChanged("VehicleID");
					this.OnVehicleIDChanged();
				}
			}
		}
		
		[Column(Storage="_LicenseNumber", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
		public string LicenseNumber
		{
			get
			{
				return this._LicenseNumber;
			}
			set
			{
				if ((this._LicenseNumber != value))
				{
					this.OnLicenseNumberChanging(value);
					this.SendPropertyChanging();
					this._LicenseNumber = value;
					this.SendPropertyChanged("LicenseNumber");
					this.OnLicenseNumberChanged();
				}
			}
		}
		
		[Column(Storage="_LicenseAreaCode", DbType="NVarChar(10) NOT NULL", CanBeNull=false)]
		public string LicenseAreaCode
		{
			get
			{
				return this._LicenseAreaCode;
			}
			set
			{
				if ((this._LicenseAreaCode != value))
				{
					this.OnLicenseAreaCodeChanging(value);
					this.SendPropertyChanging();
					this._LicenseAreaCode = value;
					this.SendPropertyChanged("LicenseAreaCode");
					this.OnLicenseAreaCodeChanged();
				}
			}
		}
		
		[Column(Storage="_OwnerID", DbType="Int NOT NULL")]
		public int OwnerID
		{
			get
			{
				return this._OwnerID;
			}
			set
			{
				if ((this._OwnerID != value))
				{
					if (this._Owner.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnOwnerIDChanging(value);
					this.SendPropertyChanging();
					this._OwnerID = value;
					this.SendPropertyChanged("OwnerID");
					this.OnOwnerIDChanged();
				}
			}
		}
		
		[Association(Name="Vehicle_TrafficViolentionEvent", Storage="_TrafficViolentionEvents", ThisKey="VehicleID", OtherKey="VehicleID")]
		public EntitySet<TrafficViolentionEvent> TrafficViolentionEvents
		{
			get
			{
				return this._TrafficViolentionEvents;
			}
			set
			{
				this._TrafficViolentionEvents.Assign(value);
			}
		}
		
		[Association(Name="Owner_Vehicle", Storage="_Owner", ThisKey="OwnerID", OtherKey="OwnerID", IsForeignKey=true)]
		public Owner Owner
		{
			get
			{
				return this._Owner.Entity;
			}
			set
			{
				Owner previousValue = this._Owner.Entity;
				if (((previousValue != value) 
							|| (this._Owner.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Owner.Entity = null;
						previousValue.Vehicles.Remove(this);
					}
					this._Owner.Entity = value;
					if ((value != null))
					{
						value.Vehicles.Add(this);
						this._OwnerID = value.OwnerID;
					}
					else
					{
						this._OwnerID = default(int);
					}
					this.SendPropertyChanged("Owner");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_TrafficViolentionEvents(TrafficViolentionEvent entity)
		{
			this.SendPropertyChanging();
			entity.Vehicle = this;
		}
		
		private void detach_TrafficViolentionEvents(TrafficViolentionEvent entity)
		{
			this.SendPropertyChanging();
			entity.Vehicle = null;
		}
	}
}
#pragma warning restore 1591
