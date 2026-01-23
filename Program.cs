using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace programmeringsspelprojekt
{
    internal class Program
    {
        public static Player Player1 = new Player();
        static void Main(string[] args)
        {
            int usersFirstChoice;
            bool hasCellKey = false;
           
            bool rejectsPlayerName = true;
            
            while (rejectsPlayerName == true)
            {
                Console.WriteLine("Skriv vad du vill heta!");
                Console.WriteLine("");

                Player1.playerName = (Console.ReadLine());
                Console.WriteLine("");
                Console.WriteLine("Du har valt " + Player1.playerName + " som namn för din spelare. Är det rätt? (Y/N)");
                Console.WriteLine("");

                var playerApprovesNameKey = Console.ReadKey().Key;
                Console.WriteLine("");
                if (playerApprovesNameKey == ConsoleKey.Y)
                {
                    rejectsPlayerName = false;   
                }
                else if (playerApprovesNameKey == ConsoleKey.N)
                {
                    rejectsPlayerName = true;
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Du måste skriva Y eller N");
                    Console.WriteLine("");
                }
            }
            Player1.playerHealth = Player1.playerBaseHealth;
            Console.Clear();
            Console.WriteLine("Du har vaknat i en cell. Kall luft blåser in från ett litet fönster, och snöflingor ladnar vid dina fötter. Du måste ta dig ut på något sätt! Du var ju falskt anklagad, naturligtvis! Varför skulle *du* någonsin råna tre banker i rad?! Det kunde ju inte hänt???!");
            Console.WriteLine("Det finns bara en säng, ett handfat och det kalla, öppna fönstret. Vad gör du?");
            Console.WriteLine("");
            while (hasCellKey == false)
            {
                if (Player1.playerHealth > 0)
                {
                    Player1.PlayerStatPrint();
                    Console.WriteLine("");

                    Console.WriteLine("\n Skriv 1 för att leta under sängen \n Skriv 2 för att leta runt handfatet \n Skriv 3 för att leta utanför fönstret");
                    Console.WriteLine("");

                    bool success = int.TryParse(Console.ReadLine(), out usersFirstChoice); //LÄR DIG DENNA!!!
                    if (!success || usersFirstChoice < 1 || usersFirstChoice > 3)
                    {
                        Console.WriteLine("Snälla windows jag behöver detta, min kod är ganska ass");
                    }
                    else if (usersFirstChoice == 1)
                    {
                        Console.WriteLine("Efter mycket sökande lyckades du inte hitta någonting. Försök igen?");
                    }
                    else if (usersFirstChoice == 2)
                    {
                        Console.WriteLine("Bakom vattenkranan har en tidigare fånge klämt fast något. Det ser ut som en krokig bit metall, som verkar passa celldörrens nyckelhål ganska bra.");
                        hasCellKey = true;
                    }
                    else if (usersFirstChoice == 3)
                    {
                        Console.WriteLine("Efter mycket sökande lyckades du inte hitta någonting, och fick dessutom ont av kylan." + "\n" + "Du tog 10 skada!" + "\n");
                        Player1.TakeDamage(10);
                        if (Player1.playerHealth >0)
                        {
                            Console.WriteLine("Försök igen?");
                        } 
                    }         
                }
                else
                {
                    break; 
                }
            }
            
            Room room1 = new Room(Player1);
            room1.start();
        }
    }
    public class Player
    {
        public string playerName;
        public int playerHealth;
        public int playerBaseHealth = 50;
        public void PlayerStatPrint()
        {
            Console.WriteLine("PLayer: \"" + playerName + "\"");
            if (playerHealth == playerBaseHealth)
            {
                Console.WriteLine("Player health: " + playerHealth + "/" + playerBaseHealth + " (MAX!!)");
            }
            else
            {
                Console.WriteLine("Player health: " + playerHealth + "/" + playerBaseHealth);
            }
        }
        public void TakeDamage(int damageTaken)
        {
            playerHealth -= damageTaken;

            if (playerHealth <0)
            {
                playerHealth = 0;
            }
            if (playerHealth == 0)
            {
                Console.WriteLine("Du dog.");
             
            }
            if(playerHealth != 0)
            {
                PlayerStatPrint();
            }
        }

        public void GainHealth(int healthGained)
        {
            playerHealth += healthGained;

            if (playerHealth > playerBaseHealth)
            {
                playerHealth = playerBaseHealth;
            }

            Console.WriteLine("Du känner dig friskare. [+" + healthGained + " hälsa]");

            PlayerStatPrint();
        }
        public void SimpleAttack1(Enemy enemy)
        {
            Console.WriteLine("Du slog fienden!");
            enemy.TakeDamage(10);
        }
        public void SimpleAttack2(Enemy enemy)
        {
            Console.WriteLine("Du slog fienden hårdare!");
            enemy.TakeDamage(15);
        }
    }
    public class Enemy
    {
        public string enemyName;
        public int enemyHealth = 50;
        public int enemyBaseHealth = 50;

        public void EnemyEncounter()
        {
            Console.WriteLine(enemyName + "dök upp.");
        }

        public void EnemyStatPrint()
        {
            Console.WriteLine("Enemy: \"" + enemyName + "\"");
            if (enemyHealth == enemyBaseHealth)
            {
                Console.WriteLine("Player health: " + enemyHealth + "/" + enemyBaseHealth + " (MAX!!)");
            }
            else
            {
                Console.WriteLine("Player health: " + enemyHealth + "/" + enemyBaseHealth);
            }
        }
        public void TakeDamage(int damageTaken)
        {
            enemyHealth -= damageTaken;

            if (enemyHealth <0)
            {
                enemyHealth = 0;
            }

            if (enemyHealth != 0)
            {
                EnemyStatPrint();
            }
        }

        public void GainHealth(int healthGained)
        {
            enemyHealth += healthGained;

            if (enemyHealth > enemyBaseHealth)
            {
                enemyHealth = enemyBaseHealth;
            }
            EnemyStatPrint();
        }

        public void SimpleAttack1(Player player1)
        {
            Console.WriteLine(enemyName + " slog dig hårt!");
            player1.TakeDamage(10);
        }
        public void SimpleAttack2(Player player1)
        {
            Console.WriteLine(enemyName + " sparkade dig hårt!");
            player1.TakeDamage(15);
        }
    }
    public class Encounter
    {
        public bool enemyTurn = false;
        public bool playerTurn = true;
        public int dodgeChance = 0;
        private Enemy enemy;
        public Player player;
        public Encounter(Player player, Enemy enemy)
        {
            this.enemy = enemy;
            this.player = player;
        }
        
        public void start()
        {
            Console.WriteLine(enemy.enemyName + " dök upp!");
          if (playerTurn == true)
            {
                Console.WriteLine("Välj vad du ska göra");
                Console.WriteLine("1. Slå fienden \n 2. Sparka fienden \n 3. Försök att undvika fiendens attack (+15% chans att fiendens attack missar)");
            }
        }
    }
    public class Room
    {
        public Player Player;
        public Room(Player player)
        { this.Player = player; }
        public bool hasEnemy;
        public string roomSize;
        public int numberOfExits;
        public string ExitsArePlural;
        public bool hasRoomKey = false;

        public void start()
        {
            Random rand = new Random();

            numberOfExits = rand.Next(0, 11);
            if (numberOfExits > 5)
            {
                roomSize = "stort";
            }
            else
            {
                roomSize = "litet";
            }

            if (numberOfExits > 1)
            {
                ExitsArePlural = "utgångar";
            }
            else
            {
                ExitsArePlural = "utgång";
            }

            int keyRequired = rand.Next(0, 3);
            int hasEnemyChecker = rand.Next(0, 3);

            Console.WriteLine("Du tar dig in i det nya rummet");
            Console.WriteLine("Rummet är " + roomSize + " och har " + numberOfExits + " " + ExitsArePlural);
            if (keyRequired > 1 && numberOfExits == 1)
            {
                Console.WriteLine("Det verkar som om dörren är låst och du behöver en nyckel.");

            }
            if(hasEnemyChecker > 1)
            {
                
                string enemyName = ("placeholder i guess. supposed to grab from a new enemy object.");
                Console.WriteLine("En " + " dök upp från ingenstans!");
                string[] names = { "Väktare", "Tjuv" };
                Random rand2 = new Random();
                Enemy e = new Enemy();
                e.enemyName = names[rand2.Next(names.Length)];

                Encounter enc = new Encounter(Player, e);
                enc.start();
            }

        }
        
    }
}
