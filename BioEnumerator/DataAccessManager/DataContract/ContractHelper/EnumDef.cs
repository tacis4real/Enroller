
namespace BioEnumerator.DataAccessManager.DataContract.ContractHelper
{


    public enum MaritalStatus
    {
        Single = 1,
        Married,
        Divorce
    }

    public enum ActionType
    {
        Add = 1,
        Update,
        Approved,
        Issued
    }
    
    public enum RegStatus
    {
        New_Registration = 1,
        Uploaded,
        Edited,
        Deleted
    }

    public enum UploadStatus
    {
        Successful = 1,
        Failed = -1,
        Incompleted = 0,
        
    }

    public enum CustomerTitle
    {
        Mr = 1,
        Miss,
        Mrs,
        Alhaji,
        Chief,
        Pastor,
        Dr,
        Lawyer,
        Prof,

    }
    public enum SpecialAnniversaryType
    {
        None = 0,
        Marriage = 1,
        House_Warming,
        Coronation,
        Others,

    }
    public enum BeneficiaryType
    {
        Customer = 1,
        Supplier,
        Staff
    }

    public enum BeneficiaryStatus
    {
        Active = 1,
        DeActivated = 0,
        Suspended = -1,

    }

    public enum DefectComplexity
    {
        Useable = 1,
        Manageable,
        Condemned
    }

    public enum ExpenseType
    {
        Cost_Of_Sales = 1,
        Operating_Expenses,
        Non_Operation_Expenses
    }


    public enum TransactionStatus
    {
        Fresh = 1,
        Approved,
        Rejected,
        Processed,
        Closed,
       
    }

    public enum IdentifierNumberType
    {
        Invoice_Number = 1,
        Receipt_Number,
        Transaction_Code,
        Reference_Number,
        Others

    }

    public enum UnitsOfMeasurement
    {
        Meter,
        Kilogram,
        Litre,
        Unit,
        Yard,
        Mass,
        Time,
        Others
    }

    public enum StoreTransactionType
    {
        Entry = 1,
        Issued,
        Disposed,
        Returned
    }
    public enum RequisitionStatus
    {
        Fresh = 1,
        Pending,
        Approved,
        Denied,
        Issued,
        Close,
      
    }
    
    
    public enum AccountInstrumentType
    {
        Teller,
        POS_Receipt,
        Online_Reference,
        Transfer_Code,
        Mobile_Reference
    }

    
    public enum Channel
    {
        Windows = 1,
        Mobile,
        Web,
       
    }

    
    public enum NotificationType
    {
        All = 0,
        SMS = 1,
        Email,
        Push_Notification,
       
    }
    public enum PreferredPaymentMethod
    {
        Any = 0,
        Bank_Deposit = 1,
        Bank_Transfer,
        Cash_Payment,
        Cheque,
        Mobile_Money,
        Online_Transfer,
        Online_Payment

    }
}
