﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="INFO2000")]
	public partial class INFO2000DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertdeposit_dtl_fact(deposit_dtl_fact instance);
    partial void Updatedeposit_dtl_fact(deposit_dtl_fact instance);
    partial void Deletedeposit_dtl_fact(deposit_dtl_fact instance);
    partial void Insertdeposit_dim(deposit_dim instance);
    partial void Updatedeposit_dim(deposit_dim instance);
    partial void Deletedeposit_dim(deposit_dim instance);
    #endregion
		
		public INFO2000DataContext() : 
				base(global::TestOMatic2012.Properties.Settings.Default.INFO2000ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public INFO2000DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public INFO2000DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public INFO2000DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public INFO2000DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<deposit_dtl_fact> deposit_dtl_facts
		{
			get
			{
				return this.GetTable<deposit_dtl_fact>();
			}
		}
		
		public System.Data.Linq.Table<deposit_dim> deposit_dims
		{
			get
			{
				return this.GetTable<deposit_dim>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.deposit_dtl_fact")]
	public partial class deposit_dtl_fact : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _deposit_dtl_id;
		
		private int _deposit_id;
		
		private int _POS_fact_id;
		
		private int _System_id;
		
		private int _Restaurant_no;
		
		private System.DateTime _Cal_Date;
		
		private System.Nullable<decimal> _Deposit_amt;
		
		private System.Nullable<System.DateTime> _create_date;
		
		private string _create_by;
		
		private System.Nullable<System.DateTime> _last_chg_date;
		
		private string _last_chg_by;
		
		private string _source;
		
		private EntityRef<deposit_dim> _deposit_dim;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Ondeposit_dtl_idChanging(int value);
    partial void Ondeposit_dtl_idChanged();
    partial void Ondeposit_idChanging(int value);
    partial void Ondeposit_idChanged();
    partial void OnPOS_fact_idChanging(int value);
    partial void OnPOS_fact_idChanged();
    partial void OnSystem_idChanging(int value);
    partial void OnSystem_idChanged();
    partial void OnRestaurant_noChanging(int value);
    partial void OnRestaurant_noChanged();
    partial void OnCal_DateChanging(System.DateTime value);
    partial void OnCal_DateChanged();
    partial void OnDeposit_amtChanging(System.Nullable<decimal> value);
    partial void OnDeposit_amtChanged();
    partial void Oncreate_dateChanging(System.Nullable<System.DateTime> value);
    partial void Oncreate_dateChanged();
    partial void Oncreate_byChanging(string value);
    partial void Oncreate_byChanged();
    partial void Onlast_chg_dateChanging(System.Nullable<System.DateTime> value);
    partial void Onlast_chg_dateChanged();
    partial void Onlast_chg_byChanging(string value);
    partial void Onlast_chg_byChanged();
    partial void OnsourceChanging(string value);
    partial void OnsourceChanged();
    #endregion
		
		public deposit_dtl_fact()
		{
			this._deposit_dim = default(EntityRef<deposit_dim>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_deposit_dtl_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int deposit_dtl_id
		{
			get
			{
				return this._deposit_dtl_id;
			}
			set
			{
				if ((this._deposit_dtl_id != value))
				{
					this.Ondeposit_dtl_idChanging(value);
					this.SendPropertyChanging();
					this._deposit_dtl_id = value;
					this.SendPropertyChanged("deposit_dtl_id");
					this.Ondeposit_dtl_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_deposit_id", DbType="Int NOT NULL")]
		public int deposit_id
		{
			get
			{
				return this._deposit_id;
			}
			set
			{
				if ((this._deposit_id != value))
				{
					if (this._deposit_dim.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.Ondeposit_idChanging(value);
					this.SendPropertyChanging();
					this._deposit_id = value;
					this.SendPropertyChanged("deposit_id");
					this.Ondeposit_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_POS_fact_id", DbType="Int NOT NULL")]
		public int POS_fact_id
		{
			get
			{
				return this._POS_fact_id;
			}
			set
			{
				if ((this._POS_fact_id != value))
				{
					this.OnPOS_fact_idChanging(value);
					this.SendPropertyChanging();
					this._POS_fact_id = value;
					this.SendPropertyChanged("POS_fact_id");
					this.OnPOS_fact_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_System_id", DbType="Int NOT NULL")]
		public int System_id
		{
			get
			{
				return this._System_id;
			}
			set
			{
				if ((this._System_id != value))
				{
					this.OnSystem_idChanging(value);
					this.SendPropertyChanging();
					this._System_id = value;
					this.SendPropertyChanged("System_id");
					this.OnSystem_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Restaurant_no", DbType="Int NOT NULL")]
		public int Restaurant_no
		{
			get
			{
				return this._Restaurant_no;
			}
			set
			{
				if ((this._Restaurant_no != value))
				{
					this.OnRestaurant_noChanging(value);
					this.SendPropertyChanging();
					this._Restaurant_no = value;
					this.SendPropertyChanged("Restaurant_no");
					this.OnRestaurant_noChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Cal_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Cal_Date
		{
			get
			{
				return this._Cal_Date;
			}
			set
			{
				if ((this._Cal_Date != value))
				{
					this.OnCal_DateChanging(value);
					this.SendPropertyChanging();
					this._Cal_Date = value;
					this.SendPropertyChanged("Cal_Date");
					this.OnCal_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Deposit_amt", DbType="Decimal(9,2)")]
		public System.Nullable<decimal> Deposit_amt
		{
			get
			{
				return this._Deposit_amt;
			}
			set
			{
				if ((this._Deposit_amt != value))
				{
					this.OnDeposit_amtChanging(value);
					this.SendPropertyChanging();
					this._Deposit_amt = value;
					this.SendPropertyChanged("Deposit_amt");
					this.OnDeposit_amtChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_create_date", DbType="DateTime")]
		public System.Nullable<System.DateTime> create_date
		{
			get
			{
				return this._create_date;
			}
			set
			{
				if ((this._create_date != value))
				{
					this.Oncreate_dateChanging(value);
					this.SendPropertyChanging();
					this._create_date = value;
					this.SendPropertyChanged("create_date");
					this.Oncreate_dateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_create_by", DbType="VarChar(20)")]
		public string create_by
		{
			get
			{
				return this._create_by;
			}
			set
			{
				if ((this._create_by != value))
				{
					this.Oncreate_byChanging(value);
					this.SendPropertyChanging();
					this._create_by = value;
					this.SendPropertyChanged("create_by");
					this.Oncreate_byChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_last_chg_date", DbType="DateTime")]
		public System.Nullable<System.DateTime> last_chg_date
		{
			get
			{
				return this._last_chg_date;
			}
			set
			{
				if ((this._last_chg_date != value))
				{
					this.Onlast_chg_dateChanging(value);
					this.SendPropertyChanging();
					this._last_chg_date = value;
					this.SendPropertyChanged("last_chg_date");
					this.Onlast_chg_dateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_last_chg_by", DbType="VarChar(20)")]
		public string last_chg_by
		{
			get
			{
				return this._last_chg_by;
			}
			set
			{
				if ((this._last_chg_by != value))
				{
					this.Onlast_chg_byChanging(value);
					this.SendPropertyChanging();
					this._last_chg_by = value;
					this.SendPropertyChanged("last_chg_by");
					this.Onlast_chg_byChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_source", DbType="VarChar(2)")]
		public string source
		{
			get
			{
				return this._source;
			}
			set
			{
				if ((this._source != value))
				{
					this.OnsourceChanging(value);
					this.SendPropertyChanging();
					this._source = value;
					this.SendPropertyChanged("source");
					this.OnsourceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="deposit_dim_deposit_dtl_fact", Storage="_deposit_dim", ThisKey="deposit_id", OtherKey="deposit_id", IsForeignKey=true)]
		public deposit_dim deposit_dim
		{
			get
			{
				return this._deposit_dim.Entity;
			}
			set
			{
				deposit_dim previousValue = this._deposit_dim.Entity;
				if (((previousValue != value) 
							|| (this._deposit_dim.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._deposit_dim.Entity = null;
						previousValue.deposit_dtl_facts.Remove(this);
					}
					this._deposit_dim.Entity = value;
					if ((value != null))
					{
						value.deposit_dtl_facts.Add(this);
						this._deposit_id = value.deposit_id;
					}
					else
					{
						this._deposit_id = default(int);
					}
					this.SendPropertyChanged("deposit_dim");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.deposit_dim")]
	public partial class deposit_dim : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _deposit_id;
		
		private System.Nullable<int> _System_id;
		
		private System.Nullable<int> _Restaurant_no;
		
		private string _deposit_type;
		
		private string _deposit_descr;
		
		private System.Nullable<System.DateTime> _eff_date;
		
		private System.Nullable<System.DateTime> _term_date;
		
		private string _deposit_group;
		
		private string _deposit_category;
		
		private EntitySet<deposit_dtl_fact> _deposit_dtl_facts;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Ondeposit_idChanging(int value);
    partial void Ondeposit_idChanged();
    partial void OnSystem_idChanging(System.Nullable<int> value);
    partial void OnSystem_idChanged();
    partial void OnRestaurant_noChanging(System.Nullable<int> value);
    partial void OnRestaurant_noChanged();
    partial void Ondeposit_typeChanging(string value);
    partial void Ondeposit_typeChanged();
    partial void Ondeposit_descrChanging(string value);
    partial void Ondeposit_descrChanged();
    partial void Oneff_dateChanging(System.Nullable<System.DateTime> value);
    partial void Oneff_dateChanged();
    partial void Onterm_dateChanging(System.Nullable<System.DateTime> value);
    partial void Onterm_dateChanged();
    partial void Ondeposit_groupChanging(string value);
    partial void Ondeposit_groupChanged();
    partial void Ondeposit_categoryChanging(string value);
    partial void Ondeposit_categoryChanged();
    #endregion
		
		public deposit_dim()
		{
			this._deposit_dtl_facts = new EntitySet<deposit_dtl_fact>(new Action<deposit_dtl_fact>(this.attach_deposit_dtl_facts), new Action<deposit_dtl_fact>(this.detach_deposit_dtl_facts));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_deposit_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int deposit_id
		{
			get
			{
				return this._deposit_id;
			}
			set
			{
				if ((this._deposit_id != value))
				{
					this.Ondeposit_idChanging(value);
					this.SendPropertyChanging();
					this._deposit_id = value;
					this.SendPropertyChanged("deposit_id");
					this.Ondeposit_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_System_id", DbType="Int")]
		public System.Nullable<int> System_id
		{
			get
			{
				return this._System_id;
			}
			set
			{
				if ((this._System_id != value))
				{
					this.OnSystem_idChanging(value);
					this.SendPropertyChanging();
					this._System_id = value;
					this.SendPropertyChanged("System_id");
					this.OnSystem_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Restaurant_no", DbType="Int")]
		public System.Nullable<int> Restaurant_no
		{
			get
			{
				return this._Restaurant_no;
			}
			set
			{
				if ((this._Restaurant_no != value))
				{
					this.OnRestaurant_noChanging(value);
					this.SendPropertyChanging();
					this._Restaurant_no = value;
					this.SendPropertyChanged("Restaurant_no");
					this.OnRestaurant_noChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_deposit_type", DbType="VarChar(20)")]
		public string deposit_type
		{
			get
			{
				return this._deposit_type;
			}
			set
			{
				if ((this._deposit_type != value))
				{
					this.Ondeposit_typeChanging(value);
					this.SendPropertyChanging();
					this._deposit_type = value;
					this.SendPropertyChanged("deposit_type");
					this.Ondeposit_typeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_deposit_descr", DbType="VarChar(80)")]
		public string deposit_descr
		{
			get
			{
				return this._deposit_descr;
			}
			set
			{
				if ((this._deposit_descr != value))
				{
					this.Ondeposit_descrChanging(value);
					this.SendPropertyChanging();
					this._deposit_descr = value;
					this.SendPropertyChanged("deposit_descr");
					this.Ondeposit_descrChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_eff_date", DbType="DateTime")]
		public System.Nullable<System.DateTime> eff_date
		{
			get
			{
				return this._eff_date;
			}
			set
			{
				if ((this._eff_date != value))
				{
					this.Oneff_dateChanging(value);
					this.SendPropertyChanging();
					this._eff_date = value;
					this.SendPropertyChanged("eff_date");
					this.Oneff_dateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_term_date", DbType="DateTime")]
		public System.Nullable<System.DateTime> term_date
		{
			get
			{
				return this._term_date;
			}
			set
			{
				if ((this._term_date != value))
				{
					this.Onterm_dateChanging(value);
					this.SendPropertyChanging();
					this._term_date = value;
					this.SendPropertyChanged("term_date");
					this.Onterm_dateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_deposit_group", DbType="VarChar(10)")]
		public string deposit_group
		{
			get
			{
				return this._deposit_group;
			}
			set
			{
				if ((this._deposit_group != value))
				{
					this.Ondeposit_groupChanging(value);
					this.SendPropertyChanging();
					this._deposit_group = value;
					this.SendPropertyChanged("deposit_group");
					this.Ondeposit_groupChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_deposit_category", DbType="VarChar(10)")]
		public string deposit_category
		{
			get
			{
				return this._deposit_category;
			}
			set
			{
				if ((this._deposit_category != value))
				{
					this.Ondeposit_categoryChanging(value);
					this.SendPropertyChanging();
					this._deposit_category = value;
					this.SendPropertyChanged("deposit_category");
					this.Ondeposit_categoryChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="deposit_dim_deposit_dtl_fact", Storage="_deposit_dtl_facts", ThisKey="deposit_id", OtherKey="deposit_id")]
		public EntitySet<deposit_dtl_fact> deposit_dtl_facts
		{
			get
			{
				return this._deposit_dtl_facts;
			}
			set
			{
				this._deposit_dtl_facts.Assign(value);
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
		
		private void attach_deposit_dtl_facts(deposit_dtl_fact entity)
		{
			this.SendPropertyChanging();
			entity.deposit_dim = this;
		}
		
		private void detach_deposit_dtl_facts(deposit_dtl_fact entity)
		{
			this.SendPropertyChanging();
			entity.deposit_dim = null;
		}
	}
}
#pragma warning restore 1591
