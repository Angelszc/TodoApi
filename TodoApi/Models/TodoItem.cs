using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}