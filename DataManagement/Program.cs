using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectV2;


namespace DataManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            ArvivaData db = new ArvivaData();

            using (db)
            {
                Arviva a1 = new Arviva()
                {
                    Id = 1,
                    Name = "Woroptr",
                    Catgeory = "Thunder Chick Arviva",
                    ImageUrl = null,
                    Hp = 49,
                    Spd = 60,
                    Atk = 40,
                    Def = 43,
                    MAtk = 63,
                    MDef = 45,
                    types = new string[] { "Electro" },
                    Description = ""
                };
                Arviva a2 = new Arviva()
                {
                    Id = 2,
                    Name = "Wosesychus",
                    Catgeory = "Anxious Lightning Arviva",
                    ImageUrl = null,
                    Hp = 87,
                    Spd = 55,
                    Atk = 40,
                    Def = 45,
                    MAtk = 95,
                    MDef = 88,
                    types = new string[] { "Electro" },
                    Description = ""
                };
                Arviva a3 = new Arviva()
                {
                    Id=3,
                    Name = "Thriderlock",
                    Catgeory = "Proud Lightning Arviva",
                    ImageUrl = null,
                    Hp = 90,
                    Spd = 100,
                    Atk = 50,
                    Def = 55,
                    MAtk = 125,
                    MDef = 90,
                    types = new string[] { "Electro" },
                    Description = ""
                };
                Arviva a4 = new Arviva()
                {
                    Id = 4,
                    Name = "Weelight",
                    Catgeory = "Small Lightbulb Arviva",
                    ImageUrl= null,
                    Hp = 40,
                    Spd = 70,
                    Atk = 30,
                    Def = 25,
                    MAtk = 35,
                    MDef = 30,
                    types = new string[] { "Lumen", "Metal" },
                    Description = ""
                };
                Arviva a5 = new Arviva()
                {
                    Id = 5,
                    Name = "Amrilight",
                    Catgeory = "Ambush Streetlamp Arviva",
                    ImageUrl = null,
                    Hp = 92,
                    Spd = 40,
                    Atk = 80,
                    Def = 65,
                    MAtk = 95,
                    MDef = 63,
                    types = new string[] { "Lumen", "Metal" },
                    Description = ""
                };
                Arviva a6 = new Arviva()
                {
                    Id = 6,
                    Name = "Fireshork",
                    Catgeory = "Firework Pup Arviva",
                    ImageUrl = null,
                    Hp = 50,
                    Spd = 121,
                    Atk = 15,
                    Def = 40,
                    MAtk = 101,
                    MDef = 40,
                    types = new string[] { "Lumen", "Metal" },
                    Description = ""
                };
                Arviva a7 = new Arviva()
                {
                    Id = 7,
                    Name = "Meshayork",
                    Catgeory = "Firework Missile Arviva",
                    ImageUrl = null,
                    Hp = 90,
                    Spd = 145,
                    Atk = 35,
                    Def = 60,
                    MAtk = 145,
                    MDef = 60,
                    types = new string[] { "Lumen", "Pyro", "Metal" },
                    Description = ""
                };
                db.Arvivas.AddRange(new List<Arviva>() { a1, a2, a3, a4, a5, a6, a7 });
                db.SaveChanges();
            }
            
        }
    }
}
