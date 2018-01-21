﻿using System;
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
using System.Windows.Shapes;
using LetsBattleBookCase;
using System.IO;
using System.Windows.Threading;
namespace Lets_battle
{
    public partial class MainWindow : Window
    {
#region variables
        public string name;
        public int classP;
        public int h;
        public int dam;
        public int def;
        public Help help = new Help();
        #endregion
        public MainWindow()
        {
            InitializeComponent(); //Must be on TOP
            Gb_create_player.Visibility = Visibility.Collapsed;
            Gb_load.Visibility = Visibility.Collapsed;
            Gb_author.Visibility = Visibility.Collapsed;
            // Gb_settings.Visibility = Visibility.Collapsed;
        }
        private void B_click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button) == B_start)
            {
                var letsBattleWindow = new LetsBattle();
                // Hide();
                Close();
                letsBattleWindow.ShowDialog();
            }
#region player
            else if ((sender as Button) == B_create_player)
            {
                // Gb_settings.Visibility = Visibility.Collapsed;
                Gb_load.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Visible;
                Width = 638;
                /*
                Gb_load.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Width = 296.305;
                */
            }
            else if ((sender as Button) == B_read_input)
            {
                name = tb_inputName.Text;
                classP = cb_Class.SelectedIndex;
                h = Convert.ToInt32(tb_Health.Text);
                dam = Convert.ToInt32(tb_Damage.Text);
                def = Convert.ToInt32(tb_Defense.Text);
                tb_inputName.Text = null;
                cb_Class.SelectedValue = -1;
                tb_Health.Text = "";
                tb_Damage.Text = "";
                tb_Defense.Text = "";
                Width = 296.305;
                Gb_load.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
            }
            else if ((sender as Button) == B_random_stats)
            {
                classP = cb_Class.SelectedIndex;
                Dice dice = new Dice();
                int h = dice.DiceRoll(20);
                int da = dice.DiceRoll(20);
                int de = dice.DiceRoll(20);
                h += dice.DiceRoll(5);
                da += dice.DiceRoll(5);
                de += dice.DiceRoll(5);
#region IDontKnow
                tb_Health.Text = Convert.ToString(h);
                tb_Damage.Text = Convert.ToString(da);
                tb_Defense.Text = Convert.ToString(de);
                int tbH = Convert.ToInt16(tb_Health.Text);
                int tbDa = Convert.ToInt16(tb_Damage.Text);
                int tbDe = Convert.ToInt16(tb_Defense.Text);
                #endregion
                switch (classP)
                {
                    case 0:
                        if (tbH < 7 || tbDa < 10 || tbDe < 5)
                        {
                            tb_Health.Text = 7.ToString();
                            tb_Damage.Text = 10.ToString();
                            tb_Defense.Text = 5.ToString();
                        }
                        break;
                    case 1:
                        if (tbH < 10 || tbDa < 8 || tbDe < 9)
                        {
                            tb_Health.Text = 10.ToString();
                            tb_Damage.Text = 8.ToString();
                            tb_Defense.Text = 9.ToString();
                        }
                        break;
                }
            }
            #endregion
#region load/save_player
            else if ((sender as Button) == B_save)
            {
                StreamWriter sw = new StreamWriter("save.txt", append: true);
                //sw.Write(DateTime.Now + Environment.NewLine + name + Environment.NewLine + classP + Environment.NewLine + h + Environment.NewLine + dam + Environment.NewLine + def + Environment.NewLine);
                sw.Write(DateTime.Now + ", " + name + ", " + classP + ", " + h + ", " + dam + ", " + def + Environment.NewLine);
                MessageBox.Show("player " + name + " was saved and you can load him anytime you want in load section");
                sw.Close();
            }
            else if ((sender as Button) == B_load)
            {
                Tb_saved_characters.Items.Clear();
                Width = 638;
                Gb_load.Visibility = Visibility.Visible;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                int whereIsNextSave = 5;
                int newLineCounter = 0;
                var sr = new StreamReader("save.txt");
                string[] line = new string[1];
                while ((line[0] = sr.ReadLine()) != null)
                {
                    Tb_saved_characters.Items.Add(line[0] + ",");
                    if (newLineCounter == whereIsNextSave)
                    {
                        Tb_saved_characters.Items.Add(Environment.NewLine);
                        whereIsNextSave += 6;
                    }
                    newLineCounter++;
                }
                sr.Close();
            }
            else if ((sender as Button) == B_load_player)
            {
                B_load.IsEnabled = true;
                Gb_load.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                Width = 296.305;
                string[] arr = new string[10];
                for (int i = 0; i < Tb_saved_characters.SelectedItems.Count; i++)
                {
                    string record = Tb_saved_characters.Items[Tb_saved_characters.SelectedIndex].ToString();
                    arr = record.Split(',');
                    //MessageBox.Show(record);
                }
                  
               help.Name = arr[1];
               help.ClassP = Convert.ToInt32(arr[2]);
               help.Health = Convert.ToInt32(arr[3]);
               help.Damage = Convert.ToInt32(arr[4]);
               help.Defense = Convert.ToInt32(arr[5]);
                /*
               String text = help.Name + "; " + help.ClassP + "; " + help.Health + "; " + help.Damage + "; " + help.Defense;
               MessageBox.Show(text);
                */
               /*
                name = arr[1];
                classP = Convert.ToInt32(arr[2]);
                h = Convert.ToInt32(arr[3]);
                dam = Convert.ToInt32(arr[4]);
                def = Convert.ToInt32(arr[5]);
                String text = name + "; " + classP + "; " + h + "; " + dam + "; " + def;
                MessageBox.Show(text);
                */
            }
            else if ((sender as Button) == B_delete_player)
            {
                StreamWriter sw = new StreamWriter("save.txt");
                Tb_saved_characters.Items.RemoveAt(Tb_saved_characters.SelectedIndex);
                string record = "";
                for (int i = 0; i < Tb_saved_characters.Items.Count; i++)  { record += Tb_saved_characters.Items[i].ToString() + Environment.NewLine; }
                sw.Write(record);
                sw.Close();
            }
            #endregion
#region author
            else if ((sender as Button) == B_author)
            {
                Gb_load.Visibility = Visibility.Visible;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                Gb_load.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Visible;
                Width = 638;
                /*
                Width = 296.305;
                Gb_load.Visibility = Visibility.Visible;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                Gb_load.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                */
            }
            else if ((sender as Button) == B_author_close)
            {
                Gb_load.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                Width = 296.305;
            }
            #endregion
            else if ((sender as Button) == B_settings)
            {
                Width = 638;
                // Gb_settings.Visibility = Visibility.Visible;
                Gb_load.Visibility = Visibility.Collapsed;
                Gb_create_player.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                Gb_author.Visibility = Visibility.Collapsed;
                //Width = 296.305;
            }
            else if ((sender as Button) == B_close) { Close(); }
        }
    }
}