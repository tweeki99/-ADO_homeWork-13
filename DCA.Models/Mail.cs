using System;

namespace DCA.Models
{
    public class Mail:Entity
    {
        public Reciver Reciver { get; set; }
        public Guid ReciverId { get; set; }
        public string Theme { get; set; }
        public string Text { get; set; }
    }
}