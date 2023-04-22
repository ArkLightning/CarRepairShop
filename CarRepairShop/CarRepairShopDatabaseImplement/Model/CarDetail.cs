using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopDatabaseImplement.Model
{
    internal class CarDetail
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int DetialId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Car Car { get; set; } = new();

        public virtual Detail Detail { get; set; } = new();
    }
}
