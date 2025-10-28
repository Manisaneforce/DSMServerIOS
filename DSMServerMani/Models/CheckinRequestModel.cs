namespace DSMServerMani.Models
{
    public class CheckinRequestModel
    {
        public string? DSMCode { get; set; }
        public string? CheckInTime { get; set; }
        public string? EntryDate { get; set; }
        public string? EKey { get; set; }
        public int? EntryType { get; set; }
        public string? CheckInImage { get; set; }
        public string? CheckInLat { get; set; }
        public string? CheckInLong { get; set; }
    }
}
