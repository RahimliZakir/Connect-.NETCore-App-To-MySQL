using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationUI.Models.Entities
{
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string ResponsiblePeople { get; set; }

        public bool IsCompleted { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
