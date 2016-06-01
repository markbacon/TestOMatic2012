﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestOMatic2012
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="TransactionData")]
	public partial class TransactionDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertTransHistFile(TransHistFile instance);
    partial void UpdateTransHistFile(TransHistFile instance);
    partial void DeleteTransHistFile(TransHistFile instance);
    #endregion
		
		public TransactionDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TransactionDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TransactionDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TransactionDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TransHistFile> TransHistFiles
		{
			get
			{
				return this.GetTable<TransHistFile>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TransHistFile")]
	public partial class TransHistFile : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _TransHistFileId;
		
		private string _UnitNumber;
		
		private System.DateTime _BusinessDate;
		
		private string _XmlData;
		
		private System.DateTime _CreateDate;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnTransHistFileIdChanging(int value);
    partial void OnTransHistFileIdChanged();
    partial void OnUnitNumberChanging(string value);
    partial void OnUnitNumberChanged();
    partial void OnBusinessDateChanging(System.DateTime value);
    partial void OnBusinessDateChanged();
    partial void OnXmlDataChanging(string value);
    partial void OnXmlDataChanged();
    partial void OnCreateDateChanging(System.DateTime value);
    partial void OnCreateDateChanged();
    #endregion
		
		public TransHistFile()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TransHistFileId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int TransHistFileId
		{
			get
			{
				return this._TransHistFileId;
			}
			set
			{
				if ((this._TransHistFileId != value))
				{
					this.OnTransHistFileIdChanging(value);
					this.SendPropertyChanging();
					this._TransHistFileId = value;
					this.SendPropertyChanged("TransHistFileId");
					this.OnTransHistFileIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UnitNumber", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string UnitNumber
		{
			get
			{
				return this._UnitNumber;
			}
			set
			{
				if ((this._UnitNumber != value))
				{
					this.OnUnitNumberChanging(value);
					this.SendPropertyChanging();
					this._UnitNumber = value;
					this.SendPropertyChanged("UnitNumber");
					this.OnUnitNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BusinessDate", DbType="Date NOT NULL")]
		public System.DateTime BusinessDate
		{
			get
			{
				return this._BusinessDate;
			}
			set
			{
				if ((this._BusinessDate != value))
				{
					this.OnBusinessDateChanging(value);
					this.SendPropertyChanging();
					this._BusinessDate = value;
					this.SendPropertyChanged("BusinessDate");
					this.OnBusinessDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_XmlData", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string XmlData
		{
			get
			{
				return this._XmlData;
			}
			set
			{
				if ((this._XmlData != value))
				{
					this.OnXmlDataChanging(value);
					this.SendPropertyChanging();
					this._XmlData = value;
					this.SendPropertyChanged("XmlData");
					this.OnXmlDataChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				if ((this._CreateDate != value))
				{
					this.OnCreateDateChanging(value);
					this.SendPropertyChanging();
					this._CreateDate = value;
					this.SendPropertyChanged("CreateDate");
					this.OnCreateDateChanged();
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
