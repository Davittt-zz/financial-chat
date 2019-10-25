using System;
using System.ComponentModel.DataAnnotations;

namespace JobsityFinancialChat.API.HubModels
{
    public class MessageModel
    {
        [Required]
        public Guid ChatroomId { get; set; }
        
        [Required]
        public string Text { get; set; }

        public  Guid UserId { get; set; }

        public int MessageType { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime? ReadDate { get; set; }
    }
}