#pragma warning disable 1591
namespace Models
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
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="gdos7825")]
	public partial class TerritoryDataContext : System.Data.Linq.DataContext
	{
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
    partial void OnCreated();
    partial void InsertUserCongregationRole(UserCongregationRole instance);
    partial void UpdateUserCongregationRole(UserCongregationRole instance);
    partial void DeleteUserCongregationRole(UserCongregationRole instance);
    partial void InsertUserApplication(UserApplication instance);
    partial void UpdateUserApplication(UserApplication instance);
    partial void DeleteUserApplication(UserApplication instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertRole(Role instance);
    partial void UpdateRole(Role instance);
    partial void DeleteRole(Role instance);
    partial void InsertCongregation(Congregation instance);
    partial void UpdateCongregation(Congregation instance);
    partial void DeleteCongregation(Congregation instance);
    partial void InsertApplication(Application instance);
    partial void UpdateApplication(Application instance);
    partial void DeleteApplication(Application instance);
    partial void InsertT_Territory(T_Territory instance);
    partial void UpdateT_Territory(T_Territory instance);
    partial void DeleteT_Territory(T_Territory instance);
    partial void InsertT_Work(T_Work instance);
    partial void UpdateT_Work(T_Work instance);
    partial void DeleteT_Work(T_Work instance);
    partial void InsertT_TerritoryType(T_TerritoryType instance);
    partial void UpdateT_TerritoryType(T_TerritoryType instance);
    partial void DeleteT_TerritoryType(T_TerritoryType instance);
    partial void InsertT_Publisher(T_Publisher instance);
    partial void UpdateT_Publisher(T_Publisher instance);
    partial void DeleteT_Publisher(T_Publisher instance);
    partial void InsertT_SpecialCampaign(T_SpecialCampaign instance);
    partial void UpdateT_SpecialCampaign(T_SpecialCampaign instance);
    partial void DeleteT_SpecialCampaign(T_SpecialCampaign instance);
    partial void InsertT_Area(T_Area instance);
    partial void UpdateT_Area(T_Area instance);
    partial void DeleteT_Area(T_Area instance);
    partial void InsertT_DoNotCall(T_DoNotCall instance);
    partial void UpdateT_DoNotCall(T_DoNotCall instance);
    partial void DeleteT_DoNotCall(T_DoNotCall instance);
		public TerritoryDataContext() : 
				base(global::Models.Properties.Settings.Default.gdos7825ConnectionString, mappingSource)
		{
			OnCreated();
		}
		public TerritoryDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		public TerritoryDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		public TerritoryDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		public TerritoryDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		public System.Data.Linq.Table<UserCongregationRole> UserCongregationRoles
		{
			get
			{
				return this.GetTable<UserCongregationRole>();
			}
		}
		public System.Data.Linq.Table<UserApplication> UserApplications
		{
			get
			{
				return this.GetTable<UserApplication>();
			}
		}
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		public System.Data.Linq.Table<Role> Roles
		{
			get
			{
				return this.GetTable<Role>();
			}
		}
		public System.Data.Linq.Table<Congregation> Congregations
		{
			get
			{
				return this.GetTable<Congregation>();
			}
		}
		public System.Data.Linq.Table<Application> Applications
		{
			get
			{
				return this.GetTable<Application>();
			}
		}
		public System.Data.Linq.Table<T_Territory> T_Territories
		{
			get
			{
				return this.GetTable<T_Territory>();
			}
		}
		public System.Data.Linq.Table<T_Work> T_Works
		{
			get
			{
				return this.GetTable<T_Work>();
			}
		}
		public System.Data.Linq.Table<T_TerritoryType> T_TerritoryTypes
		{
			get
			{
				return this.GetTable<T_TerritoryType>();
			}
		}
		public System.Data.Linq.Table<T_Publisher> T_Publishers
		{
			get
			{
				return this.GetTable<T_Publisher>();
			}
		}
		public System.Data.Linq.Table<T_SpecialCampaign> T_SpecialCampaigns
		{
			get
			{
				return this.GetTable<T_SpecialCampaign>();
			}
		}
		public System.Data.Linq.Table<T_Area> T_Areas
		{
			get
			{
				return this.GetTable<T_Area>();
			}
		}
		public System.Data.Linq.Table<T_DoNotCall> T_DoNotCalls
		{
			get
			{
				return this.GetTable<T_DoNotCall>();
			}
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UserCongregationRole")]
	public partial class UserCongregationRole : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private int _UserId;
		private int _CongregationId;
		private int _RoleId;
		private EntityRef<User> _User;
		private EntityRef<Role> _Role;
		private EntityRef<Congregation> _Congregation;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnCongregationIdChanging(int value);
    partial void OnCongregationIdChanged();
    partial void OnRoleIdChanging(int value);
    partial void OnRoleIdChanged();
		public UserCongregationRole()
		{
			this._User = default(EntityRef<User>);
			this._Role = default(EntityRef<Role>);
			this._Congregation = default(EntityRef<Congregation>);
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CongregationId", DbType="Int NOT NULL")]
		public int CongregationId
		{
			get
			{
				return this._CongregationId;
			}
			set
			{
				if ((this._CongregationId != value))
				{
					if (this._Congregation.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCongregationIdChanging(value);
					this.SendPropertyChanging();
					this._CongregationId = value;
					this.SendPropertyChanged("CongregationId");
					this.OnCongregationIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoleId", DbType="Int NOT NULL")]
		public int RoleId
		{
			get
			{
				return this._RoleId;
			}
			set
			{
				if ((this._RoleId != value))
				{
					if (this._Role.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRoleIdChanging(value);
					this.SendPropertyChanging();
					this._RoleId = value;
					this.SendPropertyChanged("RoleId");
					this.OnRoleIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserCongregationRole", Storage="_User", ThisKey="UserId", OtherKey="Id", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.UserCongregationRoles.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.UserCongregationRoles.Add(this);
						this._UserId = value.Id;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Role_UserCongregationRole", Storage="_Role", ThisKey="RoleId", OtherKey="Id", IsForeignKey=true)]
		public Role Role
		{
			get
			{
				return this._Role.Entity;
			}
			set
			{
				Role previousValue = this._Role.Entity;
				if (((previousValue != value) 
							|| (this._Role.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Role.Entity = null;
						previousValue.UserCongregationRoles.Remove(this);
					}
					this._Role.Entity = value;
					if ((value != null))
					{
						value.UserCongregationRoles.Add(this);
						this._RoleId = value.Id;
					}
					else
					{
						this._RoleId = default(int);
					}
					this.SendPropertyChanged("Role");
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Congregation_UserCongregationRole", Storage="_Congregation", ThisKey="CongregationId", OtherKey="Id", IsForeignKey=true)]
		public Congregation Congregation
		{
			get
			{
				return this._Congregation.Entity;
			}
			set
			{
				Congregation previousValue = this._Congregation.Entity;
				if (((previousValue != value) 
							|| (this._Congregation.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Congregation.Entity = null;
						previousValue.UserCongregationRoles.Remove(this);
					}
					this._Congregation.Entity = value;
					if ((value != null))
					{
						value.UserCongregationRoles.Add(this);
						this._CongregationId = value.Id;
					}
					else
					{
						this._CongregationId = default(int);
					}
					this.SendPropertyChanged("Congregation");
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
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UserApplication")]
	public partial class UserApplication : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private int _ApplicationId;
		private int _UserId;
		private EntityRef<User> _User;
		private EntityRef<Application> _Application;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnApplicationIdChanging(int value);
    partial void OnApplicationIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
		public UserApplication()
		{
			this._User = default(EntityRef<User>);
			this._Application = default(EntityRef<Application>);
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ApplicationId", DbType="Int NOT NULL")]
		public int ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				if ((this._ApplicationId != value))
				{
					if (this._Application.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnApplicationIdChanging(value);
					this.SendPropertyChanging();
					this._ApplicationId = value;
					this.SendPropertyChanged("ApplicationId");
					this.OnApplicationIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserApplication", Storage="_User", ThisKey="UserId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.UserApplications.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.UserApplications.Add(this);
						this._UserId = value.Id;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Application_UserApplication", Storage="_Application", ThisKey="ApplicationId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Application Application
		{
			get
			{
				return this._Application.Entity;
			}
			set
			{
				Application previousValue = this._Application.Entity;
				if (((previousValue != value) 
							|| (this._Application.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Application.Entity = null;
						previousValue.UserApplications.Remove(this);
					}
					this._Application.Entity = value;
					if ((value != null))
					{
						value.UserApplications.Add(this);
						this._ApplicationId = value.Id;
					}
					else
					{
						this._ApplicationId = default(int);
					}
					this.SendPropertyChanged("Application");
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
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[User]")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private string _FirstName;
		private string _LastName;
		private string _UserId;
		private string _Password;
		private bool _IsAdmin;
		private string _EMail;
		private System.DateTime _Created;
		private int _RegionId;
		private bool _PasswordChangeRequired;
		private System.Nullable<System.DateTime> _LastLogin;
		private bool _IsLoggedIn;
		private System.Nullable<System.DateTime> _LastRequest;
		private EntitySet<UserCongregationRole> _UserCongregationRoles;
		private EntitySet<UserApplication> _UserApplications;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnUserIdChanging(string value);
    partial void OnUserIdChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    partial void OnIsAdminChanging(bool value);
    partial void OnIsAdminChanged();
    partial void OnEMailChanging(string value);
    partial void OnEMailChanged();
    partial void OnCreatedChanging(System.DateTime value);
    partial void OnCreatedChanged();
    partial void OnRegionIdChanging(int value);
    partial void OnRegionIdChanged();
    partial void OnPasswordChangeRequiredChanging(bool value);
    partial void OnPasswordChangeRequiredChanged();
    partial void OnLastLoginChanging(System.Nullable<System.DateTime> value);
    partial void OnLastLoginChanged();
    partial void OnIsLoggedInChanging(bool value);
    partial void OnIsLoggedInChanged();
    partial void OnLastRequestChanging(System.Nullable<System.DateTime> value);
    partial void OnLastRequestChanged();
		public User()
		{
			this._UserCongregationRoles = new EntitySet<UserCongregationRole>(new Action<UserCongregationRole>(this.attach_UserCongregationRoles), new Action<UserCongregationRole>(this.detach_UserCongregationRoles));
			this._UserApplications = new EntitySet<UserApplication>(new Action<UserApplication>(this.attach_UserApplications), new Action<UserApplication>(this.detach_UserApplications));
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsAdmin", DbType="Bit NOT NULL")]
		public bool IsAdmin
		{
			get
			{
				return this._IsAdmin;
			}
			set
			{
				if ((this._IsAdmin != value))
				{
					this.OnIsAdminChanging(value);
					this.SendPropertyChanging();
					this._IsAdmin = value;
					this.SendPropertyChanged("IsAdmin");
					this.OnIsAdminChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EMail", DbType="NVarChar(255)")]
		public string EMail
		{
			get
			{
				return this._EMail;
			}
			set
			{
				if ((this._EMail != value))
				{
					this.OnEMailChanging(value);
					this.SendPropertyChanging();
					this._EMail = value;
					this.SendPropertyChanged("EMail");
					this.OnEMailChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Created", DbType="DateTime NOT NULL")]
		public System.DateTime Created
		{
			get
			{
				return this._Created;
			}
			set
			{
				if ((this._Created != value))
				{
					this.OnCreatedChanging(value);
					this.SendPropertyChanging();
					this._Created = value;
					this.SendPropertyChanged("Created");
					this.OnCreatedChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RegionId", DbType="Int NOT NULL")]
		public int RegionId
		{
			get
			{
				return this._RegionId;
			}
			set
			{
				if ((this._RegionId != value))
				{
					this.OnRegionIdChanging(value);
					this.SendPropertyChanging();
					this._RegionId = value;
					this.SendPropertyChanged("RegionId");
					this.OnRegionIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PasswordChangeRequired", DbType="Bit NOT NULL")]
		public bool PasswordChangeRequired
		{
			get
			{
				return this._PasswordChangeRequired;
			}
			set
			{
				if ((this._PasswordChangeRequired != value))
				{
					this.OnPasswordChangeRequiredChanging(value);
					this.SendPropertyChanging();
					this._PasswordChangeRequired = value;
					this.SendPropertyChanged("PasswordChangeRequired");
					this.OnPasswordChangeRequiredChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastLogin", DbType="DateTime")]
		public System.Nullable<System.DateTime> LastLogin
		{
			get
			{
				return this._LastLogin;
			}
			set
			{
				if ((this._LastLogin != value))
				{
					this.OnLastLoginChanging(value);
					this.SendPropertyChanging();
					this._LastLogin = value;
					this.SendPropertyChanged("LastLogin");
					this.OnLastLoginChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsLoggedIn", DbType="Bit NOT NULL")]
		public bool IsLoggedIn
		{
			get
			{
				return this._IsLoggedIn;
			}
			set
			{
				if ((this._IsLoggedIn != value))
				{
					this.OnIsLoggedInChanging(value);
					this.SendPropertyChanging();
					this._IsLoggedIn = value;
					this.SendPropertyChanged("IsLoggedIn");
					this.OnIsLoggedInChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastRequest", DbType="DateTime")]
		public System.Nullable<System.DateTime> LastRequest
		{
			get
			{
				return this._LastRequest;
			}
			set
			{
				if ((this._LastRequest != value))
				{
					this.OnLastRequestChanging(value);
					this.SendPropertyChanging();
					this._LastRequest = value;
					this.SendPropertyChanged("LastRequest");
					this.OnLastRequestChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserCongregationRole", Storage="_UserCongregationRoles", ThisKey="Id", OtherKey="UserId")]
		public EntitySet<UserCongregationRole> UserCongregationRoles
		{
			get
			{
				return this._UserCongregationRoles;
			}
			set
			{
				this._UserCongregationRoles.Assign(value);
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserApplication", Storage="_UserApplications", ThisKey="Id", OtherKey="UserId")]
		public EntitySet<UserApplication> UserApplications
		{
			get
			{
				return this._UserApplications;
			}
			set
			{
				this._UserApplications.Assign(value);
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
		private void attach_UserCongregationRoles(UserCongregationRole entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		private void detach_UserCongregationRoles(UserCongregationRole entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		private void attach_UserApplications(UserApplication entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		private void detach_UserApplications(UserApplication entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Role")]
	public partial class Role : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private string _Name;
		private string _Code;
		private EntitySet<UserCongregationRole> _UserCongregationRoles;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnCodeChanging(string value);
    partial void OnCodeChanged();
		public Role()
		{
			this._UserCongregationRoles = new EntitySet<UserCongregationRole>(new Action<UserCongregationRole>(this.attach_UserCongregationRoles), new Action<UserCongregationRole>(this.detach_UserCongregationRoles));
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", DbType="NChar(5) NOT NULL", CanBeNull=false)]
		public string Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this.OnCodeChanging(value);
					this.SendPropertyChanging();
					this._Code = value;
					this.SendPropertyChanged("Code");
					this.OnCodeChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Role_UserCongregationRole", Storage="_UserCongregationRoles", ThisKey="Id", OtherKey="RoleId")]
		public EntitySet<UserCongregationRole> UserCongregationRoles
		{
			get
			{
				return this._UserCongregationRoles;
			}
			set
			{
				this._UserCongregationRoles.Assign(value);
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
		private void attach_UserCongregationRoles(UserCongregationRole entity)
		{
			this.SendPropertyChanging();
			entity.Role = this;
		}
		private void detach_UserCongregationRoles(UserCongregationRole entity)
		{
			this.SendPropertyChanging();
			entity.Role = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Congregation")]
	public partial class Congregation : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private string _Name;
		private string _Code;
		private string _CityState;
		private EntitySet<UserCongregationRole> _UserCongregationRoles;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnCodeChanging(string value);
    partial void OnCodeChanged();
    partial void OnCityStateChanging(string value);
    partial void OnCityStateChanged();
		public Congregation()
		{
			this._UserCongregationRoles = new EntitySet<UserCongregationRole>(new Action<UserCongregationRole>(this.attach_UserCongregationRoles), new Action<UserCongregationRole>(this.detach_UserCongregationRoles));
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", DbType="NChar(5) NOT NULL", CanBeNull=false)]
		public string Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this.OnCodeChanging(value);
					this.SendPropertyChanging();
					this._Code = value;
					this.SendPropertyChanged("Code");
					this.OnCodeChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CityState", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string CityState
		{
			get
			{
				return this._CityState;
			}
			set
			{
				if ((this._CityState != value))
				{
					this.OnCityStateChanging(value);
					this.SendPropertyChanging();
					this._CityState = value;
					this.SendPropertyChanged("CityState");
					this.OnCityStateChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Congregation_UserCongregationRole", Storage="_UserCongregationRoles", ThisKey="Id", OtherKey="CongregationId")]
		public EntitySet<UserCongregationRole> UserCongregationRoles
		{
			get
			{
				return this._UserCongregationRoles;
			}
			set
			{
				this._UserCongregationRoles.Assign(value);
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
		private void attach_UserCongregationRoles(UserCongregationRole entity)
		{
			this.SendPropertyChanging();
			entity.Congregation = this;
		}
		private void detach_UserCongregationRoles(UserCongregationRole entity)
		{
			this.SendPropertyChanging();
			entity.Congregation = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Application")]
	public partial class Application : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private string _Name;
		private System.DateTime _Created;
		private EntitySet<UserApplication> _UserApplications;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnCreatedChanging(System.DateTime value);
    partial void OnCreatedChanged();
		public Application()
		{
			this._UserApplications = new EntitySet<UserApplication>(new Action<UserApplication>(this.attach_UserApplications), new Action<UserApplication>(this.detach_UserApplications));
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Created", DbType="DateTime NOT NULL")]
		public System.DateTime Created
		{
			get
			{
				return this._Created;
			}
			set
			{
				if ((this._Created != value))
				{
					this.OnCreatedChanging(value);
					this.SendPropertyChanging();
					this._Created = value;
					this.SendPropertyChanged("Created");
					this.OnCreatedChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Application_UserApplication", Storage="_UserApplications", ThisKey="Id", OtherKey="ApplicationId")]
		public EntitySet<UserApplication> UserApplications
		{
			get
			{
				return this._UserApplications;
			}
			set
			{
				this._UserApplications.Assign(value);
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
		private void attach_UserApplications(UserApplication entity)
		{
			this.SendPropertyChanging();
			entity.Application = this;
		}
		private void detach_UserApplications(UserApplication entity)
		{
			this.SendPropertyChanging();
			entity.Application = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_Territory")]
	public partial class T_Territory : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _ID;
		private string _Number;
		private System.Nullable<int> _AreaID;
		private string _FileName;
		private string _OtherFileName;
		private System.Nullable<int> _TerritoryTypeID;
		private EntitySet<T_Work> _T_Works;
		private EntitySet<T_DoNotCall> _T_DoNotCalls;
		private EntityRef<T_TerritoryType> _T_TerritoryType;
		private EntityRef<T_Area> _T_Area;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNumberChanging(string value);
    partial void OnNumberChanged();
    partial void OnAreaIDChanging(System.Nullable<int> value);
    partial void OnAreaIDChanged();
    partial void OnFileNameChanging(string value);
    partial void OnFileNameChanged();
    partial void OnOtherFileNameChanging(string value);
    partial void OnOtherFileNameChanged();
    partial void OnTerritoryTypeIDChanging(System.Nullable<int> value);
    partial void OnTerritoryTypeIDChanged();
		public T_Territory()
		{
			this._T_Works = new EntitySet<T_Work>(new Action<T_Work>(this.attach_T_Works), new Action<T_Work>(this.detach_T_Works));
			this._T_DoNotCalls = new EntitySet<T_DoNotCall>(new Action<T_DoNotCall>(this.attach_T_DoNotCalls), new Action<T_DoNotCall>(this.detach_T_DoNotCalls));
			this._T_TerritoryType = default(EntityRef<T_TerritoryType>);
			this._T_Area = default(EntityRef<T_Area>);
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Number", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string Number
		{
			get
			{
				return this._Number;
			}
			set
			{
				if ((this._Number != value))
				{
					this.OnNumberChanging(value);
					this.SendPropertyChanging();
					this._Number = value;
					this.SendPropertyChanged("Number");
					this.OnNumberChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AreaID", DbType="Int")]
		public System.Nullable<int> AreaID
		{
			get
			{
				return this._AreaID;
			}
			set
			{
				if ((this._AreaID != value))
				{
					if (this._T_Area.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAreaIDChanging(value);
					this.SendPropertyChanging();
					this._AreaID = value;
					this.SendPropertyChanged("AreaID");
					this.OnAreaIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileName", DbType="NVarChar(255)")]
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if ((this._FileName != value))
				{
					this.OnFileNameChanging(value);
					this.SendPropertyChanging();
					this._FileName = value;
					this.SendPropertyChanged("FileName");
					this.OnFileNameChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OtherFileName", DbType="NVarChar(255)")]
		public string OtherFileName
		{
			get
			{
				return this._OtherFileName;
			}
			set
			{
				if ((this._OtherFileName != value))
				{
					this.OnOtherFileNameChanging(value);
					this.SendPropertyChanging();
					this._OtherFileName = value;
					this.SendPropertyChanged("OtherFileName");
					this.OnOtherFileNameChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TerritoryTypeID", DbType="Int")]
		public System.Nullable<int> TerritoryTypeID
		{
			get
			{
				return this._TerritoryTypeID;
			}
			set
			{
				if ((this._TerritoryTypeID != value))
				{
					if (this._T_TerritoryType.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnTerritoryTypeIDChanging(value);
					this.SendPropertyChanging();
					this._TerritoryTypeID = value;
					this.SendPropertyChanged("TerritoryTypeID");
					this.OnTerritoryTypeIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_Territory_T_Work", Storage="_T_Works", ThisKey="ID", OtherKey="TerritoryID")]
		public EntitySet<T_Work> T_Works
		{
			get
			{
				return this._T_Works;
			}
			set
			{
				this._T_Works.Assign(value);
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_Territory_T_DoNotCall", Storage="_T_DoNotCalls", ThisKey="ID", OtherKey="TerritoryID")]
		public EntitySet<T_DoNotCall> T_DoNotCalls
		{
			get
			{
				return this._T_DoNotCalls;
			}
			set
			{
				this._T_DoNotCalls.Assign(value);
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_TerritoryType_T_Territory", Storage="_T_TerritoryType", ThisKey="TerritoryTypeID", OtherKey="ID", IsForeignKey=true, DeleteRule="CASCADE")]
		public T_TerritoryType T_TerritoryType
		{
			get
			{
				return this._T_TerritoryType.Entity;
			}
			set
			{
				T_TerritoryType previousValue = this._T_TerritoryType.Entity;
				if (((previousValue != value) 
							|| (this._T_TerritoryType.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._T_TerritoryType.Entity = null;
						previousValue.T_Territories.Remove(this);
					}
					this._T_TerritoryType.Entity = value;
					if ((value != null))
					{
						value.T_Territories.Add(this);
						this._TerritoryTypeID = value.ID;
					}
					else
					{
						this._TerritoryTypeID = default(Nullable<int>);
					}
					this.SendPropertyChanged("T_TerritoryType");
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_Area_T_Territory", Storage="_T_Area", ThisKey="AreaID", OtherKey="ID", IsForeignKey=true, DeleteRule="CASCADE")]
		public T_Area T_Area
		{
			get
			{
				return this._T_Area.Entity;
			}
			set
			{
				T_Area previousValue = this._T_Area.Entity;
				if (((previousValue != value) 
							|| (this._T_Area.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._T_Area.Entity = null;
						previousValue.T_Territories.Remove(this);
					}
					this._T_Area.Entity = value;
					if ((value != null))
					{
						value.T_Territories.Add(this);
						this._AreaID = value.ID;
					}
					else
					{
						this._AreaID = default(Nullable<int>);
					}
					this.SendPropertyChanged("T_Area");
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
		private void attach_T_Works(T_Work entity)
		{
			this.SendPropertyChanging();
			entity.T_Territory = this;
		}
		private void detach_T_Works(T_Work entity)
		{
			this.SendPropertyChanging();
			entity.T_Territory = null;
		}
		private void attach_T_DoNotCalls(T_DoNotCall entity)
		{
			this.SendPropertyChanging();
			entity.T_Territory = this;
		}
		private void detach_T_DoNotCalls(T_DoNotCall entity)
		{
			this.SendPropertyChanging();
			entity.T_Territory = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_Work")]
	public partial class T_Work : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _ID;
		private int _TerritoryID;
		private System.DateTime _StartDate;
		private System.Nullable<System.DateTime> _EndDate;
		private int _WorkBy;
		private System.Nullable<int> _SpecialCampaignID;
		private EntityRef<T_Territory> _T_Territory;
		private EntityRef<T_Publisher> _T_Publisher;
		private EntityRef<T_SpecialCampaign> _T_SpecialCampaign;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnTerritoryIDChanging(int value);
    partial void OnTerritoryIDChanged();
    partial void OnStartDateChanging(System.DateTime value);
    partial void OnStartDateChanged();
    partial void OnEndDateChanging(System.Nullable<System.DateTime> value);
    partial void OnEndDateChanged();
    partial void OnWorkByChanging(int value);
    partial void OnWorkByChanged();
    partial void OnSpecialCampaignIDChanging(System.Nullable<int> value);
    partial void OnSpecialCampaignIDChanged();
		public T_Work()
		{
			this._T_Territory = default(EntityRef<T_Territory>);
			this._T_Publisher = default(EntityRef<T_Publisher>);
			this._T_SpecialCampaign = default(EntityRef<T_SpecialCampaign>);
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TerritoryID", DbType="Int NOT NULL")]
		public int TerritoryID
		{
			get
			{
				return this._TerritoryID;
			}
			set
			{
				if ((this._TerritoryID != value))
				{
					if (this._T_Territory.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnTerritoryIDChanging(value);
					this.SendPropertyChanging();
					this._TerritoryID = value;
					this.SendPropertyChanged("TerritoryID");
					this.OnTerritoryIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartDate", DbType="DateTime NOT NULL")]
		public System.DateTime StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				if ((this._StartDate != value))
				{
					this.OnStartDateChanging(value);
					this.SendPropertyChanging();
					this._StartDate = value;
					this.SendPropertyChanged("StartDate");
					this.OnStartDateChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				if ((this._EndDate != value))
				{
					this.OnEndDateChanging(value);
					this.SendPropertyChanging();
					this._EndDate = value;
					this.SendPropertyChanged("EndDate");
					this.OnEndDateChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WorkBy", DbType="Int NOT NULL")]
		public int WorkBy
		{
			get
			{
				return this._WorkBy;
			}
			set
			{
				if ((this._WorkBy != value))
				{
					if (this._T_Publisher.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnWorkByChanging(value);
					this.SendPropertyChanging();
					this._WorkBy = value;
					this.SendPropertyChanged("WorkBy");
					this.OnWorkByChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SpecialCampaignID", DbType="Int")]
		public System.Nullable<int> SpecialCampaignID
		{
			get
			{
				return this._SpecialCampaignID;
			}
			set
			{
				if ((this._SpecialCampaignID != value))
				{
					if (this._T_SpecialCampaign.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSpecialCampaignIDChanging(value);
					this.SendPropertyChanging();
					this._SpecialCampaignID = value;
					this.SendPropertyChanged("SpecialCampaignID");
					this.OnSpecialCampaignIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_Territory_T_Work", Storage="_T_Territory", ThisKey="TerritoryID", OtherKey="ID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public T_Territory T_Territory
		{
			get
			{
				return this._T_Territory.Entity;
			}
			set
			{
				T_Territory previousValue = this._T_Territory.Entity;
				if (((previousValue != value) 
							|| (this._T_Territory.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._T_Territory.Entity = null;
						previousValue.T_Works.Remove(this);
					}
					this._T_Territory.Entity = value;
					if ((value != null))
					{
						value.T_Works.Add(this);
						this._TerritoryID = value.ID;
					}
					else
					{
						this._TerritoryID = default(int);
					}
					this.SendPropertyChanged("T_Territory");
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_Publisher_T_Work", Storage="_T_Publisher", ThisKey="WorkBy", OtherKey="ID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public T_Publisher T_Publisher
		{
			get
			{
				return this._T_Publisher.Entity;
			}
			set
			{
				T_Publisher previousValue = this._T_Publisher.Entity;
				if (((previousValue != value) 
							|| (this._T_Publisher.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._T_Publisher.Entity = null;
						previousValue.T_Works.Remove(this);
					}
					this._T_Publisher.Entity = value;
					if ((value != null))
					{
						value.T_Works.Add(this);
						this._WorkBy = value.ID;
					}
					else
					{
						this._WorkBy = default(int);
					}
					this.SendPropertyChanged("T_Publisher");
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_SpecialCampaign_T_Work", Storage="_T_SpecialCampaign", ThisKey="SpecialCampaignID", OtherKey="ID", IsForeignKey=true, DeleteRule="CASCADE")]
		public T_SpecialCampaign T_SpecialCampaign
		{
			get
			{
				return this._T_SpecialCampaign.Entity;
			}
			set
			{
				T_SpecialCampaign previousValue = this._T_SpecialCampaign.Entity;
				if (((previousValue != value) 
							|| (this._T_SpecialCampaign.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._T_SpecialCampaign.Entity = null;
						previousValue.T_Works.Remove(this);
					}
					this._T_SpecialCampaign.Entity = value;
					if ((value != null))
					{
						value.T_Works.Add(this);
						this._SpecialCampaignID = value.ID;
					}
					else
					{
						this._SpecialCampaignID = default(Nullable<int>);
					}
					this.SendPropertyChanged("T_SpecialCampaign");
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
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_TerritoryType")]
	public partial class T_TerritoryType : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _ID;
		private string _NAME;
		private EntitySet<T_Territory> _T_Territories;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNAMEChanging(string value);
    partial void OnNAMEChanged();
		public T_TerritoryType()
		{
			this._T_Territories = new EntitySet<T_Territory>(new Action<T_Territory>(this.attach_T_Territories), new Action<T_Territory>(this.detach_T_Territories));
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NAME", DbType="NVarChar(255)")]
		public string NAME
		{
			get
			{
				return this._NAME;
			}
			set
			{
				if ((this._NAME != value))
				{
					this.OnNAMEChanging(value);
					this.SendPropertyChanging();
					this._NAME = value;
					this.SendPropertyChanged("NAME");
					this.OnNAMEChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_TerritoryType_T_Territory", Storage="_T_Territories", ThisKey="ID", OtherKey="TerritoryTypeID")]
		public EntitySet<T_Territory> T_Territories
		{
			get
			{
				return this._T_Territories;
			}
			set
			{
				this._T_Territories.Assign(value);
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
		private void attach_T_Territories(T_Territory entity)
		{
			this.SendPropertyChanging();
			entity.T_TerritoryType = this;
		}
		private void detach_T_Territories(T_Territory entity)
		{
			this.SendPropertyChanging();
			entity.T_TerritoryType = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_Publisher")]
	public partial class T_Publisher : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _ID;
		private string _FirstName;
		private string _Lastname;
		private bool _Special;
		private EntitySet<T_Work> _T_Works;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastnameChanging(string value);
    partial void OnLastnameChanged();
    partial void OnSpecialChanging(bool value);
    partial void OnSpecialChanged();
		public T_Publisher()
		{
			this._T_Works = new EntitySet<T_Work>(new Action<T_Work>(this.attach_T_Works), new Action<T_Work>(this.detach_T_Works));
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Lastname", DbType="NVarChar(255)")]
		public string Lastname
		{
			get
			{
				return this._Lastname;
			}
			set
			{
				if ((this._Lastname != value))
				{
					this.OnLastnameChanging(value);
					this.SendPropertyChanging();
					this._Lastname = value;
					this.SendPropertyChanged("Lastname");
					this.OnLastnameChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Special", DbType="Bit NOT NULL")]
		public bool Special
		{
			get
			{
				return this._Special;
			}
			set
			{
				if ((this._Special != value))
				{
					this.OnSpecialChanging(value);
					this.SendPropertyChanging();
					this._Special = value;
					this.SendPropertyChanged("Special");
					this.OnSpecialChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_Publisher_T_Work", Storage="_T_Works", ThisKey="ID", OtherKey="WorkBy")]
		public EntitySet<T_Work> T_Works
		{
			get
			{
				return this._T_Works;
			}
			set
			{
				this._T_Works.Assign(value);
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
		private void attach_T_Works(T_Work entity)
		{
			this.SendPropertyChanging();
			entity.T_Publisher = this;
		}
		private void detach_T_Works(T_Work entity)
		{
			this.SendPropertyChanging();
			entity.T_Publisher = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_SpecialCampaign")]
	public partial class T_SpecialCampaign : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _ID;
		private string _Name;
		private EntitySet<T_Work> _T_Works;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
		public T_SpecialCampaign()
		{
			this._T_Works = new EntitySet<T_Work>(new Action<T_Work>(this.attach_T_Works), new Action<T_Work>(this.detach_T_Works));
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(255)")]
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
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_SpecialCampaign_T_Work", Storage="_T_Works", ThisKey="ID", OtherKey="SpecialCampaignID")]
		public EntitySet<T_Work> T_Works
		{
			get
			{
				return this._T_Works;
			}
			set
			{
				this._T_Works.Assign(value);
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
		private void attach_T_Works(T_Work entity)
		{
			this.SendPropertyChanging();
			entity.T_SpecialCampaign = this;
		}
		private void detach_T_Works(T_Work entity)
		{
			this.SendPropertyChanging();
			entity.T_SpecialCampaign = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_Area")]
	public partial class T_Area : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _ID;
		private string _Value;
		private EntitySet<T_Territory> _T_Territories;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnValueChanging(string value);
    partial void OnValueChanged();
		public T_Area()
		{
			this._T_Territories = new EntitySet<T_Territory>(new Action<T_Territory>(this.attach_T_Territories), new Action<T_Territory>(this.detach_T_Territories));
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Value", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if ((this._Value != value))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._Value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_Area_T_Territory", Storage="_T_Territories", ThisKey="ID", OtherKey="AreaID")]
		public EntitySet<T_Territory> T_Territories
		{
			get
			{
				return this._T_Territories;
			}
			set
			{
				this._T_Territories.Assign(value);
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
		private void attach_T_Territories(T_Territory entity)
		{
			this.SendPropertyChanging();
			entity.T_Area = this;
		}
		private void detach_T_Territories(T_Territory entity)
		{
			this.SendPropertyChanging();
			entity.T_Area = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_DoNotCall")]
	public partial class T_DoNotCall : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _ID;
		private int _TerritoryID;
		private string _Name;
		private string _Address;
		private string _Phone;
		private bool _DNCPhone;
		private bool _DNCDoorToDoor;
		private string _Notes;
		private EntityRef<T_Territory> _T_Territory;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnTerritoryIDChanging(int value);
    partial void OnTerritoryIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnAddressChanging(string value);
    partial void OnAddressChanged();
    partial void OnPhoneChanging(string value);
    partial void OnPhoneChanged();
    partial void OnDNCPhoneChanging(bool value);
    partial void OnDNCPhoneChanged();
    partial void OnDNCDoorToDoorChanging(bool value);
    partial void OnDNCDoorToDoorChanged();
    partial void OnNotesChanging(string value);
    partial void OnNotesChanged();
		public T_DoNotCall()
		{
			this._T_Territory = default(EntityRef<T_Territory>);
			OnCreated();
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TerritoryID", DbType="Int NOT NULL")]
		public int TerritoryID
		{
			get
			{
				return this._TerritoryID;
			}
			set
			{
				if ((this._TerritoryID != value))
				{
					if (this._T_Territory.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnTerritoryIDChanging(value);
					this.SendPropertyChanging();
					this._TerritoryID = value;
					this.SendPropertyChanged("TerritoryID");
					this.OnTerritoryIDChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(255)")]
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Phone", DbType="NVarChar(255)")]
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DNCPhone", DbType="Bit NOT NULL")]
		public bool DNCPhone
		{
			get
			{
				return this._DNCPhone;
			}
			set
			{
				if ((this._DNCPhone != value))
				{
					this.OnDNCPhoneChanging(value);
					this.SendPropertyChanging();
					this._DNCPhone = value;
					this.SendPropertyChanged("DNCPhone");
					this.OnDNCPhoneChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DNCDoorToDoor", DbType="Bit NOT NULL")]
		public bool DNCDoorToDoor
		{
			get
			{
				return this._DNCDoorToDoor;
			}
			set
			{
				if ((this._DNCDoorToDoor != value))
				{
					this.OnDNCDoorToDoorChanging(value);
					this.SendPropertyChanging();
					this._DNCDoorToDoor = value;
					this.SendPropertyChanged("DNCDoorToDoor");
					this.OnDNCDoorToDoorChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Notes", DbType="NVarChar(255)")]
		public string Notes
		{
			get
			{
				return this._Notes;
			}
			set
			{
				if ((this._Notes != value))
				{
					this.OnNotesChanging(value);
					this.SendPropertyChanging();
					this._Notes = value;
					this.SendPropertyChanged("Notes");
					this.OnNotesChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_Territory_T_DoNotCall", Storage="_T_Territory", ThisKey="TerritoryID", OtherKey="ID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public T_Territory T_Territory
		{
			get
			{
				return this._T_Territory.Entity;
			}
			set
			{
				T_Territory previousValue = this._T_Territory.Entity;
				if (((previousValue != value) 
							|| (this._T_Territory.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._T_Territory.Entity = null;
						previousValue.T_DoNotCalls.Remove(this);
					}
					this._T_Territory.Entity = value;
					if ((value != null))
					{
						value.T_DoNotCalls.Add(this);
						this._TerritoryID = value.ID;
					}
					else
					{
						this._TerritoryID = default(int);
					}
					this.SendPropertyChanged("T_Territory");
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
}
#pragma warning restore 1591
