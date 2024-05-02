using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinq()
        {
            string[] cars = {
                "vw Golf",
                "vw California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat Leon"
            };
            //SELECT * of cars
            var carlist = from car in cars select car;
            foreach (var car in carlist)
            { 
                Console.WriteLine(car); 
            }
            //2- SELECT WHERE car is Audi (SELECT AUDIs)
            var audiList = from car in cars where car.Contains("Audis") select car;
            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }
        //Number Example 
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1,2,3,4,5,6,7,8,9};
            //Fach Number multiplied by 3
            // take all numbers, but 9
            //Order numbers by ascending value
            var processedNumberLis =
                numbers.Select(number => number * 3) //{3,6,9 }
                .Where(number => number != 9)//{all but the 9}
                .OrderBy(number=>number); //at the end, we order ascending
        }
        static public void SearchEamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "x",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };
            //1. first of all elemens
            var first = textList.First();
            //2 first element that is "c"
            var ctext = textList.First(text => text.Equals("c"));
            //3. first element that contains "j" osea dentro de la lista buscara la letra "j" ,cualquier frase que contenga la letra es aceptada
            var jText = textList.First(text => text.Contains("j"));

            //4 First element that contains "z" or default // Primer elemento que contiene "z" o default
            var firstOrDefaulText = textList.FirstOrDefault(text => text.Contains('z'));
            //4 First element that contains "z" or default // or firs -Primer elemento que contiene "z" o default
            var lastOrDefaulText = textList.LastOrDefault(text => text.Contains('z')); //or last element that container "z"
            //single values - valores unicos
            var uniqueTexts = textList.Single(); //un unico valor
            var uniqueDefaultTexts = textList.SingleOrDefault();// si hay dos valores iguales devuleve ambas

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };
            var miEventNumbers = evenNumbers.Except(otherEvenNumbers); //[4,8]
        }
        static public void MultipleSelect()
        {
            //Select Maw
            string[] myOpinios =
            {
                "opcion 1, text 1",
                "opcion 2, text 2",
                "opcion 3, text 3"

            };
            var myOpinonSelection = myOpinios.SelectMany(opinion => opinion.Split(','));
            
            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name ="Entreprise",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id=1,
                            Name="cris",
                            Email="criyat@gmail.com",
                            Salary = 5000
                        },
                        new Employee
                        {
                            Id=2,
                            Name="cri2",
                            Email="criyat2@gmail.com",
                            Salary = 10000
                        }
                    }
                },
                 new Enterprise()
                {
                    Id = 2,
                    Name ="Entreprise",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id=4,
                            Name="cris4",
                            Email="criyat4@gmail.com",
                            Salary = 5000
                        },
                        new Employee
                        {
                            Id=5,
                            Name="cri5",
                            Email="criyat5@gmail.com",
                            Salary = 10000
                        }
                    }
                },
            };
            //obtener todos los empleados de todas las empresas
            var responseAllEnterprise = enterprises.SelectMany(enterprise => enterprise.Employees);
            //saber si la lista esta vacia
            bool haEnterprises = enterprises.Any();
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            //enterprise at least employees with at least 1000Euros of salary
            bool hasEmployeeWithSalaryMoreThanOrEqual1000 = enterprises.Any(enterprise => enterprise.Employees.Any(employee => employee.Salary >= 1000));
        }

        //ejemplo usando inner join
        static public void linqCollections()
        {
            var firstList = new List<string>() {"a","b","c"};
            var secondList = new List<string>() { "a", "c", "d" };
            //inner join - todos los elementos que se conmparte en ambas tablas
            var commonResult = from element in firstList 
                               join element2 in secondList
                               on element equals element2
                               select new {element, element2};
       

            //de otra forma para hacer con el mismo resultado
            var commonResult2 = firstList.Join(
                    secondList,
                    element => element,
                    element2 => element2,
                    (element, element2) => new{ element, element2}
                    );

            //OUTER JOIN - LEFT
            var leftOuterJoin = from element in firstList
                                join element2 in secondList
                                on element equals element2
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element }; ;
            //outer join-right
            var rightOuterJoin = from element2 in secondList
                                 join element in firstList
                                 on element2 equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where element2 != temporalElement
                                 select new { Element = element2 };
            // union de ambas tablas
            var LeftunionRight = leftOuterJoin.Union(rightOuterJoin);

        }
        //probando Skip y Take
        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };
            var skipTwoFirsValues = myList.Skip(2); //R/ {3,4,5,6,7,8,9,10}
            var skipLastTwoValues = myList.SkipLast(2); //{1,2,3,4,5,6,7,8}
            var skipWhileSmallerThan4 = myList.SkipWhile(num => num<4);//{4,5,6,7,8,9,10}

            //TAKe
            var takeFirsTwoValues = myList.Take(2);//{1,2}
            var takeLastTwoValues = myList.TakeLast(2); //{9,10}
            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); //{1,2,3}
        }
        //Paging with Skip and Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int starIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(starIndex).Take(resultsPerPage); //shik decimos que nos saltamos 10 paginas y take nos muestra los siguiente 10 page
        }

        //Variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aboveAverage = from number in numbers
                               let average = numbers.Average() //vaiables
                               let nSquared = Math.Pow(number, 2)//variables
                               where nSquared > average
                               select number;
            Console.WriteLine("Avegare:{0}", numbers.Average());

            foreach(int number in aboveAverage){
                Console.WriteLine("number:{0} Square:{1}", number, Math.Pow(number, 2));
            }
        }
        //zip
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> sipNumbers =
              numbers.Zip(stringNumbers, (number, word) => number + "-" + word);//{1=one, 2 = two}

        }
        //Repeat & Range - metodo enumerables-secuencias simples
        static public void repeatRangeLinq()
        {
            //Generate coleccciones de  1 - 1000
            var first1000 = Enumerable.Range(1, 1000);
            //Repeat a value N times 
            var fivexs = Enumerable.Repeat("x", 5); //{"x","x","x","x","x"}
            //lo mismo 
            IEnumerable<string> fiveXs = Enumerable.Repeat("x", 5);
        }

        static public void StudenLinq() {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "cris",
                    Grade = 90,
                    Certified= true
                },
                new Student
                {
                    Id = 2,
                    Name = "messi",
                    Grade = 50,
                    Certified = false
                },
                new Student
                {
                    Id = 3,
                    Name = "ronaldo",
                    Grade = 20,
                    Certified = true
                },
            };
            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;
            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == false
                                       select student; ;

            var appovedStudentsNames = from student in classRoom
                                       where student.Grade >= 50 && student.Certified == true
                                       select student.Name;

        }

        //ALL
        static public void Alllinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };
            bool allAreSmallerThan10 = numbers.All(x => x < 5);
            bool allAreBiggeOrEqualThan2 = numbers.All(x => x >= 2);

        }
        //Aggregate
        static public void aggregateQueries()
        {
            int[] numbers = {1,2,3,4,5,6,7,8,9,10 };
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            /*0,1 =1
             1,2= 3
            3,4=7 etc
            prevSum = es el resultado de la suma anterior
            current = es el numero siguiente de la lista
             */
        }
        //Disctint
        static public void distincValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5,4,3,2,1 };
            IEnumerable<int> distinctValues = numbers.Distinct();//solo numeros distintos
        }
        //GroupBy
        static public void groupByExamples()
        {
            //int[] numbers = { 1, 2, 3, 4, 5, 5};
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6 };
            //numeros pares con GroupBy
            var grouped = numbers.GroupBy(x => x % 2 == 0);
            foreach (var group in grouped)
            {
                foreach(var value in group)
                {
                    Console.WriteLine(value);
                    /*
                     * vamos a tener dos grupos
                     * el primer grupo = sera una lista impar
                     * el segundo grupo = sera una lista par
                     */
                }
            }
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "cris",
                    Grade = 90,
                    Certified= true
                },
                new Student
                {
                    Id = 2,
                    Name = "messi",
                    Grade = 50,
                    Certified = false
                },
                new Student
                {
                    Id = 3,
                    Name = "ronaldo",
                    Grade = 20,
                    Certified = true
                },
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified);
            foreach (var group in certifiedQuery)
            {
                foreach (var student in group)
                {
                    Console.WriteLine(student);
                    /*
                     * vamos a tener dos grupos
                     * el primer grupo = sera una lista certificados
                     * el segundo grupo = sera una lista no certificados
                     */
                }
            }

        }

        //archivo post.c# and comment.c#
        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id=1,
                    Title="My first post",
                    Content="My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title="My first comment",
                            Content="My content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title="My second comment",
                            Content="My other content"
                        },
                    }
                },
                new Post()
                {
                    Id=2,
                    Title="My second post",
                    Content="My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title="My other comment",
                            Content="My new content"
                        },
                        new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title="My other new comment",
                            Content="My new content"
                        },
                    }
                }
            };
            var commentsContext = posts.SelectMany(
                (post) => post.Comments,(post, comment) => new {PostId = post.Id,CommentContent = comment.Content}
                );

        }

    }
}
