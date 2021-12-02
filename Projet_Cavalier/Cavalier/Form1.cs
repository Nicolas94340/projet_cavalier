using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cavalier
{
    public partial class Form1 : Form
    {
        private Button[,] grille;
        private int[] liste_nombre;
        private Button joueur;
        private int nombre_coup = 0;
        private int [] depi = {2,1,-1,-2,-2,-1,1,2};
        private int[] depj = {1,2,2,1,-1,-2,-2,-1};
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random r = new Random();
            int alea;
            this.grille = new Button[12, 12];
            this.liste_nombre = new int[64];
            for (int i = 0; i < liste_nombre.Length; i++)
            {
                liste_nombre[i] = i + 1;
            }
            for(int l = 0; l<8; l++)
            {
                for(int c = 0; c<8; c++)
                {
                    alea = r.Next(liste_nombre.Length); 
                    Button b;
                    b = new Button();
                    if((c-l) % 2 == 0)
                    {
                        b.BackColor = Color.Gray;
                    }
                    else 
                    {
                        b.BackColor = Color.White;
                    }
                    b.Location = new Point(l * 50, c * 50);
                    b.Size = new Size(50, 50);
                    b.Text = Convert.ToString(liste_nombre[alea]);
                    b.Click += new EventHandler(this.traitementDuClic);
                    liste_nombre = liste_nombre.Where(val => val != liste_nombre[alea]).ToArray();
                    this.Controls.Add(b);
                    grille[l, c] = b;
                     
                }
            }
            label1.Text.ToUpper();

        }

        private void traitementDuClic(object sender, EventArgs e)
        {
            int pos_l=13;
            int pos_c=20;
            if(sender is Button)
            {
                Button b = sender as Button;
                b.Image = Image.FromFile("C:\\Users\\jeremy\\Desktop\\C#\\Projet_Cavalier\\Projet_Cavalier\\cavalier1.jpg");
                b.ImageAlign = ContentAlignment.MiddleCenter;
                joueur = b;
                joueur.Text = "ok";
                nombre_coup++;
                for (int l = 0; l < 8; l++)
                {
                    for (int c = 0; c < 8; c++)
                    {
                       if(grille[l,c] == joueur)
                        {
                            pos_c = c;
                            pos_l = l;
                            grille[l, c].Enabled = false;

                        }
                        if ((c - l) % 2 == 0)
                        { 
                            grille[c,l].BackColor = Color.Gray;

                        }
                        else
                        {
                            grille[c, l].BackColor = Color.White;

                        }
                    }
                }

            }

            if(nombre_coup > 0)
            {
                 
                for (int l = 0; l < 8; l++)
                {
                    for (int c = 0; c < 8; c++)
                    {
                        grille[l, c].Enabled = false;
                    }
                }

                for (int l = 0; l<8; l++)
                {
                    if(pos_l + depi[l]>= 0 && pos_l + depi[l] < 8 && pos_c + depj[l]  < 8 && pos_c + depj[l] >= 0)
                    {  
                        if (grille[pos_l + depi[l], pos_c + depj[l]].Text != "ok")
                        {
                            grille[pos_l + depi[l], pos_c + depj[l]].BackColor = Color.Red;
                            grille[pos_l + depi[l], pos_c + depj[l]].Enabled = true;
                        }
                            
                        
                    } 
                }
                joueur.Enabled = false; 
                if (AnalyseGrille() == 1)
                {
                    if (MessageBox.Show("BRAVOO Vous avez gagné !!!!!!" + "\n" + "Voulez vous rejouer ?", "REJOUER ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Close();
                    }
                    else 
                    {
                        Form1 f2 = new Form1();
                        f2.Show();
                    }
                }
                else if (AnalyseGrille() == -1)
                {
                    if (MessageBox.Show("Vous avez Perdu :( !!!!!!" + "\n" + "Voulez vous rejouer ?", "REJOUER ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.Close();
                    }
                    else
                    {
                        Form1 f2 = new Form1();
                        f2.Show();

                    }
                }


            }

            if(nombre_coup > 1)
            {
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int j = r.Next(9);
            int i = r.Next(9);
            grille[i,j].Image = Image.FromFile("C:\\Users\\jeremy\\Desktop\\C#\\Projet_Cavalier\\Projet_Cavalier\\cavalier1.jpg");
            grille[i,j].ImageAlign = ContentAlignment.MiddleCenter;
            joueur = grille[i, j];

        }

        private int AnalyseGrille()
        {
            int compteur_ok = 0;
            int compteur_rouge = 0;
            for (int l = 0; l < 8; l++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (grille[l, c].Text == "ok")
                    {
                        compteur_ok++;
                    }
                    if(grille[l, c].BackColor == Color.Red)
                    {
                        compteur_rouge ++;
                    }
                }
            }
            if (compteur_ok == 64)
            {
                return 1;
            }
            else if (compteur_ok != 64 && compteur_rouge == 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

       
    }
}
