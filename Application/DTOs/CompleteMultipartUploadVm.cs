namespace Application.DTOs;

public class CompleteMultipartUploadVm
{
    public string Key { get; set; }
    public string UploadId { get; set; }

    public List<PartETagInfo> Parts { get; set; }
}

public class PartETagInfo
{
    public int PartNumber { get; set; }
    public string ETag { get; set; }
}
