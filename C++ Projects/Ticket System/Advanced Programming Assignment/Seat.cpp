#include "Seat.h"

Seat::Seat(int row, int col) { //Constructor
	cout << "Welcome to the seating plan for your show " << endl;
	
}

Seat::~Seat() { //Destructor

}

void Seat::SeatSelection(int choice, string SeatType) {

	SeatPlan(row,col);
	cout << "\n";
	cout << "Enter the number of seats you would like to purchase between 1 and 5" << endl;
	cin >> choice;
	this->choice = choice;

	if (choice < 1) {
		cout << "Too low seat amount please enter again" << endl;
		cin >> choice;
	}

	if (choice > 5) {
		cout << "Too high seat amount please enter again" << endl;
		cin >> choice;
	}

	else {
		cout << "\n" << "Chosen Seat Amount = " << choice << endl;
		cout << "\n" << endl;
		cout << "Thank you for your selection" << endl;

	}

	

	while (seatAmount < choice - 1) {

		
		SeatChoice();
		seatAmount = seatAmount + 1;
	}
}

void Seat::SeatChoice() {
	
	int rowChoice;
	int ColChoice;

	/*for (int i = 0; i < row; ) {
		for (int f = 0; f < col; ) {
			seatplan[row][col] = 'A';
		}
	}*/

	cout << "Enter a row number between 1 and 5" << endl;
	cin >> rowChoice;

	cout << "Enter a column number between 1 and 7" << endl;
	cin >> ColChoice;
	
	seatplan[rowChoice][ColChoice] = 'R';
	SeatPlan(rowChoice, ColChoice);
		
		//system("pause");
	

	

	cout << "Are you happy with the seats you have chosen? enter Y/N" << endl;
	cin >> choice;

	if (choice == 'N') {
		SeatChoice();

	}
	if (choice == 'Y') {
		cout << "Thank you for your seat selection" << endl;
	}

	
}

void Seat::SeatPlan(int row, int col) {
	seatplan[row][col];

	cout << " Seats |  A  |   B  |  C  |  D  |  E  |  F  |  G  | \n";
	cout << "---------------------------------------------------- \n";
	cout << "Seating plan";
	seatplan[2][5] = 'R'; //Reserves a specifc seat 

	for (int row = 0; row < 5; row++) {
		cout << endl << "\n" << (row + 1);
		for (int column = 0; column < 7; column++) {
			cout << " " << setw(6) << seatplan[row][column];
		}
	}

	

	cout << endl;
}
    
void Seat::Display() { //overriding from abstract class
	Seat* seat = new Seat(row, col); //pointer
	seat->SeatPlan(row,col); //Functional Pointers
}
