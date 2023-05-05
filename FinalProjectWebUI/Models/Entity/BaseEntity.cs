using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectWebUI.Models.Entity
{
    public class BaseEntity
    {

        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
