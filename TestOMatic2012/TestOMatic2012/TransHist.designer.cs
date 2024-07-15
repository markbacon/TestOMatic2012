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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="TransHist")]
	public partial class TransHistDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertTransHistArchive(TransHistArchive instance);
    partial void UpdateTransHistArchive(TransHistArchive instance);
    partial void DeleteTransHistArchive(TransHistArchive instance);
    #endregion
		
		public TransHistDataContext() : 
				base(global::TestOMatic2012.Properties.Settings.Default.TransHistConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public TransHistDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TransHistDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TransHistDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TransHistDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TransHistArchive> TransHistArchives
		{
			get
			{
				return this.GetTable<TransHistArchive>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TransHistArchive")]
	public partial class TransHistArchive : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _TransHistArchiveId;
		
		private string _Unit;
		
		private string _FileName;
		
		private System.DateTime _FileDate;
		
		private System.Xml.Linq.XElement _FileXml;
		
		private System.DateTime _CreateDate;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnTransHistArchiveIdChanging(int value);
    partial void OnTransHistArchiveIdChanged();
    partial void OnUnitChanging(string value);
    partial void OnUnitChanged();
    partial void OnFileNameChanging(string value);
    partial void OnFileNameChanged();
    partial void OnFileDateChanging(System.DateTime value);
    partial void OnFileDateChanged();
    partial void OnFileXmlChanging(System.Xml.Linq.XElement value);
    partial void OnFileXmlChanged();
    partial void OnCreateDateChanging(System.DateTime value);
    partial void OnCreateDateChanged();
    #endregion
		
		public TransHistArchive()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TransHistArchiveId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int TransHistArchiveId
		{
			get
			{
				return this._TransHistArchiveId;
			}
			set
			{
				if ((this._TransHistArchiveId != value))
				{
					this.OnTransHistArchiveIdChanging(value);
					this.SendPropertyChanging();
					this._TransHistArchiveId = value;
					this.SendPropertyChanged("TransHistArchiveId");
					this.OnTransHistArchiveIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Unit", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Unit
		{
			get
			{
				return this._Unit;
			}
			set
			{
				if ((this._Unit != value))
				{
					this.OnUnitChanging(value);
					this.SendPropertyChanging();
					this._Unit = value;
					this.SendPropertyChanged("Unit");
					this.OnUnitChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileName", DbType="NVarChar(80) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileDate", DbType="DateTime NOT NULL")]
		public System.DateTime FileDate
		{
			get
			{
				return this._FileDate;
			}
			set
			{
				if ((this._FileDate != value))
				{
					this.OnFileDateChanging(value);
					this.SendPropertyChanging();
					this._FileDate = value;
					this.SendPropertyChanged("FileDate");
					this.OnFileDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileXml", DbType="Xml NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public System.Xml.Linq.XElement FileXml
		{
			get
			{
				return this._FileXml;
			}
			set
			{
				if ((this._FileXml != value))
				{
					this.OnFileXmlChanging(value);
					this.SendPropertyChanging();
					this._FileXml = value;
					this.SendPropertyChanged("FileXml");
					this.OnFileXmlChanged();
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
