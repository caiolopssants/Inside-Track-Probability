using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections;
using System.Reflection;
using System.IO;
namespace InsideTrackProbability
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string competitorsDataFile = $@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\data_competitors";

        readonly string competitionsDataFile = $@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\data_competitions";

        int CompetitionsCount = 0;

        Dictionary<string, DataGridColumn> DataGridDict;

        ObservableCollection<Competitor> Competitors { get; set; }      
        
        ObservableCollection<Competition> Competitions { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            #region Setting Data Grid
            comboBox_1.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillWin());
            comboBox_2.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillWin());
            comboBox_3.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillWin());
            comboBox_4.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillWin());
            comboBox_5.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillWin());
            comboBox_6.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillWin());

            comboBox_1.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillLose());
            comboBox_2.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillLose());
            comboBox_3.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillLose());
            comboBox_4.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillLose());
            comboBox_5.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillLose());
            comboBox_6.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) => WhoWillLose());


            //MinHeight = Height;
            //MinWidth = Width;

            DataGridDict = new Dictionary<string, DataGridColumn>()
            {
                {nameof(Competitor.Name) ,new DataGridTextColumn(){ Header = "Name", Binding = new Binding(nameof(Competitor.Name)){ Mode = BindingMode.OneWay} } },
                {nameof(Competitor.Competitions) ,new DataGridTextColumn(){ Header = "Competitions", Binding = new Binding(nameof(Competitor.Competitions)){ Mode = BindingMode.OneWay} } },
                {nameof(Competitor.Wins) ,new DataGridTextColumn(){ Header = "Victories", Binding = new Binding(nameof(Competitor.Wins)){ Mode = BindingMode.OneWay} } },
                {nameof(Competitor.WinProbability) ,new DataGridTextColumn(){ Header = "Probability", Binding = new Binding(nameof(Competitor.WinProbability)){ Mode = BindingMode.OneWay} } },
                {nameof(Competitor.Loses) ,new DataGridTextColumn(){ Header = "Defeats", Binding = new Binding(nameof(Competitor.Loses)){ Mode = BindingMode.OneWay} } },
                {nameof(Competitor.LoseProbability) ,new DataGridTextColumn(){ Header = "Probability", Binding = new Binding(nameof(Competitor.LoseProbability)){ Mode = BindingMode.OneWay} } },
            };

            foreach (KeyValuePair<string, DataGridColumn> valuePair in DataGridDict)
                dataGrid.Columns.Add(valuePair.Value);

            Competitors = new ObservableCollection<Competitor>()
            {
                new Competitor("1/1"),
                new Competitor("2/1"),
                new Competitor("3/1"),
                new Competitor("4/1"),
                new Competitor("5/1"),
                new Competitor("6/1"),
                new Competitor("7/1"),
                new Competitor("8/1"),
                new Competitor("9/1"),
                new Competitor("10/1"),
                new Competitor("11/1"),
                new Competitor("12/1"),
                new Competitor("13/1"),
                new Competitor("14/1"),
                new Competitor("15/1"),
                new Competitor("16/1"),
                new Competitor("17/1"),
                new Competitor("18/1"),
                new Competitor("19/1"),
                new Competitor("20/1"),
                new Competitor("21/1"),
                new Competitor("22/1"),
                new Competitor("23/1"),
                new Competitor("24/1"),
                new Competitor("25/1"),
                new Competitor("26/1"),
                new Competitor("27/1"),
                new Competitor("28/1"),
                new Competitor("29/1"),
                new Competitor("30/1"),
            };

            Competitions = new ObservableCollection<Competition>();

            dataGrid.CanUserReorderColumns = false;

            dataGrid.AutoGenerateColumns = false;

            dataGrid.ItemsSource = Competitors;

            dataGrid.CanUserDeleteRows = false;
            #endregion

            LoadDataGridCompetitorsData();

            LoadDataGridCompetitionsData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinnerSpecificGroup(competitor => competitor.Name == (string)((ListBoxItem)comboBox_1.SelectedItem).Content);

            Competitors[comboBox_1.SelectedIndex].Winner();
            Competitors.Add(Competitors[comboBox_1.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_1.SelectedIndex);
            Competitors.RemoveAt(comboBox_1.SelectedIndex + 1);

            Competitors[comboBox_2.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_2.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_2.SelectedIndex);
            Competitors.RemoveAt(comboBox_2.SelectedIndex + 1);

            Competitors[comboBox_3.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_3.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_3.SelectedIndex);
            Competitors.RemoveAt(comboBox_3.SelectedIndex + 1);

            Competitors[comboBox_4.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_4.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_4.SelectedIndex);
            Competitors.RemoveAt(comboBox_4.SelectedIndex + 1);

            Competitors[comboBox_5.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_5.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_5.SelectedIndex);
            Competitors.RemoveAt(comboBox_5.SelectedIndex + 1);

            Competitors[comboBox_6.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_6.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_6.SelectedIndex);
            Competitors.RemoveAt(comboBox_6.SelectedIndex + 1);

            CompetitionsCount++;
            WhoWillWinSpecific();
            WhoWillWin();
            WhoWillLose();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WinnerSpecificGroup(competitor => competitor.Name == (string)((ListBoxItem)comboBox_2.SelectedItem).Content);

            Competitors[comboBox_1.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_1.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_1.SelectedIndex);
            Competitors.RemoveAt(comboBox_1.SelectedIndex + 1);

            Competitors[comboBox_2.SelectedIndex].Winner();
            Competitors.Add(Competitors[comboBox_2.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_2.SelectedIndex);
            Competitors.RemoveAt(comboBox_2.SelectedIndex + 1);

            Competitors[comboBox_3.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_3.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_3.SelectedIndex);
            Competitors.RemoveAt(comboBox_3.SelectedIndex + 1);

            Competitors[comboBox_4.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_4.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_4.SelectedIndex);
            Competitors.RemoveAt(comboBox_4.SelectedIndex + 1);

            Competitors[comboBox_5.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_5.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_5.SelectedIndex);
            Competitors.RemoveAt(comboBox_5.SelectedIndex + 1);

            Competitors[comboBox_6.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_6.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_6.SelectedIndex);
            Competitors.RemoveAt(comboBox_6.SelectedIndex + 1);

            CompetitionsCount++;
            WhoWillWinSpecific();
            WhoWillWin();
            WhoWillLose();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WinnerSpecificGroup(competitor => competitor.Name == (string)((ListBoxItem)comboBox_3.SelectedItem).Content);

            Competitors[comboBox_1.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_1.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_1.SelectedIndex);
            Competitors.RemoveAt(comboBox_1.SelectedIndex + 1);

            Competitors[comboBox_2.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_2.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_2.SelectedIndex);
            Competitors.RemoveAt(comboBox_2.SelectedIndex + 1);

            Competitors[comboBox_3.SelectedIndex].Winner();
            Competitors.Add(Competitors[comboBox_3.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_3.SelectedIndex);
            Competitors.RemoveAt(comboBox_3.SelectedIndex + 1);

            Competitors[comboBox_4.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_4.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_4.SelectedIndex);
            Competitors.RemoveAt(comboBox_4.SelectedIndex + 1);

            Competitors[comboBox_5.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_5.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_5.SelectedIndex);
            Competitors.RemoveAt(comboBox_5.SelectedIndex + 1);

            Competitors[comboBox_6.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_6.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_6.SelectedIndex);
            Competitors.RemoveAt(comboBox_6.SelectedIndex + 1);

            CompetitionsCount++;
            WhoWillWinSpecific();
            WhoWillWin();
            WhoWillLose();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WinnerSpecificGroup(competitor => competitor.Name == (string)((ListBoxItem)comboBox_4.SelectedItem).Content);

            Competitors[comboBox_1.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_1.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_1.SelectedIndex);
            Competitors.RemoveAt(comboBox_1.SelectedIndex + 1);

            Competitors[comboBox_2.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_2.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_2.SelectedIndex);
            Competitors.RemoveAt(comboBox_2.SelectedIndex + 1);

            Competitors[comboBox_3.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_3.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_3.SelectedIndex);
            Competitors.RemoveAt(comboBox_3.SelectedIndex + 1);

            Competitors[comboBox_4.SelectedIndex].Winner();
            Competitors.Add(Competitors[comboBox_4.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_4.SelectedIndex);
            Competitors.RemoveAt(comboBox_4.SelectedIndex + 1);

            Competitors[comboBox_5.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_5.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_5.SelectedIndex);
            Competitors.RemoveAt(comboBox_5.SelectedIndex + 1);

            Competitors[comboBox_6.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_6.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_6.SelectedIndex);
            Competitors.RemoveAt(comboBox_6.SelectedIndex + 1);

            CompetitionsCount++;
            WhoWillWinSpecific();
            WhoWillWin();
            WhoWillLose();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            WinnerSpecificGroup(competitor => competitor.Name == (string)((ListBoxItem)comboBox_5.SelectedItem).Content);

            Competitors[comboBox_1.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_1.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_1.SelectedIndex);
            Competitors.RemoveAt(comboBox_1.SelectedIndex + 1);

            Competitors[comboBox_2.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_2.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_2.SelectedIndex);
            Competitors.RemoveAt(comboBox_2.SelectedIndex + 1);

            Competitors[comboBox_3.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_3.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_3.SelectedIndex);
            Competitors.RemoveAt(comboBox_3.SelectedIndex + 1);

            Competitors[comboBox_4.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_4.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_4.SelectedIndex);
            Competitors.RemoveAt(comboBox_4.SelectedIndex + 1);

            Competitors[comboBox_5.SelectedIndex].Winner();
            Competitors.Add(Competitors[comboBox_5.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_5.SelectedIndex);
            Competitors.RemoveAt(comboBox_5.SelectedIndex + 1);

            Competitors[comboBox_6.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_6.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_6.SelectedIndex);
            Competitors.RemoveAt(comboBox_6.SelectedIndex + 1);

            CompetitionsCount++;
            WhoWillWinSpecific();
            WhoWillWin();
            WhoWillLose();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WinnerSpecificGroup(competitor => competitor.Name == (string)((ListBoxItem)comboBox_6.SelectedItem).Content);

            Competitors[comboBox_1.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_1.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_1.SelectedIndex);
            Competitors.RemoveAt(comboBox_1.SelectedIndex + 1);

            Competitors[comboBox_2.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_2.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_2.SelectedIndex);
            Competitors.RemoveAt(comboBox_2.SelectedIndex + 1);

            Competitors[comboBox_3.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_3.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_3.SelectedIndex);
            Competitors.RemoveAt(comboBox_3.SelectedIndex + 1);

            Competitors[comboBox_4.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_4.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_4.SelectedIndex);
            Competitors.RemoveAt(comboBox_4.SelectedIndex + 1);

            Competitors[comboBox_5.SelectedIndex].Loser();
            Competitors.Add(Competitors[comboBox_5.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_5.SelectedIndex);
            Competitors.RemoveAt(comboBox_5.SelectedIndex + 1);

            Competitors[comboBox_6.SelectedIndex].Winner();
            Competitors.Add(Competitors[comboBox_6.SelectedIndex]);
            Competitors.Move(Competitors.Count - 1, comboBox_6.SelectedIndex);
            Competitors.RemoveAt(comboBox_6.SelectedIndex + 1);

            CompetitionsCount++;
            WhoWillWinSpecific();
            WhoWillWin();
            WhoWillLose();
        }
        
        private void LoadDataGridCompetitorsData()
        {
            if (!File.Exists(competitorsDataFile)) return;
            foreach (string line in File.ReadLines(competitorsDataFile))
            {
                if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line)) continue;
                string[] terms = line.Split(';');
                string name = terms[0];
                uint wins = Convert.ToUInt32(terms[1]);
                uint loses = Convert.ToUInt32(terms[2]);
                Competitor competitor = Competitors.First(comp => comp.Name == terms[0]);
                competitor.SetWins(wins);
                competitor.SetLoses(loses);
            }
        }

        private void LoadDataGridCompetitionsData()
        {
            if (!File.Exists(competitionsDataFile)) return;
            foreach (string line in File.ReadLines(competitionsDataFile))
            {
                if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line)) continue;
                string[] terms = line.Split(';');
                for(int i=0; i<terms.Length; i+=3)
                {
                    string name = terms[i];
                    uint wins = Convert.ToUInt32(terms[i+1]);
                    uint loses = Convert.ToUInt32(terms[i+2]);
                    Competitor competitor = Competitors.First(comp => comp.Name == name);
                    competitor.SetWins(wins);
                    competitor.SetLoses(loses);
                }                
            }
        }

        private void SaveDataGridCompetitorsData()
        {
            List<string> lines = new List<string>();
            foreach (Competitor competitor in Competitors)            
                lines.Add($"{competitor.Name};{competitor.Wins};{competitor.Loses}");
            
            File.WriteAllLines(competitorsDataFile, lines);
        }

        private void SaveDataGridCompetitionsData()
        {
            List<string> lines = new List<string>();
            foreach (Competition competition in Competitions)
            {
                string line = string.Empty;
                foreach (Competitor competitor in competition)                
                    line += 
                        string.IsNullOrEmpty(line) ? 
                        $"{competitor.Name};{competitor.Wins};{competitor.Loses}": 
                        $";{competitor.Name};{competitor.Wins};{competitor.Loses}";
                lines.Add(line);
            }
            File.WriteAllLines(competitionsDataFile, lines);
        }

        private void WinnerSpecificGroup(Func<Competitor,bool> func)
        {
            List<ComboBox> comboBoxes = new List<ComboBox>()
            {
                comboBox_1,
                comboBox_2,
                comboBox_3,
                comboBox_4,
                comboBox_5,
                comboBox_6,
            };            
            if (Competitions.FirstOrDefault(competition_ => competition_.All(competitor_ => comboBoxes.Any(comboBox => (string)((ListBoxItem)comboBox.SelectedItem).Content == competitor_.Name))) is Competition competition)            
                competition.Winner(func);            
            else
            {
                competition = new Competition();
                foreach (ComboBox comboBox in comboBoxes)
                    competition.Add(new Competitor((string)((ListBoxItem)comboBox.SelectedItem).Content));
                
                competition.Winner(func);
                Competitions.Add(competition);
            }
        }

        private void WhoWillWinSpecific()
        {
            whoWillWinSpecific_ListBox.Items.Clear();
            List<ComboBox> comboBoxes = new List<ComboBox>()
            {
                comboBox_1,
                comboBox_2,
                comboBox_3,
                comboBox_4,
                comboBox_5,
                comboBox_6,
            };
            if (Competitions.FirstOrDefault(competitions_ => competitions_.All(competitor_ => comboBoxes.Any(comboBox => (string)((ListBoxItem)comboBox.SelectedItem).Content == competitor_.Name))) is Competition competition)
            {
                Competitor competitor = competition.WhoWillWin();
                whoWillWinSpecific_ListBox.Items.Add($@"{competitor.Name}; {competitor.Competitions}; {competitor.Wins}; {competitor.WinProbability}; {competitor.Loses}; {competitor.LoseProbability}");
            }
            else
            {
                return;
            }
        }

        private void WhoWillWin()
        {
            whoWillWin_ListBox.Items.Clear();
            List<ComboBox> comboBoxes = new List<ComboBox>()
            {
                comboBox_1,
                comboBox_2,
                comboBox_3,
                comboBox_4,
                comboBox_5,
                comboBox_6,
            };
            decimal[] winProbabilities = new decimal[6];
            for (int i = 0; i < 6; i++)
                winProbabilities[i] = Competitors[comboBoxes[i].SelectedIndex].WinProbability;

            foreach (Competitor competitor in Competitors.Where(comp => comp.WinProbability == winProbabilities.Max()))
                whoWillWin_ListBox.Items.Add($@"{competitor.Name}");
        }

        private void WhoWillLose()
        {
            whoWillLose_ListBox.Items.Clear();
            List<ComboBox> comboBoxes = new List<ComboBox>()
            {
                comboBox_1,
                comboBox_2,
                comboBox_3,
                comboBox_4,
                comboBox_5,
                comboBox_6,
            };
            decimal[] loseProbabilities = new decimal[6];
            for (int i = 0; i < 6; i++)
                loseProbabilities[i] = Competitors[comboBoxes[i].SelectedIndex].LoseProbability;

            foreach (Competitor competitor in Competitors.Where(comp => comp.LoseProbability == loseProbabilities.Max()))
                whoWillLose_ListBox.Items.Add($@"{competitor.Name}");
        }

        public class Competitor
        {
            public string Name { get; private set; }
            public decimal Wins { get; private set; }
            public decimal Loses { get; private set; }            
            public decimal WinProbability { get { try { return (Wins / (Wins + Loses)) * 100; } catch { return 0; } } }
            public decimal LoseProbability { get { try { return (Loses / (Wins + Loses)) * 100; } catch { return 0; } } }
            public decimal Competitions { get {return Wins + Loses; } }
            public Competitor(string name)
            {
                Name = name;
                Wins = 0;
                Loses = 0;
            }
            public Competitor(string name, int wins, int loses)
            {
                Name = name;
                Wins = wins;
                Loses = loses;
            }
            public void Winner() => Wins++;
            public void Loser() => Loses++;
            public void SetWins(uint wins) => Wins = wins;
            public void SetLoses(uint loses) => Loses = loses;
        }

        public class Competition : IList<Competitor>
        {
            public Competitor this[int index] { get => Competitors[index]; set => Competitors[index] = value; }

            public int Count => Competitors.Count;

            public bool IsReadOnly => false;

            List<Competitor> Competitors { get; set; }

            /// <summary>
            /// Define victory for the first competitor founded
            /// </summary>
            /// <param name="func"></param>
            public void Winner(Func<Competitor,bool> func)
            {
                if (!Competitors.Any(func))
                    return;

                bool winner = false;
                foreach (Competitor competitor in Competitors)
                {
                    if (!winner && func(competitor))
                    {
                        winner = true;
                        competitor.Winner();

                    }
                    else
                        competitor.Loser();
                }
            }

            public Competitor WhoWillWin()
            {
                decimal[] winProbabilities = new decimal[Competitors.Count];
                for (int i = 0; i < Competitors.Count; i++)
                    winProbabilities[i] = Competitors[i].WinProbability;
                return Competitors.First(comp => comp.WinProbability == winProbabilities.Max());
            }

            public Competitor WhoWillLose()
            {
                decimal[] loseProbabilities = new decimal[Competitors.Count];
                for (int i = 0; i < Competitors.Count; i++)
                    loseProbabilities[i] = Competitors[i].LoseProbability;
                return Competitors.First(comp => comp.LoseProbability == loseProbabilities.Max());
            }

            public Competition()
            {
                Competitors = new List<Competitor>();
            }

            public void Add(Competitor item)
            {
                Competitors.Add(item);
            }

            public void Add(string competitorName)
            {
                Competitors.Add(new Competitor(competitorName));
            }

            public void Add(string competitorName, int competitorWins, int competitorLoses)
            {
                Competitors.Add(new Competitor(competitorName, competitorWins, competitorLoses));
            }

            public void AddRange(IEnumerable<Competitor> itens)
            {
                Competitors.AddRange(itens);
            }

            public void Clear()
            {
                Competitors.Clear();
            }

            public bool Contains(Competitor item)
            {
                return Competitors.Contains(item);
            }

            public void CopyTo(Competitor[] array, int arrayIndex)
            {
                Competitors.CopyTo(array, arrayIndex);
            }

            public bool Any (Func<Competitor, bool> func)
            {
                return Competitors.Any(func);
            }

            public bool All(Func<Competitor, bool> func)
            {
                return Competitors.All(func);
            }

            public IEnumerator<Competitor> GetEnumerator()
            {
                return Competitors.GetEnumerator();
            }

            public int IndexOf(Competitor item)
            {
                return Competitors.IndexOf(item);
            }

            public void Insert(int index, Competitor item)
            {
                Competitors.Insert(index, item);
            }

            public bool Remove(Competitor item)
            {
                return Competitors.Remove(item);
            }

            public void RemoveAt(int index)
            {
                Competitors.RemoveAt(index);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return Competitors.GetEnumerator();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveDataGridCompetitionsData();
            SaveDataGridCompetitorsData();
        }
    }
}
