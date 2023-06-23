#include <iostream>
#include "TicketManager.h" //Inheritance Technique
#include "Customer.h"
#include "Ticket.h"
#include "Show.h" 
#include "Seat.h"


int main()
{
	bool MenuActivated = true;
	int Choice;
	Customer* customer = new Customer("", "", "", ""); //Assigns a blank customer for a new customer to register
	Customer* customerList[2]; //Assigns a dynamic array for a list of customers
	Show* showList[3]; //Assigns a dynamic array for a list of shows 
	Show* show = new Show(1, 1, 1, ""); //Allows a customer to choose their desired show
	Seat* seat = new Seat(1, 1); //Allows a customer to choose their seats for the show
	Ticket* ticket = new Ticket(1, "", "");

	//Main menu allows the user to navigate through the program by choosing a specific number
	while (MenuActivated == true) {
		system("cls");
		cout << " ---- Main Menu ----" << endl;
		cout << "\n" << "Show Customer List (0)" << endl;
		cout << "\n" << "Register New Account (1)" << endl;
		cout << "\n" << "Display Show List (2)" << endl;
		cout << "\n" << "Show Selection (3)" << endl;
		cout << "\n" << "Purchase Ticket (4)" << endl;
		cout << "\n" << "Choose Seats For Show (5)" << endl;
		cout << "\n" << "Display All Details (6)" << endl;
		cout << "\n" << "Quit (7)" << endl;

		cin >> Choice;

		switch (Choice) {
		case 0:
			//Customers - Dynamic array
			//Polymorphic behaviour
			//Outputs a list of preset customers from the customer list defined above
			system("cls");
			customerList[0] = new Customer("James", "Owen", "Address", "PhoneNumber");
			customerList[1] = new Customer("Lily", "", "Address", "PhoneNumber");

			cout << "Customer List: " << endl;
			for (int n = 0; n < 2; n++) {
				customerList[n]->Display();

			}
			system("pause");
			break;

		case 1:

			//Pointer and functional pointers
			//Allows the customer to create their own profile 
			//sets all the values to blank for the customer to enter their own details
			system("cls");
			customer->Initialise();
			customer->GetCustomerProfile("", "", "", "");
			customer->GetLogin("", "", ',');
			customer->Display(customer);
			customer->GetTransactionDetails("", "", "", '.'); //sets all the values to blank for the customer to enter their own details
			system("pause");
			break;

		case 2:

			//Polymorphic Behaviour
			//Shows the customer one show from each category
			system("cls");
			
			showList[0] = new Show(1, 40, 1, "Rap");
			showList[1] = new Show(2, 40, 1, "Rock");
			showList[2] = new Show(3, 40, 1, "Pop");
			cout << "Welcome To the show interface: " << endl;

			for (int n = 0; n < 3; n++) {
				showList[n]->Display();
			}
			break;

		case 3:

			//Allows the customer to choose their own show after seeing the list
			system("cls");
			
			show->GenreSelection(1, ""); //Functional Pointers 
			system("cls");
			show->ShowSelection(1, 40, 1);
			show->Display();
			break;

		case 4:

			
			//Allows the customer to choose the type of ticket they would like 
			system("cls");
			ticket->PurchaseTicket(1, "", "");
			Tickets(*ticket); //Friend Function
			system("pause");
			break;
			

		case 5:

			//Pointer and functional pointers
			//Allows the customer to choose a seat for their desired show
			//system("cls");
			system("cls");
			seat->SeatSelection(1, "");
			seat->SeatChoice();
			system("pause");
			break;

	

		case 6:
			//Displays all of the details that the customer has entered in the previous steps
			system("cls"); 
			customer->Display(customer);
			show->Display();
			Tickets(*ticket);

		case 7:
			MenuActivated = false;

		}
	}
}

