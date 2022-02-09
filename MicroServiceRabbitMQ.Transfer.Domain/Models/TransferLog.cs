﻿namespace MicroServiceRabbitMQ.MicroServices.Transfer.Domain.Models
{
    public class TransferLog
    {
        public int Id { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal TransferAmont { get; set; }
    }
}
