namespace BioEnumerator.DataAccessManager.DataContract.ContractHelper
{
    public class ReportDashboardCount
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemShortName { get; set; }
        public int Count { get; set; }
    }
    public class DashboardDataCount
    {
        public int AllData { get; set; }
        public int Uploaded { get; set; }
        public int Pending { get; set; }
        public int Error { get; set; }
    }
}
