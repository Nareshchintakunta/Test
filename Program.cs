using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    #region Main

    class Program
    {
        public static List<string> UserList = new List<string>();

        public List<string> Users
        {
            get
            {
                return UserList;
            }
            set
            {
                foreach (var item in value)
                {
                    UserList.Add(item);
                }

            }
        }


        static void Main(string[] args)
        {
            #region Question 1
            Console.WriteLine("key in max number of elements in Array");
            int size = Convert.ToInt32(Console.ReadLine());

            int[] array = new int[size];

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("key in array element");
                array[i] = Convert.ToInt32(Console.ReadLine());              

            }

            Console.WriteLine("the biggest combination of neighbor element that can be found is = " + Challenge(array));
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine();

            #endregion

            #region Question 2

            while (true)
            {
                var user = new User();
                Console.WriteLine("please enter the username, or q to exit");
                var userName = Console.ReadLine();
                if (userName == "q")
                {
                    break;
                }

                user.Add(userName);
                Console.WriteLine($"number of addedUser {user.GetUsersCount()}");
            }
            Console.WriteLine("-------------------------------------------------------------------");
            #endregion

            #region Question 3
            var john = new Humanoid(new Dancing());
            Console.WriteLine(john.ShowSkill());

            var alex = new Humanoid(new Cooking());
            Console.WriteLine(alex.ShowSkill());

            var bob = new Humanoid();
            Console.WriteLine(bob.ShowSkill());
            Console.WriteLine("-----------------------------------------------");
            #endregion

            #region Question 4
            var alexa = new Alexa();
            Console.WriteLine(alexa.Talk()); //print hello, i am Alexa

            //alexa.Configure(x =>
            //{
            //    x.GreetingMessage = "Hello {OwnerName}, I'm at your service";
            //    x.OwnerName = "Bob Marley";
            //});

            Console.WriteLine(alexa.Talk()); //print Hello Bob Marley, I'm at your service

            Console.WriteLine("press any key to exit ...");
            Console.ReadKey();
            #endregion


        }

        public static int Challenge(int[] array)
        {
            #region Find Max Repeated Number 
            int[] distinctArray = array.Distinct().ToArray();
            int maxRepeats = 0;

            for (int i = 0; i < distinctArray.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[i] == array[j])
                    {
                        count = count + 1;
                    }
                }

                if (count > 1 && count > maxRepeats)
                    maxRepeats = count;
            }

            int MaxNumber = array.Max();
            #endregion

            #region filter array with M-1 repeated elements

            List<int> resultList = array.ToList();

            for (int i = 0; i < distinctArray.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[i] == array[j])
                    {
                        count = count + 1;
                    }
                }

                if (count < maxRepeats - 1)
                    resultList.Remove(array[i]);
            }

            array = resultList.ToArray();

            #endregion

            #region Find biggext Number 

            int firstResult = 0, secondResult = 0, tempResult = 0, biggestNumber = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == MaxNumber)
                {
                    //Add maxNumber + previousNumber 
                    if (i != 0)
                    {
                        tempResult = MaxNumber + array[i - 1];

                        if (tempResult > firstResult)
                            firstResult = tempResult;
                    }

                    //Add maxNumber + nextNumber 
                    if (i != array.Length - 1)
                    {
                        tempResult = MaxNumber + array[i + 1];

                        if (tempResult > secondResult)
                            secondResult = tempResult;
                    }

                    //skip next iteration
                    i++;
                }

                if (firstResult > secondResult)
                    biggestNumber = firstResult;
                else
                    biggestNumber = secondResult;
            }
            #endregion

            return biggestNumber;
           
        }

    }

    #endregion 


    public class User
    {
        public void Add(string UserName)
        {
            if (Program.UserList != null)
                Program.UserList.Add(UserName);
        }

        public int GetUsersCount()
        {
            return Program.UserList.Count();
        }

        
    }

    public class Alexa 
    {

        public string GreetingMessage { get; set; }
        public string OwnerName { get; set; }

        public void Configure()
        {
            var myAnonymousType = new
            {
                firstProperty = "First",
                secondProperty = 2,
                thirdProperty = true
            };
        }

        public string Talk()
        {        
            //if (OwnerName != null && GreetingMessage != null)
            //{
            //   return string.Format($"Hello {OwnerName}, I'm at your service");
            //}

            return "hello, i am Alexa";
        }


    }


    public class Humanoid
    {
        private ISkill _skill;

        public Humanoid(ISkill skill)
        {
            _skill = skill;
        }

        public Humanoid()
        {

        }

        public string ShowSkill()
        {
            if (_skill != null)
                return _skill.ShowSkill();
            else
                return "no skill is defined";        
        }

    }

    public interface ISkill
    {

        string ShowSkill();

    }

    public class Dancing : ISkill
    {

        public string ShowSkill()
        {
            return "dancing";
        }

    }

    public class Cooking : ISkill
    {

        public string ShowSkill()
        {
            return "Cooking";
        }

    }
}
