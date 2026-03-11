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
using static System.Net.Mime.MediaTypeNames;

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

        static readonly float[] AttackerStabBySlot = { 1.50f, 1.25f, 1.12f };

        // Effectiveness multipliers by defender slot
        static readonly (float super, float resist, float neutral)[] DefenderBySlot =
        {
            (2.00f, 0.50f, 1.00f),
            (1.50f, (2.0f / 3.0f), 1.00f),
            ((4.0f/3.0f), 0.75f, 1.00f)
        };
        static void LoadTypeDatabase()
        {
            typeChart[0] = new TypeEffectiveness(new[] { 2, 4, 5, 7, 8 }, new[] { 1, 3, 11 });
            typeChart[1] = new TypeEffectiveness(new[] { 0, 3, 9 }, new[] { 2, 5, 6, 11 });
            typeChart[2] = new TypeEffectiveness(new[] { 1, 3, 6 }, new[] { 0, 8, 7, 11 });
            typeChart[3] = new TypeEffectiveness(new[] { 0, 6, 5, 8, 9, 10 }, new[] { 2, 3, 4, 11 });//terra
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
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //set all Type Buttons to invisible on load
            BtnType1.Visibility = Visibility.Hidden;
            BtnType1.Content = "Type";
            BtnType2.Visibility = Visibility.Hidden;
            BtnType2.Content = "Type";
            BtnType3.Visibility = Visibility.Hidden;
            BtnType3.Content = "Type";

            //Hazard Screen visibility
            #region 
            BtnTerraSpikes.Visibility = Visibility.Hidden;
            BtnMetalSpikes.Visibility = Visibility.Hidden;
            BtnGlacialSpikes.Visibility = Visibility.Hidden;
            BtnNoxiousSpikes.Visibility = Visibility.Hidden;
            BtnPyroWeather.Visibility = Visibility.Hidden;
            BtnAquaWeather.Visibility = Visibility.Hidden;
            BtnElectroWeather.Visibility = Visibility.Hidden;
            BtnVephyrWeather.Visibility = Visibility.Hidden;
            BtnFloraTendrils.Visibility = Visibility.Hidden;
            BtnLumenTendrils.Visibility = Visibility.Hidden;
            BtnSpiritTendrils.Visibility = Visibility.Hidden;
            BtnVitaTendrils.Visibility = Visibility.Hidden;

            ImgHazard.Source = null;
            TxBlHazards.Text = string.Empty;
            #endregion
        }
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

        #region Defence Page
        //Element Buttons
        #region Element Buttons
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            //determine what button was clicked
            Button selectedButton = sender as Button;
            if (selectedButton != null)
            {
                string elementContent = selectedButton.Content.ToString();
                AssignElementToTypeButton(elementContent);
            }
        }

        #endregion Element Buttons 
        //Type Buttons
        #region Type Buttons
        private void BtnType1_Click(object sender, RoutedEventArgs e)
        {
            //code to remove type from button and shift other types down

            BtnType1.Content = BtnType2.Content;
            BtnType2.Content = BtnType3.Content;
            BtnType3.Content = "Type";

           


            //set visibility
            if (BtnType2.Content.ToString() == "Type" )
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
            if (BtnType3.Content.ToString() == "Type" )
            {
                BtnType3.Visibility = Visibility.Hidden;
                
            }
            if (BtnType2.Content.ToString() == "Type" )
            {
                BtnType2.Visibility = Visibility.Hidden;
                
            }

        }

        private void BtnType3_Click(object sender, RoutedEventArgs e)
        {
            BtnType3.Content = "Type";
            BtnType3.Visibility = Visibility.Hidden;
          

        }
        #endregion Type Buttons
       
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
                string result = $"{TypeName(attackerType)}: x{multiplier:F2}";
                TxtResults.Items.Add(result);
            }
        }


        private void TxtResults_Loaded(object sender, RoutedEventArgs e)
        {
            TxtResults.Items.Clear();
        }
        #endregion

        #region Hazards Page
        private void SpikesMain_Click(object sender, RoutedEventArgs e)
        {
            #region Spikes
            BtnTerraSpikes.Visibility = Visibility.Visible;
            BtnNoxiousSpikes.Visibility = Visibility.Visible;
            BtnGlacialSpikes.Visibility = Visibility.Visible;
            BtnMetalSpikes.Visibility = Visibility.Visible;
            #endregion
            #region Hide
            BtnPyroWeather.Visibility = Visibility.Hidden;
            BtnAquaWeather.Visibility = Visibility.Hidden;
            BtnElectroWeather.Visibility = Visibility.Hidden;
            BtnVephyrWeather.Visibility = Visibility.Hidden;
            BtnFloraTendrils.Visibility = Visibility.Hidden;
            BtnLumenTendrils.Visibility = Visibility.Hidden;
            BtnSpiritTendrils.Visibility = Visibility.Hidden;
            BtnVitaTendrils.Visibility = Visibility.Hidden;
            #endregion
        }

        private void WeatherMain_Click(object sender, RoutedEventArgs e)
        {
            #region Weather
            BtnPyroWeather.Visibility = Visibility.Visible;
            BtnAquaWeather.Visibility = Visibility.Visible;
            BtnElectroWeather.Visibility = Visibility.Visible;
            BtnVephyrWeather.Visibility = Visibility.Visible;
            #endregion
            #region Hide
            BtnTerraSpikes.Visibility = Visibility.Hidden;
            BtnNoxiousSpikes.Visibility = Visibility.Hidden;
            BtnGlacialSpikes.Visibility = Visibility.Hidden;
            BtnMetalSpikes.Visibility = Visibility.Hidden;
            BtnFloraTendrils.Visibility = Visibility.Hidden;
            BtnLumenTendrils.Visibility = Visibility.Hidden;
            BtnSpiritTendrils.Visibility = Visibility.Hidden;
            BtnVitaTendrils.Visibility = Visibility.Hidden;
            #endregion
        }

        private void TendrilsMain_Click(object sender, RoutedEventArgs e)
        {
            #region Tendrils
            BtnFloraTendrils.Visibility = Visibility.Visible;
            BtnLumenTendrils.Visibility = Visibility.Visible;
            BtnSpiritTendrils.Visibility = Visibility.Visible;
            BtnVitaTendrils.Visibility = Visibility.Visible;
            #endregion
            #region Hide
            BtnTerraSpikes.Visibility = Visibility.Hidden;
            BtnNoxiousSpikes.Visibility = Visibility.Hidden;
            BtnGlacialSpikes.Visibility = Visibility.Hidden;
            BtnMetalSpikes.Visibility = Visibility.Hidden;
            BtnPyroWeather.Visibility = Visibility.Hidden;
            BtnAquaWeather.Visibility = Visibility.Hidden;
            BtnElectroWeather.Visibility = Visibility.Hidden;
            BtnVephyrWeather.Visibility = Visibility.Hidden;
            #endregion
        }
        #region TendrilBtns
        private void BtnFloraTendrils_Click(object sender, RoutedEventArgs e)
        {
            if (BtnFloraTendrils.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Thorn Roots\n\nCan be applied to 1 grid piece in battle.\nTraps an Arviva that enteres the grid piece for 3 turns.\nDeals 1/16th Max HP damage and deals the user's team for equal hp.";
        }

        private void BtnLumenTendrils_Click(object sender, RoutedEventArgs e)
        {
            if (BtnLumenTendrils.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Light Roots\n\nCan be applied to 1 grid piece in battle.\nTraps an Arviva that enteres the grid piece for 2 turns.\nLower's the targets Magic Defense by 1 stage for each turn, raising the user's Magic Defense in turn.";
        }

        private void BtnSpiritTendrils_Click(object sender, RoutedEventArgs e)
        {
            if (BtnSpiritTendrils.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Grasping Souls\n\nCan be applied to 1 grid piece in battle.\nTraps an Arviva that enteres the grid piece for 2 turns.\nLower's the targets Magic Attack by 1 stage for each turn, raising the user's Magic Attack in turn.\nLumen Arviva are immune.";
        }

        private void BtnVitaTendrils_Click(object sender, RoutedEventArgs e)
        {
            if (BtnVitaTendrils.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Mycelium\n\nCan be applied to 1 grid piece in battle.\nTraps an Arviva that enteres the grid piece for 3 turns.\nDeals 1/32th Max HP damage and deals the user's team for equal hp.\nIt also confuses the target.";
        }
        #endregion

        #region WeatherBtns
        private void BtnPyroWeather_Click(object sender, RoutedEventArgs e)
        {
            if (BtnPyroWeather.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Sunny Day\n\nAffects whole board.\nBoosts Pyro and Lumen moves by 1.5x.\nHalves Aqua move damage.";
        }

        private void BtnAquaWeather_Click(object sender, RoutedEventArgs e)
        {
            if (BtnAquaWeather.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Down Pour\n\nAffects whole board.\nBoots Aqua and Spirit moves by 1.5x.\nHalves Pyro move damage.";
        }

        private void BtnVephyrWeather_Click(object sender, RoutedEventArgs e)
        {
            if (BtnVephyrWeather.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Strong Winds\n\nAffects whole board.\nBoosts Vephyr moves by 1.5x.\nRemoves hazards from the user's side and makes hazards unable to be set on their side until Strong Winds is over.";
        }

        private void BtnElectroWeather_Click(object sender, RoutedEventArgs e)
        {
            if (BtnElectroWeather.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Thunder Storm\n\nAffects whole board.\nBoosts Electro moves by 1.5x.\n Gives Electro, Metal, and Vephyr Arviva a +1 stat raise to their speed. This effect stacks.";
        }
        #endregion
        
        #region SpikeBtns
        private void BtnTerraSpikes_Click(object sender, RoutedEventArgs e)
        {
            if (BtnTerraSpikes.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Spikes \n\nCan be applied to 1 grid piece in battle. \nDoes 1/16th Max HP damage, type calculations apply.\nArviva with the 'Sky High' ability are immune.";
        }

        private void BtnMetalSpikes_Click(object sender, RoutedEventArgs e)
        {
            if (BtnMetalSpikes.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Steel Spikes \n\n Can be applied to 1 grid piece in battle. \nDoes 1/16th Max HP damage, type calculations apply.\nElectro Arviva are pulled to the grid piece the hazard is present in.\nArviva with the 'Sky High' ability are immune.";
        }

        private void BtnGlacialSpikes_Click(object sender, RoutedEventArgs e)
        {
            if (BtnGlacialSpikes.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Ice Cubes \n\n Can be applied to 1 grid piece in battle. \nDoes 1/32th Max HP damage to arviva weak to the Glacial type. \nLowers Speed and Accuracy by 1 stage each time the grid is entered.\nGlacial Types and Arviva with the 'Sky High' ability are immune.";
        }

        private void BtnNoxiousSpikes_Click(object sender, RoutedEventArgs e)
        {
            if (BtnNoxiousSpikes.Background is ImageBrush brush && brush.ImageSource != null)
            {
                ImgHazard.Source = brush.ImageSource;
            }
            TxBlHazards.Text = "Toxic Debris\n\n Can be applied to 1 grid piece in battle.\nDoes 1/32th Max HP damage, and then applies the Poison Status effect.\nNoxious types heal 1/32th Max HP damage each turn while in a grid piece with Toxic Debris.\nMetal types and Arviva with the 'Sky High' ability are immune.";
        }
        #endregion
        #endregion















        ////foreach attacker type picked, calculate how much damage they do to each type in the type chart
        //foreach (var attackerType in typeChart.Keys)
        //{
        //    var rules = typeChart[attackerType];
        //    float multiplier = 1.0f;
        //    //for loop of it attacking every type


        //    //for (int i = 0; i < TypeNames.Length; i++)
        //    //{

        //    //    multiplier *= DefenderMultiplier(rules,i, 0);
        //    //}
        //    string result = $"{TypeName(attackerType)}: x{multiplier:F2}";
        //    TxtOffense.Items.Add(result);
        //}

    }
}