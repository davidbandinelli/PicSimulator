using static PicSimulator.Globals;
/*
 PIC Simulator v0.1
 Autore: David Bandinelli (20221105)
*/

// valori decimali per accensione segmenti display per rappresentare le cifre da 0 a 9
/* 0=1110111=119, 1=0010010=36, 2=1011101=93, 3=1011011=109, 4=0101010=42, 51101011==107, 6=1101111=123, 7=1010010=37, 8=1111111=127, 9=1111011=111 */
/*
  ogni bit di PORTA e PORTB controllano quale segmento del display accendere

  ------ (1)
(2) |    | (3)
  |    |
  ------ (4)
(5) |    | (6)
  |    |
  ------ (7)

*/

namespace PicSimulator {

    // questa classe simula i registri del PIC
    public static class Globals {
        public static int TRISA = 0;
        public static int TRISB = 0;
        public static int TRISC = 0;
        public static int TRISD = 0;
        
        // controlla il display a 7 segmenti 1
        public static int PORTA = 0;
        
        // controlla il display a 7 segmenti 2
        public static int PORTB = 0;

        public static int PORTC = 0;

        // controlla gli 8 LED
        public static int PORTD = 0;
    }

    internal class Program {

        static void Main(string[] args) {
            InitPic();

            /* Inizio programma PIC */
            int[] led = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };
            // cifre da 0 a 9
            int[] cifre = new int[] { 119, 36, 93, 109, 42, 107, 123, 37, 127, 111 };

            while (true) {
                // ESERCIZIO 5
                // TODO contatore da 0 a 99 sui due display e visualizzazione del numero in binario tramite led (7 è il più significativo)

                // ESERCIZIO 4
                // contatore da 0 a 99 sui due display
                for (int d=0; d<10; d++) {
                    PORTA = cifre[d];
                    for (int u = 0; u<10; u++) {
                        PORTB = cifre[u];
                        Delay_ms(500);
                        AggiornaPic();
                    }
                }

                // ESERCIZIO 3
                /*
                // conta da 0 a 9 sul display 1
                for (int n = 0; n < 10; n++) {
                    PORTA = cifre[n];
                    Delay_ms(500);
                    AggiornaPic();
                }
                */

                // ESERCIZIO 2
                /*
                // conta da 0 a 9 sul display 1 
                for (int n = 0; n < 10; n++) {
                    PORTB = cifre[n];
                    Delay_ms(500);
                    AggiornaPic();
                }
                */

                // ESERCIZIO 1
                /*
                // accensione led da sinistra a destra
                for (int i = 0; i < 8; i++) {
                    PORTD = led[i];
                    Delay_ms(1000);
                    AggiornaPic();
                }
                */
            }
            /* Fine programma PIC */

        }

        #region funzioni di utilità
        static void Delay_ms(int ms) {
            Thread.Sleep(ms);
        }

        static void AggiornaPic() {
            #region aggiorna pic
            // visualizza display e led
            Pic.DrawDisplays();
            Pic.DrawLeds();
            // stampa contenuto registri
            Pic.DrawRegs();
            Console.SetCursorPosition(0, 20);
            #endregion
        }

        static void InitPic() {
            Pic.DrawBorder();
            AggiornaPic();
        }
        #endregion
    }

    // questa classe rappresenta la parte visuale del PIC con 2 display a 7 segmenti e 8 led
    public static class Pic {
        // disegna il bordo esterno
        public static void DrawBorder() {
            Console.SetCursorPosition(0, 0);
            Console.Write("----------------------------------------");
            for (int y = 1; y < 20; y++) {
                Console.SetCursorPosition(0, y);
                Console.Write("|");
                Console.SetCursorPosition(39, y);
                Console.Write("|");
            }
            Console.SetCursorPosition(0, 20);
            Console.Write("----------------------------------------");
        }

        public static void DrawRegs() {
            Console.SetCursorPosition(3, 18);
            Console.Write("PORTA=" + "   ");
            Console.SetCursorPosition(3, 18);
            Console.Write("PORTA=" + PORTA);

            Console.SetCursorPosition(12, 18);
            Console.Write("PORTB=" + "   ");
            Console.SetCursorPosition(12, 18);
            Console.Write("PORTB=" + PORTB);

            Console.SetCursorPosition(21, 18);
            Console.Write("PORTC=" + "   ");
            Console.SetCursorPosition(21, 18);
            Console.Write("PORTC=" + PORTC);

            Console.SetCursorPosition(30, 18);
            Console.Write("PORTD=" + "   ");
            Console.SetCursorPosition(30, 18);
            Console.Write("PORTD=" + PORTD);
        }

        public static void DrawLeds() {
            Console.SetCursorPosition(5, 14);
            Console.Write("PORTD");
            // converte PORTD in binario e lo inverte perchè nella visualizzazione il bit più significativo è a destra
            var stringBin = Convert.ToString(PORTD, 2).PadLeft(8, '0');
            string reversedBin = String.Empty;
            for (int i = stringBin.Length - 1; i > -1; i--) {
                reversedBin += stringBin[i];
            }
            // accende i led in base al contenuto di PORTD
            Console.SetCursorPosition(5, 15);
            for (int i=0; i<8; i++) {
                char tmp = reversedBin[i];
                if (tmp == '1') {
                    Console.Write("O");
                } else {
                    Console.Write("X");
                }
                Console.Write("  ");
            }
            Console.SetCursorPosition(5, 16);
            for (int i = 0; i < 8; i++) {
                Console.Write(i);
                Console.Write("  ");
            }
        }

        public static void DrawDisplays() {
            #region display 1
            // converte PORTA in binario e lo inverte perchè nella visualizzazione il bit più significativo è a destra
            var stringBin = Convert.ToString(PORTA, 2).PadLeft(7, '0');
            string reversedBin = String.Empty;
            for (int i = stringBin.Length - 1; i > -1; i--) {
                reversedBin += stringBin[i];
            }
            Console.SetCursorPosition(5, 2);
            Console.Write("PORTA");
            Console.SetCursorPosition(5, 3);
            Console.Write("Display 1");

            for (int i = 0; i < 7; i++) {
                char tmp = reversedBin[i];

                switch(i) {
                    case 0:
                        // Display 1; Segmento 1
                        Console.SetCursorPosition(5, 4);
                        if (tmp == '1') {
                            Console.Write("----------");
                        } else {
                            Console.Write("          ");
                        }
                        break;
                    case 1:
                        // Display 1; Segmento 2
                        if (tmp == '1') {
                            for (int y = 5; y < 8; y++) {
                                Console.SetCursorPosition(5, y);
                                Console.Write("|");
                            }
                        } else {
                            for (int y = 5; y < 8; y++) {
                                Console.SetCursorPosition(5, y);
                                Console.Write(" ");
                            }
                        }
                        break;
                    case 2:
                        // Display 1; Segmento 3
                        if (tmp == '1') {
                            for (int y = 5; y < 8; y++) {
                                Console.SetCursorPosition(14, y);
                                Console.Write("|");
                            }
                        } else {
                            for (int y = 5; y < 8; y++) {
                                Console.SetCursorPosition(14, y);
                                Console.Write(" ");
                            }
                        }
                        break;
                    case 3:
                        // Display 1; Segmento 4
                        Console.SetCursorPosition(5, 8);
                        if (tmp == '1') {
                            Console.Write("----------");
                        } else {
                            if (PORTA > 0) {
                                if (PORTA == 36 || PORTA == 37) {
                                    Console.Write("         |");
                                } else {
                                    Console.Write("|        |");
                                }
                            } else {
                                Console.Write("          ");
                            }
                        }
                        break;
                    case 4:
                        // Display 1; Segmento 5
                        if (tmp == '1') {
                            for (int y = 9; y < 12; y++) {
                                Console.SetCursorPosition(5, y);
                                Console.Write("|");
                            }
                        } else {
                            for (int y = 9; y < 12; y++) {
                                Console.SetCursorPosition(5, y);
                                Console.Write(" ");
                            }
                        }
                        break;
                    case 5:
                        // Display 1; Segmento 6
                        if (tmp == '1') {
                            for (int y = 9; y < 12; y++) {
                                Console.SetCursorPosition(14, y);
                                Console.Write("|");
                            }
                        } else {
                            for (int y = 9; y < 12; y++) {
                                Console.SetCursorPosition(14, y);
                                Console.Write(" ");
                            }
                        }
                        break;
                    case 6:
                        // Display 1; Segmento 7
                        Console.SetCursorPosition(5, 12);
                        if (tmp == '1') {
                            Console.Write("----------");
                        } else {
                            Console.Write("          ");
                        }
                        break;
                }
            }
            #endregion

            #region display 2
            // converte PORTA in binario e lo inverte perchè nella visualizzazione il bit più significativo è a destra
            stringBin = Convert.ToString(PORTB, 2).PadLeft(7, '0');
            reversedBin = String.Empty;
            for (int i = stringBin.Length - 1; i > -1; i--) {
                reversedBin += stringBin[i];
            }
            Console.SetCursorPosition(24, 2);
            Console.Write("PORTB");
            Console.SetCursorPosition(24, 3);
            Console.Write("Display 2");

            for (int i = 0; i < 7; i++) {
                char tmp = reversedBin[i];

                switch (i) {
                    case 0:
                        // Display 1; Segmento 1
                        Console.SetCursorPosition(24, 4);
                        if (tmp == '1') {
                            Console.Write("----------");
                        } else {
                            Console.Write("          ");
                        }
                        break;
                    case 1:
                        // Display 1; Segmento 2
                        if (tmp == '1') {
                            for (int y = 5; y < 8; y++) {
                                Console.SetCursorPosition(24, y);
                                Console.Write("|");
                            }
                        } else {
                            for (int y = 5; y < 8; y++) {
                                Console.SetCursorPosition(24, y);
                                Console.Write(" ");
                            }
                        }
                        break;
                    case 2:
                        // Display 1; Segmento 3
                        if (tmp == '1') {
                            for (int y = 5; y < 8; y++) {
                                Console.SetCursorPosition(33, y);
                                Console.Write("|");
                            }
                        } else {
                            for (int y = 5; y < 8; y++) {
                                Console.SetCursorPosition(33, y);
                                Console.Write(" ");
                            }
                        }
                        break;
                    case 3:
                        // Display 1; Segmento 4
                        Console.SetCursorPosition(24, 8);
                        if (tmp == '1') {
                            Console.Write("----------");
                        } else {
                            if (PORTB > 0) {
                                if (PORTB == 36 || PORTB == 37) {
                                    Console.Write("         |");
                                } else {
                                    Console.Write("|        |");
                                }
                            } else {
                                Console.Write("          ");
                            }
                        }
                        break;
                    case 4:
                        // Display 1; Segmento 5
                        if (tmp == '1') {
                            for (int y = 9; y < 12; y++) {
                                Console.SetCursorPosition(24, y);
                                Console.Write("|");
                            }
                        } else {
                            for (int y = 9; y < 12; y++) {
                                Console.SetCursorPosition(24, y);
                                Console.Write(" ");
                            }
                        }
                        break;
                    case 5:
                        // Display 1; Segmento 6
                        if (tmp == '1') {
                            for (int y = 9; y < 12; y++) {
                                Console.SetCursorPosition(33, y);
                                Console.Write("|");
                            }
                        } else {
                            for (int y = 9; y < 12; y++) {
                                Console.SetCursorPosition(33, y);
                                Console.Write(" ");
                            }
                        }
                        break;
                    case 6:
                        // Display 1; Segmento 7
                        Console.SetCursorPosition(24, 12);
                        if (tmp == '1') {
                            Console.Write("----------");
                        } else {
                            Console.Write("          ");
                        }
                        break;
                }
            }
            #endregion
        }
    }
}