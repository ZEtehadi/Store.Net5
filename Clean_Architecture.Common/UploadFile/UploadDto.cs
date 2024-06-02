using System;

namespace Clean_Architecture.Common.UploadFile
{
    public  class UploadDto
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public String FileNameAddress { get; set; }
    }
}
