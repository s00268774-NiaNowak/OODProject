using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ProjectV2
{
    public class Arviva
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Catgeory { get; set; }
        public string? ImageUrl { get; set; }
        public int Hp { get; set; } public int Spd { get; set; } 
        public int Atk { get; set; } public int Def { get; set; }
        public int MAtk { get; set; } public int MDef { get; set; }
        public string[]? types { get; set; }
        public string? Description { get; set; }

    }

    public class ArvivaData : DbContext
    {
        public ArvivaData() : base("MyArvivaData") { }
        public DbSet<Arviva> Arvivas { get; set; }
    }
}
