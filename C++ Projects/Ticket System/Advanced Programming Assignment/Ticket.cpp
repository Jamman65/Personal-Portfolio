#include "Ticket.h"

Ticket::Ticket(int price, string TicketType, string TicketChoice) { //Constructor 
	this->price = price;
	this->TicketType = TicketType;
	this->TicketChoice = TicketChoice;
	
	

}

Ticket::~Ticket() { //Destructor

}



void Ticket::PurchaseTicket(int price, string TicketType, string TicketChoice) {
	cout << "Welcome to the ticket interface" << endl;
	cout << "We have a selection of tickets available for this show: " << endl;
	cout << "Standard Ticket: " << "Price: 40" << endl;
	cout << "Front Row Ticket: " << "Price: 80" << endl;
	cout << "VIP Ticket: " << "Price: 120" << endl;
	cout << "\n" << "Please enter your choice of ticket" << endl;
	cin >> TicketChoice;
	this->TicketChoice = TicketChoice;
	this->TicketType = TicketType;
	
	

	//cout << TicketChoice << endl;

	if (TicketChoice == "Standard") {
		cout << "Thank you for choosing the standard ticket" << endl;
		TicketType = "STANDARD TICKET:";
		this->TicketType = TicketType;
		price = 40;
		this->price = price;
		cout << TicketType << price << endl;

	}

	else if (TicketChoice == "Front") {
		cout << "Thank you for choosing the Front Row Ticket" << endl;
		TicketType = "FRONT ROW TICKET:";
		this->TicketType = TicketType;
		price = 80;
		this->price = price;
		cout << TicketType << price << endl;
	}

	else if (TicketChoice == "VIP") {
		cout << "Thank you for choosing the VIP Ticket" << endl;
		TicketType = "VIP TICKET:";
		this->TicketType = TicketType;
	
		price = 120;
		this->price = price;
		cout << TicketType << price << endl;
	}

	else {
		Ticket* ticket = new Ticket(price, TicketType, TicketChoice);
		cout << "Invalid Ticket Selection, Please try again" << endl;
		ticket->PurchaseTicket(price, TicketType, TicketChoice); //Functional Pointers
	}
	this->price = price;
}

void Ticket::Display() {
	//this->TicketType = TicketType;
	cout <<"\n" << "Here is your purchased ticket " << endl;
	cout << TicketChoice << endl;
	cout << TicketType << endl;
	cout <<"Price: " << price << endl;
	cout << "\n" << "Enjoy your show! " << endl;
}

Ticket** Tickets(Ticket& ticket) //Friend Function
{
	cout << "Tickets" << endl;
	cout << "-----------------------" << endl;
	ticket.Display();
	cout << "-----------------------" << endl;
	return nullptr;
}
