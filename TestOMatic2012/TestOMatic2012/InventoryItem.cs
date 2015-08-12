using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestOMatic2012 {

	[Serializable]
    public class VendorIdentifier
    {
        public string CKEItemNumber = "";
        public string Vendor = "";
        public string VendorItemNumber = "";
        public string Warehouse = "";

        public bool Validate(ref string errorMessage)
        {
            bool isValid = true;

            if (VendorItemNumber.Length == 0)
            {
                errorMessage = "Missing Vendor Item Number Number\\r\\n";
                isValid = false;
            }

            if (Vendor.Length == 0)
            {
                errorMessage += "Missing Vendor\\r\\n";
                isValid = false;
            }

            if (Warehouse.Length == 0)
            {
                errorMessage += "Missing Vendor Warehouse\\r\\n";
                isValid = false;
            }

            return isValid;
        }
    }

	
	class InventoryItem {
    

        private string _acctNo;
        private string _alpha;
        private string _code;
        private int _ID;
        private string _desc;
        private DateTime _effDate;
        private DateTime _discDate;
        private string _unitOfMeasure;
        private string _altUnitOfMeasure;
        private string _class;
        private int _qtyPerUnitOfMeasure;
        private decimal _price;
        private decimal _convFactor;
        private decimal _idealConvFactor;
        private int _status;
        private string _storageCode;
        //private bool _isValid;
        private string _demandType;
        private DateTime _createDate;
        //private bool _isThirdParty;
        //private ThirdPartyDef _thirdParty;
        //private string _branchNo;
        private bool? _isReciped;

        private List<VendorIdentifier> _vendorNumbers;




        #region Properties

        //public bool IsValid
        //{
        //     get {

        //         _isValid = true;
        //         return _isValid;
        //     }            
        //}

        public string AccountNumber
        {
            get { return _acctNo; }
            set { _acctNo = value; }
        }

        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string AlphaCode
        {
            get { return _alpha; }
            set { _alpha = value; }
        }

        public List<VendorIdentifier> VendorNumbers
        {
            get { return _vendorNumbers; }
            set { _vendorNumbers = value; }

        }

        public string Description
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }


        public string AlternateUnitOfMeasure
        {
            get { return _altUnitOfMeasure; }
            set { _altUnitOfMeasure = value; }
        }

        public string UnitOfMeasure
        {
            get { return _unitOfMeasure; }
            set { _unitOfMeasure = value; }
        }

        public DateTime EffDate
        {
            get { return _effDate; }
            set { _effDate = value; }
        }

        public DateTime EndDate
        {
            get { return _discDate; }
            set { _discDate = value; }
        }

        public int QtyPerUnitOfMeasure
        {
            get { return _qtyPerUnitOfMeasure; }
            set { _qtyPerUnitOfMeasure = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public decimal ConversionFactor
        {
            get { return _convFactor; }
            set { _convFactor = value; }
        }

        public decimal IdealConversionFactor
        {
            get { return _idealConvFactor; }
            set { _idealConvFactor = value; }
        }

        public string StorageCode
        {
            get { return _storageCode; }
            set { _storageCode = value; }
        }

        public string DemandType
        {
            get { return _demandType; }
            set { _demandType = value; }
        }

        public bool? IsReciped
        {
            get { return _isReciped; }
            set { _isReciped = value; }
        }
        
        
        #endregion properties

        public bool Equals(InventoryItem item)
        {
            //TODO: values could be null 

            bool isEqual = true;

            if (this.AlphaCode != item.AlphaCode)
            {
                isEqual = false;
            }

            if (this.AlternateUnitOfMeasure != item.AlternateUnitOfMeasure)
            {
                isEqual = false;
            }

            if (this.Class != item.Class)
            {
                if (item.Class.Length > 3)
                {
                    if (this.Class != item.Class.Substring(0, 3))
                    {
                        isEqual = false;
                    }
                }
                else
                {
                    isEqual = false;
                }
            }

            if (this.ConversionFactor != item.ConversionFactor)
            {
                isEqual = false;
            }

            if (this.DemandType != item.DemandType)
            {
                isEqual = false;
            }

            if (this.Description != item.Description)
            {
                isEqual = false;
            }

            if (this.EffDate != item.EffDate)
            {
                isEqual = false;
            }

            if (this.IdealConversionFactor != item.IdealConversionFactor)
            {
                isEqual = false;
            }

            if (this.IsReciped != item.IsReciped) 
            {
                isEqual = false;
            }

			//FoodCostData data = new FoodCostData();

			//foreach (VendorIdentifier vendorNum in this.VendorNumbers)
			//{
			//	VendorIdentifier itemVendorNumber = data.GetVendorNumber(this.Code, vendorNum.VendorItemNumber, vendorNum.Vendor, vendorNum.Warehouse);

			//	if (itemVendorNumber == null)
			//	{
			//		isEqual = false;
			//		break;
			//	}
			//}


            if (this.StorageCode != item.StorageCode)
            {
                isEqual = false;
            }

            if (this.UnitOfMeasure != item.UnitOfMeasure)
            {
                isEqual = false;
            }

            if (this.Status != item.Status)
            {
                isEqual = false;
            }

            return isEqual;
        }

        public bool Validate(ref string errorMessage)
        {

            bool isValid = true;
            errorMessage = "";


            if (_code.Length == 0)
            {
                isValid = false;
                errorMessage += "Missing a Value For Item Number\\r\\n";
            }

            if (_desc.Length == 0)
            {
                isValid = false;
                errorMessage += "Missing a Description\\r\\n";
            }

            if (_storageCode.Length == 0)
            {
                isValid = false;
                errorMessage += "Missing a Storage Code\\r\\n";
            }

            if (_isReciped == null)
            {

                isValid = false;
                errorMessage += "You Must Select Reciped or Non-Reciped.\\r\\n";
            }

            return isValid;
        }
	}
}
