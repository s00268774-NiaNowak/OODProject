using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeChart;

namespace ProjectV2
{
    public class TypeChart
    {   // 0 = pyro, 1 = aqua, 2 = flora, 3 = terra, 4 = zephyr, 5 = glacial,
        // 6 = electro, 7 = metal, 8 = noxious, 9 = lumen, 10 = spirit, 11 = vita
        public static  string[] TypeNames =
        {
            "Pyro", "Aqua", "Flora", "Terra", "Zephyr", "Glacial",
            "Electro", "Metal", "Noxious", "Lumen", "Spirit", "Vita"
        };

        static string TypeName(int id)
            => (id >= 0 && id < TypeNames.Length) ? TypeNames[id] : "None";

        private static readonly Dictionary<int, TypeEffectiveness> dictionary = new();
        static readonly Dictionary<int, TypeEffectiveness> typeChart = dictionary;

        // STAB multipliers by attacker slot
        static readonly float[] AttackerStabBySlot = { 1.50f, 1.25f, 1.12f };

        // Effectiveness multipliers by defender slot
        static readonly (float super, float resist, float neutral)[] DefenderBySlot =
        {
            (2.00f, 0.50f, 1.00f),
            (1.50f, 0.66f, 1.00f),
            (1.33f, 0.75f, 1.00f)
        };

        static void LoadTypeDatabase()
        {
            typeChart[0] = new TypeEffectiveness(new[] { 2, 4, 5, 7, 8 }, new[] { 1, 3, 11 });
            typeChart[1] = new TypeEffectiveness(new[] { 0, 3, 9 }, new[] { 2, 5, 6, 11 });
            typeChart[2] = new TypeEffectiveness(new[] { 1, 3, 6 }, new[] { 0, 8, 7, 11 });
            typeChart[3] = new TypeEffectiveness(new[] { 0, 6, 5, 8, 9, 10 }, new[] { 2, 3, 4, 11 });
            typeChart[4] = new TypeEffectiveness(new[] { 0, 3, 8, 9 }, new[] { 10 });
            typeChart[5] = new TypeEffectiveness(new[] { 0, 1, 2, 3, 4, 9 }, new[] { 7, 10 });
            typeChart[6] = new TypeEffectiveness(new[] { 1, 4, 7 }, new[] { 3, 5, 6 });
            typeChart[7] = new TypeEffectiveness(new[] { 3, 5, 9, 11 }, new[] { 0, 6, 7 });
            typeChart[8] = new TypeEffectiveness(new[] { 1, 2, 11 }, new[] { 5, 7, 8 });
            typeChart[9] = new TypeEffectiveness(new[] { 5, 6, 10 }, new[] { 1, 2, 7 });
            typeChart[10] = new TypeEffectiveness(new[] { 0, 4, 7, 10, 11 }, new[] { 9, 8 });
            typeChart[11] = new TypeEffectiveness(new[] { 0, 1, 2, 3, 11 }, new[] { 7, 8, 9, 10 });
        }

        static float DefenderMultiplier(TypeEffectiveness rules, int defenderType, int slot)
        {
            if (defenderType < 0) return 1.0f;

            var (super, resist, neutral) = DefenderBySlot[slot];

            if (rules.IsStrongAgainst(defenderType)) return super;
            if (rules.IsWeakAgainst(defenderType)) return resist;
            return neutral;
        }

        static float CalculateFinalDamage(
            int baseDamage,
            int moveType,
            int a1, int a2, int a3,
            int d1, int d2, int d3)
        {
            if (!typeChart.TryGetValue(moveType, out var rules))
                return baseDamage;

            float multiplier = 1.0f;
            // Defender effectiveness
            multiplier *= DefenderMultiplier(rules, d1, 0);
            multiplier *= DefenderMultiplier(rules, d2, 1);
            multiplier *= DefenderMultiplier(rules, d3, 2);

            // Final damage 
            return (baseDamage * multiplier);
        }

        static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            return int.Parse(Console.ReadLine() ?? "0");
        }

        static void PrintTypes()
        {
            Console.WriteLine("Types:");
            for (int i = 0; i < TypeNames.Length; i++)
                Console.WriteLine($"{i} = {TypeNames[i]}");
            Console.WriteLine("-1 = None\n");
        }
    }
}
