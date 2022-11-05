using static PicSimulator.Globals;

namespace PicSimulator {
    public static class Globals {
        public static int TRISA = 0;
        public static int TRISB = 0;
        public static int TRISC = 0;
        public static int TRISD = 0;
        public static int PORTA = 0;
        public static int PORTB = 0;
        public static int PORTC = 0;
        public static int PORTD = 0;
    }

    internal class Program {

        static void Main(string[] args) {
            InitPic();

            /* Inizio programma PIC */
            int[] sta = new int[] { 1, 2, 4, 8, 16, 32, 64, 128 };

            while (true) {
                for (int i = 0; i < 8; i++) {
                    PORTD = sta[i];
                    Delay_ms(1000);
                    AggiornaPic();
                }
            }
            /* Fine programma PIC */

        }

        #region helpers
        static void Delay_ms(int ms) {
            Thread.Sleep(ms);
        }

        static void AggiornaPic() {
            #region aggiorna pic
            // disegna display e led
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
            Console.SetCursorPosition(5, 3);
            Console.Write("Display 1");

            // Display 1; Segmento 1
            Console.SetCursorPosition(5, 4);
            Console.Write("----------");

            // Display 1; Segmento 2
            for (int y = 5; y < 8; y++) {
                Console.SetCursorPosition(5, y);
                Console.Write("|");
            }

            // Display 1; Segmento 3
            for (int y = 5; y < 8; y++) {
                Console.SetCursorPosition(14, y);
                Console.Write("|");
            }

            // Display 4; Segmento 1
            Console.SetCursorPosition(5, 8);
            Console.Write("----------");

            // Display 5; Segmento 1
            for (int y = 9; y < 12; y++) {
                Console.SetCursorPosition(5, y);
                Console.Write("|");
            }

            // Display 6; Segmento 1
            for (int y = 9; y < 12; y++) {
                Console.SetCursorPosition(14, y);
                Console.Write("|");
            }

            // Display 7; Segmento 1
            Console.SetCursorPosition(5, 12);
            Console.Write("----------");

            Console.SetCursorPosition(24, 3);
            Console.Write("Display 2");

            // Display 2; Segmento 1
            Console.SetCursorPosition(24, 4);
            Console.Write("----------");

            // Display 2; Segmento 2
            for (int y = 5; y < 8; y++) {
                Console.SetCursorPosition(24, y);
                Console.Write("|");
            }

            // Display 2; Segmento 3
            for (int y = 5; y < 8; y++) {
                Console.SetCursorPosition(33, y);
                Console.Write("|");
            }

            // Display 2; Segmento 4
            Console.SetCursorPosition(24, 8);
            Console.Write("----------");

            // Display 2; Segmento 5
            for (int y = 9; y < 12; y++) {
                Console.SetCursorPosition(24, y);
                Console.Write("|");
            }

            // Display 2; Segmento 6
            for (int y = 9; y < 12; y++) {
                Console.SetCursorPosition(33, y);
                Console.Write("|");
            }

            // Display 2; Segmento 7
            Console.SetCursorPosition(24, 12);
            Console.Write("----------");

        }


    }
}