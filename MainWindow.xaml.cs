using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TypeChart;

namespace ProjectV2
{

    public partial class MainWindow : Window
    {
        #region Type Chart Data
        // 0 = pyro, 1 = aqua, 2 = flora, 3 = terra, 4 = zephyr, 5 = glacial,
        // 6 = electro, 7 = metal, 8 = noxious, 9 = lumen, 10 = spirit, 11 = vita
        public static string[] TypeNames =
        {
            "Pyro", "Aqua", "Flora", "Terra", "Zephyr", "Glacial",
            "Electro", "Metal", "Noxious", "Lumen", "Spirit", "Vita"
        };
        static string TypeName(int id)
            => (id >= 0 && id < TypeNames.Length) ? TypeNames[id] : "None";

        private static readonly Dictionary<int, TypeEffectiveness> dictionary = new();
        static readonly Dictionary<int, TypeEffectiveness> typeChart = dictionary;


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

        #endregion Type Chart Data
        public MainWindow()
        {
            InitializeComponent();
        }
        //If a type button is taken, set a clicked element button to the next available type button
        //every type has to be unique
        public void AssignElementToTypeButton(string element)
        {
            if (BtnType1.Content.ToString() == "Type")
            {
                BtnType1.Content = element;
                BtnType1.Visibility = Visibility.Visible;
            }
            else if (BtnType2.Content.ToString() == "Type" && BtnType1.Content.ToString() != element)
            {
                BtnType2.Content = element;
                BtnType2.Visibility = Visibility.Visible;
            }
            else if (BtnType3.Content.ToString() == "Type" && BtnType1.Content.ToString() != element && BtnType2.Content.ToString() != element)
            {
                BtnType3.Content = element;
                BtnType3.Visibility = Visibility.Visible;
            }

        }


        //Element Buttons

        private void BtnPyro_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Pyro");
        }

        private void BtnAqua_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Aqua");
        }
        private void BtnFlora_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Flora");
        }

        private void BtnTerra_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Terra");
        }

        private void BtnVephyr_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Zephyr");
        }

        private void BtnGlacial_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Glacial");
        }

        private void BtnNoxious_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Noxious");
        }

        private void BtnLumen_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Lumen");
        }

        private void BtnElectro_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Electro");
        }

        private void BtnMetal_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Metal");
        }

        private void BtnSpirit_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Spirit");
        }

        private void BtnVita_Click(object sender, RoutedEventArgs e)
        {
            AssignElementToTypeButton("Vita");
        }


        //Type Buttons

        private void BtnType1_Click(object sender, RoutedEventArgs e)
        {
            //code to remove type from button and shift other types down

            BtnType1.Content = BtnType2.Content;
            BtnType2.Content = BtnType3.Content;
            BtnType3.Content = "Type";

            //set visibility
            if (BtnType2.Content.ToString() == "Type")
            {
                BtnType2.Visibility = Visibility.Hidden;
            }
            if (BtnType3.Content.ToString() == "Type")
            {
                BtnType3.Visibility = Visibility.Hidden;
            }
            if (BtnType1.Content.ToString() == "Type")
            {
                BtnType1.Visibility = Visibility.Hidden;
            }

        }

        private void BtnType2_Click(object sender, RoutedEventArgs e)
        {
            BtnType2.Content = BtnType3.Content;
            BtnType3.Content = "Type";
            //set visibility
            if (BtnType3.Content.ToString() == "Type")
            {
                BtnType3.Visibility = Visibility.Hidden;
            }
            if (BtnType2.Content.ToString() == "Type")
            {
                BtnType2.Visibility = Visibility.Hidden;
            }

        }

        private void BtnType3_Click(object sender, RoutedEventArgs e)
        {
            BtnType3.Content = "Type";
            BtnType3.Visibility = Visibility.Hidden;

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //set all Type Buttons to invisible on load
            BtnType1.Visibility = Visibility.Hidden;
            BtnType1.Content = "Type";
            BtnType2.Visibility = Visibility.Hidden;
            BtnType2.Content = "Type";
            BtnType3.Visibility = Visibility.Hidden;
            BtnType3.Content = "Type";
        }

        //Match the typebutton content to the typeNames index
        public int GetTypeIndex(string typeName)
        {
            for (int i = 0; i < TypeNames.Length; i++)
            {
                if (TypeNames[i] == typeName)
                {
                    return i;
                }
            }
            return -1; // Return -1 if type not found

        }
        //In the listbox, show how types are effective against the selected types
        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Load the type database
            LoadTypeDatabase();
            // Get selected types from buttons
            int type1 = GetTypeIndex(BtnType1.Content.ToString());
            int type2 = GetTypeIndex(BtnType2.Content.ToString());
            int type3 = GetTypeIndex(BtnType3.Content.ToString());
            // Clear previous results
            TxtResults.Items.Clear();
            //foreach type in the type chart, calculate how much damage it does as an attacker
            foreach (var attackerType in typeChart.Keys)
            {
                var rules = typeChart[attackerType];
                float multiplier = 1.0f;
                multiplier *= DefenderMultiplier(rules, type1, 0);
                multiplier *= DefenderMultiplier(rules, type2, 1);
                multiplier *= DefenderMultiplier(rules, type3, 2);
                // Only display if the multiplier is not neutral
                if (multiplier != 1.0f)
                {
                    string result = $"{TypeName(attackerType)}: x{multiplier:F2}";
                    TxtResults.Items.Add(result);
                }
            }


        }

        private void TxtResults_Loaded(object sender, RoutedEventArgs e)
        {
            TxtResults.Items.Clear();
        }
    }
}