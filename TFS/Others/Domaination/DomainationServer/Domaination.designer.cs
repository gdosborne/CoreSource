#pragma warning disable 1591
namespace DomainationServer
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
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Domination")]
	public partial class DomainationDataContext : System.Data.Linq.DataContext
	{
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
    partial void OnCreated();
    partial void InsertDomDomain(DomDomain instance);
    partial void UpdateDomDomain(DomDomain instance);
    partial void DeleteDomDomain(DomDomain instance);
    partial void InsertDomFolder(DomFolder instance);
    partial void UpdateDomFolder(DomFolder instance);
    partial void DeleteDomFolder(DomFolder instance);
    partial void InsertDomUser(DomUser instance);
    partial void UpdateDomUser(DomUser instance);
    partial void DeleteDomUser(DomUser instance);
    partial void InsertDomInstance(DomInstance instance);
    partial void UpdateDomInstance(DomInstance instance);
    partial void DeleteDomInstance(DomInstance instance);
    partial void InsertDomNotification(DomNotification instance);
    partial void UpdateDomNotification(DomNotification instance);
    partial void DeleteDomNotification(DomNotification instance);
		public DomainationDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["DominationConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		public DomainationDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		public DomainationDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		public DomainationDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		public DomainationDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		public System.Data.Linq.Table<DomDomain> DomDomains
		{
			get
			{
				return this.GetTable<DomDomain>();
			}
		}
		public System.Data.Linq.Table<DomFolder> DomFolders
		{
			get
			{
				return this.GetTable<DomFolder>();
			}
		}
		public System.Data.Linq.Table<DomUser> DomUsers
		{
			get
			{
				return this.GetTable<DomUser>();
			}
		}
		public System.Data.Linq.Table<DomInstance> DomInstances
		{
			get
			{
				return this.GetTable<DomInstance>();
			}
		}
		public System.Data.Linq.Table<DomNotification> DomNotifications
		{
			get
			{
				return this.GetTable<DomNotification>();
			}
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.DomDomain")]
	public partial class DomDomain : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private int _DomInstanceId;
		private string _Name;
		private EntitySet<DomFolder> _DomFolders;
		private EntityRef<DomInstance> _DomInstance;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnDomInstanceIdChanging(int value);
    partial void OnDomInstanceIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
		public DomDomain()
		{
			this._DomFolders = new EntitySet<DomFolder>(new Action<DomFolder>(this.attach_DomFolders), new Action<DomFolder>(this.detach_DomFolders));
			this._DomInstance = default(EntityRef<DomInstance>);
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DomInstanceId", DbType="Int NOT NULL")]
		public int DomInstanceId
		{
			get
			{
				return this._DomInstanceId;
			}
			set
			{
				if ((this._DomInstanceId != value))
				{
					if (this._DomInstance.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDomInstanceIdChanging(value);
					this.SendPropertyChanging();
					this._DomInstanceId = value;
					this.SendPropertyChanged("DomInstanceId");
					this.OnDomInstanceIdChanged();
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
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DomDomain_DomFolder", Storage="_DomFolders", ThisKey="Id", OtherKey="DomDomainId")]
		public EntitySet<DomFolder> DomFolders
		{
			get
			{
				return this._DomFolders;
			}
			set
			{
				this._DomFolders.Assign(value);
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DomInstance_DomDomain", Storage="_DomInstance", ThisKey="DomInstanceId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public DomInstance DomInstance
		{
			get
			{
				return this._DomInstance.Entity;
			}
			set
			{
				DomInstance previousValue = this._DomInstance.Entity;
				if (((previousValue != value) 
							|| (this._DomInstance.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._DomInstance.Entity = null;
						previousValue.DomDomains.Remove(this);
					}
					this._DomInstance.Entity = value;
					if ((value != null))
					{
						value.DomDomains.Add(this);
						this._DomInstanceId = value.Id;
					}
					else
					{
						this._DomInstanceId = default(int);
					}
					this.SendPropertyChanged("DomInstance");
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
		private void attach_DomFolders(DomFolder entity)
		{
			this.SendPropertyChanging();
			entity.DomDomain = this;
		}
		private void detach_DomFolders(DomFolder entity)
		{
			this.SendPropertyChanging();
			entity.DomDomain = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.DomFolder")]
	public partial class DomFolder : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private int _DomDomainId;
		private string _Path;
		private EntityRef<DomDomain> _DomDomain;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnDomDomainIdChanging(int value);
    partial void OnDomDomainIdChanged();
    partial void OnPathChanging(string value);
    partial void OnPathChanged();
		public DomFolder()
		{
			this._DomDomain = default(EntityRef<DomDomain>);
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DomDomainId", DbType="Int NOT NULL")]
		public int DomDomainId
		{
			get
			{
				return this._DomDomainId;
			}
			set
			{
				if ((this._DomDomainId != value))
				{
					if (this._DomDomain.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDomDomainIdChanging(value);
					this.SendPropertyChanging();
					this._DomDomainId = value;
					this.SendPropertyChanged("DomDomainId");
					this.OnDomDomainIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Path", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				if ((this._Path != value))
				{
					this.OnPathChanging(value);
					this.SendPropertyChanging();
					this._Path = value;
					this.SendPropertyChanged("Path");
					this.OnPathChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DomDomain_DomFolder", Storage="_DomDomain", ThisKey="DomDomainId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public DomDomain DomDomain
		{
			get
			{
				return this._DomDomain.Entity;
			}
			set
			{
				DomDomain previousValue = this._DomDomain.Entity;
				if (((previousValue != value) 
							|| (this._DomDomain.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._DomDomain.Entity = null;
						previousValue.DomFolders.Remove(this);
					}
					this._DomDomain.Entity = value;
					if ((value != null))
					{
						value.DomFolders.Add(this);
						this._DomDomainId = value.Id;
					}
					else
					{
						this._DomDomainId = default(int);
					}
					this.SendPropertyChanged("DomDomain");
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
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.DomUser")]
	public partial class DomUser : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private System.Guid _ApplicationId;
		private string _EmailAddress;
		private string _FirstName;
		private string _LastName;
		private string _EncryptedPassword;
		private EntitySet<DomInstance> _DomInstances;
		private EntitySet<DomNotification> _DomNotifications;
		private EntitySet<DomNotification> _DomNotifications1;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnApplicationIdChanging(System.Guid value);
    partial void OnApplicationIdChanged();
    partial void OnEmailAddressChanging(string value);
    partial void OnEmailAddressChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnEncryptedPasswordChanging(string value);
    partial void OnEncryptedPasswordChanged();
		public DomUser()
		{
			this._DomInstances = new EntitySet<DomInstance>(new Action<DomInstance>(this.attach_DomInstances), new Action<DomInstance>(this.detach_DomInstances));
			this._DomNotifications = new EntitySet<DomNotification>(new Action<DomNotification>(this.attach_DomNotifications), new Action<DomNotification>(this.detach_DomNotifications));
			this._DomNotifications1 = new EntitySet<DomNotification>(new Action<DomNotification>(this.attach_DomNotifications1), new Action<DomNotification>(this.detach_DomNotifications1));
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ApplicationId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				if ((this._ApplicationId != value))
				{
					this.OnApplicationIdChanging(value);
					this.SendPropertyChanging();
					this._ApplicationId = value;
					this.SendPropertyChanged("ApplicationId");
					this.OnApplicationIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmailAddress", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string EmailAddress
		{
			get
			{
				return this._EmailAddress;
			}
			set
			{
				if ((this._EmailAddress != value))
				{
					this.OnEmailAddressChanging(value);
					this.SendPropertyChanging();
					this._EmailAddress = value;
					this.SendPropertyChanged("EmailAddress");
					this.OnEmailAddressChanged();
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EncryptedPassword", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string EncryptedPassword
		{
			get
			{
				return this._EncryptedPassword;
			}
			set
			{
				if ((this._EncryptedPassword != value))
				{
					this.OnEncryptedPasswordChanging(value);
					this.SendPropertyChanging();
					this._EncryptedPassword = value;
					this.SendPropertyChanged("EncryptedPassword");
					this.OnEncryptedPasswordChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DomUser_DomInstance", Storage="_DomInstances", ThisKey="Id", OtherKey="DomUserId")]
		public EntitySet<DomInstance> DomInstances
		{
			get
			{
				return this._DomInstances;
			}
			set
			{
				this._DomInstances.Assign(value);
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DomUser_DomNotification", Storage="_DomNotifications", ThisKey="Id", OtherKey="FromUserId")]
		public EntitySet<DomNotification> DomNotifications
		{
			get
			{
				return this._DomNotifications;
			}
			set
			{
				this._DomNotifications.Assign(value);
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DomUser_DomNotification1", Storage="_DomNotifications1", ThisKey="Id", OtherKey="ToUserId")]
		public EntitySet<DomNotification> DomNotifications1
		{
			get
			{
				return this._DomNotifications1;
			}
			set
			{
				this._DomNotifications1.Assign(value);
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
		private void attach_DomInstances(DomInstance entity)
		{
			this.SendPropertyChanging();
			entity.DomUser = this;
		}
		private void detach_DomInstances(DomInstance entity)
		{
			this.SendPropertyChanging();
			entity.DomUser = null;
		}
		private void attach_DomNotifications(DomNotification entity)
		{
			this.SendPropertyChanging();
			entity.DomUser = this;
		}
		private void detach_DomNotifications(DomNotification entity)
		{
			this.SendPropertyChanging();
			entity.DomUser = null;
		}
		private void attach_DomNotifications1(DomNotification entity)
		{
			this.SendPropertyChanging();
			entity.DomUser1 = this;
		}
		private void detach_DomNotifications1(DomNotification entity)
		{
			this.SendPropertyChanging();
			entity.DomUser1 = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.DomInstance")]
	public partial class DomInstance : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private int _DomUserId;
		private System.DateTime _WatchDog;
		private EntitySet<DomDomain> _DomDomains;
		private EntityRef<DomUser> _DomUser;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnDomUserIdChanging(int value);
    partial void OnDomUserIdChanged();
    partial void OnWatchDogChanging(System.DateTime value);
    partial void OnWatchDogChanged();
		public DomInstance()
		{
			this._DomDomains = new EntitySet<DomDomain>(new Action<DomDomain>(this.attach_DomDomains), new Action<DomDomain>(this.detach_DomDomains));
			this._DomUser = default(EntityRef<DomUser>);
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DomUserId", DbType="Int NOT NULL")]
		public int DomUserId
		{
			get
			{
				return this._DomUserId;
			}
			set
			{
				if ((this._DomUserId != value))
				{
					if (this._DomUser.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDomUserIdChanging(value);
					this.SendPropertyChanging();
					this._DomUserId = value;
					this.SendPropertyChanged("DomUserId");
					this.OnDomUserIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WatchDog", DbType="DateTime NOT NULL")]
		public System.DateTime WatchDog
		{
			get
			{
				return this._WatchDog;
			}
			set
			{
				if ((this._WatchDog != value))
				{
					this.OnWatchDogChanging(value);
					this.SendPropertyChanging();
					this._WatchDog = value;
					this.SendPropertyChanged("WatchDog");
					this.OnWatchDogChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DomInstance_DomDomain", Storage="_DomDomains", ThisKey="Id", OtherKey="DomInstanceId")]
		public EntitySet<DomDomain> DomDomains
		{
			get
			{
				return this._DomDomains;
			}
			set
			{
				this._DomDomains.Assign(value);
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DomUser_DomInstance", Storage="_DomUser", ThisKey="DomUserId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public DomUser DomUser
		{
			get
			{
				return this._DomUser.Entity;
			}
			set
			{
				DomUser previousValue = this._DomUser.Entity;
				if (((previousValue != value) 
							|| (this._DomUser.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._DomUser.Entity = null;
						previousValue.DomInstances.Remove(this);
					}
					this._DomUser.Entity = value;
					if ((value != null))
					{
						value.DomInstances.Add(this);
						this._DomUserId = value.Id;
					}
					else
					{
						this._DomUserId = default(int);
					}
					this.SendPropertyChanged("DomUser");
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
		private void attach_DomDomains(DomDomain entity)
		{
			this.SendPropertyChanging();
			entity.DomInstance = this;
		}
		private void detach_DomDomains(DomDomain entity)
		{
			this.SendPropertyChanging();
			entity.DomInstance = null;
		}
	}
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.DomNotification")]
	public partial class DomNotification : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		private int _Id;
		private int _FromUserId;
		private int _ToUserId;
		private int _NotificationType;
		private System.Nullable<System.Guid> _CommunicationId;
		private EntityRef<DomUser> _DomUser;
		private EntityRef<DomUser> _DomUser1;
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnFromUserIdChanging(int value);
    partial void OnFromUserIdChanged();
    partial void OnToUserIdChanging(int value);
    partial void OnToUserIdChanged();
    partial void OnNotificationTypeChanging(int value);
    partial void OnNotificationTypeChanged();
    partial void OnCommunicationIdChanging(System.Nullable<System.Guid> value);
    partial void OnCommunicationIdChanged();
		public DomNotification()
		{
			this._DomUser = default(EntityRef<DomUser>);
			this._DomUser1 = default(EntityRef<DomUser>);
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
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FromUserId", DbType="Int NOT NULL")]
		public int FromUserId
		{
			get
			{
				return this._FromUserId;
			}
			set
			{
				if ((this._FromUserId != value))
				{
					if (this._DomUser.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnFromUserIdChanging(value);
					this.SendPropertyChanging();
					this._FromUserId = value;
					this.SendPropertyChanged("FromUserId");
					this.OnFromUserIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ToUserId", DbType="Int NOT NULL")]
		public int ToUserId
		{
			get
			{
				return this._ToUserId;
			}
			set
			{
				if ((this._ToUserId != value))
				{
					if (this._DomUser1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnToUserIdChanging(value);
					this.SendPropertyChanging();
					this._ToUserId = value;
					this.SendPropertyChanged("ToUserId");
					this.OnToUserIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NotificationType", DbType="Int NOT NULL")]
		public int NotificationType
		{
			get
			{
				return this._NotificationType;
			}
			set
			{
				if ((this._NotificationType != value))
				{
					this.OnNotificationTypeChanging(value);
					this.SendPropertyChanging();
					this._NotificationType = value;
					this.SendPropertyChanged("NotificationType");
					this.OnNotificationTypeChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CommunicationId", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> CommunicationId
		{
			get
			{
				return this._CommunicationId;
			}
			set
			{
				if ((this._CommunicationId != value))
				{
					this.OnCommunicationIdChanging(value);
					this.SendPropertyChanging();
					this._CommunicationId = value;
					this.SendPropertyChanged("CommunicationId");
					this.OnCommunicationIdChanged();
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DomUser_DomNotification", Storage="_DomUser", ThisKey="FromUserId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public DomUser DomUser
		{
			get
			{
				return this._DomUser.Entity;
			}
			set
			{
				DomUser previousValue = this._DomUser.Entity;
				if (((previousValue != value) 
							|| (this._DomUser.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._DomUser.Entity = null;
						previousValue.DomNotifications.Remove(this);
					}
					this._DomUser.Entity = value;
					if ((value != null))
					{
						value.DomNotifications.Add(this);
						this._FromUserId = value.Id;
					}
					else
					{
						this._FromUserId = default(int);
					}
					this.SendPropertyChanged("DomUser");
				}
			}
		}
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="DomUser_DomNotification1", Storage="_DomUser1", ThisKey="ToUserId", OtherKey="Id", IsForeignKey=true)]
		public DomUser DomUser1
		{
			get
			{
				return this._DomUser1.Entity;
			}
			set
			{
				DomUser previousValue = this._DomUser1.Entity;
				if (((previousValue != value) 
							|| (this._DomUser1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._DomUser1.Entity = null;
						previousValue.DomNotifications1.Remove(this);
					}
					this._DomUser1.Entity = value;
					if ((value != null))
					{
						value.DomNotifications1.Add(this);
						this._ToUserId = value.Id;
					}
					else
					{
						this._ToUserId = default(int);
					}
					this.SendPropertyChanged("DomUser1");
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
