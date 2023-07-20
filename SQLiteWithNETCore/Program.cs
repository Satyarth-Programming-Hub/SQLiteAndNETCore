using SQLiteWithNETCore.Data;
using SQLiteWithNETCore.Models;

namespace SQLiteWithNETCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User
            {
                Id = 2,
                Name = "Ramesh Kumar",
                Age = 26,
                Gender = "Male",
                City = "Noida"
            };

            //int result = DbCrudOps.AddUser(user);
            //int result = DbCrudOps.UpdateUser(user);

            //int result = DbCrudOps.DeleteUser(2);

            //if (result == 0)
            //{
            //    Console.WriteLine("No record was updated");
            //}
            //else
            //{
            //    Console.WriteLine("Record Deleted successfully");
            //}


            User userfromDb = DbCrudOps.GetUserById(1);
            Console.WriteLine($"Id={userfromDb.Id} Name={userfromDb.Name} Age={user.Age} Gender={userfromDb.Gender} City={userfromDb.City}");


            Console.ReadLine();
        }
    }
}