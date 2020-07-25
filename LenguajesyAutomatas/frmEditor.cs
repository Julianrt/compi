using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LenguajesyAutomatas
{
    public partial class frmEditor : Form
    {
        int contadorPaginascreadas = 1;
        public frmEditor()
        {
            InitializeComponent();
        }

        private void frmEditor_Load(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            contadorPaginascreadas ++;

            TabPage TP = new TabPage();
            tabControl1.TabPages.Add(TP);
            TP.BackColor = Color.White;
            EditorTexto et = new EditorTexto();
            et.Name = "editorTexto"+contadorPaginascreadas;
            TP.Controls.Add(et);
            TP.Name = "tabPage"+contadorPaginascreadas;
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nombreTabpageseleccionada = tabControl1.SelectedTab.Name.ToString();
            string indiceparacontroleditort = nombreTabpageseleccionada.Substring(nombreTabpageseleccionada.Length - 1, 1);
            
            var controles = tabControl1.Controls.OfType<TabPage>().Where(x => x.Name.StartsWith(nombreTabpageseleccionada));

            foreach (var ctrl in controles)
            {
                var controleseditor = ctrl.Controls.OfType<EditorTexto>().Where(x => x.Name.StartsWith("editorTexto" + indiceparacontroleditort));
         
                foreach (var ctrleditor in controleseditor)
                {

                    ctrleditor.Guardar();

                }
            }
            
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nombreTabpageseleccionada = tabControl1.SelectedTab.Name.ToString();
            string indiceparacontroleditort = nombreTabpageseleccionada.Substring(nombreTabpageseleccionada.Length - 1, 1);

            var controles = tabControl1.Controls.OfType<TabPage>().Where(x => x.Name.StartsWith(nombreTabpageseleccionada));

            foreach (var ctrl in controles)
            {
                var controleseditor = ctrl.Controls.OfType<EditorTexto>().Where(x => x.Name.StartsWith("editorTexto" + indiceparacontroleditort));

                foreach (var ctrleditor in controleseditor)
                {
                    ctrleditor.Abir();

                }
            }

        }

        private void tsrEjecutarAnalizadorLexico_Click(object sender, EventArgs e)
        {
            string nombreTabpageseleccionada = tabControl1.SelectedTab.Name.ToString();
            string indiceparacontroleditort = nombreTabpageseleccionada.Substring(nombreTabpageseleccionada.Length - 1, 1);

            var controles = tabControl1.Controls.OfType<TabPage>().Where(x => x.Name.StartsWith(nombreTabpageseleccionada));

            foreach (var ctrl in controles)
            {
                var controleseditor = ctrl.Controls.OfType<EditorTexto>().Where(x => x.Name.StartsWith("editorTexto" + indiceparacontroleditort));

                foreach (var ctrleditor in controleseditor)
                {
                    string codigofuente = ctrleditor.Texto();
                    Lexico _lex = new Lexico();                    
                    if (codigofuente.Length > 0)
                    {
                        dgvListaTokens.DataSource = _lex.EjecutarLexico(codigofuente);
                    }
                    else
                    {
                        MessageBox.Show("Escribe algo primero :v");
                    }
                }
            }

        }

        private void tsrEjecutarAnalizadorSintactico_Click(object sender, EventArgs e)
        {
            string nombreTabpageseleccionada = tabControl1.SelectedTab.Name.ToString();
            string indiceparacontroleditort = nombreTabpageseleccionada.Substring(nombreTabpageseleccionada.Length - 1, 1);

            var controles = tabControl1.Controls.OfType<TabPage>().Where(x => x.Name.StartsWith(nombreTabpageseleccionada));

            foreach (var ctrl in controles)
            {
                var controleseditor = ctrl.Controls.OfType<EditorTexto>().Where(x => x.Name.StartsWith("editorTexto" + indiceparacontroleditort));

                foreach (var ctrleditor in controleseditor)
                {
                    string codigofuente = ctrleditor.Texto();
                    Lexico _lex = new Lexico();
                    Sintactico _sin = new Sintactico();
                    if (codigofuente.Length > 0)
                    {
                        List<Token> lista = new List<Token>();
                        List<Token> listaTablaSimbolo = new List<Token>();
                        lista = _lex.EjecutarLexico(codigofuente);
                        int[] Tokensparasintactico = new int[500];
                        string [] lexemaParaSintactico = new string[500];
                        int cantidaddetokens = 0, ubicacion = 0;
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (lista[i].token != -41 && lista[i].token != -39)
                            {
                                Tokensparasintactico[ubicacion] = lista[i].token;
                                lexemaParaSintactico[ubicacion] = lista[i].lexema;
                                listaTablaSimbolo.Add(lista[i]);
                                cantidaddetokens++;
                                ubicacion++;
                            }
                        }
                        Tokensparasintactico[cantidaddetokens] = -99;
                        //MessageBox.Show(Convert.ToString(Tokensparasintactico[0])+" "+ Convert.ToString(Tokensparasintactico[1]));

                        TablaSimbolos.TablaSimbolosClase = new Dictionary<string, NodoClase>();
                        _sin.EjecutarSintactico(Tokensparasintactico, lexemaParaSintactico, listaTablaSimbolo);
                        dgvTablaSimbolo.DataSource = TablaSimbolos.ObtenerListaClases();
                    }
                    else
                    {
                        dgvTablaSimbolo.DataSource = "";
                        MessageBox.Show("Escribe algo primero :v");
                    }
                }
            }

        }
    }
}
