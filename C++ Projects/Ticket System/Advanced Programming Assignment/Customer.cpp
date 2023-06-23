#include "Customer.h"

Customer::Customer(string firstName, string lastName, string Address, string PhoneNumber) {
	this->FirstName = firstName;
	this->LastName = lastName;
	this->Address = Address;
	this->PhoneNumber = PhoneNumber;

	
}
Customer::Customer() { //Constructor

}

void Customer::Initialise() {
	cout << "Welcome to My Online ticket system!" << endl;
}

Customer::~Customer() { //Destructor

}

void Customer::Display() { //Overloading from the abstract class
	cout << "FirstName: " << FirstName << endl;
	cout << "LastName:" << LastName << endl;
	cout << "Address:" << Address << endl;
	cout << "PhoneNumber:" << PhoneNumber << endl;
	cout << "\n" << endl;
}

void Customer::Display(Customer* customer) {//Overloading - Allows for functions of the same name to be called with different arguments
	cout << "Customer Details: " << endl;
	cout << "FirstName: " << FirstName << endl;
	cout << "LastName:" << LastName << endl;
	cout << "Address:" << Address << endl;
	cout << "PhoneNumber:" << PhoneNumber << endl;
	cout << "\n" << endl;
	cout << "Username:" << Username << endl;
	cout << "Password:" << Password << endl;

}

void Customer::GetCustomerProfile(string firstName, string lastName, string Address, string PhoneNumber) {
	

	cout << "Please enter your first name" << endl;
	cin >> firstName;
	cout << "Please enter your last name" << endl;
	cin >> lastName;
	cout << "Please enter your address" << endl;
	cin >> Address;
	cout << "Please enter your phone number" << endl;
	cin >> PhoneNumber;

	this->FirstName = firstName;
	this->LastName = lastName;
	this->Address = Address;
	this->PhoneNumber = PhoneNumber;

	//cout << "\n" << firstName << "\n" << lastName << "\n" << Address << "\n" << PhoneNumber << endl;
}

void Customer::GetTransactionDetails(string cardNumber, string cvv, string expiryDate, char choice) { //Encapsulation

	this->cardNumber = cardNumber;
	this->cvv = cvv;
	this->expiryDate = expiryDate;
	this->choice = choice;
	//char choice;
	Customer* customer = new Customer(FirstName, LastName, Address, PhoneNumber);

	cout << "Please enter your card number for payment details" << endl;
	cin >> cardNumber;
	cout << "Please enter the expiry date of the bank card" << endl;
	cin >> expiryDate;
	cout << "Please enter the 3 digits on the back of the card" << endl;
	cin >> cvv;

	cout << "Card Number: " << cardNumber << "\n" << "Expiry Date: " << expiryDate << "\n" << "Cvv Number: " << cvv << "\n" << endl;

	cout << "Are these details correct? (Y/N)" << endl;


	cin >> choice;
	switch (choice)
	{
	case 'Y':
		cout << "The bank details have been stored sucessfully" << endl; break;
		
		
	case 'N':
		customer->GetTransactionDetails(cardNumber, cvv, expiryDate, choice); //Functional Pointer
	case'y':
		cout << "The bank details have been stored sucessfully" << endl; break;
		
	case'n':
		customer->GetTransactionDetails(cardNumber, cvv, expiryDate, choice); //Functional Pointer
		//default: 

		

	}




}



void Customer::GetLogin(string Username, string Password, char choice) {
	this->choice = choice;
	
	//Customer* customer = new Customer(FirstName, LastName, Address, PhoneNumber);
	Customer customer1; //Static Object
	
	cout << "\n" << "Please enter a unique Username:";
	cin >> Username;
	cout << "\n" << "Chosen Username: " << Username;
	cout << "\n" << "Please enter a unique Password";
	cin >> Password;
	cout << "\n" << "Chosen Password:" << Password;
	this->Username = Username;
	this->Password = Password;


	cout << "\n" << "Are these details correct? (Y/N)" << endl;
	cin >> choice;
	
	if (choice == 'Y') {
		cout << "Username and password stored successfully" << endl;
	}

	if (choice == 'N') {
		cout << "Please edit your new username and password";
		//customer->GetLogin(Username, Password, choice);
		customer1.GetLogin(Username, Password, choice); //Static Object Function




		}
	}
