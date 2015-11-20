
//========================================= PCN ==============================================
//                          © 2007 CKE-PCN all rights reserved.
//=================================================================================================
//      Author      :   MandarC
//      Class       :   PCNConstants
//      Date        :   May 02, 2007
//      LLD         :   NA
//      HLD         :   NA
//      Review On   :                           Reviewed By :
//      Description :   This class intends to provide set of Constants Enums and Structures defined  
//                      in the Class and all will have a shared access to it.
//=================================================================================================
//                              Change History (For Add/Remove of methods)
//      Date        :                           Author   :
//      Comments    :
//         
//=================================================================================================
using System;
using System.Web;

namespace TestOMatic2012 {

    public static class PCNConstants
    {
        public static bool withTransaction = false;

        #region Logger Constants

        public const string LogFileDirectory = "./Log/";
        public const string LogEntryFormat = "Date     : {0} [{1}] {2} {3}";
        public const string StackTraceEntryFormat = "Stack Trace : \n{0}";
        public const string LogFileName = "PCNLog";
        public const string MMMddyyyy = "MMM dd, yyyy";
        public const string DialogTitle = "Personnel Change Notification";

        //=================================================================================================
        //      Type            :   Enum LogLevel
        //      Dependency Obj. :   Logger
        //      Date            :   May 02, 2007
        //      Review On       :                       Reviewed By     :
        //      Description     :   Stores error reporting log level.
        //=================================================================================================
        public enum LogLevel
        {
            Off = 0,
            Critical,
            Error,
            Warning,
            Info,
            All,
        }

        //=================================================================================================
        //      Type            :   Enum LogEntry
        //      Dependency Obj. :   Logger
        //      Date            :   May 02, 2007
        //      Review On       :                       Reviewed By     :
        //      Description     :   Stores log entry levels.
        //=================================================================================================
        public enum LogEntry
        {
            Critical = 1, //values of each of these should match the LogLevel
            Error,
            Warning,
            Info
        }
        #endregion

        #region Employee
        
        /// <summary>
        /// This Enum stands for status of the employee
        /// 
        /// </summary>
        public enum  EMPLOYEE_TYPE:int
        {
            ACTIVE = 1,  // Not a true type
            ACTIVE_NORMAL = 3,
            ACTIVE_BORROWED = 5,
            ACTIVE_TRAINER = 9,
            INACTIVE = 4096,     // Not a true type
            INACTIVE_LOA = 12288,
            INACTIVE_SEPARATED = 20480
        }
        internal struct PhoneTypes
        {
            public const int MESSAGE = 0;
            public const string MESSAGEDESC = "Message";
            public const int PAGER = 1;
            public const string PAGERDESC = "Pager";
            public const int MOBILE = 2;
            public const string MOBILEDESC = "Mobile";
        }        
        #endregion

        #region Action constants
        /// <summary>
        /// Action codes used by PeopleSoft system
        /// </summary>
        internal struct ActionCode
        {
              public const string kActionHire = "HIR";
              public const string kActionRehire = "REH";
              public const string kActionDemotion = "DEM";
              public const string kActionData = "DTA";
              public const string kActionLOA = "LOA";
              public const string kActionPay = "PAY";
              public const string kActionReturnFromLeave = "RFL";
              public const string kActionTermination = "TER";
              public const string kActionTransfer = "XFR";
            // PcnMgr   
              public const string kActionPromotion = "PRO";
            // end of PcnMgr   
            public const string kActionError = "ERR";     
        }


        internal struct ActionReason
        {
    /* 0*/   public const string kActionHire = "HIR";//"New Hire",
             public const string kActionRehire = "REH";// "Rehire",
             public const string kActionRehireTerminated = "TIE";// "Terminated in Error",
             public const string kActionDemotionOnEmpReq = "EER";// "Demotion, Employee Requested",
             public const string kActionDemotion = "USP";// "Demotion, Unsatisfactory Performance",
    /* 5*/   public const string kActionDataCDP = "CDP";// "Correction Department",
             public const string kActionDataJobCode = "CJC";// "Correction Job Code",
             public const string kActionDataPayRate = "CPR";// "Correction Pay Rate",
             public const string kActionLOAFML = "FML";// "Family/Medical Leave Act",
             public const string kActionLOAMED = "MED";// "Unpaid Medical Leave",
    /*10*/   public const string kActionLOAMIL = "MIL";// "Military Service",
             public const string kActionLOAPER = "PER";// "Unpaid Personal LOA",
             public const string kActionLOAWC = "WC";//  "Worker's Compensation LOA",
             public const string kActionPay = "ADJ";// "Pay Adjustment",
             public const string kActionPayMER = "MER";// "Merit",
    /*15*/   public const string kActionPayDEM = "DEM";// "Demotion",                     // Not used
             public const string kActionPayMWG = "MWG";// "Minimum Wage Increase",
             public const string kActionPayPRO = "PRO";// "Promotion",                    // Not used
             public const string kActionPayREC = "REC";// "Job Reclassification",
             public const string kActionReturnFromLeave = "RFL";// "Return From Leave",
    /*20*/   public const string kActionTermination = "ATT";// "Attendance",
             public const string kActionTerminationCHI = "CHI";// "Child/House Care",
             public const string kActionTerminationCON = "CON";// "Conduct",
             public const string kActionTerminationDEA = "DEA";// "Death",
             public const string kActionTerminationDIS = "DIS";// "Dishonesty",
    /*25*/   public const string kActionTerminationDSC = "DSC";// "Discharge",
             public const string kActionTerminationEES = "EES";// "Dissatisfaction with Fellow Employee",
             public const string kActionTerminationERT = "ERT";// "Early Retirement",
             public const string kActionTerminationELI = "ELI";// "Elimination of Position (Layoff)",
             public const string kActionTerminationFAM = "FAM";// "Family Reasons",
    /*30*/   public const string kActionTerminationHEA = "HEA";// "Health Reasons",
             public const string kActionTerminationHRS = "HRS";// "Dissatisfaction with Hours",
             public const string kActionTerminationILL = "ILL";// "Illness in Family",
             public const string kActionTerminationINS = "INS";// "Insubordination",
             public const string kActionTerminationJOB = "JOB";// "Job Abandonment",
    /*35*/   public const string kActionTerminationLOC = "LOC";// "Dissatisfaction with Location",
             public const string kActionTerminationLVE = "LVE";// "Failure to Return From Leave",
             public const string kActionTerminationMAR = "MAR";// "Marriage",
             public const string kActionTerminationMUT = "MUT";// "Mutual Consent",
             public const string kActionTerminationOTP = "OTP";// "Resignation - Other Position",
    /*40*/   public const string kActionTerminationPAY = "PAY";// "Dissatisfied with Pay",
             public const string kActionTerminationPER = "PER";// "Personal Reasons",
             public const string kActionTerminationPOL = "POL";// "Dissatisfied with Company Policies",
             public const string kActionTerminationPRM = "PRM";// "Dissatisfied with Promotion Opportunities",
             public const string kActionTerminationPTD = "PTD";// "Partial/Total Disablity",
    /*45*/   public const string kActionTerminationREF = "REF";// "Refused Transfer",
             public const string kActionTerminationREL = "REL";// "Relocation",
             public const string kActionTerminationRET = "RET";// "Returned to School",
             public const string kActionTerminationSUP = "SUP";// "Dissatisfied with Supervision",
             public const string kActionTerminationTAR = "TAR";// "Tardiness",
    /*50*/   public const string kActionTerminationTRA = "TRA";// "Transportation Problems",
             public const string kActionTerminationTYP = "TYP";// "Dissatisfied with Type of Work",
             public const string kActionTerminationUNS = "UNS";// "Unsatisfactory Performance",
             public const string kActionTerminationVIO = "VIO";// "Violation of Rules",
             public const string kActionTerminationVSP = "VSP";// "Voluntary Separation Program",
            /*55*/   public const string kActionTerminationWOR = "WOR";// "Dissatisfied with Work Coditions",
             public const string kActionTransferEER = "EER";// "Employee Request",
             public const string kActionTransferMRR = "MRR";// "Manager Request",
             public const string kActionDataPDC = "PDC";// "Personal Data Change",
             public const string kActionDataSSC = "SSC";// "SSN Change",
    /*60*/   public const string kActionDataNMC = "NMC";// "Name Change",
             public const string kActionDataTXC = "TXC";// "Tax Deduction Change",
             public const string kActionDataUWY = "UWY";// "United Fund Change",	
    // PcnMgr 
             public const string kActionPromotionNCP = "NCP";// "Normal Career Progression",  
             public const string kActionPromotionOPR = "OPR";// "Outstanding Performance",      // Not used
    // end of PcnMgr   
             public const string kActionError = "ERR";// "Unknown Action-Reason"           
        }
        internal struct ActionCodeID
        {
           // Application Operation Codes /////////////////////////////////////
        public const int AF_NULL=0;
            public const int AF_REWHIRE = 100;
            public const int AF_NEWHIRE = 101;
            public const int AF_TRANSFERIN = 102;
            public const int AF_BORROW = 103;
            public const int AF_SEPARATION = 121;
            public const int AF_LOA = 122;
            public const int AF_TRANSFEROUT = 123;
            public const int AF_PROMOTION = 141;
            public const int AF_DEMOTION = 142;
            public const int AF_MERIT = 143;
            public const int AF_DATACHANGE = 144;
            public const int AF_ADJUSTMENT = 145;
            // V3.07 dew
            public const int AF_DELETEBORROWEDEMP = 146;
            // end of V3.07 dew
            public const int AF_VIEWLIST = 151;
            public const int AF_PRINTUNIFORMSHEET = 152;
            // PcnMgr
            public const int AF_PCNMGR = 153;
            public const int AF_CHANGEPASSWORD = 154;
        }
        #endregion

        #region SeperationConstants
        // Separation Report codes //////////////////////////////////////////
            internal enum PAY_TYPE
            {
            CHECK = 0,
            CASH = 1,
            }
            internal enum  PAY_SENT
            {
            HOME=   0,
            RESTAURANT=1,
            EMPLOYEE= 2,
            }
            internal enum REHIRE
            {
            NO=0,
            YES=1,
            PROVISIONAL=2,
            }
            internal enum DISCHARGE
            {
            PERFORMANCE = 101,
            ATTENDANCE = 102,
            LAYOFF = 103,
            DEATH = 104,
            VIOLATIONOFRULES = 105,
            REFUSEDINSTRUCTIONS = 106,
            FAILEDTORETURNFROMLOA = 107,
            FAILEDTORETURNFROMVAC = 108,
            OTHER = 109,
            }
            internal enum RESIGNATION
            {
            OTHEREMPLOYMENT=          110,
            DISSATISFIEDWITHJOB=      111,
            PERSONALREASONS=          112,
            RETIREMENT=               113,
            RELOCATED=                114,
            OTHER=         115,
            }
        
        #endregion

        #region Marital Status Constants
                
        internal static char MARITALSTATUS_SINGLE = '0';
        internal static char MARITALSTATUS_MARRIED = '1';
        internal static char MARITALSTATUS_MARRIEDEX = '2';
       
#endregion

        internal static char STATUS_EMPLOYEEREQUEST= 'E';
        internal static char STATUS_COMPANYREQUEST = 'C';
        internal static string DEFAULT_SALARY_XML_STATE = "**";

        #region Unused Validation Messages
        //internal static string IDS_ABOUTBOX = "&About Personnel Change Notification...";
               
        //internal static string IDS_DUPSSNUM = "Employee %s %s in the active database has a local id number identical to this social security number. Please double check this number or call the the Carl's Jr. Help Desk.";
               
        //internal static string IDS_ERRCSCRTFILE = "%s(): An error occurred creating the Personnel Change Notification file.";
        //internal static string IDS_ERRCSOPNFILE = "%s(): An error occurred opening the Personnel Change Notification file.";

        //internal static string IDS_ERRUNITINFO = "Restaurant Information file does not contain valid information. Please contact the Help Desk.";
        //internal static string IDS_ERRPROFILE = "Cannot access the PCN Persistent Data File. Please contact the Help Desk.";
        //internal static string IDS_ERRDISPATCH = "%s(): An error occurred accessing ActiveX Controls.";
        //internal static string IDS_WARN_ADJUSTMENT = "You must complete an adjustment to change this transaction.";
        //internal static string IDS_WARN_SEPARATION = "PCN will now open a Separation Transaction to complete the action.";
        //internal static string IDS_WARN_NEWHIREAPP = "You must now terminate this employee.";

        //internal static string IDS_WARN_CONTACTHR = "Contact HR for resolution.";
        //internal static string IDS_WARN_CONTACTDM =  "Contact DM for resolution.";
        //internal static string IDS_WARN_CONTACTHRAST = "Contact HR for assistance.";
        //internal static string IDS_WARN_XFERINAPP = "This action is not permitted. Contact your DM for assistance.";
        //internal static string  IDS_WARN_HOMECHG = "Employee's home unit has changed. Contact DM for questions.";
        //internal static string IDS_WARN_BORROW = "PCN will convert this employee to 'Borrowed' to complete the action.";
        
        //internal static string IDS_ERRCPIDXEMPNUM = "%s(): An error occurred searching the AMWS file for employee number %09lu.";
        //internal static string IDS_INVALIDMERITRATE = "The new rate must be greater or equal to the old rate.";
        
        //internal static string IDS_ERRCSIDXEMPNUM = "%s(): An error occurred searching the Personnel Change Notification file for employee number %09lu.";
        //internal static string IDS_ERRCSIDXLOCALID = "%s(): An error occurred searching the Personnel Change Notification file for local id %s.";
        //internal static string IDS_ERRCSRECNOTFOUND = "%s(): Employee number %lu not found in active database file.  The record has been removed from the memo file.";
        //internal static string IDS_ERRCSINSRECORD = "%s(): An error occurred inserting record into Personnel Change Notification file.  Employee number = %09lu.";
        //internal static string IDS_ERRCSUPDRECORD = "%s(): An error occurred updating the Personnel Change Notification record indexed by employee number %09lu.";
        //internal static string IDS_ERRCSSTEPNEXT = "%s(): An error occurred stepping through physical records in the Personnel Change Notification file.";
        //internal static string IDS_ERRCSDELRECORD = "%s(): An error occurred deleting Personnel Change Notification record indexed by %09lu.";
        //internal static string IDS_INVALIDRATING = "You must select the employee's review rating.";
        //internal static string IDS_ERRSRCRTFILE = "%s(): An error occurred creating the separation file.";
        //internal static string IDS_ERRSROPNFILE = "%s(): An error occurred opening the separation file.";
        //internal static string IDS_ERRSRIDXEMPNUM = "%s(): An error occurred searching the separation file for employee number %09lu.";
        //internal static string IDS_ERRSRINSRECORD = "%s(): An error occurred inserting record into the separation file.  Employee number = %09lu.";
        //internal static string IDS_ERRSRUPDRECORD = "%s(): An error occurred updating the separation record indexed by employee number %09lu.";
        
        //internal static string IDS_ERRSRCOMPLETEDATES  = "The warning dates portion of this report must be completed.";
        //internal static string IDS_QRYSROVERWRITE  = "If you save this record, you will overwrite the original record. Are you sure you want to do this?";
        //internal static string IDS_QRYSRSAVEAGAIN = "Save again?";
        //internal static string IDS_ERRSRDELRECORD = "%s(): An error occurred deleting separation record indexed by %09lu.";
        
               
        
                
        //internal static string IDS_INFPERMITSWILLEXPIRE = "The work permits for the following employee(s) will expire within two weeks: %s";
        //internal static string IDS_INFPERMITSHAVEEXPIRED = "The work permits for the following employee(s) have expired: %s ";
        
        //internal static string IDS_ERRUIWRITEFILE = "%s(): An error occurred writing the unit data file.";
        
        //internal static string IDS_ERRMEMOTMPFILEEXISTS = "%s(): An existing temporary memo file was found, indicating  a previous incomplete process.  It will be removed.";
        //internal static string IDS_ERRMEMOOPENFILE = "%s(): An error occurred opening the adjustment memos file.";

        
        //internal static string IDS_INVALIDSEPARATIONDETAILS = "The separation details must be completed";
        
        //internal static string IDS_INVALIDREGION = "The region number must be specified.";
        //internal static string IDS_INVALIDDISTRICT = "The district number must be specified.";
        //internal static string IDS_INVALIDRESTAURANT = "The restaurant number must be specified.";
        //internal static string IDS_INVALIDCOUNTY = "The county of residence must be completed.";
        
        
               
        //internal static string IDS_ERRMINORPERMITDATE = "The Work Permit Expiration Date is invalid.";
               
        //internal static string AFX_IDS_APP_TITLE = "Personnel Change Notification";
        
        
       
        //internal static string IDS_ERRMEMOOPENTMP = "%s(): An error occurred opening the temporary adjustment memos file.";
        //internal static string IDS_ERRALLOCMEM = "%s(): An error occurred allocating memory from the heap.";
        //internal static string IDS_ERRDLL = "An error occurred loading concept data. Please call the the Carl's Jr. Help Desk.";        
        //internal static string IDS_INVALIDSSPREFIX = "%s is an invalid social security number prefix. If you have any questions, call the Carl's Jr. Help Desk.";        
        
        //internal static string IDS_WRNRATEOUTOFRANGE = "The rate entered is out of range for the job title selected. Do you want to save anyway?";
               
        
        //internal static string IDS_ERRMINORPERMITNODATE = "The Work Permit Expiration Date must be completed.";
              
              
        //internal static string IDS_SSNINVALIDIVRCODE = "IVR Code you have entered shows that SSN number you enterd on a phone was invalid. ";
        //internal static string IDS_INALIGIBLEIVRCODE = "IVR Code you have entered shows INALIGIBALE for REHIRE status.";
        //internal static string IDS_EMPLACTIVEIVRCODE = "IVR code you have entered shows ACTIVE status for this employee. Person is already an active employee of CKE.";
        
        //internal static string IDS_ERRPRTFINDRESOURCE = "%s(): The print resource could not be found.";
        //internal static string IDS_ERRPRTLOADRESOURCE = "%s(): The print resource could not be loaded.";
        //internal static string IDS_ERRPRTNOPRINTQUEUES = "%s(): No print queues exist on this system.";
                        
        #endregion

        #region Used Validation Message

        internal static string IDS_INVALIDSSNUM = "You must enter a valid social security number for this employee. The employee number and local id are both based on this number.";
        internal static string IDS_SSNNONMATCH = "The social security number does not match the original entry.";
        internal static string IDS_DUPEMPNUM = "The employee number (social security number) that you specified is already in use by ";
        
        internal static string IDS_INVALIDIVRCODE = "Invalid IVR Code or SSN number. IVR code is not corresponding to the entered SSN number. Please try again!";
        internal static string IDS_BLANKIVRCODE = "IVR Code must be entered!";
        internal static string IDS_SHORTIVRCODE = "Number of characters in IVR Code is incorrect! Please try again!";
        internal static string IDS_INCORRECTIVRCODE = "IVR Code you have entered is incorrect. Please try again!";
        
        internal static string IDS_DUPLOCALID = "This local id number is already in use by ";
        internal static string IDS_INVALIDLASTNAME = "You must enter a last name for this employee.";
        internal static string IDS_INVALIDFIRSTNAME = "You must enter a first name for this employee.";
        internal static string IDS_INVALIDADDRESS = "You must enter an address for this employee.";
        internal static string IDS_INVALIDCITY = "You must enter the city for this employee.";
        internal static string IDS_INVALIDSTATE = "You must enter the state for this employee.";
        internal static string IDS_INVALIDSTATEFORADMIN = "You must select the state.";
        internal static string IDS_INVALIDCONCEPTFORADMIN = "You must select the concept.";
        internal static string IDS_INVALIDZIP = "You must enter a valid zip code for this employee.";
        internal static string IDS_INVALIDCOUNTY = "The county of residence must be completed.";
        internal static string IDS_INVALIDBIRTHDATE = "The birthdate for this employee is invalid.";
        internal static string IDS_INVALIDSEX = "The employee's sex must be entered.";
        internal static string IDS_INVALIDETHNICGROUP = "An ethnic group for the employee must be selected.";
        internal static string IDS_WRNNOPHONETYPE1 = "You did not select a phone type for the employee's first phone.  Do you want to save anyway?";
        internal static string IDS_WRNNOPHONETYPE2 = "You did not select a phone type for the employee's second phone.  Do you want to save anyway?";
        internal static string IDS_WRNINVALIDPHONE1 = "Please enter a valid Phone #1 for the employee.";
        internal static string IDS_WRNINVALIDPHONE1TYPE = "Please enter a valid Phone type #1 for the employee.";
        internal static string IDS_WRNINVALIDPHONE2 = "Please enter a valid Phone #2 for the employee.";
        internal static string IDS_ERRSRCOMPLETEDETAILS = "The details portion of this report must be completed.";
        internal static string IDS_INVALIDNEWJOBTITLE = "The new job title must be entered.";
        internal static string IDS_WRONGNEWJOBTITLE = "The new job title is not valid for this action.";
        internal static string IDS_INVALIDNEWRATE = "The new rate must be entered.";
        internal static string IDS_VALIDRATE = "The new rate must be valid.";
        internal static string IDS_INVALIDNEWEFFDATE = "A valid new effective date must be entered.";
        internal static string IDS_BIRTHDATEINVALIDMONTH = "You must enter valid month for birthdate.";
        internal static string IDS_BIRTHDATEINVALIDDAY = "You must enter valid day for birthdate.";
        internal static string IDS_BIRTHDATEINVALIDYEAR = "You must enter valid year for birthdate.";
        internal static string IDS_SEPRATIONDATEINVALIDMONTH = "You must enter valid month for separation.";
        internal static string IDS_SEPRATIONDATEINVALIDDAY = "You must enter valid day for separation.";
        internal static string IDS_SEPRATIONDATEINVALIDYEAR = "You must enter valid year for separation.";
        internal static string IDS_LASTDATEINVALIDMONTH = "You must enter valid month for lastday.";
        internal static string IDS_LASTDATEINVALIDDAY = "You must enter valid day for lastday.";
        internal static string IDS_LASTDATEINVALIDYEAR = "You must enter valid year for lastday.";
        internal static string IDS_DATEBACKINVALIDMONTH = "You must enter valid month for return date.";
        internal static string IDS_DATEBACKINVALIDDAY = "You must enter valid day for return date.";
        internal static string IDS_DATEBACKINVALIDYEAR = "You must enter valid year for return date.";

        internal static string IDS_INJURYINVALIDMONTH = "You must enter valid month for injury date.";
        internal static string IDS_INJURYINVALIDDAY = "You must enter valid day for injury date.";
        internal static string IDS_INJURYINVALIDYEAR = "You must enter valid year for injury date.";

        internal static string IDS_NEWEFFDATEINVALIDMONTH = "You must enter valid month for effective date.";
        internal static string IDS_NEWEFFDATEINVALIDDAY = "You must enter valid day for effective date.";
        internal static string IDS_NEWEFFDATEINVALIDYEAR = "You must enter valid year for effective date.";
        internal static string IDS_INVALIDNEWEFFDATERANGE = "A new effective date should be in a given range: ";
        internal static string IDS_NEWEFFDATERLESSTHANHIREDATE = "A new effective date should not be less than hire date.";
        internal static string IDS_WRNRATEOUTOFRANGE = "The pay rate entered is out of range for the job title selected. Do you want to save anyway?";
        internal static string IDS_RATEOUTOFRANGE = "The pay rate entered is out of range for the job title selected. The pay rate should be within ";
        internal static string IDS_WARNRATEOUTOFRANGE = "The pay rate entered for the selected job title is not within the specified range of ";
        internal static string IDS_CONFIRMMSG = "Do you want to save anyway?";

        internal static string IDS_WARNAGE = "Candidate is too young to work at CKE.";
        internal static string IDS_WARNMINOR = "Candidate is underage, you must fill out the Minor Information Form.";
        
        internal static string IDS_W4WARNING = "Remember that a W4 form is required with this change.";

        internal static string IDS_INVALIDHOURSPERWEEK = "The number of hours per week must be completed.";
        internal static string IDS_INVALIDLASTDAY = "The last day worked is an invalid date.";
        internal static string IDS_INVALIDSCHEDTYPE = "The full or part time information must be completed.";
        internal static string IDS_INVALIDREASON = "A reason for separation must be selected.";
        internal static string IDS_INVALIDRESIGLEAVEREASON = "A reason for leave must be specified.";       
        internal static string IDS_INVALIDRESIGNATIONDETAILS = "Leave requested must be completed.";
        internal static string IDS_INVALIDNOTICEDAYS = "Notice days must be specified.";
        internal static string IDS_INVALIDPAYTYPE = "The separation pay type must be specified.";        
        internal static string IDS_INVALIDPAYSENTTO = "The separation pay destination must be specified.";
        internal static string IDS_INVALIDVACATIONPAYOUT = "The vacation payout information must be completed.";
        internal static string IDS_INVALIDREHIREELIGIBILITY = "The rehire eligibility must be specified.";
        internal static string IDS_INVALIDREHIREEXPLANATION = "The provisional rehire explanation must be completed.";
        internal static string IDS_INVALIDSEPARATIONDETAILDATA = "The separation details must be completed.";
        internal static string IDS_INVALIDSEPARATIONDATE = "The separation date is invalid.";
        internal static string IDS_INVALIDFAILEDTORETURNREASON = "You must select either Vacation or Leave of Absence for the reason Failed to Return.";
        internal static string IDS_INVALIDDATEDUEBACK = "The date due back is not valid.";
        internal static string IDS_INVALIDDATEBACKLOA = "The expected return date is not valid.";
        internal static string IDS_INVALIDINJURYDATELOA = "The injury date is not valid.";
        internal static string IDS_INVALIDRETURNDATE = "The expected return is invalid.";
        internal static string IDS_INVALIDINJURYDATE = "The injury date must be completed.";
        internal static string IDS_INVALIDLOAREASON = "A reason for the leave of absences must be selected.";
        internal static string IDS_INFNOACTEMPLOYEES = "There are no active employees.";
        internal static string IDS_INFNOLOAEMPLOYEES = "There are no employees on leave of absence.";
        internal static string IDS_INFNOSEPEMPLOYEES = "There are no separated employees.";
        internal static string IDS_EMPDATACHECK = "Personal information might be outdated. Please verify and change accordingly.";

        internal static string IDS_INVALIDFEDMARITALSTAT = "The employee's federal marital status must be entered.";
        internal static string IDS_INVALIDSTATEMARITALSTAT = "The employee's state marital status must be entered.";
        internal static string IDS_ISINVALIDPERCENTFED = "Please enter a valid % Fed";
        internal static string IDS_ISINVALIDSTATEWITHOLDING = "Please enter a valid value for State Add. Withholding.";
        internal static string IDS_ISINVALIDFEDERALWITHOLDING = "Please enter a valid value for Federal Add. Withholding.";
        internal static string IDS_ISINVALIDUNITEDFUND = "Please enter a valid United Fund.";
        internal static string IDS_INVALIDTAXINFO = "The employee's tax information must be entered.";
        internal static string IDS_CONFIRMATIONMSG = "Information has been saved successfully.";
        internal static string IDS_CONFIRMATIONWITHPRINTMSG = "Information has been saved successfully. Please remember to print the separation report.";
        internal static string IDS_ERRORMSG = "An error occured while saving the record please try again.";
        internal static string IDS_DELETECONFIRMATIONMSG = "A record has been deleted successfully.";
        internal static string IDS_DELETEERRORMSG = "An error occured while deleting the record please try again.";

        internal static string IDS_WARNSELECTDOCUMENT = "Please select information for Document Provided";
        //internal static string IDS_WARNMINORINCOMPLETE = "All fields should be filled out when choose 'Work Permit' option.";
        internal static string IDS_WARNMINORINCOMPLETE = "All fields required for work permit document!";
        internal static string IDS_ERRMINORHRSPERWKDAY = "The Hours per School Day must be completed.";
        internal static string IDS_ERRMINORHRSPERWKEND = "The Hours per Non-School Day must be completed.";
        internal static string IDS_ERRMINORHRSPERWEEK = "The Total Hours per Week must be completed.";
        internal static string IDS_ERRMINORWKDAYHRS = "The hour limit for weekdays must be completed.";
        internal static string IDS_ERRMINORWKENDHRS = "The hour limit for weekends must be completed.";
        internal static string IDS_ERRMINORWKDAYHRSLIMIT = "The week nights time should be within 06:00 to 12:45.";
        internal static string IDS_ERRMINORWKENDHRSLIMIT = "The weekend night time should be within 06:00 to 12:45.";
        internal static string IDS_INFPERMITSWILLEXPIRE  = "The work permits for the following employee(s) will expire within two weeks: ";
        internal static string IDS_INFPERMITSHAVEEXPIRED = "The work permits for the following employee(s) have expired: ";
        internal static string IDS_ERRMINORPERMITDATE  = "The Work Permit Expiration Date is invalid.";

        internal static string IDS_ERRTRANSUNIT = "The restaurant number must be specified.";
        internal static string IDS_ERRFROMTRANSUNIT = "The 'From' restaurant number is incorrect.";
        internal static string IDS_ERRTRANSREQUEST = "The requester for the transfer must be specified (employee or company).";
        internal static string IDS_WRNDELETERECORD = "There is no way to undo this action. Are you sure you want to unborrow this record?";
        //internal static string IDS_WRNTRANSOUT = "There is no way to undo this action. Are you sure you want to transfer this employee out?";
        internal static string IDS_WRNTRANSOUT = "There is no way to undo this action. Are you sure you want to transfer this employee out?";
        internal static string IDS_INFTRANSFEROUT = " has been permanently transferred out of this unit and removed from both the active and inactive employee lists.";
        internal static string IDS_INFNOINACTEMPLOYEES = "There are no inactive employees.";
        internal static string IDS_INFNOBORROWEDEMPLOYEES = "There are no borrowed employees.";
        internal static string IDS_RECORDREMOVED = "The employee has been unborrowed successfully.";
        internal static string IDS_ERRTRANSPAYROLL = "You must specify whether or not there is a payroll change.";
        internal static string IDS_NODATACHANGE = "The data in this record has not changed.Do you want to save anyway?";
        internal static string IDS_INFREACTIVATE = " has been reactivated and placed on the active employee list.";
        internal static string IDS_ERRAPPALREADYRUNNING = "Personnel Change Notification is already running.";
        internal static string IDS_EXITCONFIRM = "Are you sure you want to exit this application?";
        internal static string IDS_CANCELCONFIRM = "Are you sure you want to cancel this action?";
        internal static string IDS_EXITCONFIRMADMIN = "Are you sure you want to exit from Administration ?";
        internal static string IDS_NOPRINTCONFIRM = "You have not printed this record. Are you sure you want to quit?";
        internal static string IDS_INVALIDPASSWORD = "Security Code must be between 1000 and 9999";
        internal static string IDS_NOPASSWORDCHANGE = "Security Code has been not changed.";

        //Administration Section
        internal static string IDS_INVALIDJobTitle = "You must enter a job name.";        
        internal static string IDS_INVALIDFromPayRate = "You must enter a From hourly range for this job type.";
        internal static string IDS_VALIDFROMRATE = "The From hourly range must be valid.";
        internal static string IDS_VALIDTORATE = "The To hourly range must be valid.";
        internal static string IDS_INVALIDToPayRate = "You must enter a To hourly range for this job type.";
        internal static string IDS_INVALIDSalaryPeriod = "You must select salary period for this job type.";
        internal static string IDS_INVALIDJOBRANK = "You must enter a job rank for this job type.";
        internal static string IDS_DUPJOBRANK = "This job rank is already assigned to ";
        internal static string IDS_INVALIDJOBCODE = "You must enter job code for this job type.";
        internal static string IDS_INVALIDJOBCODEZERO = "Job code should be greater than zero.";
        internal static string IDS_HIGHERJOBCODE = "Job code should be less than 32768.";
        internal static string IDS_DUPJOBCODE = "This job code is already assigned to ";
        internal static string IDS_INVALIDWARNINGDATES = "The dates of verbal and written warnings must be completed.";
        internal static string IDS_ERRMEMOINCOMPLETE = "This information must be completed.";
        internal static string IDS_CANNOTRENAMECATEGORY = "Category exists. Please try another name.";
        internal static string IDS_CANNOTDELETECATEGORY = "Cannot delete category. Remove jobs from category before deletion.";
        internal static string IDS_INFDEACTIVATE = " has been deactivated and placed in the inactive employee list.";
        internal static string IDS_INVALIDMERITRATE = "The new rate must be greater or equal to the old rate.";
        internal static string IDS_INVALIDRATING = "You must select the employee's review rating.";
        internal static string IDS_INVALIDFROMRATE = "The From hourly range must be less than To hourly range.";
        internal static string IDS_INVALIDCONCEPT = "The values of Concept & State from unitinfo.ini file differ either with Concept look-up table or SalaryData.Xml. \n\t \t Please contact your Administrator for details.";
        internal static string IDS_CONFIRMSTATECHANGE = "Jobcode assigned to existing employee may mismatch with job code of newly selected state and may have negative impact on an already existing employees record. \nDo you want to save anyway ?";
        internal static string IDS_CONFIRMCONCEPTCHANGE = "Jobcode assigned to existing employee may mismatch with job code of newly selected concept. This may have negative impact on an already existing employees record. \nDo you want to save anyway?";
        internal static string IDS_CONFIRMSTATECONCEPTCHANGE = "Jobcode assigned to existing employee may mismatch with job code of newly selected state and concept. This may have negative impact on an already existing employees record. \nDo you want to save anyway?";
        internal static string IDS_JOBCODENOTMACHING = "Job title could not assigned to this employee. Please make sure the job code assigned to this employee code is in the salaryData.xml";
        internal static string IDS_PREVJOBCODENOTMACHING = "Previous job title is not assigned to this employee. Please ensure that jobcode assigned to this employee consistent with job code in salaryData.xml.";
        internal static string IDS_CONFIRMJOBCHANGE = "Are you sure you want to overwrite the job type ";
        internal static string IDS_NOJOBDATA = "SalaryData.xml file does not contain job data matching with the selected state and concept. This may have negative impact on an already existing employees record so please make sure that SalaryData.xml exists job data matching with selected state and concept. \nDo you want to save anyway?";
        internal static string IDS_WOTC = "You must enter WOTC # for New Hire. ";

        internal static string IDS_INVALIDDATEFOODHANDLER = "The Food Handler Permit Expiration Date is Within the Warning Limit.";

        #endregion
       
    }
}