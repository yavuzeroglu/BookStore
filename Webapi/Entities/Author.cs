using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.Entities
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        

    }
}