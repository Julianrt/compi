using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LenguajesyAutomatas
{
	#region Listas enum
	public enum TipoDeDato
    {
        SinEspecificar,
        Entero,
        Decimal,
        Cadena,
        Error,
        Vacio
    }
    public enum TipoDeVariable
    {
        VariableLocal,
        Parametro,
        Atributo
    }
    public enum Estado
    {
        Insertado,
        Duplicado,
        NoExisteClase,
        DuplicadoAtributoConClase
    }
    public enum Alcance
    {
        Public,
        Private,
        Static,
        Protected,
        Sealed
    }

    public enum Regreso
    {
        INT,
        STRING,
        DOUBLE,
        CHAR,
        VOID
    }
	#endregion


	#region Nodos

	public class NodoClase
    {
        private string lexema;
        private NodoClase herencia;
        private int renglonDeclaracion;
        private int[] referencias;

        private Dictionary<string, NodoAtributo> tablaSimbolosAtributos = new Dictionary<string, NodoAtributo>();
        private Dictionary<string, NodoMetodo> tablaSimbolosMetodos = new Dictionary<string, NodoMetodo>();

        public string Lexema
        {
            get
            {
                return lexema;
            }

            set
            {
                lexema = value;
            }
        }
        public NodoClase Herencia
        {
            get
            {
                return herencia;
            }

            set
            {
                herencia = value;
            }
        }
        public int RenglonDeclaracion
        {
            get
            {
                return renglonDeclaracion;
            }

            set
            {
                renglonDeclaracion = value;
            }
        }
        public int[] Referencias
        {
            get
            {
                return referencias;
            }

            set
            {
                referencias = value;
            }
        }
        public Dictionary<string, NodoAtributo> TablaSimbolosAtributos
        {
            get
            {
                return tablaSimbolosAtributos;
            }

            set
            {
                tablaSimbolosAtributos = value;
            }
        }
        public Dictionary<string, NodoMetodo> TablaSimbolosMetodos
        {
            get
            {
                return tablaSimbolosMetodos;
            }

            set
            {
                tablaSimbolosMetodos = value;
            }
        }
    }
    public class NodoAtributo {
        public string lexema;
    }
    public class NodoMetodo {
        public string lexema;

        public int linea;
        public Alcance miAlcance;
        public Regreso miRegreso;
        public Dictionary<object, NodoParametro> TablaParametro = new Dictionary<object, NodoParametro>();
        public Dictionary<object, NodoVariable> TablaVariables = new Dictionary<object, NodoVariable>();
    }
    public class NodoParametro {
        public string lexema;
    }
    public class NodoVariable {
        public string lexema;
        public TipoDeDato miTipoDato;
        public string valor;
        public TipoDeVariable miTipoVariable;
        public int reglonDec;
        public int[] referencias;
        public int lineaDeclaracion;
    }

	#endregion

	public class TablaSimbolos
    {
        public NodoClase claseActual = new NodoClase();

        public NodoMetodo metodoActual = new NodoMetodo();

        private static Dictionary<string, NodoClase> tablaSimbolosClase = new Dictionary<string, NodoClase>();
		#region Metodos TS Clase
		public static Dictionary<string, NodoClase> TablaSimbolosClase
        {
            get
            {
                return tablaSimbolosClase;
            }

            set
            {
                tablaSimbolosClase = value;
            }
        }

        public NodoClase ObtenerNodoClase(string lexema)
        {
            if (tablaSimbolosClase.ContainsKey(lexema))
                return tablaSimbolosClase.SingleOrDefault(x => x.Key.ToString() == lexema).Value;
            else
                throw new Exception("Error Semantico: No existe el nombre de Clase");
        }

        public static Estado InsertarNodoClase(NodoClase miNodoClase)
        {
            if (!TablaSimbolosClase.ContainsKey(miNodoClase.Lexema))
            {
                TablaSimbolosClase.Add(miNodoClase.Lexema, miNodoClase);
                return Estado.Insertado;
            }
            else
            {
                MessageBox.Show(Estado.Duplicado.ToString());
                return Estado.Duplicado;
            }
        }

        public Estado InsertarHerencia(NodoClase nodoClase, string _herencia)
        {
            claseActual = nodoClase;
            NodoClase _herenciaInsertar = ObtenerNodoClase(_herencia);
            if (_herenciaInsertar.Lexema != null)
            {
                claseActual.Herencia = _herenciaInsertar;

                //claseActual.Herencia.Lexema = _herenciaInsertar.Lexema;
                //claseActual.Herencia.Herencia = _herenciaInsertar.Herencia;
                //claseActual.Herencia.TablaSimbolosAtributos = _herenciaInsertar.TablaSimbolosAtributos;
                //claseActual.Herencia.TablaSimbolosMetodos = _herenciaInsertar.TablaSimbolosMetodos;

                if (tablaSimbolosClase.ContainsKey(claseActual.Lexema))
                {
                    tablaSimbolosClase.Remove(claseActual.Lexema);
                    InsertarNodoClase(claseActual);
                    return Estado.Insertado;
                }
                else
                {
                    throw new Exception("Error Semantico: No existe el nombre de Clase");
                }
            }
            else
            {
                return Estado.NoExisteClase;
            }
        }

        public bool eliminarNodoClase(string _lexema)
        {
            if (tablaSimbolosClase.ContainsKey(_lexema))
            {
                return tablaSimbolosClase.Remove(_lexema);
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de Clase");
            }
        }
        #endregion

        #region Metodos TS Atributos
        public Estado InsertarNodoAtributo(NodoAtributo nodo, NodoClase nodoClaseActiva)
        {
            if (nodoClaseActiva.Lexema != nodo.lexema)
            {
                if (!nodoClaseActiva.TablaSimbolosAtributos.ContainsKey(nodo.lexema))
                {
                    nodoClaseActiva.TablaSimbolosAtributos.Add(nodo.lexema, nodo);
                    return Estado.Insertado;
                }
                else
                {
                    return Estado.Duplicado;
                    //mandamos un error de atributo duplicado.
                }
            }
            else
            {
                return Estado.DuplicadoAtributoConClase;
                //ERROR DE NOMBRE DE ATRIBUTO IGUAL QUE SU CLASE.
            }
        }
        public bool ExisteAtributo(NodoClase nodoClaseActiva, string variable)
        {
            if (nodoClaseActiva.TablaSimbolosAtributos.ContainsKey(variable))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Metodos TS Metodos

        #endregion

        #region Metodos Variables

        #endregion

    }
}