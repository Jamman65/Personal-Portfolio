#pragma once
#include "TicketManager.h"
#include<iostream>
using namespace std;
class Customer :
	public TicketManager
{
public:
	void Initialise();
	void Display(); //Overloading from abstract class
	void Display(Customer* customer);
	void GetCustomerProfile(string firstName, string lastName, string Address, string PhoneNumber);
	void GetLogin(string Username, string Password, char choice);
	//void UpdateProfile(string Username, string Password);
	void GetTransactionDetails(string cardNumber, string cvv, string expiryDate, char choice);
	Customer(string firstName, string lastName, string Address, string PhoneNumber);
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

