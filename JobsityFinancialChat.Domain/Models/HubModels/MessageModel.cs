using System;
using System.ComponentModel.DataAnnotations;

namespace JobsityFinancialChat.API.HubModels
{
    public class MessageModel
    {
        //public int? InquiryId { get; set; }

        [Required]
        public Guid ChatroomId { get; set; }
        
        [Required]
        public string Message { get; set; }

        //public int PublicationId { get; set; }
                
        public int MessageType { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime? ReadDate { get; set; }
    }
}