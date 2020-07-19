using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LenguajesyAutomatas
{
    public class Token
    {
        public int token { get; set; }
        public string lexema { get; set; }
        public int linea { get; set; }
        public TipoToken tipoToken { get; set; }
    }
    public enum TipoToken
    {
        SimboloSimple,
        OperadorAritmetico,
        OperadorDeComparacion,
        OperadorDeAsignacion,
        PalabraReservada,
        Identificador,
        Entero,
        Decimal,
        Cadena
    }

   
    public class Lexico
    {

        private int[,] matrizdetransicionLex =
        {
                 //  A-Z ||  a-z ||  0-9  ||   _   ||    .   ||   #   ||   "   ||   '   ||   +   ||   -   ||   =   ||   *   ||   /   ||   %   ||   !   ||   <   ||   >   ||   |   ||   &   ||   :   ||   ,   ||   ;   ||   )   ||   (   ||   ]   ||   [   ||   }   ||   {   ||   Enter   ||   TAB   ||   Espacio   ||   O.C   ||   EoF   ||
        /*  0  */{    1   ,   1   ,   2    ,   1    ,   24    ,   5    ,   6    ,   6    ,   13   ,   14   ,   20   ,   15   ,   17   ,   19   ,   21   ,   22   ,   23   ,  -28   ,  -27   ,  -29   ,  -32   ,  -31   ,  -34   ,  -33   ,  -36   ,  -35   ,  -38   ,  -37   ,    -40     ,   -39    ,     -41      ,  -500    ,    0     },
        /*  1  */{    1   ,   1   ,   1    ,   1    ,  -1     ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,  -1    ,    -1      ,   -1     ,     -1       ,  -500      ,   -1     },
        /*  2  */{   -2   ,  -2   ,   2    ,  -2    ,   3     ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,  -2    ,    -2      ,   -2     ,     -2       ,  -500      ,   -2     },
        /*  3  */{   -501 ,  -501 ,   4    ,  -501  ,  -501   ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,  -501  ,    -501    ,   -501   ,     -501     ,  -501    ,   -501   },
        /*  4  */{   -3   ,  -3   ,   4    ,  -3    ,  -506     ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,  -3    ,    -3      ,   -3     ,     -3       ,  -3      ,   -3     },
        /*  5  */{    5   ,   5   ,   5    ,   5    ,   5     ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,   5    ,     0      ,    5     ,      5       ,   5      ,    5     },
        /*  6  */{    12  ,   12  ,   12   ,   12   ,   12    ,   12   ,   7    ,   7    ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,    -502    ,    12    ,      12      ,   12     ,   -503   },
        /*  7  */{   -4   ,  -4   ,  -4    ,  -4    ,  -4     ,  -4    ,   8    ,   8    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,    -4      ,   -4     ,     -4       ,  -4      ,   -4     },
        /*  8  */{    8   ,   8   ,   8    ,   8    ,   8     ,   8    ,   9    ,   9    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,     8      ,    8     ,      8       ,   8      ,    8     },
        /*  9  */{    8   ,   8   ,   8    ,   8    ,   8     ,   8    ,   10   ,   10   ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,     8      ,    8     ,      8       ,   8      ,    9     },
        /* 10  */{    8   ,   8   ,   8    ,   8    ,   8     ,   8    ,   11   ,   11   ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,   8    ,     8      ,    8     ,      8       ,   8      ,    10    },
        /* 11  */{    0   ,   0   ,   0    ,   0    ,   0     ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,   0    ,     0      ,    0     ,      0       ,   0      ,    0     },
        /* 12  */{    12  ,   12  ,   12   ,   12   ,   12    ,   12   ,  -4    ,  -4    ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,   12   ,    -502    ,    12    ,      12      ,   12     ,   -504   },
        /* 13  */{   -5   ,  -5   ,  -5    ,  -5    ,  -5     ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -20   ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,    -5      ,   -5     ,     -5       ,  -5      ,   -5     },
        /* 14  */{   -6   ,  -6   ,  -6    ,  -6    ,  -6     ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -21   ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,  -6    ,    -6      ,   -6     ,     -6       ,  -6      ,   -6     },
        /* 15  */{   -7   ,  -7   ,  -7    ,  -7    ,  -7     ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -22   ,   16   ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,  -7    ,    -7      ,   -7     ,     -7       ,  -7      ,   -7     },
        /* 16  */{   -10  ,  -10  ,  -10   ,  -10   ,  -10    ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -25   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,  -10   ,    -10     ,   -10    ,     -10      ,  -10     ,   -10    },
        /* 17  */{   -8   ,  -8   ,  -8    ,  -8    ,  -8     ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -23   ,  -8    ,   18   ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,  -8    ,    -8      ,   -8     ,     -8       ,  -8      ,   -8     },
        /* 18  */{   -11  ,  -11  ,  -11   ,  -11   ,  -11    ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -26   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,  -11   ,    -11     ,   -11    ,     -11      ,  -11     ,   -11    },
        /* 19  */{   -9   ,  -9   ,  -9    ,  -9    ,  -9     ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -24   ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,  -9    ,    -9      ,   -9     ,     -9       ,  -9      ,   -9     },
        /* 20  */{   -19  ,  -19  ,  -19   ,  -19   ,  -19    ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -12   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,    -19     ,   -19    ,     -19      ,  -19     ,   -19    },
        /* 21  */{   -505 ,  -505 ,  -505  ,  -505  ,  -505   ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -13   ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,  -505  ,    -505    ,   -505   ,     -505     ,  -505    ,   -505   },
        /* 22  */{   -16  ,  -16  ,  -16   ,  -16   ,  -16    ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -18   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -14   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,    -16     ,   -16    ,     -16      ,  -16     ,   -16    },
        /* 23  */{   -15  ,  -15  ,  -15   ,  -15   ,  -15    ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -17   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,    -15     ,   -15    ,     -15      ,  -15     ,   -15    },
        /* 24  */{   -30  ,  -30  ,   4    ,  -30   ,  -507   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,  -30   ,    -30     ,   -30    ,     -30      ,  -500     ,   -30    },
        };

        private string[,] palabrasreservada = { //||     0     ||     1     ||     2     ||     3     ||     4     ||     5     ||     6     ||     7     ||     8     ||     9     ||     10     ||     11     ||     12     ||     13     ||     14     ||     15     ||     16     ||     17     ||     18     ||     19     ||     20     ||     21     ||     22     ||     23     ||     24     ||     25     ||     26     ||     27     ||     28     ||     29     ||     30     ||     31     ||     32     ||     33     ||     34     ||     35     ||     36     ||
                                                {    "False"    ,   "def"    ,    "if"    ,   "raise"  ,   "None"   ,   "del"    ,  "import"  ,  "return"  ,   "True"   ,   "elif"   ,    "in"     ,    "try"    ,    "and"    ,    "else"   ,    "is"     ,   "while"   ,    "as"     ,   "except"  ,   "lambda"  ,   "with"    ,   "assert"  ,  "finally"  , "nonlocal"  ,   "yield"   ,   "break"   ,    "for"    ,    "not"    ,   "class"   ,   "from"    ,    "or"     ,  "continue" ,  "global"   ,   "pass"    ,   "print"   ,    "self"   ,   "input"   ,   "range"   },
                                                {     "-100"    ,   "-101"   ,   "-102"   ,   "-103"   ,   "-104"   ,  "-105"    ,   "-106"   ,   "-107"   ,   "-108"   ,   "-109"   ,   "-110"    ,   "-111"    ,    "-112"   ,    "-113"   ,   "-114"    ,    "-115"   ,   "-116"    ,    "-117"   ,    "-118"   ,   "-119"    ,    "-120"   ,    "-121"   ,   "-122"    ,    "-123"   ,    "-124"   ,   "-125"    ,    "-126"   ,    "-127"   ,   "-128"    ,   "-129"    ,    "-130"   ,   "-131"    ,   "-132"    ,    "-133"   ,    "-134"   ,   "-135"    ,    "-136"   },
        };

        public List<Token> EjecutarLexico(string codigofuente)
        {
            int _linea = 1, estadoactual = 0;
            string _lexema = "", caracter;
            TipoToken tipo;
            List<Token> listaDeTokens = new List<Token>();
            
            for (int puntero = 0; puntero <= codigofuente.Length  ; puntero++)
            {
               
                if (puntero == codigofuente.Length && estadoactual >= 0)
                {
                    caracter = "EoF";
                    estadoactual = Analizador(caracter, estadoactual);
                    if (estadoactual !=0 && estadoactual<0)
                    {
                        if (estadoactual == -1 && VerificarPalabraReservada(_lexema) < 0) 
                        {
                            estadoactual = VerificarPalabraReservada(_lexema);
                            tipo = _tipodetoken(estadoactual);
                            listaDeTokens.Add(new Token() { token = estadoactual, lexema = _lexema, linea = _linea, tipoToken = tipo });
                            _lexema = "";

                        }
                        else if (estadoactual < -499)
                        {
                            _lexema += caracter;
                            MessageBox.Show("Error Lexico Linea: " + _linea + "\n" + ErroresLexicos(estadoactual));

                        }
                        else
                        {
                            tipo = _tipodetoken(estadoactual);
                            listaDeTokens.Add(new Token() { token = estadoactual, lexema = _lexema, linea = _linea, tipoToken = tipo});
                            _lexema = "";
                        }
                    }

                }
                else
                {

                    caracter = codigofuente.Substring(puntero, 1);
                    estadoactual = Analizador(caracter, estadoactual);
                    if (estadoactual > 0)
                    {
                        _lexema += caracter;
                    }
                    else
                    {
                        if (estadoactual == -4 || (estadoactual < -11 && estadoactual > -27 && estadoactual != -15 && estadoactual != -16 && estadoactual != -19) )
                        {
                            if (estadoactual==-4)
                            {
                                if (caracter=="\"" || caracter == "\'")
                                {
                                    _lexema += caracter;
                                    puntero++;
                                }
                                tipo = _tipodetoken(estadoactual);
                                listaDeTokens.Add(new Token() { token = estadoactual, lexema = _lexema, linea = _linea, tipoToken = tipo });
                                estadoactual = 0;
                                _lexema = "";
                                puntero--;
                                
                            }
                            else
                            {
                                _lexema += caracter;
                                tipo = _tipodetoken(estadoactual);
                                listaDeTokens.Add(new Token() { token = estadoactual, lexema = _lexema, linea = _linea, tipoToken = tipo });
                                estadoactual = 0;
                                _lexema = "";
                            }
                            
                        }
                        else
                        {
                            if (estadoactual < -26 && estadoactual > -42 || (estadoactual<-499))
                            {
                                tipo = _tipodetoken(estadoactual);

                                if (estadoactual == -39)
                                {
                                    _lexema = "Tab";
                                }
                                else if (estadoactual == -40)
                                {
                                    _lexema = "Enter";
                                }
                                else if (estadoactual == -41)
                                {
                                    _lexema = "Espacio";
                                }
                                else if (estadoactual < -499)
                                {


                                    while ((caracter != "\t") && (caracter != "\n") && puntero < codigofuente.Length-1)
                                    {
                                        _lexema += caracter;
                                        puntero++;
                                        caracter = codigofuente.Substring(puntero, 1);
                                    }

                                  /*  if (caracter == "\t" || caracter =="\n" || caracter == " ")
                                    {
                                        puntero--;
                                    }*/ // CODIGO PARA QUE TE TOME LOS ESPACIO TAB Y ENTER DESPUES DE ERROR

                                    puntero--;
                                    
                                    MessageBox.Show("Error Lexico Linea: " + _linea + "\n" + ErroresLexicos(estadoactual));
                                }
                                else if (estadoactual == -30)
                                {
                                    puntero--;
                                }
                                else
                                {
                                    _lexema += caracter;
                                }
                                puntero++;

                            }
                            if (estadoactual < -499)
                            {
                                estadoactual = 0;
                                _lexema = "";
                            }
                            else
                            {
                                if (estadoactual == -1 && VerificarPalabraReservada(_lexema) <0)
                                {
                                    estadoactual = VerificarPalabraReservada(_lexema);
                                    tipo = _tipodetoken(estadoactual);

                                }
                                tipo = _tipodetoken(estadoactual);
                                if (estadoactual != 0)
                                {
                                    listaDeTokens.Add(new Token() { token = estadoactual, lexema = _lexema, linea = _linea, tipoToken = tipo });
                                }
                                if (estadoactual == -40)
                                {
                                    _linea++;
                                }
                                puntero--;
                                estadoactual = 0;
                                _lexema = "";
                            }
                        }

                    }

                }

            }

            return listaDeTokens;
        }

        public int Analizador(string caracter, int estadoRequerido)
        {
            
            int estadoMatriz = 0;
            
            Regex letraMayuscula = new Regex("[A-Z]");
            Regex letraMinuscula = new Regex("[a-z]");
            Regex digito = new Regex("[0-9]");

            if (letraMayuscula.IsMatch(caracter) && caracter != "EoF")
            {
                estadoMatriz = matrizdetransicionLex[estadoRequerido, 0];
            }
            else if (letraMinuscula.IsMatch(caracter) && caracter != "EoF")
            {
                estadoMatriz = matrizdetransicionLex[estadoRequerido, 1];
            }
            else if (digito.IsMatch(caracter))
            {
                estadoMatriz = matrizdetransicionLex[estadoRequerido, 2];
            }
            else
            {
                switch (caracter)
                {
                    case "_":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 3];
                        break;
                    case ".":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 4];
                        break;
                    case "#":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 5];
                        break;
                    case "\"":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 6];
                        break;
                    case "\'":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 7];
                        break;
                    case "+":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 8];
                        break;
                    case "-":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 9];
                        break;
                    case "=":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 10];
                        break;
                    case "*":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 11];
                        break;
                    case "/":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 12];
                        break;
                    case "%":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 13];
                        break;
                    case "!":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 14];
                        break;
                    case "<":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 15];
                        break;
                    case ">":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 16];
                        break;
                    case "|":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 17];
                        break;
                    case "&":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 18];
                        break;
                    case ":":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 19];
                        break;
                    case ",":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 20];
                        break;
                    case ";":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 21];
                        break;
                    case ")":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 22];
                        break;
                    case "(":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 23];
                        break;
                    case "]":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 24];
                        break;
                    case "[":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 25];
                        break;
                    case "}":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 26];
                        break;
                    case "{":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 27];
                        break;
                    case "\n":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 28];
                        break;
                    case "\t":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 29];
                        break;
                    case " ":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 30];
                        break;
                    case "EoF":
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 32];
                        break;
                    default:
                        estadoMatriz = matrizdetransicionLex[estadoRequerido, 31];
                        break;
                }
            }

            return estadoMatriz;
        }

        public int VerificarPalabraReservada(string palabra)
        {
            int estado = 0;
            for (int i = 0; i < 37; i++)
            {
                if (palabra == palabrasreservada[0,i])
                {
                    estado = Convert.ToInt32(palabrasreservada[1, i]);
                }
            }

            return estado;
        }

        public TipoToken _tipodetoken(int estado)
        {
            TipoToken _tipo = TipoToken.SimboloSimple;
            if (estado == -1)
            {
                _tipo = TipoToken.Identificador;
            }
            else if (estado == -2)
            {
                _tipo = TipoToken.Entero;
            }
            else if (estado == -3)
            {
                _tipo = TipoToken.Decimal;
            }
            else if (estado == -4)
            {
                _tipo = TipoToken.Cadena;
            }
            else if (estado <= -5 && estado >= -11)
            {
                _tipo = TipoToken.OperadorAritmetico;
            }
            else if (estado <= -12 && estado >= -18)
            {
                _tipo = TipoToken.OperadorDeComparacion;
            }
            else if (estado <= -19 && estado >= -26)
            {
                _tipo = TipoToken.OperadorDeAsignacion;
            }
            else if (estado <= -100 && estado >=-136)
            {
                _tipo = TipoToken.PalabraReservada;
            }
            else
            {
                _tipo = TipoToken.SimboloSimple;
            }

            return _tipo;
        }

        public string ErroresLexicos(int estado)
        {
            string _error = "";
            switch (estado)
            {
                case -500:
                    _error = "Simbolo desconocido";
                    break;
                case -501:
                    _error = "Se esperaba un digito";
                    break;
                case -502:
                    _error = "No se puede dar enter en una cadena de caracteres";
                    break;
                case -503:
                    _error = "Se esperaba algun caracter";
                    break;
                case -504:
                    _error = "Se esperaban commillas";
                    break;
                case -505:
                    _error = "Se esperaba un igual";
                    break;
                case -506:
                    _error = "No se pone punto despues de un numero decimal";
                    break;
                case -507:
                    _error = "No se pude poner puntos seguidos";
                    break;
            }
            return _error;
        }

    }
}
