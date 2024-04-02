using System;
using System.ComponentModel.DataAnnotations;

namespace Api.EF.Entities
{
    public class Tags
    {
        [Key]
        public int? Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
    }
}
