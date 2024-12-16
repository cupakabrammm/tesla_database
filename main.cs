using System;
using System.Data.SQLite;

class TeslaRentalPlatform
{
    
    private const string ConnectionString = "Data Source=tesla_rental.db;Version=3;";

    static void Main(string[] args)
    {
        
        InitializeDatabase();
        Console.WriteLine("Tesla Rental Platform initialized successfully!\n");

        
        RegisterCustomer("John Doe", "john.doe@example.com");
        RegisterCustomer("Jane Smith", "jane.smith@example.com");
        RegisterCustomer("John Doe", "john.doe@example.com"); 

        AddCar("Model 3", 15.00, 0.30);
        AddCar("Model S", 20.00, 0.50);

        DisplayCustomers();
        DisplayCars();

        int rentalId = RentCar(1, 1); 
        EndRental(rentalId, 2.5, 120); 

        DisplayRentals();
    }

    // -------------------------------
    // Database Initialization
    // -------------------------------
    static void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string carsTable = @"CREATE TABLE IF NOT EXISTS TeslaCars (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Model TEXT NOT NULL,
                HourlyRate REAL NOT NULL,
                KilometerRate REAL NOT NULL
            );";

            string customersTable = @"CREATE TABLE IF NOT EXISTS Customers (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT NOT NULL,
                Email TEXT NOT NULL UNIQUE
            );";

            string rentalsTable = @"CREATE TABLE IF NOT EXISTS Rentals (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                CustomerID INTEGER NOT NULL,
                CarID INTEGER NOT NULL,
                StartTime DATETIME NOT NULL,
                EndTime DATETIME,
                KilometersDriven REAL,
                TotalPayment REAL,
                FOREIGN KEY (CustomerID) REFERENCES Customers(ID),
                FOREIGN KEY (CarID) REFERENCES TeslaCars(ID)
            );";

            ExecuteQuery(connection, carsTable);
            ExecuteQuery(connection, customersTable);
            ExecuteQuery(connection, rentalsTable);
        }
    }

    static void ExecuteQuery(SQLiteConnection connection, string query)
    {
        using (var command = new SQLiteCommand(query, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    static void RegisterCustomer(string fullName, string email)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

        
            string checkQuery = "SELECT COUNT(*) FROM Customers WHERE Email = @Email;";
            using (var checkCommand = new SQLiteCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@Email", email);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine($"Error: A customer with the email '{email}' already exists.\n");
                    return;
                }
            }

           
            string query = "INSERT INTO Customers (FullName, Email) VALUES (@FullName, @Email);";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@Email", email);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Customer registered successfully!\n");
    }

    static void DisplayCustomers()
    {
        Console.WriteLine("Registered Customers:");
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Customers;";
            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["FullName"]}, Email: {reader["Email"]}");
                }
            }
        }
        Console.WriteLine();
    }
    static void AddCar(string model, double hourlyRate, double kilometerRate)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            string query = "INSERT INTO TeslaCars (Model, HourlyRate, KilometerRate) VALUES (@Model, @HourlyRate, @KilometerRate);";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Model", model);
                command.Parameters.AddWithValue("@HourlyRate", hourlyRate);
                command.Parameters.AddWithValue("@KilometerRate", kilometerRate);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Car added successfully!\n");
    }

    static void DisplayCars()
    {
        Console.WriteLine("Available Cars:");
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            string query = "SELECT * FROM TeslaCars;";
            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["ID"]}, Model: {reader["Model"]}, Hourly Rate: {reader["HourlyRate"]}, Kilometer Rate: {reader["KilometerRate"]}");
                }
            }
        }
        Console.WriteLine();
    }
    static int RentCar(int customerId, int carId)
    {
        Console.WriteLine("\n");
        return 0;
    }

    static void EndRental(int rentalId, double hoursUsed, double kilometersDriven)
    {
        Console.WriteLine("\n");
    }

    static void DisplayRentals()
    {
        Console.WriteLine("\n");
    }
}
