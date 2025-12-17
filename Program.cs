namespace programmeringsspelprojekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player Player1 = new Player();
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
            
            Console.WriteLine("Du har vaknat i en cell. Kall luft blåser in från ett litet fönster, och snöflingor ladnar vid dina fötter. Du måste ta dig ut på något sätt! Du var ju falskt anklagad, naturligtvis! Varför skulle *du* någonsin råna tre banker i rad?! Det kunde ju inte hänt???!");
            Console.WriteLine("Det finns bara en säng, ett handfat och det kalla, öppna fönstret. Vad gör du?");
            Console.WriteLine("");
            while (hasCellKey == false)
            {
                if (Player1.playerHealth > 0)
                {
                    Console.WriteLine("PLayer: \"" + Player1.playerName + "\"");
                    if (Player1.playerHealth == Player1.playerBaseHealth)
                    {
                        Console.WriteLine("Player health: " + Player1.playerHealth + "/" + Player1.playerBaseHealth + " (MAX!!)");
                    }
                    else
                    {
                        Console.WriteLine("Player health: " + Player1.playerHealth + "/" + Player1.playerBaseHealth);
                    }
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
                        Console.WriteLine("du valde val 2 och hittade nyckeln!! wow du vann!");
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
           
            if (Player1.playerHealth <= 0)
            {
                Console.WriteLine("Du är död.");
                    return;
            }

            //escape sequence and stuff
            Console.WriteLine("placeholder text");



            Enemy vakt = new Enemy();

        }
    }
    public class Player
    {
        public string playerName;
        public int playerHealth;
        public int playerBaseHealth = 50;

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
        }

        public void GainHealth(int healthGained)
        {
            playerHealth += healthGained;

            if (playerHealth > playerBaseHealth)
            {
                playerHealth = playerBaseHealth;
            }
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
       

        public void TakeDamage(int damageTaken)
        {
            enemyHealth -= damageTaken;

            if (enemyHealth <0)
            {
                enemyHealth = 0;
            }
        }

        public void GainHealth(int healthGained)
        {
            enemyHealth += healthGained;

            if (enemyHealth > enemyBaseHealth)
            {
                enemyHealth = enemyBaseHealth;
            }
        }

        public void SimpleAttack1(Player player1)
        {
            Console.WriteLine(enemyName + " sparkade dig hårt!");
            player1.TakeDamage(10);
        }
    }
    public class Encounter
    {
        private Enemy enemy;
        public Encounter(Enemy enemy)
        {
            this.enemy = enemy;
        }
        public void start()
        {
            Console.WriteLine("En fiende dök upp!");
        }

    }
}
