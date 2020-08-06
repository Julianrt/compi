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
        OperadorLogico,
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
        Suma,//+
        Resta,//-
        Multiplicacion,//*
        Division,// "/"
        IgualIgual, // ==
        MenorQue, //<
        MayorQue,//>
        MenorIgualQue, //<=
        MayorIgualQue, //>=
        Diferente, // !=
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
        public TipoDeDato tipoValorHijoIzquierdo;
        public TipoDeDato tipoValorHijoDerecho;
        public TipoDeDato tipoValorHermano;
        public TipoDeDato tipoValorHijoCentro;

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
            nodo.tipoValorHijoDerecho = TipoDeDato.Vacio;
            nodo.soyOperacionCondicionaDeTipo = OperacionCondicional.NADA;
            nodo.tipoValorHijoIzquierdo = TipoDeDato.Vacio;
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
            nodo.tipoValorHijoDerecho = TipoDeDato.Vacio;
            nodo.soyOperacionCondicionaDeTipo = OperacionCondicional.NADA;
            nodo.tipoValorHijoIzquierdo = TipoDeDato.Vacio;
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
            nodo.tipoValorHijoDerecho = TipoDeDato.Vacio;
            nodo.tipoValorHijoIzquierdo = TipoDeDato.Vacio;
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

        private TipoDeDato FuncionEquivalenciaDeDatos(TipoDeDato tipoValorHijoIzquierdo, TipoDeDato tipoValorHijoDerecho, OperacionCondicional soyOperacion)
        {
            switch (soyOperacion)
            {

                //artimeticas
                case OperacionCondicional.Suma:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Entero;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Decimal;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Decimal;

                    else if (tipoValorHijoIzquierdo == TipoDeDato.Cadena && tipoValorHijoDerecho == TipoDeDato.Cadena) return TipoDeDato.Cadena;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Cadena && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Cadena;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Cadena) return TipoDeDato.Cadena;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Cadena && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Cadena;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Cadena) return TipoDeDato.Cadena;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Cadena && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Cadena;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Caracter && tipoValorHijoDerecho == TipoDeDato.Cadena) return TipoDeDato.Cadena;

                    else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                case OperacionCondicional.Resta:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Entero;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Decimal;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Decimal;
                    else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;
                case OperacionCondicional.Multiplicacion:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Entero;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Decimal;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Decimal;
                    else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;
                case OperacionCondicional.Division:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Decimal;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Decimal;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Decimal;
                    else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;


                //logicas
                case OperacionCondicional.IgualIgual:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Cadena && tipoValorHijoDerecho == TipoDeDato.Cadena) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Booleano && tipoValorHijoDerecho == TipoDeDato.Booleano) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Caracter && tipoValorHijoDerecho == TipoDeDato.Caracter) return TipoDeDato.Booleano;
                    else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;
                case OperacionCondicional.Diferente:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Cadena && tipoValorHijoDerecho == TipoDeDato.Cadena) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Booleano && tipoValorHijoDerecho == TipoDeDato.Booleano) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Caracter && tipoValorHijoDerecho == TipoDeDato.Caracter) return TipoDeDato.Booleano;
                    else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;
                case OperacionCondicional.MayorQue:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;
                case OperacionCondicional.MayorIgualQue:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;
                case OperacionCondicional.MenorQue:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;
                case OperacionCondicional.MenorIgualQue:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;

            }
            return TipoDeDato.Vacio;
        }

        private TipoDeDato FuncionEquivalenciaAsignacion(TipoDeDato tipoValorAsignacion, TipoDeDato tipoValorHijoIzquierdo)
        {
            if (tipoValorHijoIzquierdo != TipoDeDato.Vacio)
            {
                if (tipoValorAsignacion == TipoDeDato.Entero && tipoValorHijoIzquierdo == TipoDeDato.Entero) return TipoDeDato.Entero;
                else if (tipoValorAsignacion == TipoDeDato.Decimal && tipoValorHijoIzquierdo == TipoDeDato.Entero) return TipoDeDato.Decimal;
                else if (tipoValorAsignacion == TipoDeDato.Decimal && tipoValorHijoIzquierdo == TipoDeDato.Decimal) return TipoDeDato.Decimal;
                else if (tipoValorAsignacion == TipoDeDato.Booleano && tipoValorHijoIzquierdo == TipoDeDato.Booleano) return TipoDeDato.Booleano;
                else if (tipoValorAsignacion == TipoDeDato.Cadena && tipoValorHijoIzquierdo == TipoDeDato.Cadena) return TipoDeDato.Cadena;
                else if (tipoValorAsignacion == TipoDeDato.Caracter && tipoValorHijoIzquierdo == TipoDeDato.Caracter) return TipoDeDato.Caracter;
                else
                    throw new Exception(string.Format("Error de tipos no se puede realizar la asignación {0} con {1} ", tipoValorAsignacion, tipoValorHijoIzquierdo));
            }
            return TipoDeDato.Vacio;
        }
        public TipoDeDato VerificacionTipos(NodoArbol miArbol)
        {
            if (miArbol.soySentenciaDeTipo == TipoSentencia.ASIGNACION)

            if ((miArbol.soySentenciaDeTipo != TipoSentencia.IF) &&
                (miArbol.hijoIzquierdo != null)) miArbol.tipoValorHijoIzquierdo = VerificacionTipos(miArbol.hijoIzquierdo);


            if ((miArbol.soySentenciaDeTipo != TipoSentencia.IF) &&
                (miArbol.hijoDerecho != null)) miArbol.tipoValorHijoDerecho = VerificacionTipos(miArbol.hijoDerecho);

            if (miArbol.soyDeTipoExpresion == tipoExpresion.Operador)
            {
                return FuncionEquivalenciaDeDatos(miArbol.tipoValorHijoIzquierdo, miArbol.tipoValorHijoDerecho, miArbol.soyOperacionCondicionaDeTipo);
            }
            else if (miArbol.soyDeTipoExpresion == tipoExpresion.OperadorLogico)
            {
                return FuncionEquivalenciaDeDatos(miArbol.tipoValorHijoIzquierdo, miArbol.tipoValorHijoDerecho, miArbol.soyOperacionCondicionaDeTipo);
            }
            else if (miArbol.soyDeTipoExpresion == tipoExpresion.Constante)
            {
                return miArbol.soyDeTipoDato;
            }
            else if (miArbol.soyDeTipoExpresion == tipoExpresion.Identificador)
            {
                return miArbol.soyDeTipoDato;
            }
            else if (miArbol.soySentenciaDeTipo == TipoSentencia.ASIGNACION)
            {
                return FuncionEquivalenciaAsignacion(miArbol.soyDeTipoDato, miArbol.tipoValorHijoIzquierdo);
            }

            return TipoDeDato.Vacio;
        }




    }
}
    
