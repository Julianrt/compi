﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

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
        Booleano,
        Caracter,
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
        private int lineaDeclaracion;
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
        public int LineaDeclaracion
        {
            get
            {
                return lineaDeclaracion;
            }

            set
            {
                lineaDeclaracion = value;
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
        private string lexema;
        private TipoDeDato tipoDato;
        private string valor;
        private int lineaDeclaracion;

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
        public TipoDeDato TipoDato
        {
            get
            {
                return tipoDato;
            }

            set
            {
                tipoDato = value;
            }
        }
        public string Valor
        {
            get
            {
                return valor;
            }

            set
            {
                valor = value;
            }
        }
        public int LineaDeclaracion
        {
            get
            {
                return lineaDeclaracion;
            }

            set
            {
                lineaDeclaracion = value;
            }
        }
    }
    public class NodoMetodo {

        private string lexema;
        private int linea;
        private Regreso miRegreso;
        private Dictionary<object, NodoParametro> tablaParametro = new Dictionary<object, NodoParametro>();
        private Dictionary<object, NodoVariable> tablaVariables = new Dictionary<object, NodoVariable>();

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
        public int LineaDeclaracion
        {
            get
            {
                return linea;
            }
            set
            {
                linea = value;
            }
        }
        public Regreso MiRegreso
        {
            get
            {
                return miRegreso;
            }
            set
            {
                miRegreso = value;
            }
        }
        public Dictionary<object, NodoParametro> TablaParametro
        {
            get
            {
                return tablaParametro;
            }
            set
            {
                tablaParametro = value;
            }
        }
        public Dictionary<object, NodoVariable> TablaVariables
        {
            get
            {
                return tablaVariables;
            }
            set
            {
                tablaVariables = value;
            }
        }
    }
    public class NodoParametro {
        private string lexema;
        private TipoDeDato tipoDato;
        private int lineaDeclaracion;

        public string Lexema { get => lexema; set => lexema = value; }
        public TipoDeDato TipoDato { get => tipoDato; set => tipoDato = value; }
        public int LineaDeclaracion { get => lineaDeclaracion; set => lineaDeclaracion = value; }
    }
    public class NodoVariable {
        private string lexema;
        private TipoDeDato miTipoDato;
        private string valor;
        private TipoDeVariable miTipoVariable;
        private int[] referencias;
        private int lineaDeclaracion;

        public string Lexema { get => lexema; set => lexema = value; }
        public TipoDeDato MiTipoDato { get => miTipoDato; set => miTipoDato = value; }
        public string Valor { get => valor; set => valor = value; }
        public TipoDeVariable MiTipoVariable { get => miTipoVariable; set => miTipoVariable = value; }
        public int[] Referencias { get => referencias; set => referencias = value; }
        public int LineaDeclaracion { get => lineaDeclaracion; set => lineaDeclaracion = value; }
    }
	#endregion

	public class TablaSimbolos
    {
        public NodoClase nodoClase = new NodoClase();

        public NodoMetodo metodoActual = new NodoMetodo();

        private static Dictionary<string, NodoClase> tablaSimbolosClase = new Dictionary<string, NodoClase>();
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

        private static List<ErrorSemanticoDGV> listaErroresSemanticos = new List<ErrorSemanticoDGV>();
        public static List<ErrorSemanticoDGV> ListaErroresSemanticos { get => listaErroresSemanticos; set => listaErroresSemanticos = value; }
		
        #region Metodos TS Clase
        public NodoClase ObtenerNodoClase(string lexema)
        {
            if (tablaSimbolosClase.ContainsKey(lexema))
                return tablaSimbolosClase.SingleOrDefault(x => x.Key.ToString() == lexema).Value;
            else
                return new NodoClase();
                //throw new Exception("Error Semantico: No existe el nombre de Clase");
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
                ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                error.Linea = miNodoClase.LineaDeclaracion;
                error.Lexema = miNodoClase.Lexema;
                error.Mensaje = "ESTADO: Duplicado - La clase " + error.Lexema + " de la linea " + error.Linea + " ya se habia escrito";
                TablaSimbolos.ListaErroresSemanticos.Add(error);
                //MessageBox.Show("La clase "+miNodoClase.Lexema+" de la linea "+miNodoClase.LineaDeclaracion+" ya se habia escrito","ESTADO: CLASE DUPLICADA");
                return Estado.Duplicado;
            }
        }
        public static Estado InsertarHerencia(NodoClase nodoClase, string _herencia)
        {
            TablaSimbolos ts = new TablaSimbolos();
            NodoClase _herenciaInsertar = ts.ObtenerNodoClase(_herencia);
            if (_herenciaInsertar.Lexema != null)
            {
                nodoClase.Herencia = _herenciaInsertar;

                //claseActual.Herencia.Lexema = _herenciaInsertar.Lexema;
                //claseActual.Herencia.Herencia = _herenciaInsertar.Herencia;
                //claseActual.Herencia.TablaSimbolosAtributos = _herenciaInsertar.TablaSimbolosAtributos;
                //claseActual.Herencia.TablaSimbolosMetodos = _herenciaInsertar.TablaSimbolosMetodos;

                if (tablaSimbolosClase.ContainsKey(nodoClase.Lexema))
                {
                    tablaSimbolosClase.Remove(nodoClase.Lexema);
                    InsertarNodoClase(nodoClase);
                    return Estado.Insertado;
                }
                else
                {

                    return Estado.NoExisteClase;
                    //throw new Exception("Error Semantico: No existe el nombre de Clase");
                }
            }
            else
            {
                ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                error.Linea = nodoClase.LineaDeclaracion;
                error.Lexema = _herencia;
                error.Mensaje = "No se encontro la clase: " + error.Lexema + " de la que quiere heredar en la linea: " + error.Linea;
                TablaSimbolos.ListaErroresSemanticos.Add(error);
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
        public static Estado InsertarNodoAtributo(NodoAtributo nodo, NodoClase nodoClaseActiva)
        {
            if (nodoClaseActiva.Lexema != nodo.Lexema)
            {
                if (!nodoClaseActiva.TablaSimbolosAtributos.ContainsKey(nodo.Lexema))
                {
                    nodoClaseActiva.TablaSimbolosAtributos.Add(nodo.Lexema, nodo);
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
                ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                error.Linea = nodo.LineaDeclaracion;
                error.Lexema = nodo.Lexema;
                error.Mensaje = "El atributo " + error.Lexema + " no se puede llamar como la clase";
                TablaSimbolos.ListaErroresSemanticos.Add(error);
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
        public bool LlamarAtributo(NodoClase nodoClase, string lexema, int linea)
        {
            if (ExisteAtributo(nodoClase, lexema))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Metodos TS Metodos
        public static Estado InsertarMetodo(NodoClase claseActual,  NodoMetodo _nuevometodo)
        {
            if (tablaSimbolosClase.ContainsKey(claseActual.Lexema))
            {
                if (claseActual.Lexema == _nuevometodo.Lexema)
                {
                    ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                    error.Linea = _nuevometodo.LineaDeclaracion;
                    error.Lexema = _nuevometodo.Lexema;
                    error.Mensaje = "El metodo "+error.Lexema+" en la linea "+error.Linea+" no puede llamarse igual a su clase";
                    TablaSimbolos.ListaErroresSemanticos.Add(error);
                    //MessageBox.Show("El metodo no se puede llamar igual a la clase","ESTADO: METODO DUPLICADO");
                    return Estado.Duplicado;
                }
                if (!claseActual.TablaSimbolosMetodos.ContainsKey(_nuevometodo.Lexema))
                {
                    claseActual.TablaSimbolosMetodos.Add(_nuevometodo.Lexema, _nuevometodo);
                    tablaSimbolosClase.Remove(claseActual.Lexema);
                    InsertarNodoClase(claseActual);
                    return Estado.Insertado;
                }
                else
                {
                    ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                    error.Linea = _nuevometodo.LineaDeclaracion;
                    error.Lexema = _nuevometodo.Lexema;
                    error.Mensaje = "El metodo " + error.Lexema + " en la linea " + error.Linea + " ya habia sido escrito";
                    TablaSimbolos.ListaErroresSemanticos.Add(error);
                    //MessageBox.Show("El metodo "+_nuevometodo.Lexema+" en la linea "+_nuevometodo.LineaDeclaracion+" ya se habia escrito","ESTADO: METODO DUPLICADO");
                    return Estado.Duplicado;
                }
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de Clase");
            }
        }
        public NodoMetodo ObtenerNodoMetodo(NodoClase _clase, string _idMetodo)
        {
            if (_clase.TablaSimbolosMetodos.ContainsKey(_idMetodo))
                return _clase.TablaSimbolosMetodos.SingleOrDefault(x => x.Key.ToString() == _idMetodo).Value;
            else
                return new NodoMetodo();
                //throw new Exception("Error Semantico: No existe el nombre del metodo");
        }
        public int ObtenerCantidadParametros(NodoMetodo nodoMetodo)
        {
            return nodoMetodo.TablaParametro.Count();
        }
        #endregion

        #region Metodos Parametros
        public static Estado InsertarParametro(NodoClase nodoClase, NodoMetodo nodoMetodo, NodoParametro nodoParametro)
        {
            if (TablaSimbolosClase.ContainsKey(nodoClase.Lexema)){
                if (nodoClase.TablaSimbolosMetodos.ContainsKey(nodoMetodo.Lexema))
                {
                    if ( !nodoMetodo.TablaParametro.ContainsKey(nodoParametro.Lexema) )
                    {
                        nodoMetodo.TablaParametro.Add(nodoParametro.Lexema, nodoParametro);
                        return Estado.Insertado;
                    }
                    else
                    {
                        MessageBox.Show("El parametro "+nodoParametro.Lexema+" en el metodo "+nodoMetodo.Lexema+" en linea "+nodoParametro.LineaDeclaracion+" ya habia sido escrito","ESTADO: PARAMETRO DUPLICADO");
                        return Estado.Duplicado;
                    }
                }
                else
                {
                    throw new Exception("Error Semantico: No existe el nombre del Metodo");
                }
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de Clase");
            }
        }
        public NodoParametro ObtenerNodoParametro(NodoClase nodoClase, NodoMetodo nodoMetodo, string lexemaParametro)
        {
            if (nodoClase.TablaSimbolosMetodos.ContainsKey(nodoMetodo.Lexema))
            {
                if (nodoMetodo.TablaParametro.ContainsKey(lexemaParametro))
                {
                    return nodoMetodo.TablaParametro.SingleOrDefault(x => x.Key.ToString() == lexemaParametro).Value;
                }
                else
                {
                    throw new Exception("Error Semantico: No existe el nombre del parametro");
                }
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre del metodo");
            }
        }
        public bool ExisteParametro(NodoMetodo nodoMetodo, string lexemaParametro)
        {
            if (nodoMetodo.TablaParametro.ContainsKey(lexemaParametro))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Metodos Variables
        public static Estado InsertarVariable(NodoClase nodoClase, NodoMetodo nodoMetodo, NodoVariable nodoVariable)
        {
            if ( TablaSimbolosClase.ContainsKey(nodoClase.Lexema))
            {
                if (nodoClase.TablaSimbolosMetodos.ContainsKey(nodoMetodo.Lexema))
                {
                    if ( nodoMetodo.TablaParametro.ContainsKey(nodoVariable.Lexema))
                    {
                        //throw new Exception("Error Semantico: Ya existe un parametro con el identificador "+nodoVariable.Lexema);
                    }
                    else if (!nodoMetodo.TablaVariables.ContainsKey(nodoVariable.Lexema))
                    {
                        nodoMetodo.TablaVariables.Add(nodoVariable.Lexema, nodoVariable);
                    }
                    return Estado.Insertado;
                    
                }
                else
                {
                    throw new Exception("Error Semantico: No existe el nombre del Metodo");
                }
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de Clase");
            }
        }
        public NodoVariable ObtenerNodoVariable(NodoClase nodoClase, NodoMetodo nodoMetodo, string lexemaVariable)
        {
            if ( TablaSimbolosClase.ContainsKey(nodoClase.Lexema))
            {
                if ( nodoClase.TablaSimbolosMetodos.ContainsKey(nodoMetodo.Lexema))
                {
                    if ( nodoMetodo.TablaVariables.ContainsKey(lexemaVariable))
                    {
                        return nodoMetodo.TablaVariables.SingleOrDefault(x => x.Key.ToString() == lexemaVariable).Value;
                    }
                    else
                    {
                        throw new Exception("Error Semantico: No existe el nombre de Variable");
                    }
                }
                else
                {
                    throw new Exception("Error Semantico: No existe el nombre del Metodo");
                }
            }
            else
            {
                throw new Exception("Error Semantico: No existe el nombre de Clase");
            }
        }
        public bool ExisteVariable(NodoMetodo nodoMetodo, string lexemaVariable)
        {
            if (nodoMetodo.TablaVariables.ContainsKey(lexemaVariable))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region invocacion
        public static bool ValidarInvocaion(string clase, string metodo)
        {
            NodoClase nodoClase;
            NodoMetodo nodoMetodo;
            TablaSimbolos ts = new TablaSimbolos();
            if (TablaSimbolosClase.ContainsKey(clase))
            {
                nodoClase = ts.ObtenerNodoClase(clase);
                if (nodoClase.TablaSimbolosMetodos.ContainsKey(metodo))
                {
                    nodoMetodo = ts.ObtenerNodoMetodo(nodoClase, metodo);
                    return true;
                }
                else
                {
                    //MessageBox.Show("La clase "+clase+" no tiene un metodo llamado "+metodo,"ESTADO: INVOCACION INCORRECTA");
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("No se encontro la clase "+clase+" de la que se quiere invocar "+metodo,"ESTADO: INVOVACION INCORRECTA");
                return false;
            }

            /*var _metodos = TablaDeSimbolos.Select(x => x.Value).Where(x => x.id.StartsWith(metodo + "*")).ToList();
            if (_metodos.Count > 0)
            {
                var _metodo = _metodos[_metodos.Count - 1];
                invocacionActual = _metodo.id;
            }
            else
            {
                throw new Exception("Error: Metodo no encontrado");
            }*/
        }
        #endregion

        public static List<TSMostrar> ObtenerListaClases()
        {
            List<TSMostrar> listaClases = new List<TSMostrar>();
            foreach(var nodoClase in TablaSimbolos.TablaSimbolosClase)
            {
                TSMostrar clase = new TSMostrar();
                clase.Lexema = nodoClase.Value.Lexema;
                if (nodoClase.Value.Herencia != null)
                    clase.Herencia = nodoClase.Value.Herencia.Lexema;
                clase.LineaDeclaracion = nodoClase.Value.LineaDeclaracion;

                foreach(var nodoAtributo in nodoClase.Value.TablaSimbolosAtributos)
                {
                    clase.Atributos += nodoAtributo.Value.Lexema + " ";
                }

                foreach (var nodoMetodo in nodoClase.Value.TablaSimbolosMetodos)
                {
                    clase.Metodos += nodoMetodo.Value.Lexema + " ";

                    if (nodoMetodo.Value.TablaParametro.Count() > 0)
                    {
                        clase.Metodos += "PARAMETROS: ";
                        foreach (var nodoParametro in nodoMetodo.Value.TablaParametro)
                        {
                            clase.Metodos += nodoParametro.Value.Lexema + " ";
                        }
                    }

                    if (nodoMetodo.Value.TablaVariables.Count() > 0)
                    {
                        clase.Metodos += "VARIABLES: ";
                        foreach (var nodoVariable in nodoMetodo.Value.TablaVariables)
                        {
                            clase.Metodos += nodoVariable.Value.Lexema + " ";
                        }
                    }
                }
                listaClases.Add(clase);
            }
            return listaClases;
        }

    }

    public class TSMostrar
    {
        private string lexema;
        private string herencia;
        private int lineaDeclaracion;
        private int[] referencias;

        private string atributos;
        private string metodos;

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
        public string Herencia
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
        public int LineaDeclaracion
        {
            get
            {
                return lineaDeclaracion;
            }

            set
            {
                lineaDeclaracion = value;
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
        public string Atributos
        {
            get
            {
                return atributos;
            }

            set
            {
                atributos = value;
            }
        }
        public string Metodos
        {
            get
            {
                return metodos;
            }

            set
            {
                metodos = value;
            }
        }
    }

    public class ErrorSemanticoDGV
    {
        private int linea;
        private string lexema;
        private string mensaje;

        public int Linea { get => linea; set => linea = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
    }
}