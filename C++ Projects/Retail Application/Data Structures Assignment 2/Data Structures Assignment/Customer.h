#pragma once
#include <iostream>
using namespace std;
class Customer
{
public:
	void Initialise();
	void Display(); //Overloading from abstract class
	void Display(Customer* customer); //Displays all of the specific customer details
	void GetCustomerProfile(string firstName, string lastName, string Address, string PhoneNumber); //Allows the customer to enter all of their personal details and creates a profile
	void GetLogin(string Username, string Password, char choice); //Allows the customer to create a new username and password
	void GetTransactionDetails(string cardNumber, string cvv, string expiryDate, char choice); //Allows the customer to enter their transaction details
	void DisplayTransaction(string cardNumber, string cvv, string expiryDate, char choice); //Displays the completed transaction
	Customer(string firstName, string lastName, string Address, string PhoneNumber); //Creates a new customer for the application
	Customer(); //Constructor 
	~Customer(); //Destructor
private: //Encapsulation
	string FirstName;
	string LastName;
	string Address;
	string PhoneNumber;
	string Username;
	string Password;

	string cardNumber;
	string cvv;
	string expiryDate;
	char choice;
};

