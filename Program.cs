using System;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters;


namespace RestaurantManagementSystem
{
    internal class Program
    {
        static string connectionString = "Server=localhost;Database=RestaurantManagementSystem;Trusted_connection=True"; //connection string to connect to the database

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the restaurant Management System"); // title
                Console.WriteLine("\n1. Admin Staff\n2. Manager\n3. Regular Staff\n4. Exit"); // menu for the user to choose from

                Console.Write("Choose an option: "); // prompt for user input

                var choice = Console.ReadLine(); //saves the user input to the variable choice

                switch (choice) // switch statement to determine which method to call based on user input
                {
                    case "1":
                        RestAdmin();
                        break;
                    case "2":
                        Manager(); // Call the Manager method directly  
                        break;
                    case "3":
                        RegularStaff(); // Call the RegularStaff method directly  
                        break;
                    case "4":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RestAdmin()
        {
            Console.Write("Add customer information: ");  // prompt for user input

            Console.Write("First Name: "); //first name for the customer
            var firstName = Console.ReadLine(); //saves the user input to the variable firstName
            Console.Write("Last Name: "); //last name for the customer
            var lastName = Console.ReadLine(); //saves the user input to the variable lastName
            Console.Write("Email: "); //email for the customer
            var email = Console.ReadLine(); //saves the user input to the variable email
            Console.Write("Phone: "); //phone number for the customer
            var phone = Console.ReadLine(); //saves the user input to the variable phone

            Console.WriteLine("Or edit menu items for users"); // prompt for user input
            var menu = Console.ReadLine(); //saves the user input to the variable menu

            string query = "INSERT INTO Reservations(FirstName, LastName, Email, Phone, OrderdetailsID) VALUES (@FirstName, @LastName, @Email, @Phone, @OrderDetailsID)"; //query to insert customer information into the database

            using (SqlConnection conn = new SqlConnection(connectionString)) //using the connection string to connect to the database
            {
                SqlCommand cmd = new SqlCommand(query, conn); // creating a new SqlCommand object with the query and connection
                cmd.Parameters.AddWithValue("@FirstName", firstName); //adding parameters to the command
                cmd.Parameters.AddWithValue("@LastName", lastName); //adding parameters to the command
                cmd.Parameters.AddWithValue("@Email", email); //adding parameters to the command
                cmd.Parameters.AddWithValue("@Phone", phone); //adding parameters to the command

                conn.Open(); //open the connection to the database
                int someIdValue = 1; // Example: Assign a default or dynamically generated value
                Console.WriteLine("Customer Added Successfully!"); //confirmation message
            }
            Console.WriteLine("Do you want to view all customers? (y/n)"); // prompt for user input
            var viewAll = Console.ReadLine(); // saves the user input to the variable viewAll
            if (viewAll.ToLower() == "y") // if the user wants to view all customers and the answer is in lower case
            {
                string selectQuery = "SELECT * FROM Reservations"; //query to select all customers from the database
                using (SqlConnection conn = new SqlConnection(connectionString)) //using the connection string to connect to the database
                {
                    SqlCommand cmd = new SqlCommand(selectQuery, conn); // creating a new SqlCommand object with the query and connection
                    conn.Open(); //open the connection to the database
                    SqlDataReader reader = cmd.ExecuteReader(); //execute the command and get the data reader
                    while (reader.Read()) //loop through the data reader
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, First Name: {reader["FirstName"]}, Last Name: {reader["LastName"]}, Email: {reader["Email"]}, Phone: {reader["Phone"]}"); //print the customer information
                    }
                }
            }

            string query1 = "INSERT INTO MenuItems (BreakfastItems, LunchItems, DinnerItems) VALUES (@BreakfastItems, @LunchItems, @DinnerItems)";

            Console.WriteLine("What new breakfast item would you like to add to the menu?"); // prompt for user input
            var breakfast = Console.ReadLine(); //saves the user input to the variable breakfast
            Console.WriteLine("What new lunch item would you like to add to the menu?"); // prompt for user input
            var lunch = Console.ReadLine(); //saves the user input to the variable lunch
            Console.WriteLine("What new dinner items would you like to add to the menu?"); // prompt for user input
            var dinner = Console.ReadLine(); //saves the user input to the variable dinner

            using (SqlConnection conn = new SqlConnection(connectionString)) //using the connection string to connect to the database
            {
                SqlCommand cmd = new SqlCommand(query1, conn); // creating a new SqlCommand object with the query and connection
                cmd.Parameters.AddWithValue("@BreakfastItems", breakfast); //adding parameters to the command
                cmd.Parameters.AddWithValue("@LunchItems", lunch); //adding parameters to the command
                cmd.Parameters.AddWithValue("@DinnerItems", dinner); //adding parameters to the command

                 var someIdValue = 1;
                conn.Open(); //open the connection to the database
                cmd.Parameters.AddWithValue("@Id", someIdValue); // Replace someIdValue with an actual value or logic to generate it
                cmd.ExecuteNonQuery(); //execute the command
                Console.WriteLine("Menu items added successfully!"); //confirmation message
            }
            Console.WriteLine("Want to see all the menu items? Y/N?"); // prompt for user input
          var viewAllMenu = Console.ReadLine(); //saves the user input to the variable viewAllMenu

            const string SelectAllMenuItemsQuery = "Select * FROM MenuItems"; //query to select all menu items from the database
            using (SqlConnection conn = new SqlConnection(connectionString)) //using the connection string to connect to the database
            {
                string selectQuery1 = "SELECT * FROM MenuItems"; //query to select all menu items from the database
                SqlCommand cmd = new SqlCommand(selectQuery1, conn); // creating a new SqlCommand object with the query and connection
                conn.Open(); //open the connection to the database
                SqlDataReader reader = cmd.ExecuteReader(); //execute the command and get the data reader
                while (reader.Read()) //loop through the data reader
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Breakfast: {reader["BreakfastItems"]}, Lunch: {reader["LunchItems"]}, Dinner: {reader["DinnerItems"]}"); // print all the saved menu items in the database
                }
            }
        }

        static void Manager()
        {
            Console.WriteLine("Which staff member would you like to shift around?"); // prompt for user input by the manager
            var staff = Console.ReadLine(); //saves the user input to the variable staff

            if (staff.ToLower() == "y") //if the staff is lower case y 
            {
                Console.WriteLine("Staff member is already on shift."); // confirmation message
            }
            else //if the staff is not lower case y
            {
                Console.WriteLine("Staff member is not on shift."); // confirmation message
            }
            Console.WriteLine("Do you want to add a new staff member? (y/n): "); // prompt for user input
            var addStaff = Console.ReadLine(); //saves the user input to the variable addStaff
            if (addStaff.ToLower() == "y") //if the user wants to add a new staff member and the answer is in lower case
            {
                Console.WriteLine("Enter first name of member: "); // prompt for user input
                var fisrtName = Console.ReadLine(); //saves the user input to the variable staffName
                string query = "INSERT INTO staff (FirstName) VALUES (@FirstName)"; //query to insert staff member into the database
                using (SqlConnection conn = new SqlConnection(connectionString)) //using the connection string to connect to the database
                {
                    SqlCommand cmd = new SqlCommand(query, conn); // creating a new SqlCommand object with the query and connection
                    cmd.Parameters.AddWithValue("@FirstName", fisrtName); //adding parameters to the command
                    conn.Open(); //open the connection to the database
                    cmd.ExecuteNonQuery(); //execute the command
                    Console.WriteLine("Staff member added successfully!"); //confirmation message
                }
            }
            else
            {
                Console.WriteLine("No action taken."); // confirmation message
            }

            Console.WriteLine("Do you want to view all staff members? (y/n): "); // prompt for user input
            var viewAllStaff = Console.ReadLine(); // saves the user input to the variable viewAllStaff
            if (viewAllStaff.ToLower() == "y") //if the user wants to view all staff members and the answer is in lower case
            {
                string selectQuery = "SELECT * FROM staff"; //query to select all staff members from the database
                using (SqlConnection conn = new SqlConnection(connectionString)) //using the connection string to connect to the database
                {
                    SqlCommand cmd = new SqlCommand(selectQuery, conn); // creating a new SqlCommand object with the query and connection
                    conn.Open(); //open the connection to the database
                    SqlDataReader reader = cmd.ExecuteReader(); //execute the command and get the data reader
                    while (reader.Read()) //loop through the data reader
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["FirstName"]}"); // print all the staff members in the database
                    }
                }
            }
            else //if the user does not want to view all the staff then it will not do anything
            {
                Console.WriteLine("No action taken."); // confirmation message
            }
            Console.WriteLine("Do you want to view all orders? (y/n): "); // prompt for user input
            var viewAllOrders = Console.ReadLine(); // saves the user input to the variable viewAllOrders
            if (viewAllOrders.ToLower() == "y") //if the user wants to view all orders and the answer is in lower case
            {
                string selectQuery = "SELECT * FROM Orders"; //query to select all orders from the database
                using (SqlConnection conn = new SqlConnection(connectionString)) //using the connection string to connect to the database
                {
                    SqlCommand cmd = new SqlCommand(selectQuery, conn); // creating a new SqlCommand object with the query and connection
                    conn.Open(); //open the connection to the database
                    SqlDataReader reader = cmd.ExecuteReader(); //execute the command and get the data reader
                    while (reader.Read()) //loop through the data reader
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, {reader["MenuItems"]}"); // print all the orders in the database
                    }
                }
            }
            else //if the user does not want to view all the orders then it will not do anything
            {
                Console.WriteLine("No action taken."); // confirmation message
            }

        }


        static void RegularStaff()
        {
            Console.WriteLine("enter the order you want to process: "); // prompt for user input
            var order = Console.ReadLine(); //saves the user input to the variable order
            Console.WriteLine("Do you want to process the order? (y/n): "); // prompt for user input
            var processOrder = Console.ReadLine(); // saves the user input to the variable processOrder
            if (processOrder.ToLower() == "y") //if the user wants to process the order and the answer is in lower case
            {
                string query = "INSERT INTO OrderDetails VALUES (@id, @Ordersid)";
                using (SqlConnection conn = new SqlConnection(connectionString)) // using the connection string to connect to the database
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    int someIdValue = 1; // Example: Assign a default or dynamically generated value
                    cmd.Parameters.AddWithValue("@id", someIdValue);
                    // Replace 'someIdValue' with a valid value or variable
                   
                    cmd.Parameters.AddWithValue("@id", someIdValue); // Remove the second occurrence
                    cmd.Parameters.AddWithValue("@Ordersid", order);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Order processed successfully!"); //confirmation message
                }
            }
            else
            {
                Console.WriteLine("No action taken."); //if no then nothing changes
            }
            Console.WriteLine("Do you want to view all orders? (y/n): "); // prompt for user input
            var viewAllOrders = Console.ReadLine(); // saves the user input to the variable viewAllOrders
            if (viewAllOrders.ToLower() == "y") //  if the user wants to view all orders and the answer is in lower case
            {
                string selectQuery = "SELECT * FROM Orders"; //query to select all orders from the database
                using (SqlConnection conn = new SqlConnection(connectionString)) // using the connection string to connect to the database
                {
                    SqlCommand cmd = new SqlCommand(selectQuery, conn); //  creating a new SqlCommand object with the query and connection
                    conn.Open(); //open the connection to the database
                    SqlDataReader reader = cmd.ExecuteReader(); //execute the command and get the data reader
                    while (reader.Read()) //loop through the data reader
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Order Details: {reader["OrderDetails"]}"); // print all the orders in the database
                    }
                }
            }
            else //if not then nothing changes
            {
                Console.WriteLine("No action taken."); // confirmation message
            }
            Console.WriteLine("do you want to shift the table status around? (y/n): "); // prompt for user input
            var shiftTable = Console.ReadLine(); // saves the user input to the variable shiftTable
            if (shiftTable.ToLower() == "y") //if the user wants to shift the table status and the answer is in lower case
            {
                Console.WriteLine("Enter table number: "); // prompt for user input
                var tableNumber = Console.ReadLine(); //saves the user input to the variable tableNumber
                string query = "UPDATE Tables SET Status = 'Available' WHERE TableNumber = @TableNumber"; //query to update the table status in the database
                using (SqlConnection conn = new SqlConnection(connectionString)) //using the connection string to connect to the database
                {
                    SqlCommand cmd = new SqlCommand(query, conn); // creating a new SqlCommand object with the query and connection
                    cmd.Parameters.AddWithValue("@TableNumber", tableNumber); //adding parameters to the command
                    conn.Open(); //open the connection to the database
                    cmd.ExecuteNonQuery(); //execute the command
                    Console.WriteLine("Table status updated successfully!"); //confirmation message
                }

            }
            else
            {
                Console.WriteLine("No action taken.");
            }
        }
    }
}
    

