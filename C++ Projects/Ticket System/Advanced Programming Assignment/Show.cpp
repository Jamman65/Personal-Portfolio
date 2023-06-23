#include "Show.h"
int Show::showPrice = 40;

Show::Show(int choice, int showPrice, int row, string GenreChoice) { //Constructor
	this->choice = choice;
	this->showPrice = showPrice;
	this->row = row;
	this->GenreChoice = GenreChoice;
}

Show::~Show() { //Destructor 

}

void Show::GenreSelection(int choice, string GenreChoice) {
	this->choice = choice;
	

	cout << "Welcome to the Genre selection page " << endl;
	cout << "Please Choose the genre of show you would like" << endl;
	cout << "Rap: " << endl;
	cout << "Pop: " << endl;
	cout << "Rock: " << endl;

	cin >> GenreChoice;
	this->GenreChoice = GenreChoice;
}

void Show::ShowSelection(int choice, int showPrice, int row) {

	cout << "Welcome to the show selection page" << endl;
	cout << "Here are the upcoming shows and details: " << endl;

	cout << GenreChoice;
	
	if (GenreChoice == "Rap") {
		for (row = 0; row < 4; row++) {
			cout << endl << "\n";

			cout << RapshowName[row];
			cout << "\n" << RapshowDate[row];
			cout << "\n" << RapshowTime[row];
			cout << "\n";
		}
	}
	

	 if (GenreChoice == "Pop") {
		for (row = 0; row < 4; row++) {
			cout << endl << "\n";

			cout << PopshowName[row];
			cout << "\n" << PopshowDate[row];
			cout << "\n" << PopshowTime[row];
			cout << "\n";
		}
	}

	else if (GenreChoice == "Rock") {
		for (row = 0; row < 4; row++) {
			cout << endl << "\n";

			cout << RockshowName[row];
			cout << "\n" << RockshowDate[row];
			cout << "\n" << RockshowTime[row];
			cout << "\n";
		}
	}


	

	cout << "\n" << "Please enter your choice of show from the selection between 0 and 4" << endl;
	cin >> choice;
	this->choice = choice;
	this->showPrice = showPrice;
	this->row = row;
	if (GenreChoice == "Rap") {
		cout << "You have chosen" << " " << RapshowName[choice] << " " << endl;

	}
	else if (GenreChoice == "Pop"){
		cout << "You have chosen" << " " << PopshowName[choice] << " " << endl;
	}

	else if (GenreChoice == "Rock") {
		cout << "You have chosen" << " " << RockshowName[choice] << " " << endl;
	}
	
}

void Show::Display() {

	if (GenreChoice == "Rap") {
		cout << "\n" << "Here are the details of the shows: " << endl;
		cout << "RAP: " << endl;
		cout << RapshowName[choice] << endl;
		cout << RapshowDate[choice] << endl;
		cout << RapshowTime[choice] << endl;
		cout << RapshowLocation[choice] << endl;
		cout << "Price of show: " << Show::showPrice << endl; //use of static members 
		system("pause");
	}

	else if (GenreChoice == "Rock") {
		cout << "\n" << "Here are the details of the shows: " << endl;
		cout << "ROCK: " << endl;
		cout << RockshowName[choice] << endl;
		cout << RockshowDate[choice] << endl;
		cout << RockshowTime[choice] << endl;
		cout << RockshowLocation[choice] << endl;
		cout << "Price of show: " << Show::showPrice << endl;
		system("pause");
	}

	else if (GenreChoice == "Pop") {
		
			cout <<"\n" << "Here are the details of the shows: " << endl;
			cout << "POP: " << endl;
			cout << PopshowName[choice] << endl;
			cout << PopshowDate[choice] << endl;
			cout << PopshowTime[choice] << endl;
			cout << PopshowLocation[choice] << endl;
			cout << "Price of show: " << Show::showPrice << endl; //Use of static member
			system("pause");
		}
	}

	

