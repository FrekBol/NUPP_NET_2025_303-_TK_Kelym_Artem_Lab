using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Airline Reservation System");

        List<Flight> flights = InitializeFlights(); // Инициализация рейсов

        while (true)
        {
            Console.WriteLine("\nAvailable flights:");
            DisplayFlights(flights);
            Console.WriteLine("Enter flight number to book, or 'exit' to quit:");

            string input = Console.ReadLine();
            if (input.ToLower() == "exit")
            {
                break;
            }

            int flightNumber;
            if (int.TryParse(input, out flightNumber) && flightNumber >= 1 && flightNumber <= flights.Count)
            {
                BookTicket(flights[flightNumber - 1]);
            }
            else
            {
                Console.WriteLine("Invalid flight number. Please try again.");
            }
        }
    }

    static List<Flight> InitializeFlights()
    {
        return new List<Flight>
        {
            new Flight { FlightNumber = 1, Destination = "New York", SeatsAvailable = 10 },
            new Flight { FlightNumber = 2, Destination = "London", SeatsAvailable = 5 },
            new Flight { FlightNumber = 3, Destination = "Tokyo", SeatsAvailable = 0 },
        };
    }

    static void DisplayFlights(List<Flight> flights)
    {
        for (int i = 0; i < flights.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Flight {flights[i].FlightNumber} to {flights[i].Destination} - Seats available: {flights[i].SeatsAvailable}");
        }
    }

    static void BookTicket(Flight flight)
    {
        if (flight.SeatsAvailable > 0)
        {
            flight.SeatsAvailable--;
            Console.WriteLine($"Ticket booked for flight {flight.FlightNumber} to {flight.Destination}. Seats left: {flight.SeatsAvailable}");
        }
        else
        {
            Console.WriteLine("No seats available for this flight.");
        }
    }
}

class Flight
{
    public int FlightNumber { get; set; }
    public string Destination { get; set; }
    public int SeatsAvailable { get; set; }
}
