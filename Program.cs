using System;

public class TaxCalculator
{
    // Set up variables for the program
    private readonly decimal taxRate = 0.20m;
    private readonly decimal standardDeduction = 10000.00m;
    private readonly decimal dependantDeduction = 3000.00m;

    private decimal grossIncome = 0.00m;
    private decimal totalDeductions = 0.00m;
    private decimal taxAmount = 0.00m;
    private int dependants = 0;

    private decimal overTimeRate = 1.5m;

    // Method for calculating deductions
    public void CalculateDeductions()
    {
        totalDeductions = standardDeduction + (dependantDeduction * dependants);
    }

    // Method for calculating tax
    public void CalculateTax()
    {
        decimal taxableIncome = Math.Max(grossIncome - totalDeductions, 0); // Ensure taxable income is not negative
        taxAmount = taxableIncome * taxRate;
        Console.WriteLine($"Gross Income: ${grossIncome:F2}");
        Console.WriteLine($"Taxable Income: ${taxableIncome:F2}");
        Console.WriteLine($"Tax Amount: ${taxAmount:F2}");
    }

    // Method for handling hourly employee input
    public void HandleHourlyEmployee()
    {
        decimal hourlyWage = GetNonNegativeDecimalInput("Enter hourly wage:");
        decimal hoursWorked = GetNonNegativeDecimalInput("Enter hours worked this year:");
        decimal overtimeHours = GetNonNegativeDecimalInput("Enter overtime hours worked this year:");
        grossIncome = (hourlyWage * hoursWorked) + (hourlyWage * overTimeRate * overtimeHours); 
        dependants = GetNonNegativeIntInput("Enter number of dependants:");
        CalculateDeductions();
        CalculateTax();
    }

    // Method for handling salary employee input
    public void HandleSalaryEmployee()
    {
        decimal annualSalary = GetNonNegativeDecimalInput("Enter annual salary:");
        grossIncome = annualSalary;
        dependants = GetNonNegativeIntInput("Enter number of dependants:");
        CalculateDeductions();
        CalculateTax();
    }

    // Method to get non-negative decimal input from user
    private decimal GetNonNegativeDecimalInput(string prompt)
    {
        decimal value;
        do
        {
            Console.WriteLine(prompt);
            if (!decimal.TryParse(Console.ReadLine(), out value) || value < 0)
            {
                Console.WriteLine("Please enter a valid non-negative number.");
            }
        } while (value < 0);
        return value;
    }

    // Method to get non-negative integer input from user
    private int GetNonNegativeIntInput(string prompt)
    {
        int value;
        do
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            if (decimal.TryParse(input, out decimal decimalValue) && decimalValue % 1 != 0)
            {
                Console.WriteLine("Please enter whole numbers only.");
                value = -1; // Set value to an invalid number to continue the loop
            }
            else if (!int.TryParse(input, out value) || value < 0)
            {
                Console.WriteLine("Please enter a valid non-negative number.");
            }
        } while (value < 0);
        return value;
    }

    // Main method
    public static void Main()
    {
        TaxCalculator taxCalculator = new TaxCalculator();

        bool running = true;

        while (running)
        {
            Console.WriteLine("|****Income Tax Calculator****|");
            Console.WriteLine("Pick an option:");
            Console.WriteLine("1. Hourly Employee");
            Console.WriteLine("2. Salary Employee");
            Console.WriteLine("3. Quit");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    taxCalculator.HandleHourlyEmployee();
                    break;

                case "2":
                    taxCalculator.HandleSalaryEmployee();
                    break;

                case "3":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}