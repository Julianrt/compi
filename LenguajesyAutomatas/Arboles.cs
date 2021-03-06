﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LenguajesyAutomatas
{
	#region Enums
	public enum TipoNodoArbol
	{
		Expresion,
        Sentencia,
        Condicional,
        Nada
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
        public string pCode = "";
        public string type;

        public static NodoArbol GetEmptyNode()
        {
            NodoArbol node = new NodoArbol();
            node.hijoIzquierdo = new NodoArbol();
            node.hijoDerecho = new NodoArbol();
            node.hijoCentro = new NodoArbol();
            node.hermano = new NodoArbol();
            return node;
        }
    }
    class Arboles
	{
        //CREAR Arbol de FOR        
        //CREAR Arbol de LEER
        //CREAR Arbol de Escribir
        // crear varios arboles unidos con hermanos
        private string claseActual;
        private string metodoActual;
        public int Puntero;
        public List<Token> listToken;
        private NodoArbol root;
        private string lexemaContadorFor;

        public Arboles (List<Token> listaDeTokens)
        {
            Puntero = 0;
            this.listToken = listaDeTokens;
        }

        #region crear arboles
        public NodoArbol CrearArbolSintacticoAbstracto()
        {
            NodoArbol nodo = InsertNodo();
            if (root == nodo) { } //BUENO
            return nodo;
        }

        private NodoArbol InsertNodo()
        {
            NodoArbol nodo = new NodoArbol();
            if (Puntero < listToken.Count-1)
            {

                switch (listToken[Puntero].token)
                {
                    case -127: //CLASE
                        claseActual = listToken[Puntero + 1].lexema;
                        AvanzarPuntero(6);
                        InsertNodo();
                        break;

                    case -101: //METODO
                        metodoActual = listToken[Puntero + 1].lexema;
                        AvanzarPuntero(6);
                        InsertNodo();
                        break;

                    case -1: //ASIGNACION
                        nodo = CrearArbolAsignacion();
                        root = nodo;
                        break;

                    case -102: //IF
                        nodo = CrearArbolIF();
                        root = nodo;
                        break;

                    case -109: //ELIF
                        nodo = CrearArbolIF();
                        root = nodo;
                        break;

                    case -113: //ELSE
                        nodo = CrearNodoElse();
                        root = nodo;
                        break;

                    case -125: //FOR
                        nodo = CrearArbolFor();
                        root = nodo;
                        break;

                    default:
                        AvanzarPuntero();
                        InsertNodo();
                        break;
                }
            }
            return nodo;
        }
        #endregion

        #region Crear Arbol FOR 

        private NodoArbol CrearArbolFor()
        {
            NodoArbol nodoFor = NuevoNodoSentencia(TipoSentencia.FOR);
            nodoFor.lexema = listToken[Puntero].lexema;

            nodoFor.hijoIzquierdo = DeclaracionFor();
            nodoFor.hijoIzquierdo.soyDeTipoDato = VerificacionTipos(nodoFor.hijoIzquierdo);
            nodoFor.pCode = nodoFor.hijoIzquierdo.pCode;

            nodoFor.hijoCentro = ValidacionFor(nodoFor.hijoIzquierdo);
            nodoFor.pCode += "lab LFOR\n";
            nodoFor.pCode += nodoFor.hijoCentro.pCode;
            nodoFor.pCode += "fjp LFOREND\n";
            VerificacionTipos(nodoFor.hijoCentro);
            
            AvanzarPuntero(6); //Nos movemos a las sentencias dentro del FOR


            while (listToken[Puntero].token == -40) //Saltar enter si hay
            {
                AvanzarPuntero();
            }
            if (listToken[Puntero].token == -1 || listToken[Puntero].token == -102 || listToken[Puntero].token == -125)
            {
                nodoFor.hijoDerecho = SentenciasFor();
            }

            nodoFor.hijoDerecho = SentenciaIncrementoFor(nodoFor.hijoDerecho, nodoFor.hijoIzquierdo);
            
            nodoFor.pCode += nodoFor.hijoDerecho.pCode;
            nodoFor.pCode += "ujp LFOR\n lab LFOREND\n";

            AvanzarPuntero(2); //Nos movemos a la sig sentencia AFUERA del FOR

            while (listToken[Puntero].token == -40) //Saltar enter si hay
            {
                AvanzarPuntero();
            }
            if (listToken[Puntero].token == -1 || listToken[Puntero].token == -102 || listToken[Puntero].token == -125)
            {
                nodoFor.hermano = InsertNodo();
                nodoFor.pCode += nodoFor.hermano.pCode;
            }

            return nodoFor;
        }

        #region Declaración del for
        private NodoArbol DeclaracionFor()
        {
            AvanzarPuntero();
            NodoArbol nodoDelaracion = NuevoNodoSentencia(TipoSentencia.ASIGNACION);
            nodoDelaracion.lexema = listToken[Puntero].lexema;
            lexemaContadorFor = nodoDelaracion.lexema;
            nodoDelaracion.hijoIzquierdo = FactorFor();
            
            nodoDelaracion.pCode = "lda " + nodoDelaracion.lexema+"\n";
            nodoDelaracion.pCode += nodoDelaracion.hijoIzquierdo.pCode;
            nodoDelaracion.pCode += "sto\n";


            return nodoDelaracion;
        }
        private NodoArbol FactorFor()
        {
            AvanzarPuntero(4);
            NodoArbol nodoFactorFor = NuevoNodoSentencia(TipoSentencia.EXPRESION);
            nodoFactorFor.lexema = listToken[Puntero].lexema;
            nodoFactorFor.soyDeTipoExpresion = tipoExpresion.Constante;

            if (listToken[Puntero+1].token == -32) //Coma
            {
                if (listToken[Puntero].token == -1)
                {
                    nodoFactorFor.soyDeTipoExpresion = tipoExpresion.Identificador;
                    nodoFactorFor.soyDeTipoDato = TipoDeDato.SinEspecificar;
                    nodoFactorFor.pCode = "lod " + nodoFactorFor.lexema + "\n";
                }
                else if (listToken[Puntero].token == -2)
                {
                    nodoFactorFor.soyDeTipoDato = TipoDeDato.Entero;
                    nodoFactorFor.pCode = "ldc "+nodoFactorFor.lexema + "\n";
                } 
                else if (listToken[Puntero].token == -3)
                {
                    nodoFactorFor.soyDeTipoDato = TipoDeDato.Decimal;
                    nodoFactorFor.pCode = "ldc " + nodoFactorFor.lexema + "\n";
                }
            }
            else if (listToken[Puntero+1].token == -34) //Parentesis
            {
                nodoFactorFor.lexema = "0";
                nodoFactorFor.soyDeTipoDato = TipoDeDato.Entero;
            }
            return nodoFactorFor;
        }
        #endregion
        #region Validación del for
        private NodoArbol ValidacionFor(NodoArbol nodoCondator)
        {
            NodoArbol nodoValidacionFor = NuevoNodoSentencia(TipoSentencia.EXPRESION);
            nodoValidacionFor.soyDeTipoExpresion = tipoExpresion.OperadorLogico;
            nodoValidacionFor.lexema = "<";
            nodoValidacionFor.hijoIzquierdo = nodoCondator;
            nodoValidacionFor.hijoIzquierdo.pCode = "lod " + nodoCondator.lexema+"\n";
            nodoValidacionFor.hijoDerecho = FactorComparacionFor();

            nodoValidacionFor.pCode = nodoValidacionFor.hijoIzquierdo.pCode + nodoValidacionFor.hijoDerecho.pCode + "min\n";

            return nodoValidacionFor;
        }
        private NodoArbol FactorComparacionFor()
        {
            AvanzarPuntero(2);
            NodoArbol nodoFactorFor = NuevoNodoSentencia(TipoSentencia.EXPRESION);
            nodoFactorFor.soyTipoNodo = TipoNodoArbol.Expresion;
            nodoFactorFor.soyDeTipoExpresion = tipoExpresion.Identificador;
            nodoFactorFor.soyDeTipoDato = TipoDeDato.Entero;

            if (listToken[Puntero].token == -32 || listToken[Puntero].token == -34)
            {
                nodoFactorFor.lexema = listToken[Puntero - 5].lexema;
            }
            else if (listToken[Puntero-1].token == -32)
            {
                nodoFactorFor.lexema = listToken[Puntero].lexema;
                nodoFactorFor.pCode = "ldc " + nodoFactorFor.lexema + "\n";
            }
            return nodoFactorFor;
        }
        #endregion
        #region Sentencias dentro del for
        private NodoArbol SentenciasFor()
        {
            NodoArbol nodo = InsertNodo();
            
            while (listToken[Puntero].token == -40) //Evalua si el token es un enter para recorrerlos
            {
                AvanzarPuntero();
            }

            if (listToken[Puntero].token == -1 || listToken[Puntero].token == -102 || listToken[Puntero].token == -125)
            {
                nodo.hermano = InsertNodo();
                nodo.pCode = nodo.hermano.pCode;
            }

            return nodo;
        }
        private NodoArbol SentenciaIncrementoFor(NodoArbol nodo, NodoArbol nodoContador)
        {
            if (nodo == null)
            {
                nodo = IncrementarContador(nodoContador);
            }
            else if (nodo.hermano == null)
            {
                nodo.hermano = IncrementarContador(nodoContador);
                nodo.pCode += nodo.hermano.pCode;
            }
            else
            {
                nodo = SentenciaIncrementoFor(nodo.hermano, nodoContador);
            }
            return nodo;
        }
        private NodoArbol IncrementarContador(NodoArbol nodoContador)
        {
            NodoArbol nodo = NuevoNodoSentencia(TipoSentencia.ASIGNACION);
            nodo.soyDeTipoExpresion = tipoExpresion.Identificador;
            nodo.soyDeTipoDato = nodoContador.soyDeTipoDato;
            nodo.lexema = lexemaContadorFor;

            NodoArbol nodoSumatoria = NuevoNodoSentencia(TipoSentencia.EXPRESION);
            nodoSumatoria.soyDeTipoExpresion = tipoExpresion.Operador;
            nodoSumatoria.soyDeTipoOperacion = tipoOperador.Suma;
            nodoSumatoria.lexema = "+";

            /*NodoArbol nodoContador = NuevoNodoSentencia(TipoSentencia.EXPRESION);
            nodoContador.soyDeTipoExpresion = tipoExpresion.Identificador;
            nodoContador.lexema = lexemaContadorFor;*/

            NodoArbol nodoUno = NuevoNodoSentencia(TipoSentencia.EXPRESION);
            nodoUno.soyDeTipoExpresion = tipoExpresion.Constante;
            nodoUno.soyDeTipoDato = TipoDeDato.Entero;
            nodoUno.lexema = "1";

            nodoSumatoria.hijoIzquierdo = nodoContador;
            nodoSumatoria.hijoDerecho = nodoUno;
            nodo.hijoIzquierdo = nodoSumatoria;

            nodo.pCode = "lda " + nodoContador.lexema + "\n";
            nodo.pCode += "lod " + nodoContador.lexema + "\n";
            nodo.pCode += "ldc 1\n";
            nodo.pCode += "adi\n";
            nodo.pCode += "sto\n";

            return nodo;
        }
        #endregion
        #endregion   //Falta este

        #region Crear Arbol Condicional

        public NodoArbol CrearArbolCondicional()
        {
            Puntero--;
            NodoArbol nodoRaiz = CrearArbolExpresion();
            //Puntero++;
            if (listToken[Puntero].lexema.Equals("==")
                || listToken[Puntero].lexema.Equals("<=")
                || listToken[Puntero].lexema.Equals(">=") 
                || listToken[Puntero].lexema.Equals(">")
                || listToken[Puntero].lexema.Equals("<"))
            {
                NodoArbol nodoTemp = NuevoNodoCondicional();

                switch (listToken[Puntero].lexema)
                {
                    case "==":
                        nodoTemp.lexema = "==";
                        nodoTemp.soyDeTipoExpresion = tipoExpresion.OperadorLogico;
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.IgualIgual;
                        nodoTemp.pCode = "equ\n";
                        break;
                    case "!=":
                        nodoTemp.lexema = "!=";
                        nodoTemp.soyDeTipoExpresion = tipoExpresion.OperadorLogico;
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.Diferente;
                        nodoTemp.pCode = "dis\n";
                        break;
                    case "<=":
                        nodoTemp.lexema = "<=";
                        nodoTemp.soyDeTipoExpresion = tipoExpresion.OperadorLogico;
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.MenorIgualQue;
                        break;
                    case ">=":
                        nodoTemp.lexema = ">=";
                        nodoTemp.soyDeTipoExpresion = tipoExpresion.OperadorLogico;
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.MayorIgualQue;
                        break;
                    case ">":
                        nodoTemp.lexema = ">";
                        nodoTemp.soyDeTipoExpresion = tipoExpresion.OperadorLogico;
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.MayorQue;
                        break;
                    case "<":
                        nodoTemp.lexema = "<";
                        nodoTemp.soyDeTipoExpresion = tipoExpresion.OperadorLogico;
                        nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.MenorQue;
                        break;
                    default:
                        break;
                }
                nodoTemp.hijoIzquierdo = nodoRaiz;
                nodoRaiz = nodoTemp;
                Puntero++;
                nodoRaiz.hijoDerecho = CrearArbolExpresion();
                nodoRaiz.pCode = nodoRaiz.hijoIzquierdo.pCode + nodoRaiz.hijoDerecho.pCode + nodoRaiz.pCode;
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
            int etiquetaLabCodigoP = 1, etiquetaTemp = etiquetaLabCodigoP;

            var nodoArbolIF = NuevoNodoSentencia(TipoSentencia.IF);
            nodoArbolIF.lexema = listToken[Puntero].lexema;
            Puntero += 2;

            nodoArbolIF.hijoIzquierdo = CrearArbolCondicional();
            VerificacionTipos(nodoArbolIF.hijoIzquierdo);
            nodoArbolIF.pCode = nodoArbolIF.hijoIzquierdo.pCode;
            nodoArbolIF.pCode += "fjp L" + etiquetaLabCodigoP+"\n";

            Puntero += 4;
            
            while (listToken[Puntero].token == -40) //Evalua si el token es un enter y recorrerlos  HIJO DERECHO(SENTENCIAS DENTRO DEL IF)
            {
                AvanzarPuntero();
            }
            if (listToken[Puntero].token == -1 || listToken[Puntero].token == -102 || listToken[Puntero].token == -125)
            {
                nodoArbolIF.hijoDerecho = InsertNodo();

                etiquetaTemp = etiquetaLabCodigoP;
                etiquetaLabCodigoP++;
                nodoArbolIF.pCode += nodoArbolIF.hijoDerecho.pCode;
            }

            Puntero += 2;            

            while (listToken[Puntero].token == -40) //Evalua si el token es un enter y recorrerlos  HERMANOS
            {
                AvanzarPuntero();
            }

            if (listToken[Puntero].token == -1 || listToken[Puntero].token == -102 || listToken[Puntero].token == -125 ||
                listToken[Puntero].token == -109 || listToken[Puntero].token == -113)
            {
                nodoArbolIF.hermano = InsertNodo();


                if (nodoArbolIF.hermano.lexema == "else")
                {
                    nodoArbolIF.pCode += "ujp L" + etiquetaLabCodigoP + "\n";
                }
                nodoArbolIF.pCode += "lab L" + etiquetaTemp + "\n";
                nodoArbolIF.pCode += nodoArbolIF.hermano.pCode;
            }
            else
            {
                nodoArbolIF.pCode += "lab L" + etiquetaTemp + "\n";
            }

            return nodoArbolIF;
        }

        private NodoArbol CrearNodoElse()
        {
            NodoArbol nodoElse = NuevoNodoSentencia(TipoSentencia.IF);
            nodoElse.lexema = listToken[Puntero].lexema;
            AvanzarPuntero(5);

            while (listToken[Puntero].token == -40) //Evaluta si el token es un enter y recorrerlos
            {
                AvanzarPuntero();
            }

            if (listToken[Puntero].token == -1 || listToken[Puntero].token == -102 || listToken[Puntero].token == -125)
            {
                nodoElse.hijoIzquierdo = InsertNodo();

                nodoElse.pCode = nodoElse.hijoIzquierdo.pCode;

                nodoElse.pCode += "lab L2 \n";
            }
            AvanzarPuntero(2);
            while (listToken[Puntero].token == -40) //Evaluta si el token es un enter y recorrerlos
            {
                AvanzarPuntero();
            }
            //MessageBox.Show(listToken[Puntero].lexema + " " + listToken[Puntero].linea);
            
            if (listToken[Puntero].token == -38)
            {
                AvanzarPuntero();
            }

            if (listToken[Puntero].token == -1 || listToken[Puntero].token == -102 || listToken[Puntero].token == -125)
            {
                nodoElse.hermano = InsertNodo();

                nodoElse.pCode += nodoElse.hermano.pCode;
            }

            return nodoElse;
        }

        private NodoArbol AgregarElse(NodoArbol nodoHermanoIF, NodoArbol nodoElse)
        {
            if (nodoHermanoIF == null)
            {
                nodoHermanoIF = nodoElse;
            }
            else if (nodoHermanoIF.hermano == null)
            {
                nodoHermanoIF.hermano = nodoElse;
            }
            else
            {
                AgregarElse(nodoHermanoIF.hermano, nodoElse);
            }
            return nodoHermanoIF;
        }
        #endregion

        #region Crear ARBOL ASIGNACION
        public NodoArbol CrearArbolAsignacion()
        {
            var sentenciaAsignacion = NuevoNodoSentencia(TipoSentencia.ASIGNACION);
            sentenciaAsignacion.lexema = listToken[Puntero].lexema;
            Puntero += 2;
            sentenciaAsignacion.hijoIzquierdo = CrearArbolExpresion();

            //codigo p
            sentenciaAsignacion.pCode = "lda " + sentenciaAsignacion.lexema+"\n";
            sentenciaAsignacion.pCode += sentenciaAsignacion.hijoIzquierdo.pCode;
            sentenciaAsignacion.pCode += "sto\n";


            AvanzarPuntero();


            sentenciaAsignacion.soyDeTipoDato = VerificacionTipos(sentenciaAsignacion);

            while (listToken[Puntero].token==-40) //Evalua si el token es un enter y recorrerlos
            {
                AvanzarPuntero();
            }

            if (listToken[Puntero].token==-1 || listToken[Puntero].token==-102 || listToken[Puntero].token == -125)
            {
                sentenciaAsignacion.hermano = InsertNodo();
                sentenciaAsignacion.pCode += sentenciaAsignacion.hermano.pCode;
            }

            return sentenciaAsignacion;
        }
        #endregion

        #region Arbol de Expresion
        public NodoArbol CrearArbolExpresion()
        {
            NodoArbol nodoRaiz = Termino();
            while (listToken[Puntero].lexema.Equals("+")
                || listToken[Puntero].lexema.Equals("-"))
            {
                NodoArbol nodoTemp = NuevoNodoExpresion(tipoExpresion.Operador);
                nodoTemp.hijoIzquierdo = nodoRaiz;

                if (listToken[Puntero].lexema.Equals("+"))
                {
                    nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.Suma;
                    nodoTemp.pCode = "adi \n";
                }
                else if (listToken[Puntero].lexema.Equals("-"))
                {
                    nodoTemp.soyOperacionCondicionaDeTipo = OperacionCondicional.Resta;
                    nodoTemp.pCode = "sbi \n";
                }

                
                nodoTemp.lexema = listToken[Puntero].lexema;
                nodoRaiz = nodoTemp;
                Puntero++;
                nodoRaiz.hijoDerecho = Termino();

                nodoRaiz.pCode = nodoRaiz.hijoIzquierdo.pCode + nodoRaiz.hijoDerecho.pCode + nodoRaiz.pCode;
            }

            return nodoRaiz;
        }

        private NodoArbol Termino()
        {
            NodoArbol t = Factor();
            while (listToken[Puntero].lexema.Equals("*")
                 || listToken[Puntero].lexema.Equals("/"))
            {
                NodoArbol p = NuevoNodoExpresion(tipoExpresion.Operador);
                p.hijoIzquierdo = t;

                if (listToken[Puntero].lexema.Equals("*"))
                {
                    p.soyOperacionCondicionaDeTipo = OperacionCondicional.Multiplicacion;
                    p.pCode = "mpi \n";
                }
                else if (listToken[Puntero].lexema.Equals("/"))
                {
                    p.soyOperacionCondicionaDeTipo = OperacionCondicional.Division;
                    p.pCode = "dvi \n";
                }

                t.lexema = listToken[Puntero].lexema;
                t = p;
                Puntero++;
                t.hijoDerecho = Factor();

                t.pCode = t.hijoIzquierdo.pCode + t.hijoDerecho.pCode + t.pCode;
            }
            return t;
        }

        private NodoArbol Factor()
        {
            NodoArbol t = new NodoArbol();
            if (listToken[Puntero].token == -1) //IDENTIFICADOR
            {
                t = NuevoNodoExpresion(tipoExpresion.Identificador);
                t.lexema = listToken[Puntero].lexema;
                t.pCode = "lod "+t.lexema + "\n";
                //t.soyDeTipoDato = TipoDeDato.Entero;
                Puntero++;
            }
            else if (listToken[Puntero].token == -2) //ENTERO
            {
                t = NuevoNodoExpresion(tipoExpresion.Constante);
                t.soyDeTipoDato = TipoDeDato.Entero;
                t.lexema = listToken[Puntero].lexema;
                t.pCode = "ldc " + t.lexema + "\n";
                Puntero++;
            }
            else if (listToken[Puntero].token == -3)  //float
            {
                t = NuevoNodoExpresion(tipoExpresion.Constante);
                t.lexema = listToken[Puntero].lexema;
                t.soyDeTipoDato = TipoDeDato.Decimal;
                t.pCode = "ldc " + t.lexema+"\n";
                Puntero++;
            }
            else if (listToken[Puntero].token == -4)  //CADENA
            {
                t = NuevoNodoExpresion(tipoExpresion.Constante);
                t.lexema = listToken[Puntero].lexema;
                t.soyDeTipoDato = TipoDeDato.Cadena;
                Puntero++;
            }
            else if (listToken[Puntero].lexema.Equals("("))
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
            switch (listToken[Puntero].token)
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
            while (listToken[Puntero].lexema.Equals("+") || listToken[Puntero].lexema.Equals("-"))
            {
                NodoArbol nodoTemp = NuevoNodoExpresion(tipoExpresion.Operador);
                nodoTemp.hijoIzquierdo = nodoRaiz;
                nodoTemp.soyOperacionCondicionaDeTipo = listToken[Puntero].lexema.Equals("+") ? OperacionCondicional.Suma : OperacionCondicional.Resta;

                nodoTemp.lexema = listToken[Puntero].lexema;

                nodoRaiz = nodoTemp;
                Puntero++;
                nodoRaiz.hijoDerecho = Termino();
            }
            return nodoRaiz;
        }

        #region Verificacion de tipos
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
                    {
                        ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                        error.Mensaje = "No se puede sumar un " + tipoValorHijoIzquierdo.ToString() + " con un " + tipoValorHijoDerecho.ToString();
                        TablaSimbolos.ListaErroresSemanticos.Add(error);
                        return TipoDeDato.Error;
                    }
                        /*throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));*/
                case OperacionCondicional.Resta:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Entero;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Decimal;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Decimal;
                    else
                    {
                        ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                        error.Mensaje = "No se puede restar un " + tipoValorHijoIzquierdo.ToString() + " con un " + tipoValorHijoDerecho.ToString();
                        TablaSimbolos.ListaErroresSemanticos.Add(error);
                        return TipoDeDato.Error;
                    }
                    /*else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));*/
                    
                case OperacionCondicional.Multiplicacion:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Entero;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Decimal;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Decimal;
                    else
                    {
                        ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                        error.Mensaje = "No se puede multiplicar un " + tipoValorHijoIzquierdo.ToString() + " con un " + tipoValorHijoDerecho.ToString();
                        TablaSimbolos.ListaErroresSemanticos.Add(error);
                        return TipoDeDato.Error;
                    }
                    /*else
                        throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));*/
                   
                case OperacionCondicional.Division:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Decimal;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Decimal;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Decimal;
                    else
                    {
                        ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                        error.Mensaje = "No se puede dividir un " + tipoValorHijoIzquierdo.ToString() + " con un " + tipoValorHijoDerecho.ToString();
                        TablaSimbolos.ListaErroresSemanticos.Add(error);
                        return TipoDeDato.Error;
                    }
                    /*throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;*/


                //logicas
                case OperacionCondicional.IgualIgual:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Cadena && tipoValorHijoDerecho == TipoDeDato.Cadena) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Booleano && tipoValorHijoDerecho == TipoDeDato.Booleano) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Caracter && tipoValorHijoDerecho == TipoDeDato.Caracter) return TipoDeDato.Booleano;
                    else
                    {
                        ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                        error.Mensaje = "No se puede comparar si un " + tipoValorHijoIzquierdo.ToString() + " es igual que " + tipoValorHijoDerecho.ToString();
                        TablaSimbolos.ListaErroresSemanticos.Add(error);
                        return TipoDeDato.Error;
                    }
                    /*throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;*/
                case OperacionCondicional.Diferente:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Cadena && tipoValorHijoDerecho == TipoDeDato.Cadena) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Booleano && tipoValorHijoDerecho == TipoDeDato.Booleano) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Caracter && tipoValorHijoDerecho == TipoDeDato.Caracter) return TipoDeDato.Booleano;
                    else
                    {
                        ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                        error.Mensaje = "No se puede comparar si un " + tipoValorHijoIzquierdo.ToString() + " es diferente que " + tipoValorHijoDerecho.ToString();
                        TablaSimbolos.ListaErroresSemanticos.Add(error);
                        return TipoDeDato.Error;
                    }
                    /*throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;*/
                case OperacionCondicional.MayorQue:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else
                    {
                        ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                        error.Mensaje = "No se puede comparar un " + tipoValorHijoIzquierdo.ToString() + " si es mayor que " + tipoValorHijoDerecho.ToString();
                        TablaSimbolos.ListaErroresSemanticos.Add(error);
                        return TipoDeDato.Error;
                    }
                    /*throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;*/
                case OperacionCondicional.MayorIgualQue:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else
                    {
                        ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                        error.Mensaje = "No se puede comparar si un " + tipoValorHijoIzquierdo.ToString() + " es mayor o igual que " + tipoValorHijoDerecho.ToString();
                        TablaSimbolos.ListaErroresSemanticos.Add(error);
                        return TipoDeDato.Error;
                    }
                    /*throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;*/
                case OperacionCondicional.MenorQue:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else
                    {
                        ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                        error.Mensaje = "No se puede comparar si un " + tipoValorHijoIzquierdo.ToString() + " es menor que " + tipoValorHijoDerecho.ToString();
                        TablaSimbolos.ListaErroresSemanticos.Add(error);
                        return TipoDeDato.Error;
                    }
                    /*throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;*/
                case OperacionCondicional.MenorIgualQue:
                    if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Entero && tipoValorHijoDerecho == TipoDeDato.Decimal) return TipoDeDato.Booleano;
                    else if (tipoValorHijoIzquierdo == TipoDeDato.Decimal && tipoValorHijoDerecho == TipoDeDato.Entero) return TipoDeDato.Booleano;
                    else
                    {
                        ErrorSemanticoDGV error = new ErrorSemanticoDGV();
                        error.Mensaje = "No se puede comparar si un " + tipoValorHijoIzquierdo.ToString() + " es menor o igual que " + tipoValorHijoDerecho.ToString();
                        TablaSimbolos.ListaErroresSemanticos.Add(error);
                        return TipoDeDato.Error;
                    }
                    /*throw new Exception(string.Format("Error de tipos no se puede realizar la operacion {0} con {1} y {2}",
                            soyOperacion, tipoValorHijoIzquierdo, tipoValorHijoDerecho));
                    break;*/

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
            if (miArbol.hijoIzquierdo != null)
            {
                miArbol.tipoValorHijoIzquierdo = VerificacionTipos(miArbol.hijoIzquierdo);
            }
            if (miArbol.hijoCentro != null)
            {

            }
            if (miArbol.hijoDerecho != null)
            {
                miArbol.tipoValorHijoDerecho = VerificacionTipos(miArbol.hijoDerecho);
            }

            if (miArbol.soyDeTipoExpresion == tipoExpresion.Constante)
            {
                return miArbol.soyDeTipoDato;
            }
            else if (miArbol.soyDeTipoExpresion == tipoExpresion.Identificador)
            {
                return miArbol.soyDeTipoDato;
            }
            else if (miArbol.soyDeTipoExpresion == tipoExpresion.Operador)
            {
                return FuncionEquivalenciaDeDatos(miArbol.tipoValorHijoIzquierdo, miArbol.tipoValorHijoDerecho, miArbol.soyOperacionCondicionaDeTipo);
            }
            else if (miArbol.soyDeTipoExpresion == tipoExpresion.OperadorLogico)
            {
                return FuncionEquivalenciaDeDatos(miArbol.tipoValorHijoIzquierdo, miArbol.tipoValorHijoDerecho, miArbol.soyOperacionCondicionaDeTipo);
            }
            else if (miArbol.soySentenciaDeTipo == TipoSentencia.ASIGNACION)
            {
                //return FuncionEquivalenciaDeDatos(miArbol.soyDeTipoDato, miArbol.tipoValorHijoIzquierdo, miArbol.soyOperacionCondicionaDeTipo);
                //return FuncionEquivalenciaAsignacion(miArbol.soyDeTipoDato, miArbol.tipoValorHijoIzquierdo);
                return miArbol.tipoValorHijoIzquierdo;
            }

            return TipoDeDato.Vacio;
        }
        #endregion

        private bool AvanzarPuntero()
        {
            if (Puntero < listToken.Count-1)
            {
                Puntero++;
                return true;
            }
            return false;
        }
        private bool AvanzarPuntero(int salto)
        {
            if (Puntero+salto < listToken.Count-1)
            {
                Puntero += salto;
                return true;
            }
            return false;
        }


    }
}
    
