//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestOMatic2012
{
    using System;
    using System.Collections.Generic;
    
    public partial class BrinkOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BrinkOrder()
        {
            this.BrinkOrderPayments = new HashSet<BrinkOrderPayment>();
        }
    
        public int BrinkOrderId { get; set; }
        public string UnitNumber { get; set; }
        public System.DateTime BusinessDate { get; set; }
        public short OrderNumber { get; set; }
        public string OrderName { get; set; }
        public Nullable<int> DestinationId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public int TerminalId { get; set; }
        public decimal NetSales { get; set; }
        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> PromoAmount { get; set; }
        public decimal DisplaySubTotal { get; set; }
        public decimal DisplayTax { get; set; }
        public decimal GrossSales { get; set; }
        public System.DateTime OpenTime { get; set; }
        public Nullable<System.DateTime> FirstSendTime { get; set; }
        public System.DateTime ModifiedTime { get; set; }
        public Nullable<System.DateTime> PickTime { get; set; }
        public Nullable<System.DateTime> ClosedTime { get; set; }
        public int Count { get; set; }
        public int GuestCount { get; set; }
        public decimal GuestCountFracional { get; set; }
        public Nullable<System.Guid> CustomerId { get; set; }
        public long IdField { get; set; }
        public string IdEncodedField { get; set; }
        public bool IsClosed { get; set; }
        public bool IsFutureOrder { get; set; }
        public bool IsRefund { get; set; }
        public bool IsTaxExempt { get; set; }
        public decimal Rounding { get; set; }
        public Nullable<int> SectionId { get; set; }
        public string TaxExemptId { get; set; }
        public Nullable<int> TillNumber { get; set; }
        public string OrderAdditionalDetails { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BrinkOrderPayment> BrinkOrderPayments { get; set; }
    }
}
