using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LenguajesyAutomatas
{
	#region Enums
	public enum TipoNodoArbol
	{
		Expresion,Sentencia,Condicional,Nada
	}

    public enum TipoSentencia
    {
        IF,
        FOR,
        ASIGNACION,
        EXPRESION,
        LEER,
        ESCRIBIR,
        NADA
    }
    public enum tipoExpresion
    {
        Operador,
        Constante,
        Identificador,
        NADA
    }
    public enum tipoOperador
    {
        Suma,
        Resta,
        Multiplicacion,
        Division,
        NADA
    }
    public enum OperacionCondicional
    {
        Suma,
        Resta,
        Multiplicacion,
        Division,
        IgualIgual,
        MenorQue,
        MayorQue,
        MenorIgualQue,
        MayorIgualQue,
        Diferente,
        NADA
    }
    #endregion

    public class NodoArbol
    {
        public NodoArbol hijoIzquierdo;
        public NodoArbol hijoDerecho;
        public NodoArbol hijoCentro;
        public NodoArbol hermano;

        public TipoNodoArbol soyTipoNodo;
        public TipoSentencia soySentenciaDeTipo;

        public tipoExpresion soyDeTipoExpresion;
        public tipoOperador soyDeTipoOperacion;
        public TipoDeDato soyDeTipoDato; // semantico ... gramatica con atributo
        public OperacionCondicional soyOperacionCondicionaDeTipo;



        //reglas semanticas // atributos
        //comprobacion de tipos
        public TipoDeDato tipoValorNodoHijoIzquierdo;
        public TipoDeDato tipoValorNodoHijoDerecho;

        public string lexema;
        public string valor;
        public string pCode;
        public string type;
    }
    class Arboles
	{

        //CREAR Arbol de FOR        
        //CREAR Arbol de LEER
        //CREAR Arbol de Escribir
        // crear varios arboles unidos con hermanos 

        public int Puntero;
        public List<Token> miListaTokenCopia;

        public Arboles (List<Token> listaDeTokens)
        {
            Puntero = 0;
            this.miListaTokenCopia = listaDeTokens;
        }

        public NodoArbol CrearArbolSintacticoAbstracto()
        {
            return CrearArbolAsignacion();
        }

        #region Crear Arbol FOR 

        private NodoArbol CrearArbolFor()
        {
            throw new NotImplementedException();
        }

        #endregion   //Falta este

        #region Crear Arbol Condicional

        public NodoArbol CrearArbolCondicional()
        {
            NodoArbol nodoRaiz = CrearArbolExpresion();
            if (miListaTokenCopia[Puntero].lexema.Equals("==")
                || miListaTokenCopia[Puntero].lexema.Equals("<=")
                || miListaTokenCopia[Puntero].lexema.Equals(">=") 
                || miListaTokenCopia[Puntero].lexema.Equals(">")
                || miListaTokenCopia[Puntero].lexema.Equals("<"))
            {
                NodoArbol nodoTemp = NuevoNodoCondicional();

                switch (miListaTokenCopia[Puntero].lexema)
                {
                    case "==":
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.IgualIgual;
                        break;
                    case "<=":
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.MenorIgualQue;
                        break;
                    case ">=":
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.MayorIgualQue;
                        break;
                    case ">":
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.MayorQue;
                        break;
                    case "<":
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.MenorQue;
                        break;
                    default:
                        break;
                }
                nodoTemp.hijoIzquierdo = nodoRaiz;
                nodoRaiz = nodoTemp;
                Puntero++;
                nodoRaiz.hijoDerecho = CrearArbolExpresion();
            }

            return nodoRaiz;
        }
        #endregion

        #region Creacion de diferentes tipos de Arboles
        public NodoArbol NuevoNodoExpresion(tipoExpresion tipoExpresion)
        {
            NodoArbol nodo = new NodoArbol();

            nodo.soyTipoNodo = TipoNodoArbol.Expresion;

            nodo.soyDeTipoExpresion = tipoExpresion;
            nodo.soyDeTipoOperacion = tipoOperador.NADA;
            nodo.soyDeTipoDato = TipoDeDato.Vacio;

            nodo.soySentenciaDeTipo = TipoSentencia.NADA;
            nodo.soyOperacionCondicionaDeTipo = OperacionCondicional.NADA;
            nodo.tipoValorNodoHijoDerecho = TipoDeDato.Vacio;
            nodo.soyOperacionCondicionaDeTipo = OperacionCondicional.NADA;
            nodo.tipoValorNodoHijoIzquierdo = TipoDeDato.Vacio;
            return nodo;
        }

        public NodoArbol NuevoNodoSentencia(TipoSentencia tipoSentencia)
        {
            NodoArbol nodo = new NodoArbol();
            nodo.soyTipoNodo = TipoNodoArbol.Sentencia;

            nodo.soyDeTipoExpresion = tipoExpresion.NADA;
            nodo.soyDeTipoOperacion = tipoOperador.NADA;
            nodo.soyDeTipoDato = TipoDeDato.Vacio;

            nodo.soySentenciaDeTipo = tipoSentencia;
            nodo.soyOperacionCondicionaDeTipo = OperacionCondicional.NADA;
            nodo.tipoValorNodoHijoDerecho = TipoDeDato.Vacio;
            nodo.soyOperacionCondicionaDeTipo = OperacionCondicional.NADA;
            nodo.tipoValorNodoHijoIzquierdo = TipoDeDato.Vacio;
            return nodo;

        }
        public NodoArbol NuevoNodoCondicional()
        {
            NodoArbol nodo = new NodoArbol();
            nodo.soyTipoNodo = TipoNodoArbol.Condicional;

            nodo.soyDeTipoExpresion = tipoExpresion.NADA;
            nodo.soyDeTipoOperacion = tipoOperador.NADA;
            nodo.soyDeTipoDato = TipoDeDato.Vacio;

            nodo.soySentenciaDeTipo = TipoSentencia.NADA;
            nodo.soyOperacionCondicionaDeTipo = OperacionCondicional.NADA;
            nodo.soyOperacionCondicionaDeTipo = OperacionCondicional.NADA;
            nodo.tipoValorNodoHijoDerecho = TipoDeDato.Vacio;
            nodo.tipoValorNodoHijoIzquierdo = TipoDeDato.Vacio;
            return nodo;

        }
        #endregion

        #region Crear Arbol IF
        public NodoArbol CrearArbolIF()
        {
            var nodoArbolIF = NuevoNodoSentencia(TipoSentencia.IF);
            Puntero += 2;
            nodoArbolIF.hijoIzquierdo = CrearArbolCondicional();
            Puntero += 2;
            nodoArbolIF.hijoCentro = CrearArbolSintacticoAbstracto();
            Puntero++;
            if (miListaTokenCopia[Puntero].lexema.Equals("Else"))
            {
                Puntero++;
                if (miListaTokenCopia[Puntero].lexema.Equals("if"))
                {
                    CrearArbolIF();
                }
                else
                {
                    Puntero++;
                    nodoArbolIF.hijoDerecho = CrearArbolSintacticoAbstracto();
                }
            }

            return nodoArbolIF;
        }


        #endregion

        #region Crear ARBOL ASIGNACION

        public NodoArbol CrearArbolAsignacion()
        {
            var sentenciaAsignacion = NuevoNodoSentencia(TipoSentencia.ASIGNACION);
            sentenciaAsignacion.lexema = miListaTokenCopia[Puntero].lexema;
            Puntero += 2;
            sentenciaAsignacion.hijoIzquierdo = CrearArbolExpresion();
            return sentenciaAsignacion;
        }


        #endregion

        #region Arbol de Expresion
        public NodoArbol CrearArbolExpresion()
        {
            NodoArbol nodoRaiz = Termino();
            while (miListaTokenCopia[Puntero].lexema.Equals("+")
                || miListaTokenCopia[Puntero].lexema.Equals("-"))
            {
                NodoArbol nodoTemp = NuevoNodoExpresion(tipoExpresion.Operador);
                nodoTemp.hijoIzquierdo = nodoRaiz;
                nodoTemp.soyDeTipoOperacion =
                    miListaTokenCopia[Puntero].lexema.Equals("+")
                    ? tipoOperador.Suma
                    : tipoOperador.Resta;
                nodoTemp.lexema = miListaTokenCopia[Puntero].lexema;
                nodoRaiz = nodoTemp;
                Puntero++;
                nodoRaiz.hijoDerecho = Termino();
            }

            return nodoRaiz;
        }

        private NodoArbol Termino()
        {
            NodoArbol t = Factor();
            while (miListaTokenCopia[Puntero].lexema.Equals("*")
                 || miListaTokenCopia[Puntero].lexema.Equals("/"))
            {
                NodoArbol p = NuevoNodoExpresion(tipoExpresion.Operador);
                p.hijoIzquierdo = t;
                p.soyDeTipoOperacion = miListaTokenCopia[Puntero].lexema.Equals("*")
                    ? tipoOperador.Multiplicacion
                    : tipoOperador.Division;
                t.lexema = miListaTokenCopia[Puntero].lexema;
                t = p;
                Puntero++;
                t.hijoDerecho = Factor();
            }
            return t;
        }

        private NodoArbol Factor()
        {
            NodoArbol t = new NodoArbol();

            if (miListaTokenCopia[Puntero].token == -2) //ENTERO
            {
                t = NuevoNodoExpresion(tipoExpresion.Constante);
                t.soyDeTipoDato = TipoDeDato.Entero;
                t.lexema = miListaTokenCopia[Puntero].lexema;
                Puntero++;
            }
            if (miListaTokenCopia[Puntero].token == -3)  //float
            {
                t = NuevoNodoExpresion(tipoExpresion.Constante);
                t.lexema = miListaTokenCopia[Puntero].lexema;
                t.soyDeTipoDato = TipoDeDato.Decimal;
                Puntero++;
            }

            else if (miListaTokenCopia[Puntero].token == -1)
            {
                t = NuevoNodoExpresion(tipoExpresion.Identificador);
                t.lexema = miListaTokenCopia[Puntero].lexema;
                //   t.TipoDato = TablaSimbolos.ObtenerTipoDato(miListaTokenCopia[puntero].Lexema, nodoClaseActiva, nombreMetodoActivo);
                Puntero++;
            }
            else if (miListaTokenCopia[Puntero].lexema.Equals("("))
            {
                Puntero++;
                t = CrearArbolExpresion();
                Puntero++;
            }
            return t;
        }
        #endregion

        #region Crear arboles Lectura y escritura

        public NodoArbol CrearArbolRead(string valor)
        {
            var Read = NuevoNodoSentencia(TipoSentencia.LEER);
            Read.lexema = valor;
            return Read;
        }

        public NodoArbol CrearArbolWrite()
        {
            var Write = NuevoNodoSentencia(TipoSentencia.ESCRIBIR);
            Write.lexema = "write";
            Write.hijoIzquierdo = SimpleExpresion();
            return Write;
        }


        #endregion

        private NodoArbol ObtenerSiguienteArbol()
        {
            switch (miListaTokenCopia[Puntero].token)
            {
                case -102: //if
                    return CrearArbolIF();

                case -1: //asignacion
                    return CrearArbolAsignacion();

                case -125:
                    return CrearArbolFor();

                case -2:
                    return CrearArbolExpresion();

                default:
                    return null;

            }
        }

        public NodoArbol SimpleExpresion()
        {
            NodoArbol nodoRaiz = Termino();
            while (miListaTokenCopia[Puntero].lexema.Equals("+") || miListaTokenCopia[Puntero].lexema.Equals("-"))
            {
                NodoArbol nodoTemp = NuevoNodoExpresion(tipoExpresion.Operador);
                nodoTemp.hijoIzquierdo = nodoRaiz;
                nodoTemp.soyOperacionCondicionaDeTipo = miListaTokenCopia[Puntero].lexema.Equals("+") ? OperacionCondicional.Suma : OperacionCondicional.Resta;

                nodoTemp.lexema = miListaTokenCopia[Puntero].lexema;

                nodoRaiz = nodoTemp;
                Puntero++;
                nodoRaiz.hijoDerecho = Termino();
            }
            return nodoRaiz;
        }



    }
}
