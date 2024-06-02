using System;

namespace Clean_Architecture.Application.Services.Finances.Commands.AddRequestPay
{
    public class ResultRequestPayDto
    {
        public Guid guid { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        public string Mobile { get; set; }
        public long RequestPayId { get; set; }
    }
}
