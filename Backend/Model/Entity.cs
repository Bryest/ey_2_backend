namespace Backend.Model
{
    public class EntityResponse
    {
        public int Hits { get; set; }
        public List<EntityRow> Rows { get; set; } = new List<EntityRow>();
    }

    public class EntityRow
    {
        public string DataFrom { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public string Jurisdiction { get; set; } = string.Empty;
        public string LinkedTo { get; set; } = string.Empty;
    }
    public class ErrorResponse
    {
        public string Error { get; set; }
    }
}
